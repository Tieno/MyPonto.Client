# Tieno.MyPonto.Client

MyPonto.Client is a  C# Client for the [MyPonto API](https://documentation.myponto.com/api)

## Installation

## [NuGet](https://www.nuget.org/packages/Tieno.MyPonto.Client/)
To install the package run the following command on the Package Manager Console:

```
PM> Install-Package Tieno.MyPonto.Client
```

## Usage

### Create the client
```c#
var clientId = Configuration["MYPONTO_CLIENTID"]; //or somewhere else
var clientSecret = Configuration["MYPONTO_CLIENTSECRET"]; //or somewhere else
var client = MyPontoService.Create(ClientId, ClientSecret);
```


### Synchronize an account
- creates 2 synchronization resources, one for account details and one for transactions.
- Afterwards you can wait for the synchronizations to complete

```c#
var pendingSynchronizations = await client.SynchronizeAccount(accountResource.Id);
await pendingSynchronizations.WaitTillCompleted();
```

### Fetch an Account
TODO

### Fetch All Transactions
TODO

### Fetch New Transactions
TODO


## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)
