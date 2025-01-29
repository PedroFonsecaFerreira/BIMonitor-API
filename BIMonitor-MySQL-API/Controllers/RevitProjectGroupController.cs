using BIMonitor_MySQL_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace BIMonitor_MySQL_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RevitProjectGroupController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public RevitProjectGroupController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public JsonResult Get()
        {
            string query = @"select revitProjectGroupId, name, thumbnailPath, lastEdited from revitprojectgroup";

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

        
        [HttpGet("userId/{id}", Order = 0)]
        public JsonResult GetByUserId(string id)
        {
            if (id == null)
            {
                return Get();
            }
            string query = @"select revitProjectGroupId, userId, name, thumbnailPath, lastEdited from revitprojectgroup where userId=@id and isDeleted=0";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("BIMonitorCon");
            MySqlDataReader myReader;

            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", (MySqlDbType)SqlDbType.Int).Value = int.Parse(id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public int Post(RevitProjectGroup revitProjectGroup)
        {
            string query = @"INSERT into revitprojectgroup(name,thumbnailPath,lastEdited,userId,isDeleted) 
                             VALUES (@name, @thumbnailPath, @lastEdited, @userId, 0);
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
                    myCommand.Parameters.AddWithValue("@userId", revitProjectGroup.UserID);
                    myCommand.Parameters.AddWithValue("@name", revitProjectGroup.Name);
                    myCommand.Parameters.AddWithValue("@thumbnailPath", revitProjectGroup.ThumbnailPath);
                    myCommand.Parameters.AddWithValue("@lastEdited", revitProjectGroup.LastEdited);
                    modified = myCommand.ExecuteScalar();
                    if (modified != null)
                    {
                        int.TryParse(modified.ToString(), out returnValue);
                    }

                    myCon.Close();
                }
            }

            return returnValue;
        }


        [HttpPut("revitProjectGroupId/{revitProjectGroupID}", Order = 0)]
        public JsonResult PutIsDeleted(int revitProjectGroupID)
        {
            string query = @"update revitprojectgroup set isDeleted = @isDeleted
                                             where revitProjectGroupId=@id";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("BIMonitorCon");
            MySqlDataReader myReader;

            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", revitProjectGroupID);
                    myCommand.Parameters.AddWithValue("@isDeleted", true);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }
        //[HttpDelete]
        //public JsonResult Delete(int userId, int modelId)
        //{
        //    string query = @"DELETE FROM userModel WHERE userID=@userId AND modelId=@modelId";

        //    DataTable table = new DataTable();
        //    string sqlDataSource = _configuration.GetConnectionString("BIMonitorCon");
        //    MySqlDataReader myReader;

        //    using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
        //    {
        //        myCon.Open();
        //        using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
        //        {
        //            myCommand.Parameters.AddWithValue("@userId", userId);
        //            myCommand.Parameters.AddWithValue("@modelId", modelId);
        //            myReader = myCommand.ExecuteReader();
        //            table.Load(myReader);

        //            myReader.Close();
        //            myCon.Close();
        //        }
        //    }

        //    return new JsonResult("Deleted Successfully");

        //}
    }
}

