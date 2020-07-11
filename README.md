# EthereumTransactionSearch
An Ethereum search service to search for Ethereum transactions

# Steps
To run application:
1. cd src
2. dotnet run

To run tests:
1. cd test/EthereumTransactionSearch.Tests/
2. dotnet test

To execute API:
1. Enter [http://localhost:5000/swagger/index.html](http://localhost:5000/swagger/index.html)
2. Input "BlockNumber" and "Address"
3. Example input, BlockNumber = 9148873 and Address = "0xc55eddadeeb47fcde0b3b6f25bd47d745ba7e7fa"

# Requirements
- .Net Core 3.1 runtime
- .Net Core 3.1 SDK

# Notes

What would I add in a real-world production system:
- Performance monitoring such as adding a New Relic agent.
- The API project is structured to be as simple as possible. In a real-world scenario, I would move models and services into their own projects so that they can be easily shared across new developments going forward.
- Deployment pipeline
- Log shipping to selected log monitoring platform such as Cloudwatch/DataDog/SumoLogic

# Author
Chester Lau
chesterlau@live.com
