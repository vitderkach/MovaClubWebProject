import UserTable from './user-table.js';
module.exports = {
    run: function () {
        UserTable.divideColumns();
        console.log(UserTable);
    } 
}