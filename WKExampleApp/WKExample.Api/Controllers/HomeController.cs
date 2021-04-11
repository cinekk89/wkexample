using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace WKExample.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return Ok(new List<string>());
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return Ok(string.Empty);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok();
        }

        [HttpPost]
        public ActionResult Post([FromBody]object person)
        {
            return Ok();
        }

        [HttpPut]
        public ActionResult Put([FromBody]object person)
        {
            return Ok();
        }
    }
}
