using Dapper;
using System.Data.SqlClient;
using TestBlazorAppGJackson.Classes;

namespace ApiProj.Services
{
    public class ToppingService
    {
        private readonly string _connectionString;

        public ToppingService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Method to get all toppings from the database
        public async Task<IEnumerable<Topping>> GetToppingsAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var toppings = await connection.QueryAsync<Topping>("SELECT * FROM Toppings");
                return toppings;
            }
        }

        // Method to add a new topping
        public async Task AddToppingAsync(Topping topping)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "INSERT INTO Toppings (ToppingName) VALUES (@ToppingName)";
                await connection.ExecuteAsync(query, topping);
            }
        }

        // Method to delete a topping
        public async Task<bool> DeleteToppingAsync(int toppingId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "DELETE FROM Toppings WHERE ToppingID = @ToppingID";
                var affectedRows = await connection.ExecuteAsync(query, new { ToppingID = toppingId });
                return affectedRows > 0;
            }
        }

        // Method to update a topping
        public async Task<bool> UpdateToppingAsync(Topping topping)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "UPDATE Toppings SET ToppingName = @ToppingName WHERE ToppingID = @ToppingID";
                var affectedRows = await connection.ExecuteAsync(query, topping);
                return affectedRows > 0;
            }
        }
    }

}
