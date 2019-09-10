import { MDCTopAppBar } from '@material/top-app-bar/index'
import { MDCTextField } from '@material/textfield'
import { MDCRipple } from '@material/ripple'
//import { MDCFloatingLabel } from '@material/floating-label'
//import { MDCNotchedOutline } from '@material/notched-outline'
import { MDCMenu } from '@material/menu';
import { MDCSelect } from '@material/select'
import { MDCList } from '@material/list'
import { MDCTextFieldHelperText } from '@material/textfield/helper-text';
import { MDCTextFieldIcon } from '@material/textfield/icon'



const topAppBar = new MDCTopAppBar(document.querySelector('.mdc-top-app-bar'));
const textFields = document.querySelectorAll('.mdc-text-field');
if (textFields != undefined) {
    textFields.forEach(textField => new MDCTextField(textField));
    
}

var observer = new MutationObserver(observeFieldHelpers);
var observeConfig = { characterData: true };
const helperTexts = document.querySelectorAll('.mdc-text-field-helper-text');
if (helperTexts != undefined) {
    helperTexts.forEach(helperText => {
        var s = new MDCTextFieldHelperText(helperText);
        observer.observe(helperText.children[0], observeConfig);
    });
}


const buttonRipples = document.querySelectorAll('.mdc-button');
if (buttonRipples  != undefined) {
    buttonRipples.forEach(buttonRipple => new MDCRipple(buttonRipple));
}

const selects = document.querySelectorAll('.mdc-select');
if (selects != undefined) {
    selects.forEach(select => {
       new MDCSelect(select);
    });
        }
const lists = document.querySelectorAll('.mdc-list');
if (lists != undefined) {
    lists.forEach(list => {
       // var mdcList = new MDCList(list);
       // mdcList.listElements.map((mdcListItemEl) => new MDCRipple(mdcListItemEl));
    });
}
//const listItemRipples = list.listElements.map((listItemEl) => new MDCRipple(listItemEl));

//const menus = document.querySelectorAll('.mdc-menu');
//if (menus != undefined) {
//    menus.forEach(menu => {
//        new MDCMenu(menu);
//    });
//}
//menu.open = true;
const iconButtonRipples = document.querySelectorAll('.mdc-icon-button');
if (iconButtonRipples != undefined) {
    iconButtonRipples.forEach(iconButtonRipple => {
        new MDCRipple(iconButtonRipple);
    });
}


const toolbarButton = document.getElementById("toolbar-button");
const toolbarMenu = document.getElementById("toolbar-menu");
if (toolbarButton != undefined && toolbarMenu != undefined) {
    let mdcMenu = new MDCMenu(toolbarMenu);
    toolbarButton.addEventListener("click", function () {
        mdcMenu.open = true;
    });
}


const languageButton = document.getElementById("language-button");
const languageMenu = document.getElementById("language-menu");
if (languageButton != undefined && languageMenu != undefined) {
    let mdcMenu = new MDCMenu(languageMenu);
    languageButton.addEventListener("click", function () {
        mdcMenu.open = true;
    });
}

const icons = document.querySelectorAll(".mdc-text-field__icon");
if (icons != undefined) {
    icons.forEach(icon => {
        new MDCTextFieldIcon(icon);
    });
}


function observeFieldHelpers(mutationList, observer) {
    mutationList.forEach((mutation) => {
        switch (mutation.type) {
            case 'characterData':
                var helper = mutation.target;
                break;
        }
    });
}
