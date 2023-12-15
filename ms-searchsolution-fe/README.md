# ms-searchsolution-fe

This is a simple web page that allows you to search for persons records. It queries a .NET web api, and displays the results in a table.

## Run FE in Terminal
Before you begin, ensure you are in the FE's directory. I.e. `{pathToRepo}/ms-searchsolution-fe`.

If you plan to run MS.SearchSolution.BE on a port other than `localhost:9049` in Docker, you will need to amend the `REACT_APP_BE_API_URL` variable in the .env file.

It is recommended you run the application in Node v20.10.0.

### Install Dependecies
Run command `npm install` to install the application's dependencies.

### Run the Application
Run command `npm start` to run the app in the development mode.

### Test the Application
Run command `npm test` to launches the test runner in the interactive watch mode.

## Run FE in Docker
Before you begin, ensure you are in the FE's directory. I.e. `{pathToRepo}/ms-searchsolution-fe`.

If you plan to run MS.SearchSolution.BE on a port other than `localhost:9049` in Docker, you will need to amend the `REACT_APP_BE_API_URL` variable in the .env file.

### Build Docker Image
To build the image, run command
`docker build -t ms-searchsolution-fe .`

Check the image has been created using command
`docker image ls`

### Run Docker Container
To run a container in the Development environment, run command
`docker run --name ms-searchsolution-fe -p {hostPort}:3000 -d ms-searchsolution-fe`

Ensure you replace `{hostPort}` with the port you wish to expose the container in on your host machine.

### Stop and Remove Docker Container
To stop the container, run command
`docker stop ms-searchsolution-fe`

To remove the container, run command
`docker rm ms-searchsolution-fe`

### Remove Docker Image
To remove the image, run command
`docker image rm ms-searchsolution-fe`

## Accessing the FE
Once the container is running, simply open your browser and naviagte to the following addresses
* http://localhost:{hostPort}/

Again, change `{hostPort}` to the port you have the application running on.