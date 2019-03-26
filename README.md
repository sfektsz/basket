# Basket API

author: Heman Dookhee

GET     api/products              => Retrieve all products

POST    api/cart/items            => Add product to cart

GET     api/cart/items            => Retrieve all items from cart

GET     api/cart/items/{itemId}   => Retrieve an item from cart

DELETE  api/cart/items            => Clear all items from cart

DELETE  api/cart/items/{itemId}   => Remove an item from cart

PATCH   api/cart/items            => Update the quantity of an item in the cart
