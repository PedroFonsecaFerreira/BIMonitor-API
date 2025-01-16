using BIMonitor_MySQL_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace BIMonitor_MySQL_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ThumbnailController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ThumbnailController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public JsonResult Get()
        {
            string query = @"select userId,username,hashedPassword,saltPassword,hashedEmail from user";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("BIMonitorCon");
            MySqlDataReader myReader;

            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);

        }

        [HttpGet]
        public JsonResult GetByUsername(string username)
        {
            if (username == null)
            {
                return Get();
            }
            string query = @"select userId,username,hashedPassword,saltPassword,hashedEmail from user where username=@username";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("BIMonitorCon");
            MySqlDataReader myReader;

            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@username", username);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);

        }


        [HttpPost]
        public JsonResult Post(User user)
        {
            string query = @"insert into user(username,hashedPassword,saltPassword,hashedEmail) values (@username, @hashedPassword, @saltPassword, @hashedEmail);";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("BIMonitorCon");
            MySqlDataReader myReader;

            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@username", user.Username);
                    myCommand.Parameters.AddWithValue("@hashedPassword", user.HashedPassword);
                    myCommand.Parameters.AddWithValue("@saltPassword", user.SaltPassword);
                    myCommand.Parameters.AddWithValue("@hashedEmail", user.HashedEmail);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return Get();

        }
    }
}
