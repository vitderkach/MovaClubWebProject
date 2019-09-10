var UserTable =
/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};
/******/
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/
/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId]) {
/******/ 			return installedModules[moduleId].exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			i: moduleId,
/******/ 			l: false,
/******/ 			exports: {}
/******/ 		};
/******/
/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/
/******/ 		// Flag the module as loaded
/******/ 		module.l = true;
/******/
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/
/******/
/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;
/******/
/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;
/******/
/******/ 	// define getter function for harmony exports
/******/ 	__webpack_require__.d = function(exports, name, getter) {
/******/ 		if(!__webpack_require__.o(exports, name)) {
/******/ 			Object.defineProperty(exports, name, { enumerable: true, get: getter });
/******/ 		}
/******/ 	};
/******/
/******/ 	// define __esModule on exports
/******/ 	__webpack_require__.r = function(exports) {
/******/ 		if(typeof Symbol !== 'undefined' && Symbol.toStringTag) {
/******/ 			Object.defineProperty(exports, Symbol.toStringTag, { value: 'Module' });
/******/ 		}
/******/ 		Object.defineProperty(exports, '__esModule', { value: true });
/******/ 	};
/******/
/******/ 	// create a fake namespace object
/******/ 	// mode & 1: value is a module id, require it
/******/ 	// mode & 2: merge all properties of value into the ns
/******/ 	// mode & 4: return value when already ns object
/******/ 	// mode & 8|1: behave like require
/******/ 	__webpack_require__.t = function(value, mode) {
/******/ 		if(mode & 1) value = __webpack_require__(value);
/******/ 		if(mode & 8) return value;
/******/ 		if((mode & 4) && typeof value === 'object' && value && value.__esModule) return value;
/******/ 		var ns = Object.create(null);
/******/ 		__webpack_require__.r(ns);
/******/ 		Object.defineProperty(ns, 'default', { enumerable: true, value: value });
/******/ 		if(mode & 2 && typeof value != 'string') for(var key in value) __webpack_require__.d(ns, key, function(key) { return value[key]; }.bind(null, key));
/******/ 		return ns;
/******/ 	};
/******/
/******/ 	// getDefaultExport function for compatibility with non-harmony modules
/******/ 	__webpack_require__.n = function(module) {
/******/ 		var getter = module && module.__esModule ?
/******/ 			function getDefault() { return module['default']; } :
/******/ 			function getModuleExports() { return module; };
/******/ 		__webpack_require__.d(getter, 'a', getter);
/******/ 		return getter;
/******/ 	};
/******/
/******/ 	// Object.prototype.hasOwnProperty.call
/******/ 	__webpack_require__.o = function(object, property) { return Object.prototype.hasOwnProperty.call(object, property); };
/******/
/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "";
/******/
/******/
/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(__webpack_require__.s = 0);
/******/ })
/************************************************************************/
/******/ ({

/***/ "./wwwroot/babel-js/areas/admin/account/user-table-startup.js":
/*!********************************************************************!*\
  !*** ./wwwroot/babel-js/areas/admin/account/user-table-startup.js ***!
  \********************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";
eval("\n\nvar _userTable = __webpack_require__(/*! ./user-table.js */ \"./wwwroot/babel-js/areas/admin/account/user-table.js\");\n\nvar _userTable2 = _interopRequireDefault(_userTable);\n\nfunction _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }\n\nmodule.exports = {\n    run: function run() {\n        _userTable2.default.divideColumns();\n        console.log(_userTable2.default);\n    }\n};\n\n//# sourceURL=webpack://UserTable/./wwwroot/babel-js/areas/admin/account/user-table-startup.js?");

/***/ }),

/***/ "./wwwroot/babel-js/areas/admin/account/user-table.js":
/*!************************************************************!*\
  !*** ./wwwroot/babel-js/areas/admin/account/user-table.js ***!
  \************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

"use strict";
eval("\n\nObject.defineProperty(exports, \"__esModule\", {\n    value: true\n});\n\nvar _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if (\"value\" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();\n\nfunction _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError(\"Cannot call a class as a function\"); } }\n\nvar UserTable = function () {\n    function UserTable() {\n        _classCallCheck(this, UserTable);\n    }\n\n    _createClass(UserTable, null, [{\n        key: \"divideColumns\",\n        value: function divideColumns() {\n            var headers = document.querySelectorAll(\".my-table__header-cell\");\n            console.log(headers);\n            var contents = document.querySelectorAll(\".my-table__content-cell\");\n            var percent = (100 / headers.length).toFixed(2);\n            headers.forEach(function (header) {\n                header.style.width = percent.toString() + \"%\";\n            });\n            contents.forEach(function (content) {\n                content.style.width = percent.toString() + \"%\";\n            });\n        }\n    }]);\n\n    return UserTable;\n}();\n\nexports.default = UserTable;\n\n//# sourceURL=webpack://UserTable/./wwwroot/babel-js/areas/admin/account/user-table.js?");

/***/ }),

/***/ 0:
/*!*******************************************************************************************************************************!*\
  !*** multi ./wwwroot/babel-js/areas/admin/account/user-table.js ./wwwroot/babel-js/areas/admin/account/user-table-startup.js ***!
  \*******************************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

eval("__webpack_require__(/*! C:\\Projects\\Git\\MovaClubWebProject\\MovaClubWebApp\\MovaClubWebApp\\wwwroot\\babel-js\\areas\\admin\\account\\user-table.js */\"./wwwroot/babel-js/areas/admin/account/user-table.js\");\nmodule.exports = __webpack_require__(/*! C:\\Projects\\Git\\MovaClubWebProject\\MovaClubWebApp\\MovaClubWebApp\\wwwroot\\babel-js\\areas\\admin\\account\\user-table-startup.js */\"./wwwroot/babel-js/areas/admin/account/user-table-startup.js\");\n\n\n//# sourceURL=webpack://UserTable/multi_./wwwroot/babel-js/areas/admin/account/user-table.js_./wwwroot/babel-js/areas/admin/account/user-table-startup.js?");

/***/ })

/******/ });