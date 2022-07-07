using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Globalization;

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

    [HttpGet("GetVaccinationSummaries")]
    public List<Vaccination> Get(string queryDate, string countryCode, int DaysToGoBack)
    {
        List<Vaccination> result = new List<Vaccination>();

        // Calculate end date
        DateTime endDate = DateTime.ParseExact(queryDate, "yyyyMMdd", CultureInfo.InvariantCulture);
        DateTime startDate = DateTime.Today.AddDays(-1 * DaysToGoBack);

        DateTime currentDate = startDate;
        while (currentDate <= endDate)
        {
            string selectDate = currentDate.ToString("yyyyMMdd");
            Vaccination v = new Vaccination();
            v.Count = 0;
            v.CountryCode = countryCode;
            v.Date = selectDate;

            // Read from database
            string q = String.Format("SELECT vaccination_count FROM vaccination_summaries WHERE location_code = '{0}' AND reporting_date = '{1}'", countryCode, selectDate);

            using (var connection = new MySqlConnection(Environment.GetEnvironmentVariable("MYSQL_CONNECTION_STRING")))
            {
                connection.Open();
                using var cmd = new MySqlCommand(q, connection);
                using MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    v.Count = rdr.GetInt32(0);
                }
            }
            result.Add(v);
            currentDate = currentDate.AddDays(1);
        }

        return result;
    }

    [HttpGet(Name = "GetVaccinationSummary")]
    public Vaccination Get(string queryDate, string countryCode)
    {
        Vaccination v = new Vaccination();
        v.Count = 0;
        v.CountryCode = countryCode;
        v.Date = queryDate;

        // Read from database
        string q = String.Format("SELECT vaccination_count FROM vaccination_summaries WHERE location_code = '{0}' AND reporting_date = '{1}'", countryCode, queryDate);

        using (var connection = new MySqlConnection(Environment.GetEnvironmentVariable("MYSQL_CONNECTION_STRING")))
        {
            connection.Open();
            using var cmd = new MySqlCommand(q, connection);
            using MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                v.Count = rdr.GetInt32(0);
            }
        }
        return v;
    }
}
