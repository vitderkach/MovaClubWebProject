import accountSettings from './settings.js'; // ES6 Module
module.exports = {
    run: function (id, contentOpened) {
        var newAccountSettings = new accountSettings(id, contentOpened);
        newAccountSettings.run();
    }
};