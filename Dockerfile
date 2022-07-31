FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY . ./src
RUN dotnet restore ./src/EmployeeMicroservice.sln
RUN dotnet build ./src/EmployeeMicroservice.sln
RUN dotnet publish ./src/Employee.API/Employee.API.csproj -c Release -o outdir

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS final
WORKDIR /app
COPY --from=build /src/outdir .
ENTRYPOINT ["dotnet", "Employee.API.dll"]
