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
- SignalR - webapck -typscript (https://docs.microsoft.com/en-us/aspnet/core/tutorials/signalr-typescript-webpack?view=aspnetcore-3.0&tabs=visual-studio-code)
- add clear field to input box
- add remaining CRUD error handling
- add JWT https://jasonwatmore.com/post/2019/10/11/aspnet-core-3-jwt-authentication-tutorial-with-example-api#tools-required
- add roles managing page

