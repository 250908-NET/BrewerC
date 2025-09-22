/*
To create the unit test structure, we can use the following commands:

dotnet new xunit -n ToDoApp.Test
dotnet new sln 
dotnet sln add ToDoApp/ToDoApp.csproj
dotnet sln add ToDoApp.Test/ToDoApp.Test.csproj
dotnet add ToDoApp.Test/ToDoApp.Test.csproj reference ../ToDoApp/ToDoApp.csproj

|/
|-unitTestingExample
    |-unitTestingExample.sln
    |-ToDoApp
        |-ToDoApp.csproj
        |-Program.cs
    |-ToDoApp.Test
        |-ToDoApp.Test.csproj
        |-UnitTestExamples.cs

To run the tests, use the command:
dotnet test
(from the solution/application folder)
*/

// We'll use these a little later...
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http.Json;
using System.Text.Json;

namespace ToDoApp.Test
{
    // Some "traditional" unit tests examples
    // Every unit test should follow the Arrange, Act, Assert pattern
    // Arrange - setup any objects, mocks, or data needed for the test
    // Act - execute the method or functionality being tested
    // Assert - verify that the outcome is as expected

    // Sometimes, steps can be handled in the constructor or setup methods to apply globally
    // to all tests in the class. We'll do this with the MathService.

    // Testing the MathService class
    public class MathServiceTests
    {
        // Arange
        private readonly IMathService _mathService;
        
        // constructor to initialize the service
        public MathServiceTests()
        {
            _mathService = new MathService();
        }

        [Fact]
        public void SingleSquareTest()
        {
            // Act
            var result = _mathService.Square(2);

            // Assert
            Assert.Equal(4, result);
        }

        // Ideally, your unit test names should follow the pattern:
        // MethodName_StateUnderTest_ExpectedBehavior
        // Example: Square_NegativeNumber_ThrowsArgumentException

