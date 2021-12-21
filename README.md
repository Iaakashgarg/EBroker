eBroker

#Running Project
To run this project, Navigate to folder Nagarro.NAGP.EBroker.API & execute following command:

dotnet run
This will run application on port 44332. All the services are accessible with "https://localhost:44332/equity" url. Also for documentation and use of API, I have used Swagger to enable user interation with controller. For using swager navigate to "https://localhost:44332/swagger".

When using Visual Studio, use Nagarro.NAGP.EBroker.sln, set Nagarro.NAGP.EBroker.API as startup project and run the application, It will run application on localhost and open the url in browser by default.

To execute all the test cases and generating coverage report. Install report generator global tool using below command

dotnet tool install -g dotnet-reportgenerator-globaltool
Now execute following commands from Nagarro.NAGP.EBroker.Tests project folder:

dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=.\TestResults\Coverage
Now using the path for coverage file, execute following to generate html report:

reportgenerator -reports:".\TestResults\Coverage\coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html
Results of the executed test cases and saved coverage report can be viewed in directory - Nagarro.NAGP.EBroker\Nagarro.NAGP.EBroker.Tests\TestResults\Coverage and Nagarro.NAGP.EBroker\Nagarro.NAGP.EBroker.Tests\coveragereport respectively.
