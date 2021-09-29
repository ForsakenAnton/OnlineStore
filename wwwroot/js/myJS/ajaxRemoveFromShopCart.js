function ajaxRemoveFromShopCart(productId, quantity) {

    fetch('/ShopCart/AjaxRemoveFromCart?productId=' + productId + "&quantity=" + quantity)
        .then((response) => {

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
                if (quantity) {
                    let countSameProducts = document.getElementById("countSameProductsId" + productId);
                    let price = document.getElementById("priceId" + productId);
                    let discount = document.getElementById("discountId" + productId);
                    let priceWithDiscount = document.getElementById("priceWithDiscountId" + productId);

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
                }



                let countAllProducts = document.getElementById("countAllProductsId");
                let priceTotal = document.getElementById("priceId")
                let discountTotal = document.getElementById("discountId");
                let priceWithDiscountTotal = document.getElementById("priceWithDiscountId");

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


                if (quantity) {
                    let minusSameProduct = document.getElementById("minusSameProductId" + productId);
                    if (minusSameProduct && result.countCurrentProduct <= 1) {
                        minusSameProduct.classList = "disabled btn text-warning text-decoration-none p-0";
                    }
                }
                else {
                    let shopCartItem = document.getElementById("shopCartItemId" + productId);
                    if (shopCartItem) {
                        shopCartItem.remove();
                    }
                }

                //priceWithDiscountTotalInCapture = document.getElementById("priceWithDiscountInCaptureId");

                //if (priceWithDiscountTotalInCapture) {
                //    priceWithDiscountTotalInCapture = "-" + result.discountTotal;
                //}

                document.getElementById("shopCartProductsCount").innerText = result.countAllProducts;
            }
        });
}