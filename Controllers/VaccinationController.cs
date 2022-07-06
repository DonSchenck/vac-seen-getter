using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace vac_seen_getter.Controllers;

[ApiController]
[Route("[controller]")]
public class VaccinationController : ControllerBase
{
    private readonly ILogger<VaccinationController> _logger;

    public VaccinationController(ILogger<VaccinationController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetVaccinationSummary")]
    public Vaccination Get(DateTime queryDate, string countryCode)
    {
        Vaccination v = new Vaccination();

        // Read from database
        string q = String.Format("SELECT vaccination_count FROM vaccination_summaries WHERE location_code = '{0}' AND reporting_date = {1}", countryCode, queryDate);

        using (var connection = new MySqlConnection(Environment.GetEnvironmentVariable("MYSQL_CONNECTION_STRING")))
        {
            connection.Open();
            using var cmd = new MySqlCommand(q, connection);
            using MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
//                Console.WriteLine($"{rdr.GetInt32(0),-4} {rdr.GetString(1),-10} {rdr.GetInt32(2),10}");
                v.CountryCode = countryCode;
                v.Count = rdr.GetInt32(0);
                v.Date = queryDate;
            }
        }
        return v;
    }
}
