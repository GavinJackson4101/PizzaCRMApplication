using System.Net.Http.Json;
using TestBlazorAppGJackson.Classes;

public class ToppingService
{
    private readonly HttpClient _httpClient;

    public ToppingService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Method to get toppings
    public async Task<IEnumerable<Topping>> GetToppingsAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Topping>>("https://localhost:7277/api/topping");
    }

    // Method to add a new topping
    public async Task<bool> AddToppingAsync(Topping topping)
    {
        var response = await _httpClient.PostAsJsonAsync("https://localhost:7277/api/topping", topping);
        return response.IsSuccessStatusCode;
    }

    // Method to delete a topping
    public async Task<bool> DeleteToppingAsync(int toppingId)
    {
        var response = await _httpClient.DeleteAsync($"https://localhost:7277/api/topping/{toppingId}");
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateToppingAsync(Topping topping)
    {
        var response = await _httpClient.PutAsJsonAsync($"https://localhost:7277/api/topping/{topping.ToppingID}", topping);
        return response.IsSuccessStatusCode;
    }

}
