using Dapper;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ApiProj.Model;

namespace ApiProj.Services
{
    public class PizzaService
    {
        private readonly string _connectionString;

        public PizzaService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Method to retrieve pizzas from the database
        public async Task<IEnumerable<Pizza>> GetPizzasAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var pizzas = await connection.QueryAsync<Pizza>("SELECT * FROM [TestDB].[dbo].[Pizza]");
                return pizzas;
            }
        }

        // Method to insert a new pizza into the database
        public async Task AddPizzaAsync(Pizza pizza)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Convert the list of toppings into a comma-separated string
                string toppingsString = string.Join(",", pizza.Toppings);

                // Create the query with the comma-separated toppings
                var query = "INSERT INTO [TestDB].[dbo].[Pizza] (PizzaName, PizzaSize, Toppings) VALUES (@PizzaName, @PizzaSize, @Toppings)";

                // Execute the query with the updated toppings string
                await connection.ExecuteAsync(query, new { pizza.PizzaName, pizza.PizzaSize, Toppings = toppingsString });
            }
        }


        // Method to delete a pizza by its ID
        public async Task DeletePizzaAsync(int pizzaId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var query = "DELETE FROM [TestDB].[dbo].[Pizza] WHERE PizzaID = @PizzaId";
                    var affectedRows = await connection.ExecuteAsync(query, new { PizzaId = pizzaId });

                    if (affectedRows == 0)
                    {
                        throw new Exception($"Pizza with ID {pizzaId} not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error (you can use a logger here)
                Console.WriteLine($"Error deleting pizza: {ex.Message}");
                throw; // Re-throw the exception to be handled elsewhere (e.g., API controller)
            }
        }

        // Method to update a pizza by its ID
        public async Task<bool> UpdatePizzaAsync(Pizza pizza)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "UPDATE [TestDB].[dbo].[Pizza] SET PizzaName = @PizzaName, PizzaSize = @PizzaSize, Toppings = @Toppings WHERE PizzaID = @PizzaID";
                var rowsAffected = await connection.ExecuteAsync(query, pizza);

                return rowsAffected > 0;  // Returns true if at least one row was affected
            }
        }


    }
}
