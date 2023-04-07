using AddressBookDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using AddressBookDemo.Areas.MST_ContactCategory.Models;

namespace AddressBookDemo.Areas.MST_ContactCategory.Controllers
{
    [Area("MST_ContactCategory")]
    [Route("MST_ContactCategory/[controller]/[action]")]
    public class MST_ContactCategoryController : Controller
    {
        private IConfiguration Configuration;
        public MST_ContactCategoryController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        public IActionResult Index()
        {
            DataTable dt = new DataTable();
            string str = Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_MST_ContactCategory_SelectAll";
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            return View("MST_ContactCategoryList", dt);
        }
        #region Delete
        public IActionResult Delete(int ContactCategoryID)
        {
            DataTable dt = new DataTable();
            string str = Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_MST_ContactCatagory_DeleteByPK";
            cmd.Parameters.AddWithValue("@ContactCategoryID", ContactCategoryID);
            cmd.ExecuteNonQuery();
            conn.Close();
            return RedirectToAction("Index");
        }
        #endregion

        #region Add
        public IActionResult Add(int? ContactCategoryID)
        {
            if (ContactCategoryID != null)
            {
                string str = Configuration.GetConnectionString("myConnectionString");
                SqlConnection conn = new SqlConnection(str);
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_MST_ContactCategory_SelectByPK";
                cmd.Parameters.Add("@ContactCategoryID", SqlDbType.Int).Value = ContactCategoryID;
                DataTable dt = new DataTable();
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);

                MST_ContactCategoryModel modelMST_ConatctCategory = new MST_ContactCategoryModel();


                foreach (DataRow dr in dt.Rows)
                {
                    modelMST_ConatctCategory.ContactCategoryID = Convert.ToInt32(dr["ContactCategoryID"]);
                    modelMST_ConatctCategory.ContactCategoryName = dr["ContactCategoryName"].ToString();

                    modelMST_ConatctCategory.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelMST_ConatctCategory.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                }
                return View("MST_ContactCategoryAddEdit", modelMST_ConatctCategory);


            }
            return View("MST_ContactCategoryAddEdit");
        }
        #endregion

        #region Save
        [HttpPost]
        public IActionResult Save(MST_ContactCategoryModel modelMST_ConatctCategory)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;


            if (modelMST_ConatctCategory.ContactCategoryID == null)
            {
                cmd.CommandText = "PR_MST_ContactCategory_Insert";
                cmd.Parameters.Add("@CreationDate", SqlDbType.Date).Value = modelMST_ConatctCategory.CreationDate;
            }
            else
            {
                cmd.CommandText = "PR_MST_ContactCategory_UpdateByPK";
                cmd.Parameters.Add("@ContactCategoryID", SqlDbType.Int).Value = modelMST_ConatctCategory.ContactCategoryID;
            }
            cmd.Parameters.Add("@ContactCategoryName", SqlDbType.NVarChar).Value = modelMST_ConatctCategory.ContactCategoryName;

            cmd.Parameters.Add("@ModificationDate", SqlDbType.Date).Value = modelMST_ConatctCategory.ModificationDate;

            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                if (modelMST_ConatctCategory.ContactCategoryID == null)
                {
                    TempData["SuccessMSG"] = "Record Inserted Successfully ! ";
                }
                else
                {
                    TempData["SuccessMSG"] = "Record Updated Successfully ! ";

                }
            }

            conn.Close();
            return View("MST_ContactCategoryAddEdit");
        }

        #endregion
    }
}
