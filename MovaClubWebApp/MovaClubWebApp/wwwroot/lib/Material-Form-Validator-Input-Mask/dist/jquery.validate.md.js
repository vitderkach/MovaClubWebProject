"use strict";

/*
    jQuery Validation with support Material Design + Masked Input Plugin
    Copyright (c) 2018 Vladislav Mager
    Released under the MIT license
    Version: 1.0
*/
(function ($) {
  $.fn.initValidate = function () {
    var rules = arguments.length > 0 && arguments[0] !== undefined ? arguments[0] : {};
    var message = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : {};
    var debug = arguments.length > 2 && arguments[2] !== undefined ? arguments[2] : false;
    $(this).validate({
      debug: debug,
      rules: rules,
      messages: message,
      onkeyup: function onkeyup(element, event) {
        var el = $(element);
        el.valid();
        el.parent().addClass('error');

        if (el.hasClass('error')) {
          el.parent().removeClass('valid');
          el.siblings('.mdc-text-field__icon').text('error').css('pointer-events', 'none');
        } else if (el.hasClass('valid')) {
          el.parent().removeClass('error');
          el.siblings('.mdc-text-field__icon').text('check_circle').css('pointer-events', 'none');
        }
      },
      onfocusout: function onfocusout(element, event) {
        var el = $(element);
        el.valid();
        el.parent().addClass('error');

        if (el.hasClass('error')) {
          el.parent().removeClass('valid');
          el.siblings('.mdc-text-field__icon').text('error').css('pointer-events', 'none');
        } else if (el.hasClass('valid')) {
          el.parent().removeClass('error');
          el.siblings('.mdc-text-field__icon').text('check_circle').css('pointer-events', 'none');
        }
      },
      errorPlacement: function errorPlacement(error, element) {
        var placement = $(element).data('error');

        if (placement) {
          $('#' + placement).html(error);
        } else {
          console.log('no errors');
        }
      },
      invalidHandler: function invalidHandler(event, validator) {
        Object.keys(validator.invalid).forEach(function (element) {
          validator.currentElements.parent().addClass('error');
          validator.currentElements.parent().removeClass('valid');
          validator.currentElements.siblings('.mdc-text-field__icon').text('error').css('pointer-events', 'none');
        });
      },
      success: function success(label) {
        label.parent().prev().removeClass('error');
        label.parent().prev().addClass('valid');
        label.parent().prev().find('.mdc-text-field__icon').text('check_circle').css('pointer-events', 'none');
      }
    });
    return this;
  };
})(jQuery);
/* Messages */


$.extend($.validator.messages, {
  fullemail: 'Please enter a valid email address.',
  requiredphone: "This field is required.",
  minlengthphone: "Please enter a valid phone number.",
  fio: "Please enter a valid your real name"
});
/* Custom Validation Method */

$.validator.addMethod('minlengthphone', function (value, element) {
  return value.replace(/\D+/g, '').length > 10;
}, $.validator.messages.minlengthphone);
$.validator.addMethod('requiredphone', function (value, elements) {
  return value.replace(/\D+/g, '').length > 1;
}, $.validator.messages.requiredphone);
$.validator.addMethod('emailfull', function (value, element) {
  return this.optional(element) || /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/.test(value);
}, $.validator.messages.fullemail); // Validate method for Фамилия Имя Отчество || Фамилия Имя

$.validator.addMethod('fio', function (value, element) {
  return this.optional(element) || /^[А-ЯЁ][а-яё]+(-[А-ЯЁ][а-яё]+)? [А-ЯЁ][а-яё]+( [А-ЯЁ][а-яё]+)?$/.test(value);
}, $.validator.messages.fio);
/* Validator Rules */

$.validator.addClassRules({
  tel: {
    requiredphone: true,
    minlengthphone: true
  }
});