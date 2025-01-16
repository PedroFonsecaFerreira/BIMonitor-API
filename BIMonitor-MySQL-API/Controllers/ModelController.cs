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
    public class ModelController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ModelController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        
        public JsonResult Get()
        {
            string query = @"select name, revitName, objPath, thumbnailPath, jsonPath, csvPath, location, owner, units, modelQuality from model";

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
        public JsonResult GetById(string id)
        {
            if (id == null)
            {
                Console.WriteLine("id=null");
                return Get();
            }
            string query = @"select modelId, name, revitName, objPath, thumbnailPath, jsonPath, csvPath, location, owner, modelUnits, modelQuality, lastEdited, timeCreated, lastVerified from model where modelId=@id";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("BIMonitorCon");
            MySqlDataReader myReader;

            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.Add("@id", (MySqlDbType)SqlDbType.Int).Value = Int32.Parse(id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);

        }

        [HttpPost]
        public int Post(Model model)
        {
            string query = @"INSERT INTO model(name, objPath, thumbnailPath, jsonPath, csvPath, owner, modelQuality, modelUnits, location, revitName, timeCreated, lastEdited, lastVerified) VALUES 
                                              (@name, @objPath, @thumbnailPath, @jsonPath, @csvPath, @owner, @modelQuality, 
                                              @units, @location, @revitName, @timeCreated, @lastEdited, @lastVerified;
                            SELECT LAST_INSERT_ID();";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("BIMonitorCon");
            object modified; 
            int returnValue = -1;

            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@name", model.Name);
                    myCommand.Parameters.AddWithValue("@objPath", model.OBJPath);
                    myCommand.Parameters.AddWithValue("@thumbnailPath", model.ThumbnailPath);
                    myCommand.Parameters.AddWithValue("@jsonPath", model.JSONPath);
                    myCommand.Parameters.AddWithValue("@csvPath", model.CSVPath);
                    myCommand.Parameters.AddWithValue("@location", model.Location);
                    myCommand.Parameters.AddWithValue("@owner", model.Owner);
                    myCommand.Parameters.AddWithValue("@modelUnits", model.ModelUnits);
                    myCommand.Parameters.AddWithValue("@modelQuality", model.ModelQuality);
                    myCommand.Parameters.AddWithValue("@revitName", model.RevitName);
                    myCommand.Parameters.AddWithValue("@timeCreated", model.TimeCreated);
                    myCommand.Parameters.AddWithValue("@lastEdited", model.LastEdited);
                    myCommand.Parameters.AddWithValue("@lastVerified", model.LastVerified);
                    //myReader = myCommand.ExecuteReader();
                    modified = myCommand.ExecuteScalar();
                    //table.Load(myReader);
                    if (modified != null)
                    {
                        int.TryParse(modified.ToString(), out returnValue);
                    }
                    //myReader.Close();
                    myCon.Close();
                }
            }

            return returnValue;

        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"DELETE FROM model WHERE modelId=@id";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("BIMonitorCon");
            MySqlDataReader myReader;

            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", id);
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
