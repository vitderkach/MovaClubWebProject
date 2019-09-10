import { MDCTextField } from '@material/textfield';
import { MDCTextFieldHelperText } from '@material/textfield/helper-text';
import { MDCSelect } from '@material/select';
export default class accountSettings {
    constructor(id, contentOpened) {
        this.id = id;
        this.contentOpened = contentOpened;
    }
    run() {

        var switcher = document.getElementById(this.id);

        var parentItem = switcher.parentElement;
        var listItemMeta = switcher.querySelector(".settings-arrow");
        if (this.idcontentOpened) {
            parentItem.querySelector(".settings-form").classList.remove("settings-form--hidden");
            listItemMeta.innerHTML = "keyboard_arrow_down";
        }
        var mdcList = switcher.parentElement;
        var listHeight = 0;
        for (var i = 0; i < mdcList.children.length; i++) {
            listHeight += mdcList.children.item(i).clientHeight;
        }
        mdcList.style.height = listHeight + "px";



        var listFields = parentItem.querySelectorAll('.mdc-text-field');
        if (listFields != undefined) {
            listFields.forEach(listField => {
                var mdcField = new MDCTextField(listField);
                var helperTextId = listField.querySelector(".mdc-text-field__input").getAttribute("aria-controls");
                if (helperTextId != null) {
                    var helperText = document.getElementById(helperTextId);
                    new MDCTextFieldHelperText(helperText);
                    if (helperText.children.item(0).innerHTML != "") {
                        mdcField.valid = false;
                    }
                }
            });
        }

        var selects = parentItem.querySelectorAll('.mdc-select');
        if (selects != null) {
            selects.forEach(select => {
                new MDCSelect(select);
            });
        }


        switcher.addEventListener("click", function () {

            var formIndex;
            for (var i = 0; i < parentItem.childNodes.length; i++) {
                if (parentItem.childNodes[i] == switcher) {
                    formIndex = i + 2;
                    break;
                }
            }
            if (listItemMeta.innerHTML == "keyboard_arrow_right") {
                listItemMeta.innerHTML = "keyboard_arrow_down";
            }
            else {
                listItemMeta.innerHTML = "keyboard_arrow_right";
            }
            parentItem.childNodes[formIndex].querySelector(".settings-form").classList.toggle("settings-form--hidden");
            var listHeight = 0;
            for (var i = 0; i < mdcList.children.length; i++) {
                listHeight += mdcList.children.item(i).clientHeight;
            }
            mdcList.style.height = listHeight + "px";

        }, false);
    }

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

