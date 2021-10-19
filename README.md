# Welcome to The Bank Machine!

## Some technical stuff first

I enjoy using the [Clean Architecture ](http://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html) as it provides a great way for me to structure the project without having to guess too much. The basis for the clean architecture basically states that an API project (using a controller) sits in the layer after the UI layer of your project. An API project might render endpoints to JSON - which can be seen as a framework-specific thing. Your domain layer should ideally be independent of knowing how to render something to JSON, for instance. The layers represent dependencies - where the inner layers have no dependency on an outer layer. 

The second thing I have enjoyed using a lot recently is the Mediator Pattern using the MediatR library. It allows me to decouple components from each other without one component knowing how to interact with another component. 

The third thing I enjoy using is Entity Framework Core with SQLite. 

For this project I will focus on those three things.

IMPORTANT WARNING: Project uses .NET 6.0 with Visual Studio 2022. 

Please remember the unit tests are not for 100% coverage!

## The Design

We are going to be building an API for an ATM machine. Some rules:

* The customer cannot withdraw more funds then they have access to
* The ATM should not dispense funds if the pin is incorrect
* The ATM should not expose the customer balance if the pin is incorrect
* The ATM should only dispense the exact amounts requested

For all intents and purposes we can have an Account Entity to start off with. We can also have a Transaction entity related to the entity. The Account Entity can either have a running balance property or we can create a method to calculate the balance through the transactions. The Account Entity will also contain the PIN - the security is important but for now it will be plain text. Our goal is to get a basic set of functions going. Also security is a relatively complicated topic. 


