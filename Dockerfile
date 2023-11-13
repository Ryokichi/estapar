FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source

COPY ./ ./
WORKDIR /source/estapar_web_api
# RUN dotnet user-secrets init
# RUN dotnet user-secrets set ConnectionStrings:ConnectToEstaparDB "Data Source=localhost;Initial Catalog=EstaparDB;User Id=SA;Password=o4bLty#m;TrustServerCertificate=True"
# RUN dotnet run seeddata
RUN dotnet restore
RUN dotnet publish -c release -o /app --no-restore


# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app ./
EXPOSE 80 8080
ENTRYPOINT ["dotnet", "estapar_web_api.dll"]