# Blog Web API built using C# & .NET 5
# Overview
This is a single blog API project demonstrating Web APIs in .net 5 using TDD princicples and user authentication with JWT tokens. 
I used the test driven development approach aka (TDD) to build this sample project, it contains 17 unit tests using XUnit framework.
The tests are not complete yet especially for the authentication (Users) controller.

# Installation & testing instructions (Windows):
1. Clone the repo.
2. Make sure you have .net 5 installed from https://dotnet.microsoft.com/en-us/download/dotnet/5.0
3. Navigate to the project directory in the terminal: "CD:\Path_To_Project_Folder"
4. To build the project type "dotnet build"
5. To run all unit tests type "dotnet test"
6. To run the project CD:\WebApi\ then type "dotnet run"
7. The web server should be running on port 8090 now.
8. Download and open Postman: https://www.postman.com/downloads/
9. Start experimenting with the APIs using postman (watch this video guide)


# Areas to improve if I had more time:
1. Use Async methods instead of synchronous to gain more performance.
2. Use Swagger to serve an API manual page.
3. Create a containerized version of the app.
4. Further expand on unit tests.
