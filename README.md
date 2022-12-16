# BevNet Challenge

## Requirements

  - .Net Core 6
  - Reactjs 18.0.26

## Installation

### Running API

The location of the API project is \api\Search.WebAPI

 - Open command prompt as administrator
 - enter the command 'dotnet run'
 - This will be start running the web api, the expected base url is  https://localhost:7139


### Running WEB App

The location of the web app is \ui\movie-searcher

Install dependencies:

```bash
yarn install
```

Run the server:

```bash
yarn dev
```
### Configuring API Url in WEB App

The api URL must be configure if different of https://localhost:7139.

To configure go to the file \ui\movie-searcher.env.local and change the url.