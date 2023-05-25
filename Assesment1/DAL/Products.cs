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
    public class Products
    {
        private Database db;
        #region variable Declaration

        #region Constructors

        public Products()
        {
            this.db = DatabaseFactory.CreateDatabase();
        }
        public Products(int BookID)
        {
            this.db = DatabaseFactory.CreateDatabase();
            this.ProductID = ProductID;
        }
        #endregion
        #region Properties
        [Display(Name = "Book Name")]
        public int ProductID
        {
            get; set;
        }
        public decimal TotalCount { get; set; }
        public string ProductName
        {
            get; set;
        }
        public string Description { get; set; }
        public int ProductCategoryID { get; set; }
        public double Price { get; set; }

        public bool IsActive
        {
            get; set;
        }
        public decimal PageNumber { get; set; }
        public decimal PageSize { get; set; }
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
                if (this.ProductID != 0)
                {
                    DbCommand com = this.db.GetStoredProcCommand("ProductsDetails");
                    this.db.AddInParameter(com, "ProductID", System.Data.DbType.Int32, this.ProductID);
                    DataSet ds = this.db.ExecuteDataSet(com);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        this.ProductID = Convert.ToInt32(dt.Rows[0]["ProductID"]);
                        this.ProductName = Convert.ToString(dt.Rows[0]["ProductName"]);
                        this.Description=dt.Rows[0]["Description"].ToString();
                        this.ProductCategoryID = Convert.ToInt32(dt.Rows[0]["ProductCategoryID"]);
                        this.Price = Convert.ToDouble(dt.Rows[0]["Price"]);
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
            if (this.ProductID == 0)
            {
                return this.Insert();
            }
            else
            {
                if (this.ProductID > 0)
                {
                    return this.Update();
                }
                else
                {
                    this.ProductID = 0;
                    return false;
                }
            }
        }
        public bool Insert()
        {
            try
            {
                DbCommand com = this.db.GetStoredProcCommand("ProductsInsert");
                this.db.AddOutParameter(com, "ProductID", DbType.Int32, 1024);
                if (!String.IsNullOrEmpty(this.ProductName))
                {
                    this.db.AddInParameter(com, "ProductName", DbType.String, this.ProductName);
                }
                else
                {
                    this.db.AddInParameter(com, "ProductName", DbType.String, DBNull.Value);
                }
                if (!String.IsNullOrEmpty(this.Description))
                {
                    this.db.AddInParameter(com, "Description", DbType.String, this.Description);
                }
                else
                {
                    this.db.AddInParameter(com, "Description", DbType.String, DBNull.Value);
                }
                if (this.ProductCategoryID>0)
                {
                    this.db.AddInParameter(com, "ProductCategoryID", DbType.Int32, this.ProductCategoryID);
                }
                else
                {
                    this.db.AddInParameter(com, "ProductCategoryID", DbType.Int32, DBNull.Value);
                }
                if (this.Price>0)
                {
                    this.db.AddInParameter(com, "Price", DbType.Double, this.Price);
                }
                else
                {
                    this.db.AddInParameter(com, "Price", DbType.Double, DBNull.Value);
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
                this.ProductID = Convert.ToInt32(this.db.GetParameterValue(com, "ProductID"));
            }
            catch (Exception)
            {

                return false;
            }
            return this.ProductID > 0;
        }
        private bool Update()
        {
            try
            {
                DbCommand com = this.db.GetStoredProcCommand("ProductsUpdate");
                this.db.AddInParameter(com, "ProductID", DbType.Int32, this.ProductID);
                if (!String.IsNullOrEmpty(this.ProductName))
                {
                    this.db.AddInParameter(com, "ProductName", DbType.String, this.ProductName);
                }
                else
                {
                    this.db.AddInParameter(com, "ProductName", DbType.String, DBNull.Value);
                }
                if (!String.IsNullOrEmpty(this.Description))
                {
                    this.db.AddInParameter(com, "Description", DbType.String, this.Description);
                }
                else
                {
                    this.db.AddInParameter(com, "Description", DbType.String, DBNull.Value);
                }
                if (this.ProductCategoryID > 0)
                {
                    this.db.AddInParameter(com, "ProductCategoryID", DbType.Int32, this.ProductCategoryID);
                }
                else
                {
                    this.db.AddInParameter(com, "ProductCategoryID", DbType.Int32, DBNull.Value);
                }
                if (this.Price > 0)
                {
                    this.db.AddInParameter(com, "Price", DbType.Double, this.Price);
                }
                else
                {
                    this.db.AddInParameter(com, "Price", DbType.Double, DBNull.Value);
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
                DbCommand com = this.db.GetStoredProcCommand("ProductsDelete");
                this.db.AddInParameter(com, "ProductID", DbType.Int32, this.ProductID);
                this.db.ExecuteNonQuery(com);
            }
            catch (Exception ex)
            {
                // To Do: Handle Exception
                return false;
            }

            return true;
        }

        public List<Products> GetList()
        {
            DataSet ds = null;
            try
            {
               
                DbCommand com = db.GetStoredProcCommand("ProductsGetList");
                this.db.AddOutParameter(com, "TotalCount", DbType.Int32, 1024);
                if (!string.IsNullOrEmpty(this.ProductName))
                {
                    this.db.AddInParameter(com, "ProductName", DbType.String, this.ProductName);
                }
                else
                {
                    this.db.AddInParameter(com, "ProductName", DbType.String, DBNull.Value);
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
                if (this.ProductCategoryID > 0)
                {
                    this.db.AddInParameter(com, "ProductCategoryID", DbType.Int32, this.ProductCategoryID);
                }
                else
                {
                    this.db.AddInParameter(com, "ProductCategoryID", DbType.Int32, DBNull.Value);
                }
                //if (this.ProductID>0)
                //{
                //    this.db.AddInParameter(com, "ProductID", DbType.Int32, this.ProductID);
                //}
                //else
                //{
                //    this.db.AddInParameter(com, "ProductID", DbType.Int32, DBNull.Value);
                //}




                //if (!string.IsNullOrEmpty(this.Description))
                //{
                //    this.db.AddInParameter(com, "Description", DbType.String, this.Description);
                //}
                //else
                //{
                //    this.db.AddInParameter(com, "Description", DbType.String, DBNull.Value);
                //}
                ////this.db.AddInParameter(com, "authorID", DbType.Int32, this.AuthorId);

                //if (this.Price > 0)
                //{
                //    this.db.AddInParameter(com, "Price", DbType.Int32, this.Price);
                //}
                //else
                //{
                //    this.db.AddInParameter(com, "Price", DbType.Int32, DBNull.Value);
                //}
                ds = db.ExecuteDataSet(com);
                this.TotalCount = Convert.ToInt32(this.db.GetParameterValue(com, "TotalCount"));
                List<Products> ProductList = new List<Products>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {

                    ProductList.Add(new Products
                    {
                        ProductID = Convert.ToInt32(row["ProductID"]),
                        ProductName = row["ProductName"].ToString(),
                        Description = row["Description"].ToString(),
                        Price = Convert.ToDouble(row["Price"])
                    });
                }
                return ProductList;
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