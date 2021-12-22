# eBroker

_eBroker is a web application for online trading of equities. The user who acts as a trader can perform operations like adding funds, buy new equity or sell their equities_

**Running Project**

To run this project, Navigate to folder Nagarro.NAGP.EBroker.API & execute following command:

```dotnet run```

_This will run the application on port 44332. All the services are accessible with URL [https://localhost:44332/equity](https://localhost:44332/equity)._

_Also for documentation and use of API, I have used Swagger to enable user interation with controller. For using swagger navigate to [https://localhost:44332/swagger](https://localhost:44332/swagger)._

    When using Visual Studio, use Nagarro.NAGP.EBroker.sln, set Nagarro.NAGP.EBroker.API as startup project and run the application, It will run application on localhost and open the url in browser by default.

*To execute all the test cases in Visual Studio, Open Test Explorer and Click Run all Test Cases button*

-----------

*To execute all the test cases and generating coverage report. Install report generator global tool using below command*

```dotnet tool install -g dotnet-reportgenerator-globaltool```

Now execute following commands from Nagarro.NAGP.EBroker.Tests project folder:

```dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=.\TestResults\Coverage```

Now using the path for coverage file, execute following to generate html report:

```reportgenerator -reports:".\TestResults\Coverage\coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html```

__Results of the executed test cases and saved coverage report can be viewed in directory -__
`Nagarro.NAGP.EBroker\Nagarro.NAGP.EBroker.Tests\TestResults\Coverage` _and_ `Nagarro.NAGP.EBroker\Nagarro.NAGP.EBroker.Tests\coveragereport`

