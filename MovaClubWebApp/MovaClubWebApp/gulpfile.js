"use strict";
var gulp = require("gulp"),
    webpack = require("webpack"),
    webpackStream = require("webpack-stream");
    
var paths = {
    webroot: "./wwwroot/"
};

gulp.task("sass", function () {
    return gulp.src(paths.webroot + 'sass/app.scss')
        .pipe(webpackStream(require('./webpack.config.js'), webpack))
        .pipe(gulp.dest(paths.webroot + '/css'));
});

gulp.task("script", function () {
    return gulp.src(paths.webroot + 'babel-js/app.js')
        .pipe(webpackStream(require('./webpack.config.js'), webpack))
        .pipe(gulp.dest(paths.webroot + '/js'));
});




