using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MyFirstWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValuesController : ControllerBase
    {
        private static List<string> values = new List<string> { "value1", "value2" };

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(values); 
        }

        [HttpPost]
        public IActionResult Post([FromBody] string newValue)
        {
            values.Add(newValue);
            return Ok("Added: " + newValue);
        }
    }
}
