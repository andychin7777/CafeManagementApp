# CafeManagementApp
1)  DATABASE

Adjust the connection string in the solution  Solution\CafeManagementApp.Server to point to your target database server accordingly.

"CafeManagementApp": "Data Source=localhost\\MSSQLSERVER2022;Initial Catalog=CafeManagementApp;Integrated Security=True;TrustServerCertificate=True"

Run the DeployScript.sql on that database in Solution\DeployScript.sql

2) Open the solution in visual studio 2022 and run the project.
Accept any of the setup SSL certificates as required to run in local host

Project should start up both the react website and the backend API.