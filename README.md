You will need to have installed Docker Desktop to run this application .

After Pulling the repo to a folder , open a powershell inside the main folder .

Run docker compose up -d 

in the main folder (the one containing the docker-compose.yaml)
It will build the BTCapp and then deploy an instance of BTCapp and a MariaDb which is already configured and with the latest migration.

To test functionality you can go at http://localhost:6999/swagger/index.html
where there is a swagger With 1 controller ,2 endpoints

First endpoint implements the first spec of the dev task: to fetch an aggregate price at a certain timepoint.
Second endpoint implements the second spec of the dev task: to fetch various aggregate prices at a range of timepoints.

The implementation is for the REST API solution , There is also a non tested GRPC implementation.

Some Comments:
Less effort than planned went to the unit/integration tests.
The db configuration is quick and dirty .
Swagger API Documentation was left as last and never done.

