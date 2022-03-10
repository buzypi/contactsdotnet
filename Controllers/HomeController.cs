using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Contacts.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Contacts.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration Configuration)
        {
            _logger = logger;
            _configuration = Configuration;
        }

        public IActionResult Contacts()
        {
            MySqlConnection conn;
            List<Contact> contactsList = new List<Contact>();
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = _configuration["ConnectionStrings:Default"];
                conn.Open();
                string sql = "SELECT * FROM people";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    contactsList.Add(new Contact
                    {
                        Id = rdr.GetInt16(0),
                        Name = rdr.GetString(1),
                        Email = rdr.GetString(2)
                    }
                    );
                }
                rdr.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return Json(contactsList, new JsonSerializerOptions
            {
                WriteIndented = true,
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
