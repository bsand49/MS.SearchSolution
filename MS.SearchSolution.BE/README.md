# MS.SearchSolution.BE

This is a simple web api that can be used to return a collection of Person records.
The API can be run locally on you machine via a terminal session, or utilising Docker.

## Endpoints

### HealthCheck
This endpoint is used to identify the API is running. It simply returns a 200 response code.
`GET ../api/healthcheck`

### Search
This endpoint is used to search for person records, which are filtered using the searchTerm you provide.
`GET ../api/search/persons?searchTerm={searchTerm}`

## Run BE API in Terminal
Before you begin, ensure you are in the BE's directory. I.e. `{pathToRepo}/MS.SearchSolution.BE`.

If you plan to run ms-searchsolution-fe on a port other than `localhost:3000` locally, or `localhost:9050` in Docker, you will need to amend the URLs specified in the CORS policy configured as standard in `MS.SearchSolutions.BE.API.Program.cs`, before you run the application.

### Clean Solution
Run command
`dotnet clean`

### Restore Solution Dependencies
Run command
`dotnet restore`

### Build Solution
Run command
`dotnet build --no-restore`

### Run Solution
Run command
`dotnet run --no-build MS.SearchSolution.BE.API`

### Test Solution
Run command
`dotnet test --no-build`

## Run BE API in Docker
Before you begin, ensure you are in the BE's directory. I.e. `{pathToRepo}/MS.SearchSolution.BE`.

If you plan to run ms-searchsolution-fe on a port other than `localhost:3000` locally, or `localhost:9050` in Docker, you will need to amend the URLs specified in the CORS policy configured as standard in `MS.SearchSolutions.BE.API.Program.cs`, before you build the docker image.

### Build Docker Image
To build the image, and run command
`docker build -t ms-searchsolution-be-api .`

Check the image has been created using command
`docker image ls`

### Run Docker Container
To run a container in the Development environment, run command
`docker run --name ms-searchsolution-be-api-dev -p {hostPort}:8080 -e ASPNETCORE_ENVIRONMENT=Development -d ms-searchsolution-be-api`

Ensure you replace `{hostPort}` with the port you wish to expose the container in on your host machine.

If you wish to run in a Staging or Production environment, replace `dev` with either `stg` or `prod` in the container name, and replace `Development` with either `Staging`, or `Production` when setting the `ASPNETCORE_ENVIRONMENT` environment variable. I.e.
`docker run --name ms-searchsolution-be-api-stg -p {hostPort}:8080 -e ASPNETCORE_ENVIRONMENT=Staging -d ms-searchsolution-be-api`
`docker run --name ms-searchsolution-be-api-prod -p {hostPort}:8080 -e ASPNETCORE_ENVIRONMENT=Production -d ms-searchsolution-be-api`

To check the container is running, run command `docker ps -a`

### Stop and Remove Docker Container
To stop the container, run command
`docker stop ms-searchsolution-be-api-{env}`

To remove the container, run command
`docker rm ms-searchsolution-be-api-{env}`

Again, be sure to replace `{env}` with `dev`, `stg` or `prod` depending on the environment you are running in

### Remove Docker Image
To remove the image, run command
`docker image rm ms-searchsolution-be-api`

## Making API Requests

### Browser
In your browser's address bar, enter the following web address `http://localhost:{hostPort}/{endpointPath}` changing `{hostPort}` to the port you have the application running on, and `{endpointPath}` to the path of the endpoint you wish to make a request to.

You can only send `GET` requests using this method, and the responses that have content will be displayed in a raw format.

### API Platform
Using the API platform of your choice, e.g. Postman, send a web request to `http://localhost:{hostPort}/{endpointPath}`. Again, changing `{hostPort}` to the port you have the application running on, and `{endpointPath}` to the path of the endpoint you wish to make a request to. Also ensure you have set the correct request method, i.e. `GET`, `POST`, `PUT`, etc.

### Swagger
A swagger front end for the API is available, but only when running in the Development environment. To access it, navigate to `http://localhost:{hostPort}/swagger` in your browser.
Again, change `{hostPort}` to the port you have the application running on.

This page contains a lot of information about the API, and is an easy way of visualising each of the endpoints, the HTTP methods supported, the request content required and the response content. It also contains the schemas the endpoints use, so you can see the data structures without even having to send a request.