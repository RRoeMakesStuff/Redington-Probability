using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using ProbabilityTool.DataStore.Interfaces;
using ProbabilityTool.Models.DataModels;

namespace ProbabilityTool.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DataController: ControllerBase
{
    private readonly IDataStoreReader<Calculation> _dataStoreReader;
    
    public DataController(IDataStoreReader<Calculation> dataStoreReader)
    {
        _dataStoreReader = dataStoreReader;
    }
    
    [HttpGet("{id}")]
    public IActionResult GetProbabilityResult(string id)
    {
        try
        {
            return Ok(_dataStoreReader.GetObjectById(id));
        }
        catch (IOException e)
        {
            return NotFound(e.Message);
        }
        catch (JsonException e)
        {
            return Problem(e.Message);
        }
    }
    
    [HttpGet("GetAll")]
    public IActionResult GetAllProbabilityResults()
    {
        try
        {
            return Ok(_dataStoreReader.GetAll());
        }
        catch (IOException e)
        {
            return Problem(e.Message);
        }
    }
}