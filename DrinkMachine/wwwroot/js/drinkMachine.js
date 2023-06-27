function depositCoin(val) {
    $.ajax({
        url: 'https://localhost:7017/User/DepositCoin',
        type: 'POST',
        data: { id: val },
        success: function (response) {
            $('#balance').text(response);
            updateDrinkButtons(response);
        },
        error: function (error) {
            console.error(error);
        }
    });
}


function purchaseDrink(drinkId) {
    $.ajax({
        url: '/User/PurchaseDrink',
        type: 'POST',
        data: { id: drinkId },
        success: function (data) {
            $('#balance').text(data.balance);
            updateDrinkButtons(data.balance);

            let quantityElement = $('#quantity-' + drinkId);
            quantityElement.text(data.drink.quantity);

            $('#drink-' + drinkId).data('quantity', data.drink.quantity);

            if (data.drink.quantity <= 0) {
                $('#drink-' + drinkId).prop('disabled', true);
            }
        },
        error: function (error) {
            console.error(error);
        }
    });
}



function getChange() {
    $.ajax({
        url: '/User/GetChange',
        type: 'POST',
        success: function (response) {
            $('#changeResult').empty();

            let coinDisplay = $('<div class="row"></div>');

            $.each(response, function (index, coin) {
                let coinElement =
                    '<div class="col-md-2 text-center mb-3">' +
                    '<img src="' + coin.imageUrl + '" class="img-fluid rounded-circle mb-3" alt="coin"/>' +
                    '<p>' + coin.quantity + 'x ' + coin.value + ' рублей</p>' +
                    '</div>';

                coinDisplay.append(coinElement);
            });

            $('#changeResult').append(coinDisplay);

            $('#balance').text(0);

            updateDrinkButtons(0);
        },
        error: function (error) {
            console.error(error);
        }
    });
}




function updateDrinkButtons(balance) {
    $('button[id^="drink-"]').each(function () {
        let price = parseFloat($(this).attr('data-price'));
        let quantity = parseFloat($(this).attr('data-quantity'));

        if (price > balance || quantity <= 0) {
            $(this).attr('disabled', true);
        } else {
            $(this).attr('disabled', false);
        }
    });
}
