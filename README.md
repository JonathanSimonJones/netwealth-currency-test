# netwealth-currency-test
A solution to the test provided by Netwealth. Take a value in a initial currency and convert it to another currency.

Projects developed have been deployed to Azure at URL: https://cc-front-end-22-01-01-14-46.azurewebsites.net/

## Overview 

### currency-api-backend 
This is a Azure function project written in C# using .NetCore. It provides 1 endpoint at *domain*/api/Currency for use by currency-converter-front-end. 

### CurrencyConverter
This is a .NetCore library project. It's primary purpose is to provide functionality to get currency data.

### currency-converter-front-end
This is an Angular project. It's primary purpose is to take the data provided by currency-api-backend and CurrencyConverter projects and transform it into a web based format. 

-----------------------------------------
## Building and running

### Requirements
- Visual Studio with Azure and .Net Core development packages
- VS Code with Angular development packages
- *See individual projects for further dependicies*

### Steps
1. Clone netwealth-currency-test 
2. Open netwealth-currency-test.sln (found in the root of the project) in Visual Studio
3. Build and Run CurrencyConverter and currency-api-backend projects

4. Open VS Code 
5. Choose to work from folder currency-converter-front-end
6. Run as an angular project
