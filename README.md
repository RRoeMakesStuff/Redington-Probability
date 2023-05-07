# Getting started
## Requirements 
- Docker including Docker-Compose
- .NET6
- React

### Recommendations
It's recommended to run this project from VSCode, as it'll be easiest to view files, as well as manage multiple terminals if needed.

## Starting the API
The API can be started using the command `docker-compose up` from the root directory. You may wish to run it in detatched mode if you want to reuse the terminal for running the UI. This can be done by running `docker-compose up -d`

## Running the UI
Once the API container is active, navigate to the `probability-ui` folder in your terminal, and run the following commands
`npm install`
`npm start`