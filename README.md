# Products Service API

## Objective
Develop a minified version of the products module api. The API should provide endpoints to make it possible to fetch all the products, a single one, add a new product, edit and delete it. Requirements (object structure, etc.) are described via the mockups images below and should be derived from them.

## Running with Docker
In the Dockerfile directory, run the following
```
docker build . -t myimage
docker run -d -p 80:80 myimage:latest
```
### Note
* Have Redis running on the host machine on port 6379 for caching


## Implementation
* Database
    * MySQL
    * EF Core ORM framework to work with database
* Tests project
* Project setup to run as a docker container
* [Automapper library](https://automapper.org/) to map models to view models
* Attach user object
    * GET by id request: product should include the user object
    * the POST/PUT request will include a `userId` which is the owner of the product
* Caching
    * Redis
* Swagger
* Get all products request should include the user object
* CORS to allow working with deployed service from localhost

### Notice
This is the first time writing C#, some things may have been done incorrectly. Eitherway, I'm kinda proud of it.

### Database View
![Database View](images/database_view.jpg)
### Create/Edit View
![Edit View](images/edit_view.jpg)

