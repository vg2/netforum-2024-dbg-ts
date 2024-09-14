using Debugging.Service;
using Microsoft.AspNetCore.Mvc;

namespace MyDemoApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DemoController : ControllerBase
{
    private readonly ICalculationService _calculationService;

    public DemoController(ICalculationService calculationService)
    {
        _calculationService = calculationService;
    }

    // GET api/demo/{id}
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        // Some logic to demonstrate breakpoints

        // 1. Check if the ID is valid
        if (id <= 0)
        {
            return BadRequest("Invalid ID");
        }

        // 2. Mocked data source
        var items = new List<string> { "Item1", "Item2", "Item3", "Item4" };

        // 3. Calculate index based on input ID
        int index = id % items.Count;  // breakpoint here to inspect 'index'

        // 4. Retrieve the item from the list
        var selectedItem = items[index];  // breakpoint here to inspect 'selectedItem'

        // 5. Construct a response
        var response = new
        {
            Item = selectedItem,
            Message = $"You selected item with ID: {id}",  // breakpoint here to inspect 'response'
        };

        // 6. Return the result
        return Ok(response);  // final breakpoint to observe the return value
    }

    // New action that calls into a service
    [HttpGet("calculate/{num1}/{num2}")]
    public IActionResult Calculate(int num1, int num2)
    {
        // Call into the service to perform a calculation
        var result = _calculationService.AddNumbers(num1, num2);  // Step into here

        // Return the result
        var response = new
        {
            Result = result,
            Message = $"The sum of {num1} and {num2} is {result}",
        };

        return Ok(response);  // Step out here
    }

    [HttpGet("divide/{numerator}/{denominator}")]
    public IActionResult Divide(int numerator, int denominator)
    {
        try
        {
            // Call the service to perform division
            var result = _calculationService.DivideByNumbersNoNo(numerator, denominator);  // Step into here

            var response = new
            {
                Result = result,
                Message = $"The result of dividing {numerator} by {denominator} is {result}",
            };

            return Ok(response);
        }
        catch (DivideByZeroException ex)
        {
            // Handle the divide by zero exception
            return BadRequest(new { Error = ex.Message });  // Step out here
        }
        catch (Exception e)
        {
            return BadRequest(new { Error = e.Message });
        }
    }

}
