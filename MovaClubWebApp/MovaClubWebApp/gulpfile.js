//"use strict";
var gulp = require("gulp"),
    rename = require("gulp-rename"),
    webpack = require("webpack"),
    webpackStream = require("webpack-stream");

    
var paths = {
    webroot: "./wwwroot/"
};

gulp.task("account/login.scss", function () {
    return gulp.src(paths.webroot + 'sass/login.scss')
        .pipe(webpackStream(require('./webpack.config.js'), webpack))
        .pipe(gulp.dest(paths.webroot + '/css/account'));
});

gulp.task("account/settings.scss", function () {
    return gulp.src(paths.webroot + 'sass/account/settings.scss')
        .pipe(webpackStream(require('./webpack.config.js'), webpack))
        .pipe(gulp.dest(paths.webroot + '/css/account'));
});


gulp.task("account/register.scss", function () {
    return gulp.src(paths.webroot + 'sass/account/register.scss')
        .pipe(webpackStream(require('./webpack.config.js'), webpack))
        .pipe(gulp.dest(paths.webroot + '/css/account'));
});

gulp.task("app.scss", function () {
    return gulp.src(paths.webroot + 'sass/app.scss')
        .pipe(webpackStream(require('./webpack.config.js'), webpack))
        .pipe(gulp.dest(paths.webroot + '/css'));
});

gulp.task("calendar.scss", function () {
    return gulp.src(paths.webroot + 'sass/calendar.scss')
        .pipe(webpackStream(require('./webpack.config.js'), webpack))
        .pipe(gulp.dest(paths.webroot + '/css'));
});


gulp.task("app.js", function () {
    return gulp.src(paths.webroot + 'babel-js/app.js')
        .pipe(webpackStream(require('./webpack.config.js'), webpack))
        .pipe(rename({
            basename: "app"
        }))
        .pipe(gulp.dest(paths.webroot + '/js'));
});



gulp.task("account/settings-startup.js", function () {
    return gulp.src(paths.webroot + 'babel-js/account/settings-startup.js')
        .pipe(webpackStream(require('./weppack-library.config.js'), webpack))
        .pipe(rename({
            basename: "settings-startup"
        }))
        .pipe(gulp.dest(paths.webroot + '/js/account'));
});

gulp.task("account/settings.js", function () {
    return gulp.src(paths.webroot + 'babel-js/account/settings.js')
        .pipe(webpackStream(require('./weppack-library.config.js'), webpack))
        .pipe(rename({
            basename: "settings"
        }))
        .pipe(gulp.dest(paths.webroot + '/js/account'));
});


gulp.task("areas/admin/account/manage.scss", function () {
    return gulp.src(paths.webroot + 'sass/admnlogin.scss')
        .pipe(webpackStream(require('./webpack.config.js'), webpack))
        .pipe(gulp.dest(paths.webroot + '/css/account'));
});

gulp.task("areas/admin/account/user-table.scss", function () {
    return gulp.src(paths.webroot + 'sass/areas/admin/account/user-table.scss')
        .pipe(webpackStream(require('./webpack.config.js'), webpack))
        .pipe(gulp.dest(paths.webroot + '/css/admin/account/'));
});


gulp.task("areas/admin/account/user-table.js", function () {
    return gulp.src([ paths.webroot + 'babel-js/areas/admin/account/user-table.js', paths.webroot + 'babel-js/areas/admin/account/user-table-startup.js'])
        .pipe(webpackStream(require('./webpack-user-table.config.js'), webpack))
        .pipe(rename({
            basename: "user-table"
        }))
        .pipe(gulp.dest(paths.webroot + '/js/areas/admin/account/'));
});



gulp.task("areas/admin/account/manage.scss", function () {
    return gulp.src(paths.webroot + 'sass/areas/admin/account/manage.scss')
        .pipe(webpackStream(require('./webpack.config.js'), webpack))
        .pipe(gulp.dest(paths.webroot + '/css/admin/account/'));
});


gulp.task("areas/admin/account/about-user", function () {
    return gulp.src(paths.webroot + 'sass/areas/admin/account/about-user.scss')
        .pipe(webpackStream(require('./webpack.config.js'), webpack))
        .pipe(gulp.dest(paths.webroot + '/css/admin/account/'));
});




