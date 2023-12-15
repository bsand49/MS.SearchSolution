# MS.SearchSolution
This repository contains a .NET web api that is used to retrieve data, and a React front end that offers a search function, and queries the .NET web api.

It is recommended you run both the web api and front end in Docker on ports 9049 and 9050 respectively. If you wish to run the applications outside of Docker, or in Docker but exposed to different ports, visit the READMEs below for more information on what changes are required
* [MS.SearchSolution.BE](MS.SearchSolution.BE/README.md)
* [ms-searchsolution-fe](ms-searchsolution-fe/README.md)

## Run in Docker
Before you begin, ensure you are in the repsitory's base directory.

### Build Docker Images
To build the image for the BE, run command
`docker build -t ms-searchsolution-be-api ./MS.SearchSolution.BE/.`

To build the image for the FE, run command
`docker build -t ms-searchsolution-fe ./ms-searchsolution-fe/.`

### Run Docker Containers
To run a container for the BE, run command
`docker run --name ms-searchsolution-be-api -p 9049:8080 -e ASPNETCORE_ENVIRONMENT=Development -d ms-searchsolution-be-api`

To run a container for the FE, run command
`docker run --name ms-searchsolution-fe -p 9050:3000 -d ms-searchsolution-fe`

### Stop and Remove Docker Containers
To stop the containers, run commands

`docker stop ms-searchsolution-be-api`

`docker stop ms-searchsolution-fe`

To remove the containers, run commands
`docker rm ms-searchsolution-be-api`

`docker rm ms-searchsolution-fe`


### Remove Docker Images
To remove the image, run commands

`docker image rm ms-searchsolution-be-api`

`docker image rm ms-searchsolution-fe`

## Accessing the Applications
Once the containers are running, simply open your browser and naviagte to the following addresses for the BE and FE respectively
* http://localhost:9049/swagger/index.html
* http://localhost:9050/