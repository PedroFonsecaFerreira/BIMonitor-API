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
    public class RevitProjectController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public RevitProjectController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public JsonResult Get()
        {
            string query = @"SELECT name, revitName, objPath, thumbnailPath, jsonPath, csvPath, location, owner, units, modelQuality 
                             FROM revitproject";

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

        [HttpGet("revitProjectGroupId/{id}", Order = 0)]
        public JsonResult GetById(string id)
        {
            if (id == null)
            {
                Console.WriteLine("id=null");
                return Get();
            }
            string query = @"SELECT revitProjectId, revitProjectGroupId, name, folderName, location, selectedQuality, lastEdited, timeCreated, 
                                    lastVerified, isAllProjectViews, isAllParameters, taskElementsParametersCsvLocalPath, taskTimelinersCsvLocalPath
                             FROM revitproject WHERE revitProjectGroupId=@id and isDeleted=0";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("BIMonitorCon");
            MySqlDataReader myReader;

            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.Add("@id", (MySqlDbType)SqlDbType.Int).Value = int.Parse(id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);

        }

        [HttpPost]
        public int Post(RevitProject revitProject)
        {
            string query = @"INSERT INTO revitproject(name, folderName, location, selectedQuality, 
                                                      lastEdited, timeCreated, lastVerified, isAllProjectViews, isAllParameters, revitProjectGroupId, taskElementsParametersCsvLocalPath, taskTimelinersCsvLocalPath) 
                            VALUES 
                                                     (@name, @folderName, @location, @selectedQuality, 
                                                      @lastEdited, @timeCreated, @lastVerified, @isAllProjectViews, @isAllParameters, @revitProjectGroupId, @taskElementsParametersCsvLocalPath , @taskTimelinersCsvLocalPath);
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
                    myCommand.Parameters.AddWithValue("@name", revitProject.Name);
                    myCommand.Parameters.AddWithValue("@folderName", revitProject.FolderName);
                    myCommand.Parameters.AddWithValue("@location", revitProject.Location);
                    myCommand.Parameters.AddWithValue("@selectedQuality", revitProject.SelectedQuality);
                    myCommand.Parameters.AddWithValue("@timeCreated", revitProject.TimeCreated);
                    myCommand.Parameters.AddWithValue("@lastEdited", revitProject.LastEdited);
                    myCommand.Parameters.AddWithValue("@lastVerified", revitProject.LastVerified);
                    myCommand.Parameters.AddWithValue("@isAllProjectViews", revitProject.IsAllProjectViews);
                    myCommand.Parameters.AddWithValue("@isAllParameters", revitProject.IsAllParameters);
                    myCommand.Parameters.AddWithValue("@revitProjectGroupId", revitProject.RevitProjectGroupID);
                    myCommand.Parameters.AddWithValue("@taskElementsParametersCsvLocalPath", revitProject.TaskElementsParametersCsvLocalPath);
                    myCommand.Parameters.AddWithValue("@taskTimelinersCsvLocalPath", revitProject.TaskTimelinersCsvLocalPath);
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

        [HttpPut]
        public JsonResult Put(RevitProject revitProject)
        {
            string query = @"update revitproject set name = @name,
                                                     foldername = @foldername,
                                                     location = @location,
                                                     selectedQuality = @selectedQuality,
                                                     timeCreated = @timeCreated,
                                                     lastEdited = @lastEdited,
                                                     lastVerified = @lastVerified,
                                                     isAllProjectViews = @isAllProjectViews,
                                                     isAllParameters = @isAllParameters,
                                                     revitProjectGroupId = @revitProjectGroupId,
                                                     taskElementsParametersCsvLocalPath = @taskElementsParametersCsvLocalPath,
                                                     taskTimelinersCsvLocalPath = @taskTimelinersCsvLocalPath,
                                                     isDeleted = 0
                            where revitProjectId=@id";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("BIMonitorCon");
            MySqlDataReader myReader;

            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", revitProject.RevitProjectID);
                    myCommand.Parameters.AddWithValue("@name", revitProject.Name);
                    myCommand.Parameters.AddWithValue("@folderName", revitProject.FolderName);
                    myCommand.Parameters.AddWithValue("@location", revitProject.Location);
                    myCommand.Parameters.AddWithValue("@selectedQuality", revitProject.SelectedQuality);
                    myCommand.Parameters.AddWithValue("@timeCreated", revitProject.TimeCreated);
                    myCommand.Parameters.AddWithValue("@lastEdited", revitProject.LastEdited);
                    myCommand.Parameters.AddWithValue("@lastVerified", revitProject.LastVerified);
                    myCommand.Parameters.AddWithValue("@isAllProjectViews", revitProject.IsAllProjectViews);
                    myCommand.Parameters.AddWithValue("@isAllParameters", revitProject.IsAllParameters);
                    myCommand.Parameters.AddWithValue("@revitProjectGroupId", revitProject.RevitProjectGroupID);
                    myCommand.Parameters.AddWithValue("@taskElementsParametersCsvLocalPath", revitProject.TaskElementsParametersCsvLocalPath);
                    myCommand.Parameters.AddWithValue("@taskTimelinersCsvLocalPath", revitProject.TaskTimelinersCsvLocalPath);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return GetById(revitProject.RevitProjectID.ToString());

        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"DELETE FROM revitproject WHERE revitProjectId=@id";

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

        [HttpPut("revitProjectId/{revitProjectID}", Order = 1)]
        public JsonResult PutIsDeleted(int revitProjectID)
        {
            string query = @"update revitproject set isDeleted = @isDeleted
                                             where revitProjectId=@id";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("BIMonitorCon");
            MySqlDataReader myReader;

            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", revitProjectID);
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
