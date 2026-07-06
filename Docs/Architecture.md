# Architechure

### Flow
UI -> API [Controller -> MediatR -> Request Handler] -> DB

### UI
* Angular [version 22.0.5]
* additional node modules used
	* #### @angular/material @angular/cdk @angular/animations
  Purpose : Better UI look and feel for Grids and Fields  
	* #### Karma
  Purpose : Unit tests

### API
* .Net [version 10]
* additional Nugget packages used
  * #### SPA Proxy
  Purpose : launching SPA CLI
  * #### MediatR
  Purpose : for loose coupling between controllers and business logic
  * #### OpenAPI
  Purpose : annotate route handler endpoints
  * #### Swashbuckle
  Purpose : swagger UI
  * #### Entity Framework
  Purpose : DB connection

  ### Database
  * In Memory
  * additional Nugget packages used
    * #### Entity Framework In memory
      Purpose : to use In Memory

  ### Test
  * Unit Tests
  * additional Nugget packages used
    * #### XUnit
      Purpose : testing framework
    * #### FluentValidation
      Purpose : for validation rules