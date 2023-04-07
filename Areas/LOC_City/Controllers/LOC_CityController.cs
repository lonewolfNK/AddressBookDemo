using AddressBookDemo.Areas.LOC_City.Models;
using AddressBookDemo.Areas.LOC_Country.Models;
using AddressBookDemo.Areas.LOC_State.Models;
using AddressBookDemo.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.SqlClient;

namespace AddressBookDemo.Areas.LOC_City.Controllers
{
    [Area("LOC_City")]
    [Route("LOC_City/[controller]/[action]")]

    public class LOC_CityController : Controller
    {
        private IConfiguration Configuration;
        public LOC_CityController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        #region Select All
        public IActionResult Index(LOC_CityModel modelLOC_City)
        {
            /*DataTable dt = new DataTable();*/
            string str = Configuration.GetConnectionString("myConnectionString");
            /* SqlConnection conn = new SqlConnection(str);
             conn.Open();
             SqlCommand cmd = conn.CreateCommand();
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.CommandText = "PR_LOC_City_SelectAll";
             SqlDataReader sdr = cmd.ExecuteReader();
             dt.Load(sdr);*/
            LOC_DAL dalLOC = new LOC_DAL();
            SqlDatabase sqlDB = new SqlDatabase(str);
            DataTable dt = dalLOC.dbo_PR_LOC_City_SelectAll(str, modelLOC_City);
            return View("LOC_CityList", dt);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int CityID)
        {
            DataTable dt = new DataTable();
            string str = Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_City_DeleteByPK";
            cmd.Parameters.AddWithValue("@CityID", CityID);
            cmd.ExecuteNonQuery();
            conn.Close();
            return RedirectToAction("Index");
        }
        #endregion

        #region Add
        public IActionResult Add(int? CityID)
        {
            #region Select for  Dropdown
            string str1 = Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn1 = new SqlConnection(str1);
            conn1.Open();
            SqlCommand cmd1 = conn1.CreateCommand();
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.CommandText = "PR_LOC_Country_SelectForDropDown";

            DataTable dt1 = new DataTable();
            SqlDataReader sdr1 = cmd1.ExecuteReader();
            dt1.Load(sdr1);

            List<LOC_CountryDropDownModel> List = new List<LOC_CountryDropDownModel>();
            foreach (DataRow dr1 in dt1.Rows)
            {
                LOC_CountryDropDownModel vlst = new LOC_CountryDropDownModel();
                vlst.CountryID = Convert.ToInt32(dr1["CountryID"]);
                vlst.CountryName = dr1["CountryName"].ToString();
                List.Add(vlst);
            }
            ViewBag.CountryList = List;
            conn1.Close();
            #endregion

            #region Select for  Dropdown
            //string str2 = this.Configuration.GetConnectionString("myConnectionString");
            //SqlConnection conn2 = new SqlConnection(str2);
            //conn2.Open();
            //SqlCommand cmd2 = conn2.CreateCommand();
            //cmd2.CommandType = CommandType.StoredProcedure;
            //cmd2.CommandText = "PR_LOC_State_SelectForDropDown";

            //DataTable dt2 = new DataTable();
            //SqlDataReader sdr2 = cmd2.ExecuteReader();
            //dt2.Load(sdr2);

            List<LOC_StateDropDownModel> List2 = new List<LOC_StateDropDownModel>();
            //foreach (DataRow dr2 in dt2.Rows)
            //{
            //    LOC_StateDropDownModel vlst2 = new LOC_StateDropDownModel();
            //    vlst2.StateID = Convert.ToInt32(dr2["StateID"]);
            //    vlst2.StateName = dr2["StateName"].ToString();
            //    List2.Add(vlst2);
            //}
            ViewBag.StateList = List2;
            #endregion

            if (CityID != null)
            {
                string str = Configuration.GetConnectionString("myConnectionString");
                SqlConnection conn = new SqlConnection(str);
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_City_SelectByPK";
                cmd.Parameters.Add("@CityID", SqlDbType.Int).Value = CityID;
                DataTable dt = new DataTable();
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);

                LOC_CityModel modelLOC_City = new LOC_CityModel();


                foreach (DataRow dr in dt.Rows)
                {
                    DropDownByCountry(Convert.ToInt32(dr["CountryID"]));
                    modelLOC_City.CityID = Convert.ToInt32(dr["CityID"]);
                    modelLOC_City.CityName = dr["CityName"].ToString();
                    modelLOC_City.CityCode = dr["CityCode"].ToString();
                    modelLOC_City.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelLOC_City.StateID = Convert.ToInt32(dr["StateID"]);
                    modelLOC_City.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelLOC_City.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                }
                return View("LOC_CityAddEdit", modelLOC_City);


            }
            return View("LOC_CityAddEdit");
        }
        #endregion

        #region Save
        [HttpPost]
        public IActionResult Save(LOC_CityModel modelLOC_City)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            /*SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;*/

            LOC_DAL dalLOC = new LOC_DAL();
            SqlDatabase sqlDB = new SqlDatabase(str);

            if (modelLOC_City.CityID == null)
            {
                DataTable dt = dalLOC.PR_LOC_City_Insert(str, modelLOC_City);
                TempData["SuccessMSG"] = "Record Inserted Successfully ! ";
                /*cmd.CommandText = "PR_LOC_City_Insert";
                cmd.Parameters.Add("@CreationDate", SqlDbType.Date).Value = modelLOC_City.CreationDate;*/
            }
            else
            {
                DataTable dt = dalLOC.PR_LOC_City_UpdateByPK(str, modelLOC_City);
                TempData["SuccessMSG"] = "Record Updated Successfully ! ";
                /*cmd.CommandText = "PR_LOC_City_UpdateByPK";
                cmd.Parameters.Add("@CityID", SqlDbType.Int).Value = modelLOC_City.CityID;*/
            }
/*            cmd.Parameters.Add("@CityName", SqlDbType.NVarChar).Value = modelLOC_City.CityName;
            cmd.Parameters.Add("@CityCode", SqlDbType.NVarChar).Value = modelLOC_City.CityCode;
            cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelLOC_City.CountryID;
            cmd.Parameters.Add("@StateID", SqlDbType.Int).Value = modelLOC_City.StateID;
            cmd.Parameters.Add("@ModificationDate", SqlDbType.Date).Value = modelLOC_City.ModificationDate;*/

           /* if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                if (modelLOC_City.CityID == null)
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

        #region DropDownByCountry
        public IActionResult DropDownByCountry(int CountryID)
        {
            #region State Dropdwon

            string str2 = Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn2 = new SqlConnection(str2);
            conn2.Open();
            SqlCommand cmd2 = conn2.CreateCommand();
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.CommandText = "PR_LOC_State_SelectDropDownByCountryID";
            cmd2.Parameters.AddWithValue("@CountryID", CountryID);

            DataTable dt2 = new DataTable();
            SqlDataReader sdr2 = cmd2.ExecuteReader();
            dt2.Load(sdr2);
            conn2.Close();

            List<LOC_StateDropDownModel> List2 = new List<LOC_StateDropDownModel>();
            foreach (DataRow dr2 in dt2.Rows)
            {
                LOC_StateDropDownModel vlst2 = new LOC_StateDropDownModel();
                vlst2.StateID = Convert.ToInt32(dr2["StateID"]);
                vlst2.StateName = dr2["StateName"].ToString();
                List2.Add(vlst2);
            }
            ViewBag.StateList = List2;
            var vModel = List2;
            return Json(vModel);
            #endregion

        }
        #endregion
    }
}
