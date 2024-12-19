using Microsoft.AspNetCore.Mvc;

namespace PersistentAssessmentApi.Controllers;


/// <summary>
/// A controller for alphabets related operations
/// </summary>
[ApiController]
[Route("api/v1/alphabet")]
public class AlphabetController : ControllerBase
{
    private readonly ILogger<AlphabetController> _logger;

    public AlphabetController(ILogger<AlphabetController> logger)
    {
        _logger = logger;
    }


    /// <summary>
    /// Action method to check if input string holds all the alphabet characters
    /// </summary>
    /// <param name="input"></param>
    /// <returns>true/false</returns>
    [HttpPost]
    [Route("CheckAlphabet")]
    public ActionResult<bool> CheckAlpabet([FromBody] string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return BadRequest("Input string cannot be null or empty");
        }

        var alphabet = "abcdefghijklmnopqrstuvwxyz";
        var lowerInput = input.ToLower();

        return alphabet.All(letter => lowerInput.Contains(letter));
    }
}
