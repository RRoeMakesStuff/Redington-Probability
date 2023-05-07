using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using ProbabilityTool.Calculations.Services;
using ProbabilityTool.DataStore.Interfaces;
using ProbabilityTool.Models.DataModels;

namespace ProbabilityTool.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CalculationController: ControllerBase
{
    private readonly ICalculationService _calculationService;
    private readonly IDataStoreWriter _dataStoreWriter;
    
    public CalculationController(ICalculationService calculationService, IDataStoreWriter dataStoreWriter)
    {
        _calculationService = calculationService;
        _dataStoreWriter = dataStoreWriter;
    }
    
    [HttpPost]
    [Route("and")]
    public IActionResult CalculateCombinedProbability([FromBody] Calculation calc)
    {
        try
        {
            var result = _calculationService.CalculateAndProbability(calc);
            _dataStoreWriter.SaveDataToStorage(calc);
            return Ok(result);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
            return BadRequest(e.Message);
        }
        catch (JsonException e)
        {
            Console.WriteLine(e.Message);
            return Problem(e.Message);
        }
    }
    
    [HttpPost]
    [Route("or")]
    public IActionResult CalculateOrProbability([FromBody] Calculation calc)
    {
        try
        {
            var result = _calculationService.CalculateOrProbability(calc);
            _dataStoreWriter.SaveDataToStorage(calc);
            return Ok(result);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
            return BadRequest(e.Message);
        }
        catch (JsonException e)
        {
            Console.WriteLine(e.Message);
            return Problem(e.Message);
        }
    }
}