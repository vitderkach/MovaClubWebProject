export default class UserTable {
    public static divideColumns() {
        var headers = document.querySelectorAll(".my-table__header-cell");
        console.log(headers);
        var contents = document.querySelectorAll(".my-table__content-cell");
        var percent = (100 / headers.length).toFixed(2);
        headers.forEach(header => {
            header.style.width = percent.toString() + "%";
        });
        contents.forEach(content => {
            content.style.width = percent.toString() + "%";
        });

    }
}



