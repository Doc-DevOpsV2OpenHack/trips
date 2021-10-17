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
            var connectionString = GetConnectionString();
            var cn = new System.Data.SqlClient.SqlConnection(connectionString);
            var customerId = GetCustomerId();
            var sql = "SELECT CompanyName from customers where CustomerID = '" + customerId  + "'";
            var cmd = new System.Data.SqlClient.SqlCommand(sql, cn);

            var name = cmd.ExecuteScalar();
            return Ok(name);
        }

        private string GetConnectionString(){
            return "Server=(local);UserId=sa;Password=PA$$W0rd;Database=notfakedb";
        }
        
        private string GetCustomerId(){
            return Request.Form["customerId"];
        }
    }

}
