//gulpfile

const gulp = require('gulp');
const clean_css = require('gulp-clean-css');
const concatCSS = require('gulp-concat');
const browserSync = require('browser-sync').create();
const { src, dest } = require('gulp');
const { series, parallel } = require('gulp');
const uglify = require('gulp-uglifycss');
const sourcemaps = require("gulp-sourcemaps");
const concatJS = require("gulp-concat-js");


gulp.task('build', function (done) {
    console.log('Running the build task')
    done()
});

gulp.task('default', function (done) {
    console.log('Running the default task')
    done()
});

gulp.task('promise-task', function (done) {
    return new Promise(function (resolve, reject) {
        resolve('promise-task is done')
    })
});


gulp.task('css', function () {
    return src('./wwwroot/css/*.css')
        .pipe(concatCSS('style.min.css'))
        .pipe(uglify({
            "maxLineLen": 80,
            "uglyComments": true
        }))
        .pipe(clean_css({ compatibility: 'ie9' }))
        .pipe(gulp.dest('./wwwroot/dist/src/css'));
});

gulp.task('js', function () {
    return src('./wwwroot/js/*.js')
        .pipe(sourcemaps.init())
        .pipe(concatJS({
            "target": "concatenated.js"}))
        .pipe(sourcemaps.write())
        .pipe(uglify({
            "maxLineLen": 80,
            "uglyComments": true
        }))
        .pipe(gulp.dest('./wwwroot/dist/src/js'));
});

//for watch functions
const css = function () {
    return src('./wwwroot/css/**/*.css')
        .pipe(gulp.dest('./dist'))
        .pipe(browserSync.stream());
};

//for watch functions
const js = function () {
    return src('./wwwroot/js/**/*.js')
        .pipe(gulp.dest('./dist'))
        .pipe(browserSync.stream());
}

gulp.task('serve', function () {

    browserSync.init({
        server: "./wwwroot/css"
    });

    gulp.watch("./wwwroot/css/**/*.css", series(css))
    gulp.watch("./wwwroot/js/**/*.js", series(js))
    gulp.watch("./wwwroot/css/**/*.css").on('change', browserSync.reload);
    gulp.watch("./wwwroot/js/**/*.js").on('change', browserSync.reload);
});
