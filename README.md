# PizzaCRMApplication
This is a client side C# .NET blazor WASM application with a REST Web API project connected in the same solution that connects to a SSMS SQL database. The current project is only set for local host connection currently but could easily work when published via the cloud or IIS with simple rework to the location the API is accessing and the client side is sending the request to. NOTE: if used, you will need to setup a appsettings.json for the project to ensure DB connection. 

This application was made in practice to allow for the user to manage pizza and configure toppings with full validation and full CRUD with the DB. 

Manage Toppings -
Allows the user to add new toppings
Allows the user to delete existing toppings
Allows the user to update existing toppings
Does not allow the user to enter duplicate toppings

Manage Pizza -
Allows the user to create new pizzas with toppings configured in toppings UI
Allows the user to delete existing pizzas
Allows the user to update existing pizza and toppings
Does not allow the user to enter duplicate pizzas based on name and toppings 
