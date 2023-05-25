using Assesment1.Models;
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
    public class OrderDetails
    {
        private Database db;
        #region variable Declaration

        #region Constructors

        public OrderDetails()
        {
            this.db = DatabaseFactory.CreateDatabase();
        }
        public OrderDetails(int OrderDetailID)
        {
            this.db = DatabaseFactory.CreateDatabase();
            this.OrderDetailID = OrderDetailID;
        }
        #endregion
        #region Properties
        [Display(Name = "Book Name")]
        public int OrderDetailID
        {
            get; set;
        }
        public string IssueXML { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public string Notes
        {
            get; set;
        }
        
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

        public List<OrderDetailsVM> Load()
        {
            try
            {
                //if (this.OrderDetailID != 0)
                if (this.OrderId != 0)
                {
                    DbCommand com = this.db.GetStoredProcCommand("OrdersDetailsDetails");
                    //this.db.AddInParameter(com, "OrderDetailID", System.Data.DbType.Int32, this.OrderDetailID);
                    this.db.AddInParameter(com, "OrderID", System.Data.DbType.Int32, this.OrderId);
                    DataSet ds = this.db.ExecuteDataSet(com);
                    List<OrderDetailsVM> OrderDetailsList = new List<OrderDetailsVM>();
                    foreach (DataRow Row in ds.Tables[0].Rows)
                    {
                        OrderDetailsList.Add(new OrderDetailsVM
                        {
                            OrderDetailID = Convert.ToInt32(Row["OrderDetailID"]),
                            OrderId = Convert.ToInt32(Row["OrderId"]),
                            ProductId = Convert.ToInt32(Row["ProductId"]),
                            Price = Convert.ToDouble(Row["Price"]),
                            Quantity = Convert.ToInt32(Row["Quantity"]),
                            Notes = Convert.ToString(Row["Notes"]),
                            TotalPrice = Convert.ToDouble(Row["TotalPrice"]),
                            ProductName = Convert.ToString(Row["ProductName"])
                        });
                       
                   }
                    return OrderDetailsList;
                }
               
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
               
            }
            return null;
        }

        public bool Save()
        {
            if (this.OrderDetailID == 0)
            {
                return this.Insert();
            }
            else
            {
                if (this.OrderDetailID > 0)
                {
                    return this.Update();
                }
                else
                {
                    this.OrderDetailID = 0;
                    return false;
                }
            }
        }
        public bool Insert()
        {
            try
            {
                DbCommand com = this.db.GetStoredProcCommand("OrderDetailsInsert");
                //this.db.AddOutParameter(com, "OrderDetailID", DbType.Int32, 1024);
                if(this.OrderId>0)
                {
                    this.db.AddInParameter(com, "OrderId", DbType.Int32, this.OrderId);
                }
                else
                {
                    this.db.AddInParameter(com, "OrderId", DbType.Int32, DBNull.Value);
                }
                if (!String.IsNullOrEmpty(this.IssueXML))
                {
                    this.db.AddInParameter(com, "xml", DbType.Xml, this.IssueXML);
                }
                else
                {
                    this.db.AddInParameter(com, "xml", DbType.Xml, DBNull.Value);
                }
                //if(this.OrderId>0)
                //{
                //    this.db.AddInParameter(com, "OrderId", DbType.Int32, this.OrderId);
                //}
                //else
                //{
                //    this.db.AddInParameter(com, "OrderId", DbType.Int32, DBNull.Value);
                //}
                //if(this.ProductId>0)
                //{
                //    this.db.AddInParameter(com, "ProductId", DbType.Int32, this.ProductId);
                //}
                //else
                //{
                //    this.db.AddInParameter(com, "ProductId", DbType.Int32, DBNull.Value);
                //}
                //if(this.Price>0)
                //{
                //    this.db.AddInParameter(com, "Price", DbType.Double, this.Price);
                //}
                //else
                //{
                //    this.db.AddInParameter(com, "Price", DbType.Double, DBNull.Value);
                //}
                //if (this.Quantity > 0)
                //{
                //    this.db.AddInParameter(com, "Quantity", DbType.Int32, this.Quantity);
                //}
                //else
                //{
                //    this.db.AddInParameter(com, "Quantity", DbType.Int32, DBNull.Value);
                //}
                //if (!string.IsNullOrEmpty(this.Notes))
                //{
                //    this.db.AddInParameter(com, "Notes", DbType.String, this.Notes);
                //}
                //else
                //{
                //    this.db.AddInParameter(com, "Notes", DbType.String, DBNull.Value);
                //}

                //this.db.AddInParameter(com, "IsActive", DbType.Int32, this.IsActive);
                //if (this.CreatedBy > 0)
                //{
                //    this.db.AddInParameter(com, "CreatedBy", DbType.Int32, this.CreatedBy);
                //}
                //else
                //{
                //    this.db.AddInParameter(com, "CreatedBy", DbType.Int32, DBNull.Value);
                //}
                //if (this.CreatedOn > DateTime.MinValue)
                //{
                //    this.db.AddInParameter(com, "CreatedOn", DbType.DateTime, this.CreatedOn);
                //}
                //else
                //{
                //    this.db.AddInParameter(com, "CreatedOn", DbType.DateTime, DBNull.Value);
                //}
                //if (this.ModifiedBy > 0)
                //{
                //    this.db.AddInParameter(com, "ModifiedBy", DbType.Int32, this.ModifiedBy);
                //}
                //else
                //{
                //    this.db.AddInParameter(com, "ModifiedBy", DbType.Int32, DBNull.Value);
                //}
                //if (this.ModifiedOn > DateTime.MinValue)
                //{
                //    this.db.AddInParameter(com, "ModifiedOn", DbType.DateTime, this.ModifiedOn);
                //}
                //else
                //{
                //    this.db.AddInParameter(com, "ModifiedOn", DbType.DateTime, DBNull.Value);
                //}

                this.db.ExecuteNonQuery(com);
                //this.OrderDetailID = Convert.ToInt32(this.db.GetParameterValue(com, "OrderDetailID"));
            }
            catch (Exception ex)
            {

                return false;
            }
            return this.OrderDetailID > 0;
        }
        private bool Update()
        {
            try
            {
                DbCommand com = this.db.GetStoredProcCommand("OrderDetailsUpdate");
                this.db.AddOutParameter(com, "OrderDetailID", DbType.Int32, 1024);
                if (this.OrderId > 0)
                {
                    this.db.AddInParameter(com, "OrderId", DbType.Int32, this.OrderId);
                }
                else
                {
                    this.db.AddInParameter(com, "OrderId", DbType.Int32, DBNull.Value);
                }
                if (this.ProductId > 0)
                {
                    this.db.AddInParameter(com, "ProductId", DbType.Int32, this.ProductId);
                }
                else
                {
                    this.db.AddInParameter(com, "ProductId", DbType.Int32, DBNull.Value);
                }
                if (this.Price > 0)
                {
                    this.db.AddInParameter(com, "Price", DbType.Double, this.Price);
                }
                else
                {
                    this.db.AddInParameter(com, "Price", DbType.Double, DBNull.Value);
                }
                if (this.Quantity > 0)
                {
                    this.db.AddInParameter(com, "Quantity", DbType.Int32, this.Quantity);
                }
                else
                {
                    this.db.AddInParameter(com, "Quantity", DbType.Int32, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(this.Notes))
                {
                    this.db.AddInParameter(com, "Notes", DbType.String, this.Notes);
                }
                else
                {
                    this.db.AddInParameter(com, "Notes", DbType.String, DBNull.Value);
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
                DbCommand com = this.db.GetStoredProcCommand("OrdersDetailsDelete");
                this.db.AddInParameter(com, "OrderDetailsID", DbType.Int32, this.OrderDetailID);
                this.db.ExecuteNonQuery(com);
            }
            catch (Exception ex)
            {
                // To Do: Handle Exception
                return false;
            }

            return true;
        }

        public List<OrderDetails> GetList()
        {
            DataSet ds = null;
            try
            {

                DbCommand com = db.GetStoredProcCommand("OrdersDetailsGetList");

                this.db.AddOutParameter(com, "OrderDetailID", DbType.Int32, 1024);
                if (this.OrderId > 0)
                {
                    this.db.AddInParameter(com, "OrderId", DbType.Int32, this.OrderId);
                }
                else
                {
                    this.db.AddInParameter(com, "OrderId", DbType.Int32, DBNull.Value);
                }
                if (this.ProductId > 0)
                {
                    this.db.AddInParameter(com, "ProductId", DbType.Int32, this.ProductId);
                }
                else
                {
                    this.db.AddInParameter(com, "ProductId", DbType.Int32, DBNull.Value);
                }
                if (this.Price > 0)
                {
                    this.db.AddInParameter(com, "Price", DbType.Double, this.Price);
                }
                else
                {
                    this.db.AddInParameter(com, "Price", DbType.Double, DBNull.Value);
                }
                if (this.Quantity > 0)
                {
                    this.db.AddInParameter(com, "Quantity", DbType.Int32, this.Quantity);
                }
                else
                {
                    this.db.AddInParameter(com, "Quantity", DbType.Int32, DBNull.Value);
                }
                if (!string.IsNullOrEmpty(this.Notes))
                {
                    this.db.AddInParameter(com, "Notes", DbType.String, this.Notes);
                }
                else
                {
                    this.db.AddInParameter(com, "Notes", DbType.String, DBNull.Value);
                }
                ds = db.ExecuteDataSet(com);
                List<OrderDetails> bookList = new List<OrderDetails>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {

                    bookList.Add(new OrderDetails
                    {
                        OrderDetailID = Convert.ToInt32(row["OrderDetailID"]),
                        OrderId = Convert.ToInt32(row["OrderId"]),
                        ProductId = Convert.ToInt32(row["ProductId"]),
                        Price = Convert.ToDouble(row["Price"]),
                        Quantity = Convert.ToInt32(row["Quantity"]),
                        Notes = Convert.ToString(row["Notes"])                     
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


        public List<OrderDetailsVM> LoadForEdit()
        {
            try
            {
                //if (this.OrderDetailID != 0)
                if (this.OrderDetailID != 0)
                {
                    DbCommand com = this.db.GetStoredProcCommand("OrdersDetailsDetailsForEdit");
                    //this.db.AddInParameter(com, "OrderDetailID", System.Data.DbType.Int32, this.OrderDetailID);
                    this.db.AddInParameter(com, "OrderDetailID", System.Data.DbType.Int32, this.OrderDetailID);
                    DataSet ds = this.db.ExecuteDataSet(com);
                    List<OrderDetailsVM> OrderDetailsList = new List<OrderDetailsVM>();
                    foreach (DataRow Row in ds.Tables[0].Rows)
                    {
                        OrderDetailsList.Add(new OrderDetailsVM
                        {
                            OrderDetailID = Convert.ToInt32(Row["OrderDetailID"]),
                            OrderId = Convert.ToInt32(Row["OrderId"]),
                            ProductId = Convert.ToInt32(Row["ProductId"]),
                            Price = Convert.ToDouble(Row["Price"]),
                            Quantity = Convert.ToInt32(Row["Quantity"]),
                            Notes = Convert.ToString(Row["Notes"]),
                            TotalPrice = Convert.ToDouble(Row["TotalPrice"]),
                            ProductName = Convert.ToString(Row["ProductName"])
                        });

                    }
                    return OrderDetailsList;
                }

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);

            }
            return null;
        }


    }
    #endregion

    #endregion
}