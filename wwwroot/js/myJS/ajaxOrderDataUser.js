function ajaxEditOrderDataUser() {

    let userId = document.getElementById("editId").attributes.getNamedItem("data-id").nodeValue;
    console.log(userId);
    fetch('/ShopCart/AjaxEditOrderDataUser?userId=' + userId)
        .then((response) => {

            if (response.status == 204) {
                return;
            }

            return response.text();
        })
        .then((result) => { 
            //console.log(result);
            document.getElementById("orderDataUserId").innerHTML = result;

        }).catch(ex => {
            console.log(ex);
        });
}


//function ajaxPostEditPostOrderDataUser(e) {
//    //console.log("post");
//    //e.preventDefault();
//    let form = document.getElementById("editOrderDataUserFormId");
//    let id = document.getElementById("id").value;
//    let name = document.getElementById("name").value;
//    let surname = document.getElementById("surname").value;
//    let email = document.getElementById("email").value;
//    //console.log(form);
//    const data = {
//        Id: id,
//        Name: name,
//        Surname: surname,
//        Email: email
//    };

//    console.log("Zero then");
//    fetch('/ShopCart/AjaxPostEditOrderDataUser', {
//        method: "POST",
//        body: JSON.stringify(data),
//        headers: {
//            'Content-Type': 'application/json'
//        }
//    })
//        .then((response) => {
//            console.log(response.status);
//            console.log("First then");
//            if (response.status == 204) {
//                return;
//            }
//            console.log("After 204");
//            return response.text();
//        })
//        .then((result) => {
//            console.log("Third then");
//            console.log(result);
//            document.getElementById("orderDataUserId").innerHTML = "123";

//        }).catch(ex => {
//            console.log("catch then");
//            console.log(ex);
//        });
//    console.log("Last then");
//}