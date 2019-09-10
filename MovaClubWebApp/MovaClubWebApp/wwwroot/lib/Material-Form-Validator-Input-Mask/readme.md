[jQuery Validation with support Material Design + Masked Input Plugin](https://github.com/magersoft/jQuery-Validation-with-support-Material-Design-Masked-Input-Plugin) - Form validation made easy
================================

If you use Material Design from Google and you need form validation, then these are extensions for you.

## Getting Started

### Install Plugin

With bower
```code
bower install --save jquery.validate.md
```

With npm
```code
npm install --save-dev jquery.validate.md
```

### Including it on your page

Include jQuery, jQuery Validation, jQuery Masked Input and the plugin on a page. Then select a material design form [(Text field)](https://material.io/develop/web/components/input-controls/text-field/) to validate and call the `initValidate` method.

```html
<form>
<div class="mdc-text-field mdc-text-field--outlined mdc-text-field--with-trailing-icon">
  <input type="text" id="tf-outlined" class="mdc-text-field__input" data-error="error">
  <label for="tf-outlined" class="mdc-floating-label">Your Name</label>
  <i class="material-icons mdc-text-field__icon" tabindex="0" role="button"></i>
  <div class="mdc-notched-outline">
    <svg>
      <path class="mdc-notched-outline__path"/>
    </svg>
  </div>
  <div class="mdc-notched-outline__idle"></div>
</div>
<p class="mdc-text-field-helper-text mdc-text-field-helper-text--persistent" id="error" aria-hidden="true"></p>
</form>
<script src="jquery.js"></script>
<script src="jquery.validate.js"></script>
<script src="jquery.maskedinput.js"></script>
<script src="jquery.validate.md.js"></script>
<script>
    $('form').initValidate();
</script>
```


The method takes the following arguments: 
rules = array, messages = array, debug = boolean

```js
$('form').initValidate(rules, messages, debug);
```

Example argument rules
```js
var rules = {
        email: {
            required: true,
            minlength: 4,
            emailfull: true
            },
}
```

## New Validation Method
```js
$.validator.addClassRules({
    email: {
      emailfull: true  
    },
    tel: {
        requiredphone: true,
        minlengthphone: true
    },
    name: {
        fio: true
    }
});
```
## New Messages
```js
$.validator.messages({
    requiredphone: "This field is required.",
    minlengthphone: "Please enter a valid phone number.",
    fio: "Please enter a valid your real name"
});
```

You can override the methods and messages with jQuery Validation

## License
Copyright &copy; Vladislav Mager<br>
Licensed under the MIT license.
