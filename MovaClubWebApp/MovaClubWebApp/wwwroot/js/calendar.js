var currDate = new Date();
var month = currDate.getMonth();
var year = currDate.getFullYear();
var daysCount = daysInMonth(month, year);
var day = currDate.getDate();

var calendarDays = document.getElementsByClassName("calendar__days");
var calendarMonth = document.getElementById("month");
var calendarYear = document.getElementById("year");
var calendarPrev = document.getElementsByClassName("month__prev");
var calendarNext = document.getElementsByClassName("month__next");

var calendarDay = document.getElementsByClassName("schedule__day");
var calendarWeekDay = document.getElementsByClassName("schedule__weekday");
calendarPrev[0].addEventListener("click", prevMonth);
calendarNext[0].addEventListener("click", nextMonth);
initialize();
var scheduleContent = document.querySelector(".schedule__content");
scheduleContent.addEventListener("click", function (e) {
    var relativeX = e.clientX - scheduleContent.getBoundingClientRect().left;
    var relativeY = e.clientY - scheduleContent.getBoundingClientRect().top;
    var divCoeff = scheduleContent.scrollHeight / 24;
    var currItem = relativeY / divCoeff;
    var numb1 = ((parseInt(currItem * 10)) / 10);
    var numb2 = (parseInt(currItem));
    var betweenContents = (numb1 - numb2 >= 0.5);
    if (betweenContents == true) {

    }
    else {

    }
}, false);
function daysInMonth(iMonth, iYear)
{
    return 32 - new Date(iYear, iMonth, 32).getDate();
}

function prevMonth()
{
    if (month == 0) {
        month = 11;
        year--;
    }
    else {
        month--;
    }
    daysCount = daysInMonth(month, year); 
    initialize();
}

function nextMonth() {
    if (month == 11) {
        month = 0;
        year++;
    }
    else {
        month++;
    }
    daysCount = daysInMonth(month, year);
    initialize();
}

function getMonth(i)
{
    var month;
    switch (i) {
        case 0:
            month = january;
            break;
        case 1:
            month = february;
            break;
        case 2:
            month = march;
            break;
        case 3:
            month = april;
            break;
        case 4:
            month = may;
            break;
        case 5:
            month = june;
            break;
        case 6:
            month = july;
            break;
        case 7:
            month = august;
            break;
        case 8:
            month = september;
            break;
        case 9:
            month = october;
            break;
        case 10:
            month = november;
            break;
        case 11:
            month = december;
            break;
        default:
            break;
    }

    return month;
}

function getWeekDay(i) {
    var weekDay;
    switch (i) {
        case 0:
            weekDay = monday;
            break;
        case 1:
            weekDay = tuesday;
            break;
        case 2:
            weekDay = wednesday;
            break;
        case 3:
            weekDay = thursday;
            break;
        case 4:
            weekDay = friday;
            break;
        case 5:
            weekDay = saturday;
            break;
        case 6:
            weekDay = sunday;
            break;
        default:
            break;
    }
            return weekDay;
}

function initialize()
{
    while (calendarDays[0].firstChild) {
        calendarDays[0].removeChild(calendarDays[0].firstChild);
    }
    var firstDay = new Date(year, month, 1);
    for (var i = 1; i < firstDay.getDay(); i++) {
        var li = document.createElement('li');
        li.className = "days__item";
        calendarDays[0].appendChild(li);
    }
    for (var i = 1; i <= daysCount; i++) {
        var li = document.createElement('li');
        li.className = "days__item";
        if (i == day && month == currDate.getMonth() && year == currDate.getFullYear()) {
            var span = document.createElement('span');
            span.className = "days__item--active";
            span.innerHTML = i.toString();
            li.appendChild(span);
        }
        else {
            li.innerHTML = i.toString();
        }
            
        
        
        calendarDays[0].appendChild(li);
    }
    calendarYear.innerHTML = year;
    calendarMonth.innerHTML = getMonth(month);
    calendarDay[0].innerHTML = day;
    calendarWeekDay[0].innerHTML = getWeekDay(currDate.getDay());
    return;
}

