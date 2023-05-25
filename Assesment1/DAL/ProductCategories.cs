using System;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Linq;
using System.Web;
using System.Data.Common;
using System.Data;

namespace Assesment1.DAL
{
    public class ProductCategories
    {
            #region Basic Functionality

            #region Variable Declaration

            private Database db;
            #endregion

            #region Constructors

            public ProductCategories()
            {
                this.db = DatabaseFactory.CreateDatabase();
            }

            public ProductCategories(int ProductCategoryID)
            {
                this.db = DatabaseFactory.CreateDatabase();
                this.ProductCategoryID = ProductCategoryID;
            }
            #endregion

            #region Properties

            public int ProductCategoryID
        {
                get; set;
            }

            public string CategoryName
        {
                get; set;
            }

            public String Description
        {
                get; set;
            }

            public bool IsActive
            {
                get; set;
            }

            public int CreatedBy
            {
                get; set;
            }

            public DateTime CreatedOn
            {
                get; set;
            }

            public int ModifiedBy
            {
                get; set;
            }

            public DateTime ModifiedOn
            {
                get; set;
            }
            #endregion

            #region Actions

            public bool Load()
            {
                try
                {
                    if (this.ProductCategoryID != 0)
                    {
                        DbCommand com = this.db.GetStoredProcCommand("ProductCategoriesDetails");
                        this.db.AddInParameter(com, "ProductCategoryID", DbType.Int32, this.ProductCategoryID);
                        DataSet ds = this.db.ExecuteDataSet(com);
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            DataTable dt = ds.Tables[0];
                            this.ProductCategoryID = Convert.ToInt32(dt.Rows[0]["ProductCategoryID"]);
                            this.CategoryName = dt.Rows[0]["CategoryName"].ToString();
                            this.Description = dt.Rows[0]["Description"].ToString();
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
                catch (Exception ex)
                {
                    // To Do: Handle Exception
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }

            public bool Save()
            {
                if (this.ProductCategoryID == 0)
                {
                    return this.Insert();
                }
                else
                {
                    if (this.ProductCategoryID > 0)
                    {
                        return this.Update();
                    }
                    else
                    {
                        this.ProductCategoryID = 0;
                        return false;
                    }
                }
            }

            private bool Insert()
            {
                try
                {
                    DbCommand com = this.db.GetStoredProcCommand("ProductCategoriesInsert");
                    this.db.AddOutParameter(com, "ProductCategoryID", DbType.Int32, 1024);
                    //if (this.BookIssueId > 0)
                    //{
                    //    this.db.AddInParameter(com, "BookIssueID", DbType.Int32, this.BookIssueId);
                    //}
                    //else
                    //{
                    //    this.db.AddInParameter(com, "BookIssueID", DbType.Int32, DBNull.Value);
                    //}
                    if (!string.IsNullOrEmpty(this.CategoryName))
                    {
                        this.db.AddInParameter(com, "CategoryName", DbType.Int32, this.CategoryName);
                    }
                    else
                    {
                        this.db.AddInParameter(com, "CategoryName", DbType.Int32, DBNull.Value);
                    }
                    if (!String.IsNullOrEmpty(this.Description))
                    {
                        this.db.AddInParameter(com, "Description", DbType.DateTime, this.Description);
                    }
                    else
                    {
                        this.db.AddInParameter(com, "Description", DbType.DateTime, DBNull.Value);
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
                    this.ProductCategoryID = Convert.ToInt32(this.db.GetParameterValue(com, "ProductCategoryID"));
                }
                catch (Exception ex)
                {
                    // To Do: Handle Exception
                    Console.WriteLine(ex.Message);
                    return false;
                }

                return this.ProductCategoryID > 0; // Return whether ID was returned
            }

            private bool Update()
            {
                try
                {
                    DbCommand com = this.db.GetStoredProcCommand("ProductCategoriesUpdate");
                    //this.db.AddInParameter(com, "BookIssueID", DbType.Int32, this.BookIssueId);
                    if (this.ProductCategoryID > 0)
                    {
                        this.db.AddInParameter(com, "ProductCategoryID", DbType.Int32, this.ProductCategoryID);
                    }
                    else
                    {
                        this.db.AddInParameter(com, "ProductCategoryID", DbType.Int32, DBNull.Value);
                    }
                    if (!string.IsNullOrEmpty(this.CategoryName))
                    {
                        this.db.AddInParameter(com, "CategoryName", DbType.Int32, this.CategoryName);
                    }
                    else
                    {
                        this.db.AddInParameter(com, "CategoryName", DbType.Int32, DBNull.Value);
                    }
                    if (!String.IsNullOrEmpty(this.Description))
                    {
                        this.db.AddInParameter(com, "Description", DbType.DateTime, this.Description);
                    }
                    else
                    {
                        this.db.AddInParameter(com, "Description", DbType.DateTime, DBNull.Value);
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
                    // To Do: Handle Exception
                    Console.WriteLine(ex.Message);
                    return false;
                }

                return true;
            }

            public bool Delete()
            {
                try
                {
                    DbCommand com = this.db.GetStoredProcCommand("ProductCategoriesDelete");
                    this.db.AddInParameter(com, "ProductCategoriesID", DbType.Int32, this.ProductCategoryID);
                    this.db.ExecuteNonQuery(com);
                }
                catch (Exception ex)
                {
                    // To Do: Handle Exception
                    Console.WriteLine(ex.Message);
                    return false;
                }

                return true;
            }

            public List<ProductCategories> GetList()
            {
                DataSet ds = null;
                try
                {
                    DbCommand com = db.GetStoredProcCommand("ProductCategoriesGetList");
                    ds = db.ExecuteDataSet(com);


                    List<ProductCategories> ProductCategoryList = new List<ProductCategories>();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                    ProductCategoryList.Add(new ProductCategories
                        {
                            ProductCategoryID = Convert.ToInt32(row["ProductCategoryID"]),
                            CategoryName=row["CategoryName"].ToString(),
                            Description=row["Description"].ToString()
                        });
                    }
                    return ProductCategoryList;
                }
                catch (Exception ex)
                {
                    //To Do: Handle Exception
                    Console.WriteLine(ex.Message);
                }

                return null;
            }
            #endregion

            #endregion
        }
}