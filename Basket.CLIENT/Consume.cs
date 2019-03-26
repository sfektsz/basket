using Basket.CLIENT.Model;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Basket.CLIENT
{
    /// <summary>
    /// This class is a client library that makes use of the Basket API endpoints.
    /// </summary>
    public class Consume
    {
        private static readonly String BaseUrl = "http://localhost:5001/api";

        /// <summary>
        /// Return all products in a list
        /// </summary>
        /// <returns></returns>
        public static List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();

            var client = new RestClient(BaseUrl);
            var request = new RestRequest("/products", Method.GET);

            IRestResponse<List<Product>> response = client.Execute<List<Product>>(request);
            products = response.Data;
            
            return products;
        }

        /// <summary>
        /// Add a product in the database
        /// </summary>
        /// <param name="productId">The id of the product we want to add to the cart</param>
        /// <param name="quantity">The quantity of the product to add to the cart</param>
        /// <returns></returns>
        public static Item AddItemToCart(long productId, int quantity)
        {
            var client = new RestClient(BaseUrl);
            
            var request = new RestRequest("/cart/items", Method.POST);
			request.RequestFormat = DataFormat.Json;
			request.AddJsonBody(new { productId = productId, quantity = quantity });
			
            IRestResponse<Item> response = client.Execute<Item>(request);

            return response.Data;
        }
		
        /// <summary>
        /// Return all items present in the cart in a list
        /// </summary>
        /// <returns></returns>
		public static List<Item> GetCartItems()
		{
			List<Item> items = new List<Item>();
			
			var client = new RestClient(BaseUrl);
			var request = new RestRequest("/cart/items", Method.GET);
			
			IRestResponse<List<Item>> response = client.Execute<List<Item>>(request);
			items = response.Data;
			
			return items;
		}

        /// <summary>
        /// Return the item present in the cart as an Item object
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public static Item GetCartItem(string itemId)
        {
            Item item = new Item();

            var client = new RestClient(BaseUrl);
            var request = new RestRequest("/cart/items/" + itemId, Method.GET);

            IRestResponse<Item> response = client.Execute<Item>(request);
            item = response.Data;

            return item;
        }
		
        /// <summary>
        /// Delete an item present in the cart
        /// </summary>
        /// <param name="itemId">The id of the item we want to remove from the cart</param>
        /// <returns></returns>
		public static IRestResponse DeleteItemInCart(String itemId)
		{
			var client = new RestClient(BaseUrl);
			var request = new RestRequest("/cart/items/" + itemId, Method.DELETE);
			
			IRestResponse response = client.Execute(request);

            return response;
		}
		
        /// <summary>
        /// Clear all items present in the cart
        /// </summary>
        /// <returns></returns>
		public static IRestResponse DeleteAllItemsInCart()
		{
			var client = new RestClient(BaseUrl);
			var request = new RestRequest("/cart/items", Method.DELETE);
			
			IRestResponse response = client.Execute(request);

            return response;
		}
		
        /// <summary>
        /// Update the quantity of an item in the cart
        /// </summary>
        /// <param name="itemId">The id of the item we want to update</param>
        /// <param name="quantity">The new quantity value</param>
        /// <returns></returns>
		public static IRestResponse UpdateItemQuantity(string itemId, int quantity)
		{
			var client = new RestClient(BaseUrl);
			var request = new RestRequest("/cart/items/" + itemId, Method.PATCH);
			request.RequestFormat = DataFormat.Json;
			request.AddJsonBody(new { quantity = quantity });
			
			IRestResponse response = client.Execute(request);

            return response;
		}
    }
}
