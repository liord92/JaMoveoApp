url signup to admin -- > http://localhost:4200/signup/admin

url signup to user -- > http://localhost:4200/signup/user

url login --> http://localhost:4200/login

Using SQL Server Express

To create the database and tables, run the following commands in Developer PowerShell:

dotnet ef migrations add InitialCreate

dotnet ef database update

