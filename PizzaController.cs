using ApiProj.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiProj.Model;

namespace ApiProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly PizzaService _pizzaService;

        // Constructor that injects PizzaService
        public PizzaController(PizzaService pizzaService)
        {
            _pizzaService = pizzaService;
        }

        // GET: api/pizza
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var pizzas = await _pizzaService.GetPizzasAsync();
            return Ok(pizzas); // Returns the list of pizzas
        }

        // POST: api/pizza
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Pizza pizza)
        {
            // Add the new pizza using the service layer
            await _pizzaService.AddPizzaAsync(pizza);

            // Respond with a 201 Created status and include the created pizza
            return CreatedAtAction(nameof(Get), new { id = pizza.PizzaID }, pizza);
        }

        // DELETE: api/pizza/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Call the service layer to delete the pizza
            await _pizzaService.DeletePizzaAsync(id);

            // Respond with a 204 No Content status to indicate successful deletion
            return NoContent();
        }

        // PUT: api/pizza/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Pizza pizza)
        {
            // Check for a mismatch between the pizza ID in the URL and the pizza object in the request body
            if (id != pizza.PizzaID)
            {
                return BadRequest("Pizza ID mismatch");
            }

            // Call the service to update the pizza
            var success = await _pizzaService.UpdatePizzaAsync(pizza);

            if (!success)
            {
                return NotFound(); // If the pizza isn't found, return 404
            }

            return NoContent(); // Return a 204 No Content on successful update
        }
    }
}
