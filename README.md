
## TwitterMicroservice
Created as as an assignment for system integration



## To run this project

### requirements
- you need to have Dot Net 7 installed
- You need to have docker installed and running

1. Run the following command to pull down the git repository
``` 
git clone https://github.com/Kathurjan/TwitterMicroService:
```
2. Run the following command to get the rabbit container
``` 
docker run -d --name my-rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
``` 

3. Setup a docker container for sql. The default password is used for this project
``` 
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=YourStrong@Passw0rd' \
   -p 1433:1433 --name sql_server_demo \
   -d mcr.microsoft.com/mssql/server:2019-latest
``` 
4. At the root of the project run 
```
Docker compose up -d
```
After which the services should be running on ports 
- Authmicroservice: "8070:8080"
- Userinteractionmicroservice: "8080:8080"
- Feedhandlingmicroservice: "8090:8080"
- First run the api url for rebuilding the db "RebuildDB" to ensure that the db is build

### Handling "Socket Hang Up" Error
Incase you get a "Socket hang up" you will have to run the services without docker compose. 
1. Run the Sql and RabbitMQ on docker
2. Then each of the projects you would need to run locally instead of with docker
```
dotnet run
```
Which should fix this Error

