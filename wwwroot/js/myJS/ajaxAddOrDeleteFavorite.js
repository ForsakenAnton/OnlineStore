function ajaxAddOrDeleteFavorite(productId, element) {
    fetch('/FavoriteProducts/AddOrDeleteFavorite?productId=' + productId)
        .then((response) => {
            //console.log(response.status == 204);
            if (response.status == 204) {
                return;
            }

            return response.json();
        })
        .then((result) => { // addToFavorite: bool, count: int
            console.log(result);
            if (result.addToFavorite == true) {
                console.log(true);
                element.className = "h4 bi bi-suit-heart-fill text-success";
                document.getElementById("favoriteProductsCount").innerText = result.count;

            } else if (result.addToFavorite == false) {
                console.log(true);
                element.className = "h4 bi bi-suit-heart fw-bolder text-secondary";
                document.getElementById("favoriteProductsCount").innerText = result.count;
            }
            //else
        });
}