using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestProject.Elasticsearch.Core.Elasticsearch;

namespace TestProject.Elasticsearch.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IElasticSearchService _elasticSearchService;

        public WeatherForecastController(IElasticSearchService elasticSearchService)
        {
            _elasticSearchService = elasticSearchService;
        }

        [HttpGet]
        [ApiVersion("2.0")]
        public IActionResult Get()
        {
            return Ok("v2");
        }

        [HttpGet]
        public IActionResult GetV2()
        {
            return Ok();
        }
    }
}
