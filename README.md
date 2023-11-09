dotnet new webapi -n estapar_web_api
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet user-secrets init
dotnet user-secrets set ConnectionStrings:ConnectToEstaparDB "Data Source=localhost;Initial Catalog=EstaparDB;User Id=SA;Password=o4bLty#m;TrustServerCertificate=True"
dotnet ef dbcontext scaffold Name=ConnectionStrings:ConnectToEstaparDB Microsoft.EntityFrameworkCore.SqlServer
dotnet ef migrations add InitialCreate
dotnet ef database update

dotnet run seeddata
