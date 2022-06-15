# SSTTEK.PhoneDirectory
This repository is an assessment for a company called SST Technology. The purpose of this project is to design and develop a phone book application that constains minimum of two microservices. By design my project has two main domains: **Contacts** and **Reports**. Each domain has its own microservice and running application. Integration between them established using RabbitMQ.

## How to Run
You can run this application by running a single command on a shell in the root directory. Before that make sure you have installed Docker on your computer and running the Docker Daemon. After that run the below command:

`docker-compose up`

The apis for the contacts and reports services will run on the ports **7200** and **7201** respectively. Swagger GUI can be accessed on both urls which is "/swagger".
