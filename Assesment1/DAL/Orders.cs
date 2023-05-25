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
    public class Orders
    {
        private Database db;
        #region variable Declaration

        #region Constructors

        public Orders()
        {
            this.db = DatabaseFactory.CreateDatabase();
        }
        public Orders(int BookID)
        {
            this.db = DatabaseFactory.CreateDatabase();
            this.OrderId = OrderId;
        }
        #endregion
        #region Properties
        [Display(Name = "Book Name")]
        public int OrderId
        {
            get; set;
        }
        public DateTime OrderDateFrom { get; set; }
        public DateTime OrderDateTo { get; set; }
        public string OrderDateFromStr { get; set; }
        public string OrderDateToStr { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalCount { get; set; }
        public int? PageNumber { get; set; }
        public decimal PageSize { get; set; }
        public string DeliveryDateStr { get; set; }
        public string Firstname
        {
            get; set;
        }
        public string Lastname { get; set; }
        public string OrderNotes
        {
            get; set;
        }
        public string OrderRerenceNo { get; set; }
        public string ContactPerson { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string DeliveryNotes { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
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
        public int ProductCategoryID { get; set; }
        public int ProductID { get; set; }
        #endregion
        #region Actions

        public bool Load()
        {
            try
            {
                if (this.OrderId != 0)
                {
                    DbCommand com = this.db.GetStoredProcCommand("OrdersDetails");
                    this.db.AddInParameter(com, "OrderID", System.Data.DbType.Int32, this.OrderId);
                    DataSet ds = this.db.ExecuteDataSet(com);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        this.OrderId = Convert.ToInt32(dt.Rows[0]["OrderId"]);
                        this.OrderDateFromStr = Convert.ToString(dt.Rows[0]["OrderDateFrom"]);
                        //this.OrderDateTo = Convert.ToDateTime(dt.Rows[0]["OrderDate"]);
                        this.Firstname = Convert.ToString(dt.Rows[0]["Firstname"]);
                        this.Lastname = Convert.ToString(dt.Rows[0]["Lastname"]);
                        this.CustomerId = Convert.ToInt32(dt.Rows[0]["CustomerId"]);                  
                        this.OrderRerenceNo = Convert.ToString(dt.Rows[0]["OrderRerenceNo"]);                                          
                        this.DeliveryDateStr = Convert.ToString(dt.Rows[0]["DeliveryDate"]);                   
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
            if (this.OrderId == 0)
            {
                return this.Insert();
            }
            else
            {
                if (this.OrderId > 0)
                {
                    return this.Update();
                }
                else
                {
                    this.OrderId = 0;
                    return false;
                }
            }
        }
        public bool Insert()
        {
            try
            {
                DbCommand com = this.db.GetStoredProcCommand("OrdersInsert");
                this.db.AddInParameter(com, "OrderID", DbType.Int32, 1024);
                if (this.OrderDateFrom> DateTime.MinValue)
                {
                    this.db.AddInParameter(com, "OrderDate", DbType.DateTime, this.OrderDateFrom);
                }
                else
                {
                    this.db.AddInParameter(com, "OrderDate", DbType.DateTime, DBNull.Value);
                }
                if (this.OrderDateTo > DateTime.MinValue)
                {
                    this.db.AddInParameter(com, "OrderDate", DbType.DateTime, this.OrderDateTo);
                }
                else
                {
                    this.db.AddInParameter(com, "OrderDate", DbType.DateTime, DBNull.Value);
                }
                if (this.CustomerId>0)
                {
                    this.db.AddInParameter(com, "CustomerId", DbType.Int32, this.CustomerId);
                }
                else
                {
                    this.db.AddInParameter(com, "CustomerId", DbType.Int32, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(this.OrderNotes))
                {
                    this.db.AddInParameter(com, "OrderNotes", DbType.String, this.OrderNotes);
                }
                else
                {
                    this.db.AddInParameter(com, "OrderNotes", DbType.String, DBNull.Value);
                }
                if(!string.IsNullOrEmpty(this.OrderRerenceNo))
                {
                    this.db.AddInParameter(com, "OrderRerenceNo", DbType.String, this.OrderRerenceNo);
                }
                else
                {
                    this.db.AddInParameter(com, "OrderRerenceNo", DbType.String, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(this.ContactPerson))
                {
                    this.db.AddInParameter(com, "ContactPerson", DbType.String, this.ContactPerson);
                }
                else
                {
                    this.db.AddInParameter(com, "ContactPerson", DbType.String, DBNull.Value);
                }
                if (this.DeliveryDate > DateTime.MinValue)
                {
                    this.db.AddInParameter(com, "DeliveryDate", DbType.DateTime, this.DeliveryDate);
                }
                else
                {
                    this.db.AddInParameter(com, "DeliveryDate", DbType.DateTime, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(this.DeliveryNotes))
                {
                    this.db.AddInParameter(com, "DeliveryNotes", DbType.String, this.DeliveryNotes);
                }
                else
                {
                    this.db.AddInParameter(com, "DeliveryNotes", DbType.String, DBNull.Value);
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
                this.OrderId = Convert.ToInt32(this.db.GetParameterValue(com, "OrderId"));
            }
            catch (Exception)
            {

                return false;
            }
            return this.OrderId > 0;
        }
        private bool Update()
        {
            try
            {
                DbCommand com = this.db.GetStoredProcCommand("OrdersUpdate");
               
                if(this.OrderId>0)
                {
                    this.db.AddInParameter(com, "OrderID", DbType.Int32,this.OrderId);
                }
                else
                {
                    this.db.AddInParameter(com, "OrderID", DbType.Int32, DBNull.Value);
                }
                if (this.OrderDateFrom > DateTime.MinValue)
                {
                    this.db.AddInParameter(com, "OrderDate", DbType.DateTime, this.OrderDateFrom);
                }
                else
                {
                    this.db.AddInParameter(com, "OrderDate", DbType.DateTime, DBNull.Value);
                }
                if (this.CustomerId > 0)
                {
                    this.db.AddInParameter(com, "CustomerId", DbType.Int32, this.CustomerId);
                }
                else
                {
                    this.db.AddInParameter(com, "CustomerId", DbType.Int32, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(this.OrderNotes))
                {
                    this.db.AddInParameter(com, "OrderNotes", DbType.String, this.OrderNotes);
                }
                else
                {
                    this.db.AddInParameter(com, "OrderNotes", DbType.String, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(this.OrderRerenceNo))
                {
                    this.db.AddInParameter(com, "OrderRerenceNo", DbType.String, this.OrderRerenceNo);
                }
                else
                {
                    this.db.AddInParameter(com, "OrderRerenceNo", DbType.String, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(this.ContactPerson))
                {
                    this.db.AddInParameter(com, "ContactPerson", DbType.String, this.ContactPerson);
                }
                else
                {
                    this.db.AddInParameter(com, "ContactPerson", DbType.String, DBNull.Value);
                }
                if (this.DeliveryDate > DateTime.MinValue)
                {
                    this.db.AddInParameter(com, "DeliveryDate", DbType.DateTime, this.DeliveryDate);
                }
                else
                {
                    this.db.AddInParameter(com, "DeliveryDate", DbType.DateTime, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(this.DeliveryNotes))
                {
                    this.db.AddInParameter(com, "DeliveryNotes", DbType.String, this.DeliveryNotes);
                }
                else
                {
                    this.db.AddInParameter(com, "DeliveryNotes", DbType.String, DBNull.Value);
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
                DbCommand com = this.db.GetStoredProcCommand("OrdersDelete");
                this.db.AddInParameter(com, "OrderID", DbType.Int32, this.OrderId);
                this.db.ExecuteNonQuery(com);
            }
            catch (Exception ex)
            {
                // To Do: Handle Exception
                return false;
            }

            return true;
        }

        public List<Orders> GetList()
        {
            DataSet ds = null;
            try
            {

                DbCommand com = db.GetStoredProcCommand("OrdersGetList");
                this.db.AddOutParameter(com, "TotalCount", DbType.Int32, 1024);
                if (this.ProductCategoryID>0)
                {
                    this.db.AddInParameter(com, "ProductCategoryID", DbType.Int32, this.ProductCategoryID);
                }
                else
                {
                    this.db.AddInParameter(com, "ProductCategoryID", DbType.Int32, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(this.ProductName))
                {
                    this.db.AddInParameter(com, "ProductName", DbType.String, this.ProductName);
                }
                else
                {
                    this.db.AddInParameter(com, "ProductName", DbType.String, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(this.Firstname))
                {
                    this.db.AddInParameter(com, "Firstname", DbType.String, this.Firstname);
                }
                else
                {
                    this.db.AddInParameter(com, "Firstname", DbType.String, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(this.Lastname))
                {
                    this.db.AddInParameter(com, "Lastname", DbType.String, this.Lastname);
                }
                else
                {
                    this.db.AddInParameter(com, "Lastname", DbType.String, DBNull.Value);
                }
                if (this.OrderDateFrom > DateTime.MinValue)
                {
                    this.db.AddInParameter(com, "OrderDateFrom", DbType.DateTime, this.OrderDateFrom);
                }
                else
                {
                    this.db.AddInParameter(com, "OrderDateFrom", DbType.DateTime, DBNull.Value);
                }
                if (this.OrderDateTo > DateTime.MinValue)
                {
                    this.db.AddInParameter(com, "OrderDateTo", DbType.DateTime, this.OrderDateTo);
                }
                else
                {
                    this.db.AddInParameter(com, "OrderDateTo", DbType.DateTime, DBNull.Value);
                }
                if (this.PageNumber > 0)
                {
                    this.db.AddInParameter(com, "pageNo", DbType.Int32, this.PageNumber);
                }
                else
                {
                    this.db.AddInParameter(com, "pageNo", DbType.Int32, DBNull.Value);
                }
                if (this.PageSize > 0)
                {
                    this.db.AddInParameter(com, "pageSize", DbType.Int32, this.PageSize);
                }
                else
                {
                    this.db.AddInParameter(com, "pageSize", DbType.Int32, DBNull.Value);
                }
                ds = db.ExecuteDataSet(com);
                this.TotalCount = Convert.ToInt32(this.db.GetParameterValue(com, "TotalCount"));
                List<Orders> bookList = new List<Orders>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {

                    bookList.Add(new Orders
                    {
                        OrderId = Convert.ToInt32(row["OrderId"]),
                        OrderDateFromStr=Convert.ToString(row["OrderDateFrom"]),
                       // OrderDateToStr = Convert.ToString(row["OrderDateTo"]),
                        CustomerId =Convert.ToInt32(row["CustomerId"]),
                        DeliveryDateStr = Convert.ToString(row["DeliveryDate"]),
                        //OrderNotes = row["OrderNotes"].ToString(),
                        OrderRerenceNo = row["OrderRerenceNo"].ToString(),
                        //ContactPerson = Convert.ToString(row["ContactPerson"]),
                        //DeliveryNotes= Convert.ToString(row["DeliveryNotes"]),
                        Firstname=Convert.ToString(row["Firstname"]),
                        Lastname = Convert.ToString(row["Lastname"]),
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
