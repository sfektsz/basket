using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;
using Basket.CLIENT;
using Basket.CLIENT.Model;

namespace Basket.UnitTest
{
    public class ProductsControllerTest
    {
        [Fact]
        public void GetProducts()
        {
            // Act
            List<Product> products = Consume.GetProducts();

            // Assert
            var items = Assert.IsType<List<Product>>(products);
            bool isEmpty = !items.Any();

            Assert.False(isEmpty);
        }

        [Fact]
        public void AddItemToCart()
        {
            // Arrange
            long productId = 1;
            int quantity = 2;

            // Act
			Item item1 = Consume.AddItemToCart(productId, quantity);
            Assert.NotNull(item1);

            Item item = Consume.GetCartItem(item1.Id);
            Assert.NotNull(item);

            // Assert
            Assert.Equal(item1.Id, item.Id);
        }

        [Fact]
        public void DeleteItemInCart()
        {
            // Arrange
			Item item2 = Consume.AddItemToCart(2, 1);
            Assert.NotNull(item2);

            // Act
            Consume.DeleteItemInCart(item2.Id);

            // Assert
            Item item = Consume.GetCartItem(item2.Id);
            Assert.Null(item.Id);
        }

        [Fact]
        public void DeleteAllItemsInCart()
        {
            // Arrange
            Item item3 = Consume.AddItemToCart(3, 1);
            Assert.NotNull(item3);

            Item item4 = Consume.AddItemToCart(4, 1);
            Assert.NotNull(item4);

            List<Item> items = Consume.GetCartItems();
            Assert.False(!items.Any());

            // Act
            Consume.DeleteAllItemsInCart();

            // Assert
            List<Item> i = Consume.GetCartItems();
            bool isEmpty = !i.Any();
            Assert.True(isEmpty);
        }

        [Fact]
        public void UpdateItemQuantity()
        {
            // Arrange
            Consume.DeleteAllItemsInCart();
            Item item2 = Consume.AddItemToCart(1, 1);
            Assert.NotNull(item2);
            Assert.Equal(1, item2.Quantity);

            // Act
            Consume.UpdateItemQuantity(item2.Id, 123);

            // Assert
            Item item = Consume.GetCartItem(item2.Id);
            Assert.NotNull(item);
            Assert.Equal(123, item.Quantity);
        }

    }
}
