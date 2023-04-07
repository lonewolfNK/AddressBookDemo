using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using AddressBookDemo.Areas.LOC_Country.Models;
using AddressBookDemo.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace AddressBookDemo.Areas.LOC_Country.Controllers
{
    [Area("LOC_Country")]
    [Route("LOC_Country/[controller]/[action]")]
    public class LOC_CountryController : Controller
    {
        

        #region Configuration Data
        private IConfiguration Configuration;
        public LOC_CountryController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        #endregion

        #region SelectAll
        public IActionResult Index(LOC_CountryModel modelLOC_Country)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            /*            SqlConnection conn = new SqlConnection(str);
                        conn.Open();
                        SqlCommand cmd = conn.CreateCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        DataTable dt = new DataTable();*/
            LOC_DAL dalLOC = new LOC_DAL();
            SqlDatabase sqlDB = new SqlDatabase(str);
            DataTable dt = dalLOC.dbo_PR_LOC_Country_SelectAll(str, modelLOC_Country);
            /*SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);*/
            return View("LOC_CountryList", dt);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int CountryID)
        {
            DataTable dt = new DataTable();
            string str = Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_Country_DeleteByPK";
            cmd.Parameters.AddWithValue("@CountryID", CountryID);
            cmd.ExecuteNonQuery();
            conn.Close();
            return RedirectToAction("Index");
        }
        #endregion

        #region Add
        public IActionResult Add(int? CountryID)
        {
            if (CountryID != null)
            {
                string str = Configuration.GetConnectionString("myConnectionString");
                SqlConnection conn = new SqlConnection(str);
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_Country_SelectByPK";
                cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryID;
                DataTable dt = new DataTable();
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);

                LOC_CountryModel modelLOC_Country = new LOC_CountryModel();

                foreach (DataRow dr in dt.Rows)
                {
                    modelLOC_Country.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelLOC_Country.CountryName = dr["CountryName"].ToString();
                    modelLOC_Country.CountryCode = dr["CountryCode"].ToString();
                    modelLOC_Country.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelLOC_Country.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                }
                return View("LOC_CountryAddEdit", modelLOC_Country);
            }
            return View("LOC_CountryAddEdit");
        }
        #endregion

        #region Save
        [HttpPost]
        public IActionResult Save(LOC_CountryModel modelLOC_Country)
        {
            if (modelLOC_Country.File != null)
            {
                string FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileNameWithPath = Path.Combine(path, modelLOC_Country.File.FileName);
                modelLOC_Country.PhotoPath = FilePath.Replace("wwwroot\\", "/") + "/" + modelLOC_Country.File.FileName;
                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    modelLOC_Country.File.CopyTo(stream);
                }
            }

            string str = Configuration.GetConnectionString("myConnectionString");
            /* SqlConnection conn = new SqlConnection(str);
             conn.Open();
             SqlCommand cmd = conn.CreateCommand();
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.CommandText = "PR_LOC_Country_Insert";*/

            LOC_DAL dalLOC = new LOC_DAL();
            SqlDatabase sqlDB = new SqlDatabase(str);
            if (modelLOC_Country.CountryID == null)
            {
                DataTable dt = dalLOC.PR_LOC_Country_Insert(str, modelLOC_Country);
                TempData["SuccessMSG"] = "Record Inserted Successfully ! ";
                /*                cmd.CommandText = "PR_LOC_Country_Insert";
                                cmd.Parameters.Add("@CountryName", SqlDbType.NVarChar).Value = modelLOC_Country.CountryName;
                                cmd.Parameters.Add("@CountryCode", SqlDbType.NVarChar).Value = modelLOC_Country.CountryCode;
                                cmd.Parameters.Add("@PhotoPath", SqlDbType.NVarChar).Value = modelLOC_Country.PhotoPath;
                                cmd.Parameters.Add("@CreationDate", SqlDbType.Date).Value = modelLOC_Country.CreationDate;
                                cmd.Parameters.Add("@ModificationDate", SqlDbType.Date).Value = modelLOC_Country.ModificationDate;*/
            }
            else
            {
                DataTable dt = dalLOC.PR_LOC_Country_UpdateByPK(str, modelLOC_Country);
                TempData["SuccessMSG"] = "Record Updated Successfully ! ";
                /*                cmd.CommandText = "PR_LOC_Country_UpdateByPK";
                                cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelLOC_Country.CountryID;
                                cmd.Parameters.Add("@CountryName", SqlDbType.VarChar).Value = modelLOC_Country.CountryName;
                                cmd.Parameters.Add("@CountryCode", SqlDbType.VarChar).Value = modelLOC_Country.CountryCode;
                                cmd.Parameters.Add("@PhotoPath", SqlDbType.NVarChar).Value = modelLOC_Country.PhotoPath;
                                cmd.Parameters.Add("@ModificationDate", SqlDbType.Date).Value = modelLOC_Country.ModificationDate;*/
            }
            /*if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                if (modelLOC_Country.CountryID == null)
                {
                    TempData["SuccessMSG"] = "Record Inserted Successfully ! ";
                }
                else
                {
                    TempData["SuccessMSG"] = "Record Updated Successfully ! ";

                }
            }
            conn.Close();*/
            return RedirectToAction("Index");
        }

        #endregion
    }
}
