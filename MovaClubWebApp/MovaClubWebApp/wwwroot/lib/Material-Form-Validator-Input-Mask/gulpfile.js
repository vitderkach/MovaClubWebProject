const gulp   = require('gulp'),
      babel  = require('gulp-babel'),
      uglify = require('gulp-uglify'),
      rename = require('gulp-rename'),
      merge  = require('merge-stream'),
      del    = require('del');

gulp.task('default', function () {
   let dist = gulp.src('src/*.js')
       .pipe(babel({
           presets: ['@babel/env']
       }))
       .pipe(rename('jquery.validate.md.js'))
       .pipe(gulp.dest('dist'));

   let minify = gulp.src('dist/*.js')
       .pipe(uglify())
       .pipe(rename('jquery.validate.md.min.js'))
       .pipe(gulp.dest('dist'));

    return merge(dist, minify);
});

gulp.task('del', function () {
   del.sync('dist');
});