## DOTNET - VirtualMind

* Database creation script can be found in ./scripts/db.sql
* You should change the connection string in ./api/appsettings.json to match your configuration
* To run the app, you must install the npm packages

## API - Available endpoints are:

#### Get a specific currency rate
* **[GET]** /api/currency/{ISO} 
* **Sample:** /api/currency/usd
* **Headers:** { "Content-Type", "application/json" }

#### Create new transaction
* **[POST]** /api/transaction 
* **Body:** { "userId": 1, "Amount": 1, "CurrencyCode": "USD" }
* **Headers:** { "Content-Type", "application/json" }