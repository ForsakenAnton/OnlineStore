function ajaxAddToShopCart(productId, element) {

    fetch('/ShopCart/AjaxAddToCart?productId=' + productId)
        .then((response) => {
            //console.log(response.status == 204);
            if (response.status == 204) {
                return;
            }

            return response.json();
        })
        .then((result) => { // isAdded: bool
            console.log(result);
            if (result.isAdded == true) {
                console.log(true);
                element.className = "bi bi-cart btn btn-lg btn-success w-100";
                document.getElementById("ByeButtonId" + productId).innerText = "Added to cart";
                document.getElementById("shopCartProductsCount").innerText = result.countProducts;
            }
        });
}