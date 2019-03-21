using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using SPA.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SPA.DAL
{

    public class PlayerAccessLayer
    {

        private const string _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ReversiDb;Integrated Security=True;";

        public string Salt(string playerEmail)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                conn.Open();
                string queryStringSalt = "SELECT PlayerProviderKey FROM Player WHERE PlayerEmail = @PlayerEmail OR PlayerName = @PlayerEmail";
                SqlCommand sqlSalt = new SqlCommand(queryStringSalt, conn);
                sqlSalt.Parameters.AddWithValue("@PlayerEmail", playerEmail);
                sqlSalt.Parameters.AddWithValue("@PlayerName", playerEmail);

                SqlDataReader rdr = sqlSalt.ExecuteReader(CommandBehavior.SingleRow);

                var accountModel = new Player();

                while (rdr.Read())
                {
                    // Fill the grid\

                    accountModel.PlayerProviderKey = rdr["PlayerProviderKey"].ToString();

                }

                conn.Close();
                return accountModel.PlayerProviderKey;
            }
        }

        public bool AddPlayer(Player playerModel)
        {
            var accountSalt = BCrypt.Net.BCrypt.GenerateSalt(10, SaltRevision.Revision2A);
            var password = BCrypt.Net.BCrypt.HashPassword(playerModel.PlayerPassword, accountSalt);

            int result;
            using (SqlConnection sqlCon = new SqlConnection(_connectionString))
            {
                string sqlQuery = $"INSERT INTO Player VALUES(@PlayerEmail, @PlayerPassword, @PlayerRole, @PlayerToken, @PlayerProviderKey, @PlayerName)";

                SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon);
                sqlCmd.Parameters.AddWithValue("@PlayerEmail", playerModel.PlayerEmail);
                sqlCmd.Parameters.AddWithValue("@PlayerPassword", password);
                if(playerModel.PlayerRole == null && playerModel.PlayerToken == null)
                {
                    sqlCmd.Parameters.AddWithValue("@PlayerRole", "User");
                    sqlCmd.Parameters.AddWithValue("@PlayerToken", "User01");

                }
                else
                {
                    sqlCmd.Parameters.AddWithValue("@PlayerRole", playerModel.PlayerRole);
                    sqlCmd.Parameters.AddWithValue("@PlayerToken", playerModel.PlayerToken);
                }
                sqlCmd.Parameters.AddWithValue("@PlayerProviderKey", accountSalt);
                sqlCmd.Parameters.AddWithValue("@PlayerName", playerModel.PlayerName);

                sqlCon.Open();
                result = sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
            }

            return (result == 1);

        }

        public bool EditPlayer(Player playerModel)
        {
            string sqlQuery = "UPDATE Account SET PlayerEmail = @PlayerEmail, PlayerRole = @PlayerRole, PlayerToken = @PlayerToken WHERE PlayerId = @PlayerId";
            int result;
            using (SqlConnection sqlCon = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon);
                
                sqlCmd.Parameters.AddWithValue("@PlayerEmail", playerModel.PlayerEmail);
                sqlCmd.Parameters.AddWithValue("@PlayerRole", playerModel.PlayerRole);
                sqlCmd.Parameters.AddWithValue("@PlayerToken", playerModel.PlayerToken);
                sqlCmd.Parameters.AddWithValue("@PlayerId", playerModel.PlayerId);

                sqlCon.Open();
                result = sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
            }
            return (result == 1);
        }

        public bool DeletePlayer(int id)
        {
            string sqlQuery = "DELETE FROM Player WHERE PlayerId = " + id;
            int result;

            using (SqlConnection sqlCon = new SqlConnection(_connectionString))
            {
                SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon);
                sqlCon.Open();
                result = sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
            }
            return (result == 1);
        }

        public IEnumerable<Claim> GetAccountRoleClaims(Player playerModel)
        {
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, playerModel.PlayerName.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, playerModel.PlayerRole.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, playerModel.PlayerId.ToString()));


            return claims;
        }

        private IEnumerable<Claim> GetAccountClaims(Player playerModel)
        {
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, playerModel.PlayerName.ToString()));
            claims.Add(new Claim(ClaimTypes.Email, playerModel.PlayerEmail));
            claims.Add(new Claim(ClaimTypes.Name, playerModel.PlayerId.ToString()));

            claims.AddRange(this.GetAccountRoleClaims(playerModel));

            return claims;
        }

        public async Task<bool> SignIn(HttpContext httpContext, Player model, bool isPersistent = false)
        {
            using (var sqlCon = new SqlConnection(_connectionString))
            {
                string NameOrEmail;
                if(model.PlayerName == null)
                {
                    NameOrEmail = model.PlayerEmail;
                }
                else
                {
                    NameOrEmail = model.PlayerName;
                }
                string salty = Salt(NameOrEmail);
                var password = BCrypt.Net.BCrypt.HashPassword(model.PlayerPassword, salty);
                var verify = BCrypt.Net.BCrypt.Verify(model.PlayerPassword, password);
                string correctPassword = verify.ToString();

                bool ingelogd = false;
                string sqlQuery = "SELECT PlayerId, PlayerEmail, PlayerPassword, PlayerRole, PlayerToken, PlayerProviderKey, PlayerName FROM Player WHERE (PlayerEmail = @PlayerEmail OR PlayerName = @PlayerEmail) AND PlayerPassword = @PlayerPassword";

                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon);
                sqlCmd.Parameters.AddWithValue("@PlayerEmail", model.PlayerEmail);
                sqlCmd.Parameters.AddWithValue("@PlayerPassword", password);
                sqlCmd.Parameters.AddWithValue("@PlayerName", model.PlayerEmail);

                SqlDataReader rdr = sqlCmd.ExecuteReader();
                var playerModel = new Player();

                if (rdr.Read())
                {
                    playerModel.PlayerId = Convert.ToInt32(rdr["PlayerId"]);
                    playerModel.PlayerEmail = rdr["PlayerEmail"].ToString();
                    playerModel.PlayerPassword = rdr["PlayerPassword"].ToString();
                    playerModel.PlayerRole = rdr["PlayerRole"].ToString();
                    playerModel.PlayerToken = rdr["PlayerToken"].ToString();
                    playerModel.PlayerProviderKey = rdr["PlayerProviderKey"].ToString();
                    playerModel.PlayerName = rdr["PlayerName"].ToString();



                    ingelogd = true;
                }

                sqlCon.Close();

                ClaimsIdentity identity = new ClaimsIdentity(this.GetAccountClaims(playerModel), CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return ingelogd;
            }
        }

        public async void SignOut(HttpContext httpContext)
        {
            await httpContext.SignOutAsync();
        }

        public Player GetPlayer(int id)
        {
            var playerModel = new Player();
            using(SqlConnection sqlCon = new SqlConnection(_connectionString))
            {
                string sqlQuery = "SELECT PlayerId, PlayerEmail, PlayerPassword, PlayerRole, PlayerToken, PlayerProviderKey FROM Player WHERE PlayerId = " + id ;
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlCon);
                SqlDataReader rdr = sqlCmd.ExecuteReader();

                while (rdr.Read())
                {
                    playerModel.PlayerId = Convert.ToInt32(rdr["PlayerId"]);
                    playerModel.PlayerEmail = rdr["PlayerEmail"].ToString();
                    playerModel.PlayerPassword = rdr["PlayerPassword"].ToString();
                    playerModel.PlayerRole = rdr["PlayerRole"].ToString();
                    playerModel.PlayerToken = rdr["PlayerToken"].ToString();
                    playerModel.PlayerProviderKey = rdr["PlayerProviderKey"].ToString();
                }

                sqlCon.Close();
            }

            return playerModel;
        }
    }
}
