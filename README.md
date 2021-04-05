# docker-rabbitmq

It is a simple application consist of customer service(which saves the customer information into a mysql db), queue service(which publish message to specified queue) and a worker service which consumes data from a specific queue(welcoming-mail).

MAC OS:

* First install [Docker Desktop](https://hub.docker.com/editions/community/docker-ce-desktop-mac/)
* Then pull the repository and run the command below at the root directory(where the docker-compose.yml file is)
    * docker-compose -f docker-compose.yml up --build
    
* The Customer Service will be running on http://localhost:5000
* The Queue Service will be running on http://localhost:5001
* You can reach rabbitmq management panel on http://localhost:15672(the username and password will be guest)
