using BIMonitor_MySQL_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace BIMonitor_MySQL_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserModelController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UserModelController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public JsonResult Get()
        {
            string query = @"select userId,modelId from userModel";

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
        public JsonResult GetByUserId(string id)
        {
            if (id == null)
            {
                return Get();
            }
            string query = @"select userId,modelId from userModel where userId=@id";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("BIMonitorCon");
            MySqlDataReader myReader;

            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", (MySqlDbType)SqlDbType.Int).Value = Int32.Parse(id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);

        }


        [HttpPost]
        public JsonResult Post(UserModel userModel)
        {
            string query = @"insert into userModel(userId,modelId) values (@userId, @modelId);";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("BIMonitorCon");
            MySqlDataReader myReader;

            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@userId", userModel.UserID);
                    myCommand.Parameters.AddWithValue("@modelId", userModel.ModelID);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return Get();

        }

        [HttpDelete]
        public JsonResult Delete(int userId, int modelId)
        {
            string query = @"DELETE FROM userModel WHERE userID=@userId AND modelId=@modelId";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("BIMonitorCon");
            MySqlDataReader myReader;

            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@userId", userId);
                    myCommand.Parameters.AddWithValue("@modelId", modelId);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");

        }
    }
}
