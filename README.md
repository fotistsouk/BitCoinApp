A sample Microservice implementation of a BTC price aggregator , complete with database caching .

You will need to have installed Docker Desktop to run this application .

After Pulling the repo to a folder , open a powershell inside the main folder .

Run docker compose up -d 

in the main folder (the one containing the docker-compose.yaml)
It will build the BTCapp and then deploy an instance of BTCapp and a MariaDb which is already configured and with the latest migration.

To test functionality you can go at http://localhost:6999/swagger/index.html
where there is a swagger With 1 controller ,2 endpoints

First endpoint use is to fetch an aggregate price of BTC at a certain timepoint.
Second endpoint implements fetch various aggregate prices at a range of timepoints.

The implementation is for the REST API solution , There is also a non tested GRPC implementation.


