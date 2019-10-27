## DEMO :
- https://asp-mvc-movies.herokuapp.com/

## SETUP :

appsettings.json

```
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
      "encryptionKey" : "{{key1}}",
      "encryptionIV" : "{{key2}}"
    }
}
```

## TODO :
- add encryption DONE
- db logging
- add reviews to movies (one to many)
- Implement repository pattern
- Controller tests
- Identity/Fake user tests
- add remaining CRUD error handling