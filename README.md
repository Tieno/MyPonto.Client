# Tieno.MyPonto.Client

MyPotno.Client is a  C# Client for the [MyPonto API](https://documentation.myponto.com/api)

## Installation

## [NuGet](https://www.nuget.org/packages/MyPonto.Client/)
To install the package run the following command on the Package Manager Console:

```
PM> Install-Package MyPonto.Client
```

## Usage

```c#
var clientId = Configuration["MYPONTO_CLIENTID"]; //or somewhere else
var clientSecret = Configuration["MYPONTO_CLIENTSECRET"]; //or somewhere else
var client = MyPontoService.Create(ClientId, ClientSecret);
```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)
