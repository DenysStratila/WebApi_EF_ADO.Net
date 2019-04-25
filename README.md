# WebApi_EF_ADO.Net
 A three-tier architecture by using one of two DALs, the first one with ADO.Net technology and the second one with Entity Framework 6. Also, there is Web Api 2.0 for PL.

To change library between ADO.Net and EF uncomment and comment needed namespaces in Services(BLL) and ServiceModule(BLL => Infrastructure).

You need to change the route to the database (the “Database” directory) in the connection string (App.config and Web.config files) for the application to work correctly.
