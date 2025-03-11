namespace ApiProj.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TestBlazorAppGJackson.Classes;
    using global::ApiProj.Services;

    namespace ApiProj.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class ToppingController : ControllerBase
        {
            private readonly Services.ToppingService _toppingService;

            // Constructor that injects ToppingService
            public ToppingController(ToppingService toppingService)
            {
                _toppingService = toppingService;
            }

            // GET: api/topping
            [HttpGet]
            public async Task<IActionResult> Get()
            {
                var toppings = await _toppingService.GetToppingsAsync();
                return Ok(toppings); // Returns the list of toppings
            }

            // POST: api/topping
            [HttpPost]
            public async Task<IActionResult> Post([FromBody] Topping topping)
            {
                if (topping == null || string.IsNullOrEmpty(topping.ToppingName))
                {
                    return BadRequest("Invalid topping data.");
                }

                // Add the new topping using the service layer
                await _toppingService.AddToppingAsync(topping);

                // Respond with a 201 Created status and include the created topping
                return CreatedAtAction(nameof(Get), new { id = topping.ToppingID }, topping);
            }

            // DELETE: api/topping/{id}
            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                // Call the service layer to delete the topping
                var success = await _toppingService.DeleteToppingAsync(id);

                if (!success)
                {
                    return NotFound(); // If the topping isn't found, return 404
                }

                // Respond with a 204 No Content status to indicate successful deletion
                return NoContent();
            }

            // PUT: api/topping/{id}
            [HttpPut("{id}")]
            public async Task<IActionResult> Put(int id, [FromBody] Topping topping)
            {
                // Check for a mismatch between the topping ID in the URL and the topping object in the request body
                if (id != topping.ToppingID)
                {
                    return BadRequest("Topping ID mismatch");
                }

                // Call the service to update the topping
                var success = await _toppingService.UpdateToppingAsync(topping);

                if (!success)
                {
                    return NotFound(); // If the topping isn't found, return 404
                }

                return NoContent(); // Return a 204 No Content on successful update
            }
        }
    }

}
