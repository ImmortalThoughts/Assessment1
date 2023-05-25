using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace Assesment1.DAL
{
    public class Customers
    {
        private Database db;
        #region variable Declaration

        #region Constructors

        public Customers()
        {
            this.db = DatabaseFactory.CreateDatabase();
        }
        public Customers(int CustomerID)
        {
            this.db = DatabaseFactory.CreateDatabase();
            this.CustomerID = CustomerID;
        }
        #endregion
        #region Properties
        [Display(Name = "Book Name")]
        public int CustomerID
        {
            get; set;
        }
        public string Firstname
        {
            get; set;
        }
        public string Lastname { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }

        public bool IsActive
        {
            get; set;
        }
        public int? CreatedBy
        {
            get; set;
        }
        public DateTime? CreatedOn
        {
            get; set;
        }
        public int? ModifiedBy
        {
            get; set;
        }
        public DateTime? ModifiedOn
        {
            get; set;
        }
        #endregion
        #region Actions

        public bool Load()
        {
            try
            {
                if (this.CustomerID != 0)
                {
                    DbCommand com = this.db.GetStoredProcCommand("CustomersDetails");
                    this.db.AddInParameter(com, "CustomerID", System.Data.DbType.Int32, this.CustomerID);
                    DataSet ds = this.db.ExecuteDataSet(com);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        this.CustomerID = Convert.ToInt32(dt.Rows[0]["CustomerID"]);
                        this.Firstname = Convert.ToString(dt.Rows[0]["Firstname"]);
                        this.Lastname = Convert.ToString(dt.Rows[0]["Lastname"]);
                        this.PhoneNo = dt.Rows[0]["PhoneNo"].ToString();
                        this.Address = Convert.ToString(dt.Rows[0]["Address"]);
                        this.City = Convert.ToString(dt.Rows[0]["City"]);
                        this.Postcode = Convert.ToString(dt.Rows[0]["Postcode"]);
                        this.IsActive = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                        this.CreatedBy = Convert.ToInt32(dt.Rows[0]["CreatedBy"]);
                        this.CreatedOn = Convert.ToDateTime(dt.Rows[0]["CreatedOn"]);
                        this.ModifiedBy = Convert.ToInt32(dt.Rows[0]["ModifiedBy"]);
                        this.ModifiedOn = Convert.ToDateTime(dt.Rows[0]["ModifiedOn"]);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool Save()
        {
            if (this.CustomerID == 0)
            {
                return this.Insert();
            }
            else
            {
                if (this.CustomerID > 0)
                {
                    return this.Update();
                }
                else
                {
                    this.CustomerID = 0;
                    return false;
                }
            }
        }
        public bool Insert()
        {
            try
            {
                DbCommand com = this.db.GetStoredProcCommand("CustomersInsert");
                this.db.AddOutParameter(com, "CustomerID", DbType.Int32, 1024);
                if (!String.IsNullOrEmpty(this.Firstname))
                {
                    this.db.AddInParameter(com, "Firstname", DbType.String, this.Firstname);
                }
                else
                {
                    this.db.AddInParameter(com, "Firstname", DbType.String, DBNull.Value);
                }
                if (!String.IsNullOrEmpty(this.Lastname))
                {
                    this.db.AddInParameter(com, "Lastname", DbType.String, this.Lastname);
                }
                else
                {
                    this.db.AddInParameter(com, "Lastname", DbType.String, DBNull.Value);
                }
                if (!String.IsNullOrEmpty(this.PhoneNo))
                {
                    this.db.AddInParameter(com, "PhoneNo", DbType.String, this.PhoneNo);
                }
                else
                {
                    this.db.AddInParameter(com, "PhoneNo", DbType.String, DBNull.Value);
                }
                if (!String.IsNullOrEmpty(this.Address))
                {
                    this.db.AddInParameter(com, "Address", DbType.String, this.Address);
                }
                else
                {
                    this.db.AddInParameter(com, "Address", DbType.String, DBNull.Value);
                }
                if (!String.IsNullOrEmpty(this.City))
                {
                    this.db.AddInParameter(com, "City", DbType.String, this.City);
                }
                else
                {
                    this.db.AddInParameter(com, "City", DbType.String, DBNull.Value);
                }
                if (!String.IsNullOrEmpty(this.Postcode))
                {
                    this.db.AddInParameter(com, "Postcode", DbType.String, this.City);
                }
                else
                {
                    this.db.AddInParameter(com, "Postcode", DbType.String, DBNull.Value);
                }
                
                this.db.AddInParameter(com, "IsActive", DbType.Int32, this.IsActive);
                if (this.CreatedBy > 0)
                {
                    this.db.AddInParameter(com, "CreatedBy", DbType.Int32, this.CreatedBy);
                }
                else
                {
                    this.db.AddInParameter(com, "CreatedBy", DbType.Int32, DBNull.Value);
                }
                if (this.CreatedOn > DateTime.MinValue)
                {
                    this.db.AddInParameter(com, "CreatedOn", DbType.DateTime, this.CreatedOn);
                }
                else
                {
                    this.db.AddInParameter(com, "CreatedOn", DbType.DateTime, DBNull.Value);
                }
                if (this.ModifiedBy > 0)
                {
                    this.db.AddInParameter(com, "ModifiedBy", DbType.Int32, this.ModifiedBy);
                }
                else
                {
                    this.db.AddInParameter(com, "ModifiedBy", DbType.Int32, DBNull.Value);
                }
                if (this.ModifiedOn > DateTime.MinValue)
                {
                    this.db.AddInParameter(com, "ModifiedOn", DbType.DateTime, this.ModifiedOn);
                }
                else
                {
                    this.db.AddInParameter(com, "ModifiedOn", DbType.DateTime, DBNull.Value);
                }

                this.db.ExecuteNonQuery(com);
                this.CustomerID = Convert.ToInt32(this.db.GetParameterValue(com, "CustomerID"));
            }
            catch (Exception)
            {

                return false;
            }
            return this.CustomerID > 0;
        }
        private bool Update()
        {
            try
            {
                DbCommand com = this.db.GetStoredProcCommand("CustomersUpdate");
                this.db.AddInParameter(com, "CustomerID", DbType.Int32, this.CustomerID);
                if (!String.IsNullOrEmpty(this.Firstname))
                {
                    this.db.AddInParameter(com, "Firstname", DbType.String, this.Firstname);
                }
                else
                {
                    this.db.AddInParameter(com, "Firstname", DbType.String, DBNull.Value);
                }
                if (!String.IsNullOrEmpty(this.Lastname))
                {
                    this.db.AddInParameter(com, "Lastname", DbType.String, this.Lastname);
                }
                else
                {
                    this.db.AddInParameter(com, "Lastname", DbType.String, DBNull.Value);
                }
                if (!String.IsNullOrEmpty(this.PhoneNo))
                {
                    this.db.AddInParameter(com, "PhoneNo", DbType.String, this.PhoneNo);
                }
                else
                {
                    this.db.AddInParameter(com, "PhoneNo", DbType.String, DBNull.Value);
                }
                if (!String.IsNullOrEmpty(this.Address))
                {
                    this.db.AddInParameter(com, "Address", DbType.String, this.Address);
                }
                else
                {
                    this.db.AddInParameter(com, "Address", DbType.String, DBNull.Value);
                }
                if (!String.IsNullOrEmpty(this.City))
                {
                    this.db.AddInParameter(com, "City", DbType.String, this.City);
                }
                else
                {
                    this.db.AddInParameter(com, "City", DbType.String, DBNull.Value);
                }
                if (!String.IsNullOrEmpty(this.Postcode))
                {
                    this.db.AddInParameter(com, "Postcode", DbType.String, this.City);
                }
                else
                {
                    this.db.AddInParameter(com, "Postcode", DbType.String, DBNull.Value);
                }

                this.db.AddInParameter(com, "IsActive", DbType.Boolean, this.IsActive);
                if (this.CreatedBy > 0)
                {
                    this.db.AddInParameter(com, "CreatedBy", DbType.Int32, this.CreatedBy);
                }
                else
                {
                    this.db.AddInParameter(com, "CreatedBy", DbType.Int32, DBNull.Value);
                }
                if (this.CreatedOn > DateTime.MinValue)
                {
                    this.db.AddInParameter(com, "CreatedOn", DbType.DateTime, this.CreatedOn);
                }
                else
                {
                    this.db.AddInParameter(com, "CreatedOn", DbType.DateTime, DBNull.Value);
                }
                if (this.ModifiedBy > 0)
                {
                    this.db.AddInParameter(com, "ModifiedBy", DbType.Int32, this.ModifiedBy);
                }
                else
                {
                    this.db.AddInParameter(com, "ModifiedBy", DbType.Int32, DBNull.Value);
                }
                if (this.ModifiedOn > DateTime.MinValue)
                {
                    this.db.AddInParameter(com, "ModifiedOn", DbType.DateTime, this.ModifiedOn);
                }
                else
                {
                    this.db.AddInParameter(com, "ModifiedOn", DbType.DateTime, DBNull.Value);
                }

                this.db.ExecuteNonQuery(com);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public bool Delete()
        {
            try
            {
                DbCommand com = this.db.GetStoredProcCommand("CustomersDelete");
                this.db.AddInParameter(com, "CustomerID", DbType.Int32, this.CustomerID);
                this.db.ExecuteNonQuery(com);
            }
            catch (Exception ex)
            {
                // To Do: Handle Exception
                return false;
            }

            return true;
        }

        public List<Customers> GetList()
        {
            DataSet ds = null;
            try
            {

                DbCommand com = db.GetStoredProcCommand("CustomerGetList");

                if (this.CustomerID > 0)
                {
                    this.db.AddInParameter(com, "CustomerID", DbType.Int32, this.CustomerID);
                }
                else
                {
                    this.db.AddInParameter(com, "CustomerID", DbType.Int32, DBNull.Value);
                }
                if (!String.IsNullOrEmpty(this.Firstname))
                {
                    this.db.AddInParameter(com, "Firstname", DbType.String, this.Firstname);
                }
                else
                {
                    this.db.AddInParameter(com, "Firstname", DbType.String, DBNull.Value);
                }
                if (!String.IsNullOrEmpty(this.Lastname))
                {
                    this.db.AddInParameter(com, "Lastname", DbType.String, this.Lastname);
                }
                else
                {
                    this.db.AddInParameter(com, "Lastname", DbType.String, DBNull.Value);
                }
                if (!String.IsNullOrEmpty(this.PhoneNo))
                {
                    this.db.AddInParameter(com, "PhoneNo", DbType.String, this.PhoneNo);
                }
                else
                {
                    this.db.AddInParameter(com, "PhoneNo", DbType.String, DBNull.Value);
                }
                if (!String.IsNullOrEmpty(this.Address))
                {
                    this.db.AddInParameter(com, "Address", DbType.String, this.Address);
                }
                else
                {
                    this.db.AddInParameter(com, "Address", DbType.String, DBNull.Value);
                }
                if (!String.IsNullOrEmpty(this.City))
                {
                    this.db.AddInParameter(com, "City", DbType.String, this.City);
                }
                else
                {
                    this.db.AddInParameter(com, "City", DbType.String, DBNull.Value);
                }
                if (!String.IsNullOrEmpty(this.Postcode))
                {
                    this.db.AddInParameter(com, "Postcode", DbType.String, this.City);
                }
                else
                {
                    this.db.AddInParameter(com, "Postcode", DbType.String, DBNull.Value);
                }

                ds = db.ExecuteDataSet(com);
                List<Customers> bookList = new List<Customers>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {

                    bookList.Add(new Customers
                    {
                        CustomerID = Convert.ToInt32(row["CustomerID"]),
                        Firstname = row["Firstname"].ToString(),
                        Lastname = row["Lastname"].ToString(),
                        PhoneNo = row["PhoneNo"].ToString(),
                        Address = Convert.ToString(row["Address"]),
                        City=Convert.ToString(row["City"])
                    });
                }
                return bookList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }


    }
    #endregion

    #endregion
}