        [Fact]
        public void Square_NegativeNumber_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _mathService.Square(-2));
        }

        // Fact is used for simple unit tests that do not require parameters.
        // You can also use Theory and InlineData to test multiple cases

        [Theory]
        //[InlineData(input, expected)]
        [InlineData(2, 4)]
        [InlineData(3, 9)]
        [InlineData(4, 16)]
        public void Square_ValidInput_ReturnsExpectedResult(int input, int expected)
        {
            // Act
            var result = _mathService.Square(input);

            // Assert
            Assert.Equal(expected, result);
        }

        // Fluent Assertions library can be used for more readable assertions
        // dotnet add package FluentAssertions

        [Fact]
        public void Square_UsingFluentAssertions()
        {
            // Act
            var result = _mathService.Square(3);

            // Assert
            result.Should().Be(9); // WAY more readable!
            
            // You can also chain multiple assertions to check various conditions as part of the same test
            result.Should().BePositive();
            result.Should().BeGreaterThan(5)
                          .And.BeLessThan(15); // and even use method chaining with .And
        }
    }

    /*
    Doing basic integration tests for the API functionality of our ToDo application
    To test the API endpoints, we can use the WebApplicationFactory<T> class from the Microsoft.AspNetCore.Mvc.Testing package:

    dotnet add package Microsoft.AspNetCore.Mvc.Testing

    The WebApplicationFactory<T> class allows us to create a test server for our ASP.NET Core application.
    We can then use an HttpClient to send requests to the API endpoints and verify the responses
    */

    // the first time we try to do this, we will probably forget that the Program class is not public by default,
    // and our test will fail to compile. To fix this, we need to make the Program class public in Program.cs
    
    public class ToDoApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        // HttpClient to send requests to the test server
        private readonly HttpClient _client;

        // Constructor to initialize the HttpClient using the WebApplicationFactory
        public ToDoApiTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        // A simple test to verify that the root endpoint ("/") returns a successful response
        // we have new keywords here: async and Task.
        // async indicates that the method is asynchronous and can use the await keyword
        // Task represents an asynchronous operation that can return a value in the future
        [Fact]
        public async Task GetRoot_NoVal_ReturnsOkResponse()
        {
            // Arrange is done in the constructor

            // Act
            var response = await _client.GetAsync("/");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetRoot_NoVal_ReturnsHelloWorld()
        {
            // Arrange is done in the constructor

            // Act
            var response = await _client.GetAsync("/");
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal("Hello World!", content);
        }
        
        [Theory]
        [InlineData(2, 4)]
        [InlineData(3, 9)]
        [InlineData(4, 16)]
        public async Task GetSquare_ReturnsExpectedResult(int input, int expected)
        {
            // Arrange is done in the constructor

            // Act
            var response = await _client.GetAsync($"/square/{input}");
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var content = await response.Content.ReadAsStringAsync();
            var result = int.Parse(content); // convert the string response to an integer

            // Assert
            Assert.Equal(expected, result);
            result.Should().Be(expected); // using FluentAssertions for readability

            // We can validate the returned values
            result.Should().BePositive()
                            .And.BeGreaterThan(1)
                            .And.BeLessThan(20); 
            
            // and we can also validate the response and code itself 
            // we should also be able to use negative tests, proving that the value under testing is NOT something
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK) // 2xx's
                            .And.NotBe(System.Net.HttpStatusCode.BadRequest); // not 4xx's
        }

        [Theory]
        [InlineData(3, 4, 12)]
        [InlineData(5, 6, 30)]
        [InlineData(7, 8, 56)]
        public async Task PostArea_ValidJSONSerializedData_ReturnsExpectedResult(double width, double height, double expected)
        {
            // Most of the Arrange is done in the constructor
            // but we still need to create the request body as JSON
            var requestBody = new
            {
                Width = width,
                Height = height
            };

            // this will take the requestBody object, serialize it to JSON, and create the appropriate HttpContent
            // it works because the API endpoint expects a JSON body to be accepted and deserialized into an AreaRequest object
            var jsonContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(requestBody), System.Text.Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/area", jsonContent);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = double.Parse(content);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(3, 4, 12)]
        [InlineData(5, 6, 30)]
        [InlineData(7, 8, 56)]
        public async Task PostArea_ValidDataAsObject_ReturnsExpectedResult(double width, double height, double expected)
        {
            // Arrange
            // We can also create an object for the request body, and borrow that from Program.cs
            var request = new AreaRequest(width, height);

            // Act
            var response = await _client.PostAsJsonAsync("/area", request);

            // Assert
            response.EnsureSuccessStatusCode();

            // we can validate the returned value after converting from JSON to a double
            var area = await response.Content.ReadFromJsonAsync<JsonElement>();
            var areaValue = area.GetDouble();
            areaValue.Should().Be(expected);
        }

        /*
        This one may be a little weird to think about, but I wanted to demonstrate that we can also test for error conditions.
        In this case, we know that the Square endpoint will throw an exception if we provide a negative number.
        We can test that the API responds with a 500 Internal Server Error in this case.
        
        Throwing a 500 may not be the best option for this method, instead we should probably return a 400 Bad Request
        with a message indicating that the input was invalid. But for now, we'll just test for the 500 response.
        
        This is probably a good time to make sure we have out HTTP status codes down:
        1xx - Informational
        2xx - Success
        3xx - Redirection
        4xx - Client Error - bad request
        5xx - Server Error - the serer broke
        */

        [Fact]
        public async Task GetSquare_NegativeNumber_ReturnsServerError()
        {
            // Arrange is done in the constructor

            // Act
            var response = await _client.GetAsync($"/square/-2");

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.InternalServerError); // 5xx's
        }
    }
}

/*
I mentioned that we can run code coverage analysis to determine how many lines, what percentage of our application, and how many branches are covered by our tests.
Here's how we do that:

First off, xUnit projects come with the coverage tool "coverage.collector", we can use that to generate the data we want. It won't be pretty, but we'll be able to fix that next.
To collect the coverage report, we want to use the command
dotnet test --collect:"XPlat Code Coverage"

The --collect will tell the collector to do it's thing, the "XPlat..." is telling it what format to save the data in. 
It should produce a TestResults folder, and coverage report for the test run, inside of the unit test project directory.

Next, we need something that can make that coverage report pretty. The recommended tool is the dotnet-reportgenerator-globaltool, we can install it like this:
dotnet tool install -g dotnet-reportgenerator-globaltool

-g will make this available globally on your system, so we should only ever have to do this ONCE.
After it's installed, we can use that tool to create .html files of our report!

reportgenerator
-reports:"Path\To\TestProject\TestResults\{guid}\coverage.cobertura.xml"
-targetdir:"coveragereport"
-reporttypes:Html

The path can be relative (ie ./../app.test/) but needs to point to the coverage report.
To see how you did, open up the index.html file that is generated in your browser, and bask in the beautiful coverage report you've created!
(I've included mine as well, see how we did!)
*/