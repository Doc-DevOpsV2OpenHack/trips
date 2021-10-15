using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using poi.Models;
using poi.Data;
using poi.Utility;
using Newtonsoft.Json;


namespace poi.Controllers
{
    [Produces("application/json")]
    [Route("api/healthcheck/poi")]
    public class HealthCheckController : ControllerBase
    {

        private readonly ILogger _logger;

        public HealthCheckController(ILogger<HealthCheckController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Produces("application/json", Type = typeof(Healthcheck))]
        public IActionResult Get()
        {
            _logger.LogInformation(LoggingEvents.Healthcheck, "Healthcheck Requested");
            return Ok(new Healthcheck());
        }

        [HttpPost]
        [Produces("application/json", Type = typeof(Healthcheck))]
        public IActionResult Post()
        {
            var cn = new System.Data.SqlClient.SqlConnection("Server=(local);UserId=sa;Password=PA$$W0rd");
            var sql = "SELECT CompanyName from customers where CustomerID = '" + Request.Form["customerId"] + "'";
            var cmd = new System.Data.SqlClient.SqlCommand(sql, cn);

            var name = cmd.ExecuteScalar();
            return Ok(name);
        }

    }

}