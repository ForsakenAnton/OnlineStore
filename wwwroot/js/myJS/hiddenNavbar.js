let prevScrollPosition = window.pageYOffset;

let navbar = document.getElementById("navbar");
let firstRowOfNavbar = document.getElementById("firstRowOfNavbar");
let secondRowOfNavbar = document.getElementById("secondRowOfNavbar");

//let navbarHeight = navbar.clientHeight;
let firstRowHeight = firstRowOfNavbar.clientHeight;

let windowInnerWidth = document.body.clientWidth;
//let windowInnerHeight = document.body.clientHeight;


let showBsCollapseNavbarHeight = 0;
// изменяем ширину браузера, так же может измениться и высота элементов. С помощью события onresize фиксим проблему


window.onload = function () {
    if (windowInnerWidth < 974) {
        replaceOrders("2", "1");
    }
}

function replaceOrders(order1, order2) {
    firstRowOfNavbar.className = firstRowOfNavbar.className.slice(0, firstRowOfNavbar.className.length - 2) + firstRowOfNavbar.className.slice(firstRowOfNavbar.className.length - 2, firstRowOfNavbar.className.length - 1) + order1;
    secondRowOfNavbar.className = secondRowOfNavbar.className.slice(0, secondRowOfNavbar.className.length - 2) + secondRowOfNavbar.className.slice(secondRowOfNavbar.className.length - 2, secondRowOfNavbar.className.length - 1) + order2;

    //console.log(firstRowOfNavbar.className);
    //console.log(secondRowOfNavbar.className);
}


window.onresize = function (event) {

    //navbarHeight = navbar.clientHeight;
    firstRowHeight = firstRowOfNavbar.clientHeight;
    windowInnerWidth = document.body.clientWidth;
    //windowInnerHeight = document.body.clientHeight;
    console.log(windowInnerWidth);

    if (windowInnerWidth < 974) {
        if (firstRowOfNavbar.className.slice(9) == "order-1") {
            replaceOrders("2", "1");
        }
    } else {
        if (firstRowOfNavbar.className.slice(9) == "order-2") {
            replaceOrders("1", "2");        
        }
    }
};

window.onscroll = function (e) {

    let currentScrollPosition = window.pageYOffset;
    let currentNavbarHeight = navbar.clientHeight;

    if (prevScrollPosition > currentScrollPosition) {
        /*if (windowInnerWidth < 991.98) {*/
        if (windowInnerWidth < 974) {
            navbar.style.top = "0";
        } else {
            navbar.style.top = - (firstRowHeight) + "px"; // минусовое значение (половинка остаестся, там где основная навигация)
        }
    } else {
        if (showBsCollapseNavbarHeight != 0 && windowInnerWidth < 974) {
            navbar.style.top = - (showBsCollapseNavbarHeight) + "px";
        } else {
            navbar.style.top = - (currentNavbarHeight) + "px"; // минусовое значение (вообще скрываем)
        }
    }

    prevScrollPosition = currentScrollPosition;
};



var myCollapsible = document.getElementById('navbarToggler')
myCollapsible.addEventListener('show.bs.collapse', function () {
    showBsCollapseNavbarHeight = navbar.clientHeight;
    console.log("show ============ " + showBsCollapseNavbarHeight);
});

myCollapsible.addEventListener('hide.bs.collapse', function () {
    showBsCollapseNavbarHeight = "0";
    console.log("hide ============ " + showBsCollapseNavbarHeight);
});