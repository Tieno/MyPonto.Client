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
var pontoClient = MyPontoApi.Create(ClientId, ClientSecret);
```


### Synchronize an account
- creates 2 synchronization resources, one for account details and one for transactions.
- Afterwards you can wait for the synchronizations to complete

```c#
var pendingSynchronizations = await client.SynchronizeAccount(accountResource.Id);
await pendingSynchronizations.WaitTillCompleted();
```

### Fetching Accounts
```C#
var accounts = await pontoClient.Accounts.GetAccounts();

var account = await pontoClient.Accounts.GetAccount(accountId);
```


### Fetching Transactions
besides the 1-1 api methods on IMyPontoApi.ITransactionApi
there are a couple of conveninience extension methods
```C#
var allTransactions = await pontoClient.Transactions.GetAllTransactions();
var newTransactions = await pontoClient.Transactions.GetNewTransactions(accountId, lastKnownTransactionId);
```


## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)
