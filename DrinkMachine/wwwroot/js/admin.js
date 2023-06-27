function updateDrink(id) {
    let drinkName = $('#drinkName').val();
    let drinkPrice = $('#drinkPrice').val();
    let drinkQuantity = $('#drinkQuantity').val();

    $.ajax({
        url: '/Admin/UpdateDrink',
        type: 'POST',
        data: {
            Id: id,
            Name: drinkName,
            Price: drinkPrice,
            Quantity: drinkQuantity
        },
        success: function (response) {
            location.reload();
        },
        error: function (error) {
            console.error(error);
        }
    });
}

function deleteDrink(id) {
    $.ajax({
        url: '/Admin/DeleteDrink',
        type: 'POST',
        data: { id: id },
        success: function (response) {
            document.getElementById('drink-' + id).remove();
        },
        error: function (error) {
            console.error(error);
        }
    });
}


$('#addDrinkForm').on('submit', function (e) {
    e.preventDefault();

    let drinkName = $('#drinkName').val();
    let drinkPrice = $('#drinkPrice').val();
    let drinkQuantity = $('#drinkQuantity').val();

    $.ajax({
        url: '/Admin/AddDrink',
        type: 'POST',
        data: JSON.stringify({
            'name': drinkName,
            'price': drinkPrice,
            'quantity': drinkQuantity
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $('#addDrinkForm').trigger("reset");
        },
        error: function (error) {
            console.error(error);
        }
    });
});

function toggleBlockCoin(id) {
    $.ajax({
        url: '/Admin/ToggleBlockCoin',
        type: 'POST',
        data: { coinId: id },
        success: function (response) {
            location.reload();
        },
        error: function (error) {
            console.error(error);
        }
    });
}

function addDrink() {
    let drinkName = document.getElementById('drinkName').value;
    let drinkPrice = document.getElementById('drinkPrice').value;
    let drinkQuantity = document.getElementById('drinkQuantity').value;
    let drinkImageUrl = document.getElementById('drinkImageUrl').value;


    $.ajax({
        url: '/Admin/AddDrink',
        type: 'POST',
        data: {
            Name: drinkName,
            Price: drinkPrice,
            Quantity: drinkQuantity,
            ImageUrl: drinkImageUrl
        },
        success: function (response) {
            document.getElementById('drinkName').value = '';
            document.getElementById('drinkPrice').value = '';
            document.getElementById('drinkQuantity').value = '';
            document.getElementById('drinkImageUrl').value = '';


            let drinksList = document.getElementById('drinksList');

            let newRow = document.createElement('tr');

            let nameCell = document.createElement('td');
            nameCell.textContent = response.name;
            newRow.appendChild(nameCell);

            let priceCell = document.createElement('td');
            priceCell.textContent = response.price;
            newRow.appendChild(priceCell);

            let quantityCell = document.createElement('td');
            quantityCell.textContent = response.quantity;
            newRow.appendChild(quantityCell);

            let actionCell = document.createElement('td');

            let updateButton = document.createElement('button');
            updateButton.classList.add("btn", "btn-primary", "btn-sm");
            updateButton.textContent = "Update";
            updateButton.onclick = function () {
                updateDrink(response.id);
            };
            actionCell.appendChild(updateButton);

            let deleteButton = document.createElement('button');
            deleteButton.classList.add("btn", "btn-danger", "btn-sm");
            deleteButton.textContent = "Delete";
            deleteButton.onclick = function () {
                deleteDrink(response.id);
            };
            actionCell.appendChild(deleteButton);

            newRow.appendChild(actionCell);
            drinksList.appendChild(newRow);
        },

        error: function (error) {
            console.error(error);
        }
    });
}