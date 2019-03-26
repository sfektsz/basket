using Basket.API.Models;
using Basket.API.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartsController : Controller
    {
        private readonly ItemContext _itemContext;
        private readonly IProductRepository<Product> _productRepository;

        public CartsController(ItemContext itemContext, IProductRepository<Product> productRepository)
        {
            _itemContext = itemContext;
            _productRepository = productRepository;
        }

        // GET api/cart/items
        [HttpGet("items")]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            try
            {
                List<Item> items = await _itemContext.FindAll();
                List<object> result = new List<object>();
                items.ForEach((i) => result.Add(new { Product = _productRepository.GetById(i.ProductId), Quantity = i.Quantity, Id = i.Id }));
                
                return Ok(result);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "An unexpected fault happened. Try again later.");
            }
        }

        // GET api/cart/items/{itemId}
        [HttpGet("items/{id}")]
        public async Task<ActionResult<Item>> GetItem(string id)
        {
            try
            {
                var item = await _itemContext.Items.FindAsync(id);

                if (item == null)
                {
                    return NotFound();
                }
                
                return item;
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "An unexpected fault happened. Try again later.");
            }
        }

        // POST api/cart/items
        [HttpPost("items")]
        public async Task<ActionResult<Item>> PostItem(Item item)
        {
            try
            {
                // Check if item already exists in cart based on the ProductId
                var cartItem = _itemContext.Items.SingleOrDefault(c => c.ProductId == item.ProductId);

                if (cartItem == null)
                {
                    // Retrieve the product from the database
                    Product product = _productRepository.GetById(item.ProductId);

                    if (product == null)
                    {
                        return NotFound("The product you are trying to add does not exist.");
                    }

                    // Create new cart item
                    cartItem = new Item
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity
                    };

                    _itemContext.Items.Add(cartItem);
                    await _itemContext.SaveChangesAsync();

                    return CreatedAtAction(nameof(GetItem), new { id = cartItem.Id }, cartItem);
                }
                else
                {
                    // Cart item already exists, update the quantity
                    cartItem.Quantity = ++cartItem.Quantity;
                    _itemContext.Items.Update(cartItem);
                    await _itemContext.SaveChangesAsync();

                    return Ok(cartItem);
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "An unexpected fault happened. Try again later.");
            }
        }

        // DELETE api/cart/items/{id}
        [HttpDelete("items/{id}")]
        public async Task<IActionResult> DeleteItem(string id)
        {
            try
            {
                var item = await _itemContext.Items.FindAsync(id);

                if (item == null)
                {
                    return NotFound();
                }

                _itemContext.Items.Remove(item);
                await _itemContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "An unexpected fault happened. Try again later.");
            }
        }

        // DELETE api/cart/items
        [HttpDelete("items")]
        public async Task<IActionResult> DeleteItems()
        {
            try
            {
                List<Item> items = await _itemContext.FindAll();
                items.ForEach((item) => _itemContext.Items.Remove(item));
                await _itemContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "An unexpected fault happened. Try again later.");
            }
        }

        // PATCH api/cart/items/{id}
        [HttpPatch("items/{id}")]
        public async Task<IActionResult> PatchItemQuantity(Item itemIn, string id)
        {
            try
            {
                var item = await _itemContext.Items.FindAsync(id);

                if (item == null)
                {
                    return NotFound();
                }

                item.Quantity = itemIn.Quantity;
                _itemContext.Items.Update(item);
                await _itemContext.SaveChangesAsync();
                
                return Ok(item);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "An unexpected fault happened. Try again later.");
            }
        }
    }
}
