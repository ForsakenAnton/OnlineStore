let mainSearchInput = document.getElementById('mainSearchInput');
let mainSearchAjaxResult = document.getElementById('mainSearchAjaxResult')

mainSearchInput.addEventListener('input', ajaxSearch);
mainSearchInput.addEventListener('focus', ajaxSearch);
mainSearchInput.addEventListener('blur', blurMainSearchInput);


function ajaxSearch() {
    //alert(searchInput.value);
    fetch('/home/ajaxmainsearchpartial?searchString=' + mainSearchInput.value)
        .then((response) => {
            return response.text();
        })
        .then((result) => {
            mainSearchAjaxResult.style.display = "block";
            mainSearchAjaxResult.innerHTML = result;
        });

    // делаем активной/неактивной кнопку поиска в зависимости от наличия текста в mainSearchInput
    if (mainSearchInput.value.length === 0) {
        console.log(mainSearchInput.value.length === 0);
        document.getElementById('submitSearchButton').disabled = true;
    }
    else {
        console.log(mainSearchInput.value.length !== 0);
        document.getElementById('submitSearchButton').disabled = false;
    }
}

function blurMainSearchInput() {
    // не успевает перейти по ссылке при клике по ней и бегом сворачивает поиск, потому и setTimeout
    setTimeout(() => {
        mainSearchAjaxResult.style.display = "none";
    }, 200);
}










//mainSearchInput.addEventListener('input', () => {
//    //alert(searchInput.value);
//    fetch('/home/ajaxmainsearchpartial?searchString=' + mainSearchInput.value)
//        .then((response) => {
//            return response.text();
//            alert(response.text());
//        })
//        .then((result) => {
//            document.getElementById('mainSearchAjaxResult').innerHTML = result;
//        });

//    // делаем активной/неактивной кнопку поиска в зависимости от наличия текста в mainSearchInput
//    if (mainSearchInput.value.length === 0) {
//        console.log(mainSearchInput.value.length === 0);
//        document.getElementById('submitSearchButton').disabled = true;
//    }
//    else {
//        console.log(mainSearchInput.value.length !== 0);
//        document.getElementById('submitSearchButton').disabled = false;
//    }
//});