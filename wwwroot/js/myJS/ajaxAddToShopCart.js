function ajaxAddToShopCart(productId) {
    fetch('/ShopCart/AjaxAddToCart?productId=' + productId)
        .then((response) => {
            //console.log(response.status == 204);
            if (response.status == 204) {
                return;
            }

            return response.json();
        })
        .then((result) => {
            console.log(result);
                //isSuccess = true,
                //countAllProducts : int
                //countCurrentProduct : int
                //price : decimal
                //discount : decimal
                //priceWithDiscount : decimal
                //priceTotal
                //discountTotal
                //priceWithDiscountTotal


            if (result.isSuccess == true) {
                console.log(true);

                let addToShopCartButton = document.getElementById("addToShopCartButtonId" + productId);
                let byeButton = document.getElementById("byeButtonId" + productId);
                //let countCurrentProduct = document.getElementById("countCurrentProductId" + productId);

                if (addToShopCartButton) {
                    addToShopCartButton.className = "bi bi-cart btn btn-lg btn-success w-100 disabled";
                }
                if (byeButton) {
                    byeButton.innerText = "Added to cart";
                }
                //if (countCurrentProduct) {
                //    countCurrentProduct.innerText = result.countCurrentProduct;
                //}


                let lastViewedAddToShopCartButton = document.getElementById("lastViewedAddToShopCartButtonId" + productId);
                let lastViewedByeButton = document.getElementById("lastViewedByeButtonId" + productId);
                //let lastViewedCountCurrentProduct = document.getElementById("lastViewedCountCurrentProductId" + productId);

                if (lastViewedAddToShopCartButton) {
                    lastViewedAddToShopCartButton.classList = "bi bi-cart btn btn-lg btn-success w-100 disabled";
                }
                if (lastViewedByeButton) {
                    lastViewedByeButton.innerText = "Added to cart";
                }
                //if (lastViewedCountCurrentProduct) {
                //    lastViewedCountCurrentProduct.innerText = result.countCurrentProduct;
                //}

                document.getElementById("shopCartProductsCount").innerText = result.countAllProducts;

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


                let countSameProducts = document.getElementById("countSameProductsId" + productId);
                let price = document.getElementById("priceId" + productId);
                let discount = document.getElementById("discountId" + productId);
                let priceWithDiscount = document.getElementById("priceWithDiscountId" + productId);

                let countAllProducts = document.getElementById("countAllProductsId");
                let priceTotal = document.getElementById("priceId")
                let discountTotal = document.getElementById("discountId");
                let priceWithDiscountTotal = document.getElementById("priceWithDiscountId");

                let minusSameProduct = document.getElementById("minusSameProductId" + productId);

                if (countSameProducts) {
                    countSameProducts.innerText = result.countCurrentProduct;
                }
                if (price) {
                    price.innerText = result.price + "$";
                }
                if (discount) {
                    discount.innerText = "-" + result.discount + "$";
                }
                if (priceWithDiscount) {
                    priceWithDiscount.innerText = result.priceWithDiscount + "$";
                }

                if (countAllProducts) {
                    countAllProducts.innerText = result.countAllProducts;
                }
                if (priceTotal) {
                    priceTotal.innerText = result.priceTotal + "$";
                }
                if (discountTotal) {
                    discountTotal.innerText = "-" + result.discountTotal + "$";
                }
                if (priceWithDiscountTotal) {
                    priceWithDiscountTotal.innerText = result.priceWithDiscountTotal + "$";
                }

                if (minusSameProduct) {
                    minusSameProduct.classList = "btn text-warning text-decoration-none p-0";
                }

            //    priceWithDiscountTotalInCapture = document.getElementById("priceWithDiscountInCaptureId");

            //    if (priceWithDiscountTotalInCapture) {
            //        priceWithDiscountTotalInCapture = "-" + result.discountTotal;
            //    }
            }
        });
}