using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestBlazorAppGJackson.Classes;

public class PizzaService
{
    private readonly HttpClient _httpClient;

    public PizzaService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Pizza>> GetPizzasAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Pizza>>("https://localhost:7277/api/pizza"); // Full URL for the API
        //return await _httpClient.GetFromJsonAsync<IEnumerable<Pizza>>("api/pizza"); // Endpoint for fetching pizzas
    }

    public async Task<bool> DeletePizzaAsync(int pizzaId)
    {
        var response = await _httpClient.DeleteAsync($"https://localhost:7277/api/pizza/{pizzaId}");

        // Check if the delete request was successful (status code 204 - No Content)
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdatePizzaAsync(Pizza pizza)
    {
        // Send a PUT request to update the pizza on the API
        var response = await _httpClient.PutAsJsonAsync($"https://localhost:7277/api/pizza/{pizza.PizzaID}", pizza);

        // Check if the update request was successful (status code 200 - OK)
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> CreatePizzaAsync(Pizza pizza)
    {
        // Send a POST request to create the pizza on the API
        var response = await _httpClient.PostAsJsonAsync("https://localhost:7277/api/pizza", pizza);

        // Check if the create request was successful (status code 201 - Created)
        return response.IsSuccessStatusCode;
    }
}
