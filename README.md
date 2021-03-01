# Net Core Microservice Demo
This is a microservice demo project using ASP .NET Core 3.1 based on DDD principles. I have made use of CQRS to separates the concerns of reading and writing data and the Mediator pattern, Automapper, Docker and RabbitMQ.

## Data
I have used Entity Framework Core, an in-memory database(since it's a demo for testing only) and the repository pattern to manage and hold the microservices' information. Feel free to use a normal database, all you have to do is delete the adding customer code present in `CustomerDataContext` constructor and change the following line in the `Startup` class to take your connection string instead of using an in-memory database.

```services.AddDbContext<CustomerContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));```

I recommend setting the database connection string in `appsetting.json` and read the connection string information from `Startup`:

```services.AddDbContext<CustomerContext>(options =>options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));```

## Messaging
I have used RabbitMQ as message broker for sending messages among microservices. You must [install RabbitMQ](https://www.rabbitmq.com/download.html) and I recommend use [Management Plugin](https://www.rabbitmq.com/management.html) since it provides an HTTP API for management and monitoring RabbitMQ nodes.

## How test Microservices
I have used [Swagger](https://swagger.io/solutions/api-documentation/) to document and test the microservices and make this information easily accessible even for none technical people. For microservices testing, you need to start them. This should display the Swagger GUI which gives you information about all endpoints, models and also lets you send requests.

## TODO
* Implement Product microservice and use BackgroundService abstract class to receive messages from Customer Microservice. 
* Implement Logging using Serilog.
* Implement Testing using xUnit.


