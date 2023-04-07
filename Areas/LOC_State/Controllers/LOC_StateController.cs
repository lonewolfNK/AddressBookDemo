using Microsoft.AspNetCore.Mvc; 
using System.Data;
using System.Data.SqlClient;
using AddressBookDemo.Areas.LOC_Country.Models;
using AddressBookDemo.Areas.LOC_State.Models;
using AddressBookDemo.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace AddressBookDemo.Areas.LOC_State.Controllers
{
    [Area("LOC_State")]
    [Route("LOC_State/[controller]/[action]")]

    public class LOC_StateController : Controller
    {
        private IConfiguration Configuration;
        public LOC_StateController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        #region SelectAll
        public IActionResult Index(LOC_StateModel modelLOC_State)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            LOC_DAL dalLOC = new LOC_DAL();
            SqlDatabase sqlDB = new SqlDatabase(str);
            DataTable dt = dalLOC.dbo_PR_LOC_State_SelectAll(str, modelLOC_State);
            return View("LOC_StateList", dt);
        }
        #endregion

        #region Add
        public IActionResult Add(int? StateID)
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
            #endregion

            #region Select by pk

            if (StateID != null)
            {
                string str = Configuration.GetConnectionString("myConnectionString");
                SqlConnection conn = new SqlConnection(str);
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_State_SelectByPK";
                cmd.Parameters.Add("@StateID", SqlDbType.Int).Value = StateID;
                DataTable dt = new DataTable();
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);

                LOC_StateModel modelLOC_State = new LOC_StateModel();


                foreach (DataRow dr in dt.Rows)
                {
                    modelLOC_State.StateID = Convert.ToInt32(dr["StateID"]);
                    modelLOC_State.StateName = dr["StateName"].ToString();
                    modelLOC_State.StateCode = dr["StateCode"].ToString();
                    modelLOC_State.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelLOC_State.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelLOC_State.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                }
                return View("LOC_StateAddEdit", modelLOC_State);


            }
            return View("LOC_StateAddEdit");
            #endregion
        }
        #endregion

        #region Delete
        public IActionResult Delete(int StateID)
        {
            DataTable dt = new DataTable();
            string str = Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_State_DeleteByPK";
            cmd.Parameters.AddWithValue("@StateID", StateID);
            cmd.ExecuteNonQuery();
            conn.Close();
            return RedirectToAction("Index");
        }
        #endregion

        #region Save
        [HttpPost]
        public IActionResult Save(LOC_StateModel modelLOC_State)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            /*SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;*/
            LOC_DAL dalLOC= new LOC_DAL();
            SqlDatabase sqlDB = new SqlDatabase(str); 
            if (modelLOC_State.StateID == null)
            {
                DataTable dt = dalLOC.PR_LOC_State_Insert(str, modelLOC_State);
                TempData["SuccessMSG"] = "Record Inserted Successfully ! ";

            }
            else
            {
                DataTable dt = dalLOC.PR_LOC_State_UpdateByPK(str, modelLOC_State);
                TempData["SuccessMSG"] = "Record Updated Successfully ! ";
            } 
            return RedirectToAction("Add");
        }

        #endregion
    }
}
