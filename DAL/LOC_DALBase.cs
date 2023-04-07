using AddressBookDemo.Areas.LOC_City.Models;
using AddressBookDemo.Areas.LOC_Country.Controllers;
using AddressBookDemo.Areas.LOC_Country.Models;
using AddressBookDemo.Areas.LOC_State.Models;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace AddressBookDemo.DAL
{
    public class LOC_DALBase
    {

        #region dbo.PR_LOC_Country_SelectAll
        public DataTable dbo_PR_LOC_Country_SelectAll(string conn,LOC_CountryModel modelLOC_Country)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_Country_SelectAll");

                DataTable dt = new DataTable();
                if (modelLOC_Country.CountryName != null || modelLOC_Country.CountryCode != null)
                {

                    dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_SelectByCountryNameCode");


                    if (modelLOC_Country.CountryName != null)
                        sqlDB.AddInParameter(dbCMD, "CountryName", SqlDbType.NVarChar, modelLOC_Country.CountryName);

                    else
                        sqlDB.AddInParameter(dbCMD, "CountryName", SqlDbType.NVarChar, "");


                    if (modelLOC_Country.CountryCode != null)
                        sqlDB.AddInParameter(dbCMD, "CountryCode", SqlDbType.NVarChar, modelLOC_Country.CountryCode);

                    else
                        sqlDB.AddInParameter(dbCMD, "CountryCode", SqlDbType.NVarChar, "");
                }


                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region dbo.PR_LOC_State_SelectAll
        public DataTable dbo_PR_LOC_State_SelectAll(string conn,LOC_StateModel modelLOC_State)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_State_SelectAll");

                DataTable dt = new DataTable();
                if (modelLOC_State.CountryName != null || modelLOC_State.StateCode != null || modelLOC_State.StateName != null)
                {

                    dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_SelectByStateNameCountryNameStateCode");


                    if (modelLOC_State.CountryName != null)
                        sqlDB.AddInParameter(dbCMD, "CountryName", SqlDbType.NVarChar, modelLOC_State.CountryName);

                    else
                        sqlDB.AddInParameter(dbCMD, "CountryName", SqlDbType.NVarChar, "");

                    if (modelLOC_State.StateName != null)
                        sqlDB.AddInParameter(dbCMD, "StateName", SqlDbType.NVarChar, modelLOC_State.StateName);

                    else
                        sqlDB.AddInParameter(dbCMD, "StateName", SqlDbType.NVarChar, "");


                    if (modelLOC_State.StateCode != null)
                        sqlDB.AddInParameter(dbCMD, "StateCode", SqlDbType.NVarChar, modelLOC_State.StateCode);

                    else
                        sqlDB.AddInParameter(dbCMD, "StateCode", SqlDbType.NVarChar, "");


                }

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            } 
        }
        #endregion

        #region dbo.PR_LOC_City_SelectAll
        public DataTable dbo_PR_LOC_City_SelectAll(string conn,LOC_CityModel modelLOC_City)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_City_SelectAll");

                DataTable dt = new DataTable();
                if (modelLOC_City.CountryName != null || modelLOC_City.StateName != null || modelLOC_City.CityName != null || modelLOC_City.CityCode != null)
                {

                    dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_SelectByCityNameCountryNameStateNameCityCode");


                    if (modelLOC_City.CountryName != null)
                        sqlDB.AddInParameter(dbCMD, "CountryName", SqlDbType.NVarChar, modelLOC_City.CountryName);

                    else
                        sqlDB.AddInParameter(dbCMD, "CountryName", SqlDbType.NVarChar, "");

                    if (modelLOC_City.StateName != null)
                        sqlDB.AddInParameter(dbCMD, "StateName", SqlDbType.NVarChar, modelLOC_City.StateName);

                    else
                        sqlDB.AddInParameter(dbCMD, "StateName", SqlDbType.NVarChar, "");

                    if (modelLOC_City.CityName != null)
                        sqlDB.AddInParameter(dbCMD, "CityName", SqlDbType.NVarChar, modelLOC_City.CityName);

                    else
                        sqlDB.AddInParameter(dbCMD, "CityName", SqlDbType.NVarChar, "");

                    if (modelLOC_City.CityCode != null)
                        sqlDB.AddInParameter(dbCMD, "CityCode", SqlDbType.NVarChar, modelLOC_City.CityCode);

                    else
                        sqlDB.AddInParameter(dbCMD, "CityCode", SqlDbType.NVarChar, "");


                }

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method: dbo_PR_LOC_State_Delete
        public bool? dbo_PR_LOC_State_Delete(string conn,int? StateID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_State_Delete");
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, StateID);

                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method: dbo_PR_LOC_Country_Insert
        public DataTable PR_LOC_Country_Insert(string conn, LOC_CountryModel modelLOC_Country)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_Insert");

                DataTable dt = new DataTable();
                sqlDB.AddInParameter(dbCMD, "CountryName", SqlDbType.NVarChar, modelLOC_Country.CountryName);
                sqlDB.AddInParameter(dbCMD, "CountryCode", SqlDbType.NVarChar, modelLOC_Country.CountryCode);
                sqlDB.AddInParameter(dbCMD, "PhotoPath", SqlDbType.NVarChar, modelLOC_Country.PhotoPath);
                sqlDB.AddInParameter(dbCMD, "CreationDate", SqlDbType.Date, modelLOC_Country.CreationDate);
                sqlDB.AddInParameter(dbCMD, "ModificationDate", SqlDbType.Date, modelLOC_Country.ModificationDate);
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        #endregion

        #region Method: dbo_PR_LOC_State_Insert
        public DataTable PR_LOC_State_Insert(string conn, LOC_StateModel modelLOC_State)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State__Insert");

                DataTable dt = new DataTable();
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.NVarChar, modelLOC_State.CountryID);
                sqlDB.AddInParameter(dbCMD, "StateName", SqlDbType.NVarChar, modelLOC_State.StateName);
                sqlDB.AddInParameter(dbCMD, "StateCode", SqlDbType.NVarChar, modelLOC_State.StateCode);
                sqlDB.AddInParameter(dbCMD, "CreationDate", SqlDbType.Date, modelLOC_State.CreationDate);
                sqlDB.AddInParameter(dbCMD, "ModificationDate", SqlDbType.Date, modelLOC_State.ModificationDate);
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        #endregion

        #region Method: dbo_PR_LOC_City_Insert
        public DataTable PR_LOC_City_Insert(string conn, LOC_CityModel modelLOC_City)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_Insert");

                DataTable dt = new DataTable();
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.NVarChar, modelLOC_City.CountryID);
                sqlDB.AddInParameter(dbCMD, "CityName", SqlDbType.NVarChar, modelLOC_City.CityName);
                sqlDB.AddInParameter(dbCMD, "CityCode", SqlDbType.NVarChar, modelLOC_City.CityCode);
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.NVarChar, modelLOC_City.StateID);
                sqlDB.AddInParameter(dbCMD, "CreationDate", SqlDbType.Date, modelLOC_City.CreationDate);
                sqlDB.AddInParameter(dbCMD, "ModificationDate", SqlDbType.Date, modelLOC_City.ModificationDate);
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        #endregion

        #region Method: dbo_PR_LOC_Country_SelectbyDropdown
        public DataTable dbo_PR_LOC_Country_SelectByDropdownList(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_Country_SelectComboBox");

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method: dbo_PR_LOC_State_SelectbyDropdown
        public DataTable dbo_PR_LOC_State_SelectByDropdownList(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_State_SelectComboBox");

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Method: dbo_PR_LOC_State_SelectByPK
        public DataTable PR_LOC_State_SelectByPK(string conn, int? StateID)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_SelectByPK");

                DataTable dt = new DataTable();
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, StateID);

                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        #endregion

        #region Method: dbo_PR_LOC_Country_Update
        public DataTable PR_LOC_Country_UpdateByPK(string conn, LOC_CountryModel modelLOC_Country)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_UpdateByPK");

                DataTable dt = new DataTable();
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, modelLOC_Country.CountryID);
                sqlDB.AddInParameter(dbCMD, "CountryName", SqlDbType.NVarChar, modelLOC_Country.CountryName);
                sqlDB.AddInParameter(dbCMD, "CountryCode", SqlDbType.NVarChar, modelLOC_Country.CountryCode);
                sqlDB.AddInParameter(dbCMD, "PhotoPath", SqlDbType.NVarChar, modelLOC_Country.PhotoPath);
                sqlDB.AddInParameter(dbCMD, "ModificationDate", SqlDbType.Date, modelLOC_Country.ModificationDate);
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        #endregion

        #region Method: dbo_PR_LOC_State_Update
        public DataTable PR_LOC_State_UpdateByPK(string conn, LOC_StateModel modelLOC_State)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_UpdateByPK");

                DataTable dt = new DataTable();
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, modelLOC_State.StateID);
                sqlDB.AddInParameter(dbCMD, "StateName", SqlDbType.NVarChar, modelLOC_State.StateName);
                sqlDB.AddInParameter(dbCMD, "StateCode", SqlDbType.NVarChar, modelLOC_State.StateCode);
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, modelLOC_State.CountryID);
                sqlDB.AddInParameter(dbCMD, "ModificationDate", SqlDbType.Date, modelLOC_State.ModificationDate);
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        #endregion

        #region Method: dbo_PR_LOC_City_Update
        public DataTable PR_LOC_City_UpdateByPK(string conn, LOC_CityModel modelLOC_City)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_UpdateByPK");

                DataTable dt = new DataTable();
                sqlDB.AddInParameter(dbCMD, "CityID", SqlDbType.Int, modelLOC_City.CityID);
                sqlDB.AddInParameter(dbCMD, "CityName", SqlDbType.NVarChar, modelLOC_City.CityName);
                sqlDB.AddInParameter(dbCMD, "CityCode", SqlDbType.NVarChar, modelLOC_City.CityCode);
                sqlDB.AddInParameter(dbCMD, "CountryID", SqlDbType.Int, modelLOC_City.CountryID);
                sqlDB.AddInParameter(dbCMD, "StateID", SqlDbType.Int, modelLOC_City.StateID);
                sqlDB.AddInParameter(dbCMD, "ModificationDate", SqlDbType.Date, modelLOC_City   .ModificationDate);
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }
                return dt;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        #endregion

    }
}