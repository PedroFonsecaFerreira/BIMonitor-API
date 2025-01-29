using BIMonitor_MySQL_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace BIMonitor_MySQL_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        
        public JsonResult Get()
        {
            string query = @"select userId,username,hashedPassword,saltPassword,hashedEmail,organization,activated from user";

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

        [HttpGet("username/{username}", Order = 0)]
        public JsonResult GetByUsername(string username)
        {
            if(username == null) { 
                return Get();
            }
            string query = @"select userId,username,hashedPassword,saltPassword,hashedEmail,organization,activated from user where username=@username and isDeleted = 0";

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

        [HttpGet("email/{email}", Order = 1)]
        public JsonResult GetByEmail(string email)
        {
            if (email == null)
            {
                return Get();
            }
            string query = @"select userId,username,hashedPassword,saltPassword,hashedEmail,organization,activated from user where hashedEmail=@email and isDeleted = 0";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("BIMonitorCon");
            MySqlDataReader myReader;

            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@email", email);
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
            string query = @"insert into user(username,hashedPassword,saltPassword,hashedEmail,organization,activated, isDeleted) values (@username, @hashedPassword, @saltPassword, @hashedEmail, @organization, @activated, 0);";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("BIMonitorCon");
            MySqlDataReader myReader;

            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@username",user.Username);
                    myCommand.Parameters.AddWithValue("@hashedPassword", user.HashedPassword);
                    myCommand.Parameters.AddWithValue("@saltPassword", user.SaltPassword);
                    myCommand.Parameters.AddWithValue("@hashedEmail", user.HashedEmail);
                    myCommand.Parameters.AddWithValue("@activated", user.Activated);
                    myCommand.Parameters.AddWithValue("@organization", user.Organization);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return Get();

        }

        [HttpPut]
        public JsonResult Put(User user)
        {
            string query = @"update user set hashedPassword = @hashedPassword,
                                             saltPassword = @saltPassword,
                                             activated = @activated
                                             where userID=@id";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("BIMonitorCon");
            MySqlDataReader myReader;

            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", user.UserID);
                    myCommand.Parameters.AddWithValue("@hashedPassword", user.HashedPassword);
                    myCommand.Parameters.AddWithValue("@saltPassword", user.SaltPassword);
                    myCommand.Parameters.AddWithValue("@activated", true);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return GetByUsername(user.Username);

        }

        [HttpPut("user/{userID}", Order = 1)]
        public JsonResult PutIsDeleted(int userID)
        {
            string query = @"update user set isDeleted = @isDeleted
                                             where userID=@id";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("BIMonitorCon");
            MySqlDataReader myReader;

            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", userID);
                    myCommand.Parameters.AddWithValue("@isDeleted", true);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);

        }
    }
}
