import { MDCTopAppBar } from '@material/top-app-bar/index'
import { MDCTextField } from '@material/textfield'
import { MDCRipple } from "@material/ripple"
const topAppBarElement = document.querySelector('.mdc-top-app-bar');
const topAppBar = new MDCTopAppBar(topAppBarElement);
const textFields = new MDCTextField(document.querySelectorAll('.mdc-text-field'));
const buttonRipple = new MDCRipple(document.querySelectorAll('.mdc-button'));