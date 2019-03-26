
$(document).ready(function() {
  getProducts();
  getCartItems();
});

function refresh() {
  getProducts();
  getCartItems();
}

// GET api/products
function getProducts() {
  $.ajax({
    type: "GET",
    url: "api/products",
    cache: false,
    success: function(data) {
      console.log("Retrieving Products: " + JSON.stringify(data));
      const tBody = $("#products");
      
      $(tBody).empty();

      $.each(data, function(key, item) {
        const tr = $("<tr></tr>")
          .append($("<td></td>").text(item.name))
          .append($("<td></td>").text(item.description))
          .append($("<td></td>").text(item.price));

        tr.append($("<td></td>").append(
          $("<button>Add To Cart</button>").on("click", function() {
            addItem(item.id, 123);
          })
        ));

        tr.appendTo(tBody);
      });
    }
  });
}

// GET api/cart/items
function getCartItems() {
  $.ajax({
    type: "GET",
    url: "api/cart/items",
    cache: false,
    success: function(data) {
      console.log("Retrieving Cart Items:\n" + JSON.stringify(data));
      const tBody = $("#cart");

      $(tBody).empty();

      $.each(data, function(key, item) {
        console.log("Data: " + JSON.stringify(data));
        const tr = $("<tr></tr>")
          .append($("<td></td>").text(item.product.name))
          .append($("<td></td>").text(item.product.description))
          .append($("<td></td>").text(item.product.price))
          .append($("<td></td>").text(item.quantity));

        tr.append($("<td></td>").append(
          $("<button>Remove</button>").on("click", function() {
            deleteCartItem(item.id);
          })
        ));

          tr.appendTo(tBody);
      });
    }
  });
}

//POST api/cart/items
function addItem(_productId, _quantity) {
  const item = {
    productId: _productId,
    quantity: _quantity
  };

  $.ajax({
    type: "POST",
    accepts: "application/json",
    url: "api/cart/items",
    contentType: "application/json",
    data: JSON.stringify(item),
    error: function(jqXHR, textStatus, errorThrown) {
      alert("Item already added to cart!");
    },
    success: function(result) {
      refresh();
    }
  });
}

// DELETE api/cart/items
function deleteCartItem(itemId) {
  $.ajax({
    type: "DELETE",
    url: "api/cart/items/" + itemId,
    cache: false,
    success: function(data) {
      refresh();
    }
  });
}
