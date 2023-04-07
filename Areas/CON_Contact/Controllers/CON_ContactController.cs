using AddressBookDemo.Areas.CON_Contact.Models;
using AddressBookDemo.Areas.LOC_City.Models;
using AddressBookDemo.Areas.LOC_Country.Models;
using AddressBookDemo.Areas.LOC_State.Models;
using AddressBookDemo.Areas.MST_ContactCategory.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace AddressBookDemo.Areas.CON_Contact.Controllers
{
    [Area("CON_Contact")]
    [Route("CON_Contact/[controller]/[action]")]
    public class CON_ContactController : Controller
    {
        private IConfiguration Configuration;
        public CON_ContactController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        #region SelectAll
        public IActionResult Index()
        {
            DataTable dt = new DataTable();
            string str = Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_CON_Contact_SelectAll";
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            return View("CON_ContactList", dt);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int ContactID)
        {
            DataTable dt = new DataTable();
            string str = Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_CON_Contact_DeleteByPK";
            cmd.Parameters.AddWithValue("@ContactID", ContactID);
            cmd.ExecuteNonQuery();
            conn.Close();
            return RedirectToAction("Index");
        }
        #endregion

        #region Add
        public IActionResult Add(int? ContactID)
        {
            #region Country Drop Down

            SqlConnection conn1 = new SqlConnection(Configuration.GetConnectionString("myConnectionString"));

            conn1.Open();

            SqlCommand Cmd1 = conn1.CreateCommand();
            Cmd1.CommandType = CommandType.StoredProcedure;
            Cmd1.CommandText = "PR_LOC_Country_SelectForDropDown";
            SqlDataReader objSDR1 = Cmd1.ExecuteReader();
            DataTable dt1 = new DataTable();
            dt1.Load(objSDR1);

            List<LOC_CountryDropDownModel> list = new List<LOC_CountryDropDownModel>();
            foreach (DataRow dr in dt1.Rows)
            {
                LOC_CountryDropDownModel vlst = new LOC_CountryDropDownModel();
                vlst.CountryID = Convert.ToInt32(dr["CountryID"]);
                vlst.CountryName = dr["CountryName"].ToString();
                list.Add(vlst);
            }
            ViewBag.CountryList = list;
            conn1.Close();
            #endregion

            #region State Drop Down

            //string connectionstr2 = this.Configuration.GetConnectionString("myConnectionString");
            //DataTable dt2 = new DataTable();

            //SqlConnection conn2 = new SqlConnection(connectionstr2);

            //conn2.Open();

            //SqlCommand objCmd2 = conn2.CreateCommand();
            //objCmd2.CommandType = CommandType.StoredProcedure;
            //objCmd2.CommandText = "PR_LOC_State_SelectForDropDown";
            //SqlDataReader objSDR2 = objCmd2.ExecuteReader();
            //dt2.Load(objSDR2);

            List<LOC_StateDropDownModel> state_list = new List<LOC_StateDropDownModel>();
            /*     foreach (DataRow dr in dt2.Rows)
                 {
                     LOC_StateDropDownModel vlst2 = new LOC_StateDropDownModel();
                     vlst2.StateID = Convert.ToInt32(dr["StateID"]);
                     vlst2.StateName = dr["StateName"].ToString();
                     list2.Add(vlst2);
                 }*/
            ViewBag.StateList = state_list;
            //conn2.Close();
            #endregion

            #region City Drop Down
            //string connectionstr3 = this.Configuration.GetConnectionString("myConnectionString");
            //DataTable dt3 = new DataTable();

            //SqlConnection conn3 = new SqlConnection(connectionstr3);

            //conn3.Open();

            //SqlCommand objCmd3 = conn3.CreateCommand();
            //objCmd3.CommandType = CommandType.StoredProcedure;
            //objCmd3.CommandText = "PR_LOC_City_SelectForDropDown";
            //SqlDataReader objSDR3 = objCmd3.ExecuteReader();
            //dt3.Load(objSDR3);

            List<LOC_CityDropDownModel> list3 = new List<LOC_CityDropDownModel>();
            //foreach (DataRow dr in dt3.Rows)
            //{
            //    LOC_CityDropDownModel vlst3 = new LOC_CityDropDownModel();
            //    vlst3.CityID = Convert.ToInt32(dr["CityID"]);
            //    vlst3.CityName = dr["CityName"].ToString();
            //    list3.Add(vlst3);
            //}
            ViewBag.CityList = list3;
            //conn3.Close();
            #endregion

            #region Contact Category Drop Down
            string connectionstr4 = Configuration.GetConnectionString("myConnectionString");
            DataTable dt4 = new DataTable();

            SqlConnection conn4 = new SqlConnection(connectionstr4);

            conn4.Open();

            SqlCommand objCmd4 = conn4.CreateCommand();
            objCmd4.CommandType = CommandType.StoredProcedure;
            objCmd4.CommandText = "PR_MST_ContactCategory_SelectForDropDown";
            SqlDataReader objSDR4 = objCmd4.ExecuteReader();
            dt4.Load(objSDR4);

            List<MST_ContactCategoryDropDownModel> list4 = new List<MST_ContactCategoryDropDownModel>();
            foreach (DataRow dr in dt4.Rows)
            {
                MST_ContactCategoryDropDownModel vlst4 = new MST_ContactCategoryDropDownModel();
                vlst4.ContactCategoryID = Convert.ToInt32(dr["ContactCategoryID"]);
                vlst4.ContactCategoryName = dr["ContactCategoryName"].ToString();
                list4.Add(vlst4);
            }
            ViewBag.ContactCategoryList = list4;
            conn4.Close();
            #endregion

            if (ContactID != null)
            {

                string str = Configuration.GetConnectionString("myConnectionString");
                SqlConnection conn = new SqlConnection(str);
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_CON_Contact_SelectByPK";
                cmd.Parameters.Add("@ContactID", SqlDbType.Int).Value = ContactID;

                DataTable dt = new DataTable();
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                CON_ContactModel modelCON_Contact = new CON_ContactModel();

                foreach (DataRow dr in dt.Rows)
                {
                    //DropDownByCountry(Convert.ToInt32(dr["CountryID"]));
                    DropDownByCountry(Convert.ToInt32(dr["CountryID"]));
                    DropDownByState(Convert.ToInt32(dr["StateID"]));
                    modelCON_Contact.ContactID = Convert.ToInt32(dr["ContactID"]);
                    modelCON_Contact.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelCON_Contact.StateID = Convert.ToInt32(dr["StateID"]);
                    modelCON_Contact.CityID = Convert.ToInt32(dr["CityID"]);
                    modelCON_Contact.ContactCategoryID = Convert.ToInt32(dr["ContactCategoryID"]);
                    modelCON_Contact.ContactName = dr["ContactName"].ToString();
                    modelCON_Contact.Address = dr["Address"].ToString();
                    modelCON_Contact.PinCode = dr["PinCode"].ToString();
                    modelCON_Contact.Mobile = dr["Mobile"].ToString();
                    modelCON_Contact.AlternateContact = dr["AlternateContact"].ToString();
                    modelCON_Contact.Email = dr["Email"].ToString();
                    modelCON_Contact.BirthDate = Convert.ToDateTime(dr["BirthDate"]);
                    modelCON_Contact.LinkedIn = dr["LinkedIn"].ToString();
                    modelCON_Contact.Twitter = dr["Twitter"].ToString();
                    modelCON_Contact.Instagram = dr["Instagram"].ToString();
                    modelCON_Contact.Gender = dr["Gender"].ToString();
                    modelCON_Contact.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                }

                return View("CON_ContactAddEdit", modelCON_Contact);
            }
            return View("CON_ContactAddEdit");
        }
        #endregion

        #region Insert
        [HttpPost]
        public IActionResult Save(CON_ContactModel modelCON_Contact)
        {
            if (modelCON_Contact.File != null)
            {
                string FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileNameWithPath = Path.Combine(path, modelCON_Contact.File.FileName);
                modelCON_Contact.PhotoPath = FilePath.Replace("wwwroot\\", "/") + "/" + modelCON_Contact.File.FileName;
                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    modelCON_Contact.File.CopyTo(stream);
                }
            }
            string str = Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            if (modelCON_Contact.ContactID == null)
            {
                cmd.CommandText = "PR_CON_Contact_Insert";
                cmd.Parameters.Add("@CreationDate", SqlDbType.Date).Value = modelCON_Contact.CreationDate;
            }
            else
            {
                cmd.CommandText = "PR_CON_Contact_UpdateByPK";
                cmd.Parameters.Add("@ContactID", SqlDbType.Int).Value = modelCON_Contact.ContactID;
            }

            cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = modelCON_Contact.CountryID;
            cmd.Parameters.Add("@StateID", SqlDbType.Int).Value = modelCON_Contact.StateID;
            cmd.Parameters.Add("@CityID", SqlDbType.Int).Value = modelCON_Contact.CityID;
            cmd.Parameters.Add("@ContactCategoryID", SqlDbType.Int).Value = modelCON_Contact.ContactCategoryID;
            cmd.Parameters.Add("@ContactName", SqlDbType.NVarChar).Value = modelCON_Contact.ContactName;
            cmd.Parameters.Add("@PhotoPath", SqlDbType.NVarChar).Value = modelCON_Contact.PhotoPath;
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = modelCON_Contact.Address;
            cmd.Parameters.Add("@PinCode", SqlDbType.NVarChar).Value = modelCON_Contact.PinCode;
            cmd.Parameters.Add("@Mobile", SqlDbType.NVarChar).Value = modelCON_Contact.Mobile;
            cmd.Parameters.Add("@AlternateContact", SqlDbType.NVarChar).Value = modelCON_Contact.AlternateContact;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = modelCON_Contact.Email;
            cmd.Parameters.Add("@BirthDate", SqlDbType.Date).Value = modelCON_Contact.BirthDate;
            cmd.Parameters.Add("@LinkedIn", SqlDbType.NVarChar).Value = modelCON_Contact.LinkedIn;
            cmd.Parameters.Add("@Twitter", SqlDbType.NVarChar).Value = modelCON_Contact.Twitter;
            cmd.Parameters.Add("@Instagram", SqlDbType.NVarChar).Value = modelCON_Contact.Instagram;
            cmd.Parameters.Add("@Gender", SqlDbType.NVarChar).Value = modelCON_Contact.Gender;
            cmd.Parameters.Add("@ModificationDate", SqlDbType.Date).Value = modelCON_Contact.ModificationDate;

            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                if (modelCON_Contact.ContactID == null)
                    TempData["ContactInsertMsg"] = "Record Inserted Sucessfully!!";
                else
                    TempData["ContactInsertMsg"] = "Record Updated Sucessfully!!";
            }
            conn.Close();
            return RedirectToAction("Index");
        }
        #endregion

        #region DropDownByCountry
        public IActionResult DropDownByCountry(int CountryID)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn1 = new SqlConnection(str);
            DataTable dt1 = new DataTable();
            conn1.Open();
            SqlCommand cmd1 = conn1.CreateCommand();
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.CommandText = "PR_LOC_State_SelectDropDownByCountryID";
            cmd1.Parameters.AddWithValue("@CountryID", CountryID);
            SqlDataReader sdr1 = cmd1.ExecuteReader();
            dt1.Load(sdr1);


            List<LOC_StateDropDownModel> state_list = new List<LOC_StateDropDownModel>();
            foreach (DataRow dr1 in dt1.Rows)
            {
                LOC_StateDropDownModel vlst1 = new LOC_StateDropDownModel();
                vlst1.StateID = Convert.ToInt32(dr1["StateID"]);
                vlst1.StateName = dr1["StateName"].ToString();
                state_list.Add(vlst1);
            }
            ViewBag.StateList = state_list;
            var vModel = state_list;
            return Json(vModel);
            conn1.Close();
        }
        #endregion

        #region DropDownByState
        public IActionResult DropDownByState(int StateID)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn2 = new SqlConnection(str);
            DataTable dt2 = new DataTable();
            conn2.Open();
            SqlCommand cmd1 = conn2.CreateCommand();
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.CommandText = "PR_LOC_City_SelectDropDownByStateID";
            cmd1.Parameters.AddWithValue("@StateID", StateID);
            SqlDataReader sdr1 = cmd1.ExecuteReader();
            dt2.Load(sdr1);


            List<LOC_CityDropDownModel> list3 = new List<LOC_CityDropDownModel>();
            foreach (DataRow dr2 in dt2.Rows)
            {
                LOC_CityDropDownModel vlst1 = new LOC_CityDropDownModel();
                vlst1.CityID = Convert.ToInt32(dr2["CityID"]);
                vlst1.CityName = dr2["CityName"].ToString();
                list3.Add(vlst1);
            }
            ViewBag.CityList = list3;
            var vModel = list3;
            conn2.Close();
            return Json(vModel);
        }
        #endregion
    }
}
