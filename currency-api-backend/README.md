#currency-api-backend

This is a Azure function project written in C# using .NetCore. It provides 1 endpoint at *domain*/api/Currency which takes 3 URL parameters:

- amount / The amount of the currency to convert
- base / The currency code of the initial country
- desired / The currency code the desired country

e.g https://*domain*/api/Currency?base=GBP&desired=JPY&amount=123123

The URL parameters determine the JSON returned from the endpoint.   

Deployed to Azure through Visual Studio. 

Make sure to enable CORS on production environments.

##Details
.NET Core 3.1
Azure Windows Function Runtime version 3
