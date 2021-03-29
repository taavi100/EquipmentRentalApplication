using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EquipmentRentalApplication.Models;
using EquipmentRentalApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentRentalApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpPost]
        public async Task<ActionResult<Cart>> PostCart(Cart cart)
        {
            if (ModelState.IsValid)
            {
                Invoice invoice = await _cartService.SaveCart(cart);
                return CreatedAtAction("InvoiceCreated", new { id = invoice.InvoiceId }, invoice);
            }
            else
            {
                return BadRequest(cart);
            }
        }

    }
}
