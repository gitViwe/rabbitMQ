# RabbitMQ
RabbitMQ is open source message broker software (sometimes called message-oriented middleware) that implements the Advanced Message Queuing Protocol (AMQP). The RabbitMQ server is written in the Erlang programming language and is built on the Open Telecom Platform framework for clustering and failover. Client libraries to interface with the broker are available for all major programming languages.

### Built With

* [.NET 6](https://devblogs.microsoft.com/dotnet/announcing-net-6/)
* [ASP.NET Web API](https://dotnet.microsoft.com/apps/aspnet/apis)

### Prerequisites

Things you need to use the software and how to install them.
* [Visual Studio](https://visualstudio.microsoft.com/)
* [.NET 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
* [Docker](https://www.docker.com/)

### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/gitViwe/rabbitMQ.git
   ```

## Getting Started

First you need to run RabbitMQ management service, docker will make this easy
Run `docker compose up -d` on the root folder, then you can go to http://localhost:15672 in a browser.
