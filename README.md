# How to launch the app?
In the root folder **/ProjetBell** there is a **docker-compose.yml**, in which all the stacks are configured, simply execute the commande
`docker compose up` to launch it. Normally the front end web app will be accessible from `http://localhost:4200/`. Of course, we will need [Docker desktop](https://www.docker.com/products/docker-desktop/) to excute the command. The data migration towards the database should be executed automatic during the first launch. The database connection is binded with the windows authentication so no user creation is needed. 

Side notes: 
* Front web page: `http://localhost:4200/`
* Backend API: `http://localhost:8080/api/Asset/GetAssets`
* All connection string locations: [InvoiceModule](https://github.com/WutikuerA/ProjetBell/blob/master/ProjetBellAPI/InvoiceModule/appsettings.json), [AssetModule](https://github.com/WutikuerA/ProjetBell/blob/master/ProjetBellAPI/AssetModule/appsettings.json), [ProjetBellAPI](https://github.com/WutikuerA/ProjetBell/blob/master/ProjetBellAPI/ProjetBellAPI/appsettings.json)
* All data context files calling the the connection string: [AssetDataContext.cs](https://github.com/WutikuerA/ProjetBell/blob/master/ProjetBellAPI/AssetModule/DataService/AssetDataContext.cs), [InvoiceDataContext.cs](https://github.com/WutikuerA/ProjetBell/blob/master/ProjetBellAPI/InvoiceModule/DataService/InvoiceDataContext.cs)

# What is the app?
It's small webapp to provide a simple asset creation/edition and invoice generation service. It's created with ASP.NET core 6, Angular 15 as it's front end, SQL server for the database.

# What to do if docker-compose fails?
If, for any reason the docker compose failed, we will have to launch the front and back manually, and configurate the database connection, here is the steps:

1. Go to \ProjetBell\ProjetBellAPI\ProjetBellAPI (yes, repeated twice), this is where the startup project is located, open a powershell console and launch it with `dotnet run`
2. Go to \ProjetBell\projet-bell-webapp, open a powershell console and install the package with `npm install` then `ng serve` to launch the front end.
3. Open the browser and navigate to `http://localhost:4200/` click the Asset button in the menu, and you should see the 3 pre-created service(assets). As shown in the image below.

![image](https://user-images.githubusercontent.com/123587884/215633157-91b6c6b7-b488-4769-afc7-b2a26445b5dc.png)



***
