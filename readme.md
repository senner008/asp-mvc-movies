## DEMO :
- https://asp-mvc-movies.herokuapp.com/

## appsettings.json

{
    "Logging": {
      "LogLevel": {
        "Default": "Information",
        "Microsoft": "Warning",
        "Microsoft.Hosting.Lifetime": "Information"
      }
    },
    "AllowedHosts": "*",
    "ConnectionStrings": {
      "HerokuJawsDB": "{{mysqldb-string}}",
      "UnoEuroDb": "{{mysqldb-string}}"
    },
    "Passwords" : {
      "adminpass" : "{{seeded-admin-user-pass}}"
    }
}