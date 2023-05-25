using Assesment1.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assesment1.Models
{
    public class OrderDetailsVM
    {
       
        public int OrderDetailID { get; set; }
        public int OrderId { get; set; }
        public DateTime OrderDateFrom { get; set; }
        public DateTime OrderDateTo { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int Quantity { get; set; }
        public string Notes { get; set; }
        public int CustomerID { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }
        public List<OrderDetailsVM> OrderDetailsList { get; set; }
        public string ProductName { get; set; }
        public double TotalPrice { get; set; }
        public string DeliveryDateStr { get; set; }
        public string OrderDateFromStr { get; set; }
    }
    public class ProductCategoriesVM
    {
        public string CategoryName { get; set; }
        public int ProductCategoryID { get; set; }
        public string Description { get; set; }
    }

    public class CustomersVM
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }

    }

    public class ProductsVM
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int ProductCategoryID { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public List<ProductCategories> ProductCategoryList { get; set; }
        public List<Products> productList { get; set; }
        public decimal PageNumber { get; set; }
        public decimal PageSize { get; set; }
        public decimal TotalCount { get; set; }
    }

    public class OrdersVM
    {
        public int OrderId { get; set; }
        public DateTime OrderDateFrom { get; set; }
        public DateTime OrderDateTo { get; set; }
        public int CustomerId { get; set; }
        public string OrderNotes { get; set; }
        public string OrderRerenceNo { get; set; }
        public string ContactPerson { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string DeliveryNotes { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public List<Orders> OrderGetList { get; set; }
        public int ProductId { get; set; }
        public int ProductCategoryID { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public List<ProductCategories> ProductCategoryList { get; set; }
        public decimal TotalCount { get; set; }
        public int? PageNumber { get; set; }
        public decimal PageSize { get; set; }
        public string DeliveryDateStr { get; set; }
        public string OrderDateFromStr { get; set; }
    }
}