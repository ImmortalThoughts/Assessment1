using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assesment1.DAL;
using Assesment1.Models;
using System.Xml.Serialization;
using System.IO;

namespace Assesment1.Controllers
{
    public class OrdersController : Controller
    {
        // GET: Orders
        public ActionResult Index()
        {
            try
            {
                OrdersVM orders = new OrdersVM();
                orders.PageNumber = 1;
                if (Session["BookSession"] != null)
                {
                    orders = (OrdersVM)Session["BookSession"];
                }
                ProductCategories productcategory = new ProductCategories();
                orders.ProductCategoryList = productcategory.GetList();
                return View(orders);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        //Created to getlist of orders data
        public ActionResult Index1(OrdersVM obj)
        {
            try
            {
                Orders order = new Orders();
                OrdersVM orderVModel = new OrdersVM();
                Session["BookSession"] = obj;
                order.Firstname = obj.Firstname;
                order.Lastname = obj.Lastname;
                order.OrderDateFrom = obj.OrderDateFrom;
                order.OrderDateTo = obj.OrderDateTo;
                order.PageNumber = obj.PageNumber;
                order.PageSize = obj.PageSize;
                order.ProductName = obj.ProductName;
                order.ProductCategoryID = obj.ProductCategoryID;
                orderVModel.OrderGetList = order.GetList();
                orderVModel.TotalCount = Math.Ceiling((Convert.ToDecimal(order.TotalCount) / (obj.PageSize)));
                return Json(orderVModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        //created to Add New Customer Records
        public ActionResult AddCustomer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCustomers(CustomersVM form_data)
        {
            try
            {
                Customers cust = new Customers();
                cust.Firstname = form_data.Firstname;
                cust.Lastname = form_data.Lastname;
                cust.PhoneNo = form_data.PhoneNo;
                cust.Postcode = form_data.Postcode;
                cust.Address = form_data.Address;
                cust.City = form_data.City;
                cust.Save();
                return Json("");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        //created for showing orders details from OrderDetails Table
      
        public ActionResult OrderDetails(int? OrderID)
        {
            try
            {
                OrderDetailsVM orderdetailsVM = new OrderDetailsVM();
                OrderDetails orderdetails = new DAL.OrderDetails();
                //OrdersVM ordersVM = new OrdersVM();
                Orders orders = new Orders();
                orders.OrderId = OrderID ?? 0;
                bool OrderLoad = orders.Load();
                orderdetails.OrderId = OrderID ?? 0;
                orderdetailsVM.Firstname = orders.Firstname;
                orderdetailsVM.Lastname = orders.Lastname;
                DateTime OrderDateFrom = Convert.ToDateTime(orders.OrderDateFromStr);
                DateTime DeliveryDate = Convert.ToDateTime(orders.DeliveryDateStr);
                orderdetailsVM.OrderDateFromStr = OrderDateFrom.ToString("yyyy-MM-dd HH:mm:ss");
                orderdetailsVM.DeliveryDateStr = DeliveryDate.ToString("yyyy-MM-dd HH:mm:ss"); 
                orderdetailsVM.CustomerID = orders.CustomerId;
                return View(orderdetailsVM);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        [HttpPost]
        public ActionResult OrderDetailsData(int? OrderID)
        {
            try
            {
                OrderDetailsVM orderdetailsVM = new OrderDetailsVM();
                OrderDetails orderdetails = new DAL.OrderDetails();
                orderdetails.OrderId = (int)OrderID;
                orderdetailsVM.OrderDetailsList = orderdetails.Load();
                return Json(orderdetailsVM, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        //Created for Adding Product Item

        public ActionResult _AddProduct()
        {
            try
            {
                Products products = new Products();
                ProductsVM productsList = new ProductsVM();
                ProductCategories productcategory = new ProductCategories();
                productsList.ProductCategoryList = productcategory.GetList();

                return View(productsList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        //created for adding data into table inside _AddProduct
        public ActionResult AddProduct(ProductsVM obj)
        {
            try
            {
                Products products = new Products();
                ProductsVM productsList = new ProductsVM();
                products.ProductCategoryID = obj.ProductCategoryID;
                products.ProductName = obj.ProductName;
                products.PageSize = obj.PageSize;
                products.PageNumber = obj.PageNumber;
                productsList.productList = products.GetList();
                productsList.PageSize = obj.PageSize;
                productsList.PageNumber = obj.PageNumber;
                productsList.TotalCount = Math.Ceiling((Convert.ToDecimal(products.TotalCount) / (obj.PageSize)));
                return Json(productsList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        //Created for Editing(Updation) operation in OrderDetails View
        public ActionResult _Edit(string OrderDetailsID)
        {
            try
            {
                OrderDetailsVM orderdetailsVM = new OrderDetailsVM();
                OrderDetails orderdetails = new DAL.OrderDetails();
                orderdetails.OrderDetailID = Convert.ToInt32(OrderDetailsID);
                //orderdetails.OrderDetailID = OrderDetailsID ?? 0;
                orderdetailsVM.OrderDetailsList = orderdetails.LoadForEdit();
                foreach (var item in orderdetailsVM.OrderDetailsList)
                {
                    orderdetailsVM.OrderDetailID = item.OrderDetailID;
                    orderdetailsVM.ProductId = item.ProductId;
                    orderdetailsVM.ProductName = item.ProductName;
                    orderdetailsVM.Quantity = item.Quantity;
                    orderdetailsVM.TotalPrice = item.TotalPrice;
                    orderdetailsVM.Price = item.Price;
                }
                return View(orderdetailsVM);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public ActionResult Edit(OrderDetailsVM obj)
        {
            try
            {
                OrderDetails orderdetails = new DAL.OrderDetails();
                OrderDetailsVM orderdetailsVM = new OrderDetailsVM();
                orderdetailsVM.OrderDetailID = obj.OrderDetailID;
                orderdetailsVM.ProductId = obj.ProductId;
                orderdetailsVM.ProductName = obj.ProductName;
                orderdetailsVM.Quantity = obj.Quantity;
                orderdetailsVM.TotalPrice = obj.TotalPrice;
                orderdetailsVM.Price = obj.Price;

                //orderdetails.OrderDetailID = Convert.ToInt32(OrderDetailsID);
                //orderdetails.OrderDetailID = OrderDetailsID ?? 0;
                //orderdetailsVM.OrderDetailsList = orderdetails.LoadForEdit();
                return Json(orderdetailsVM);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        //Created For adding Updated records through xml file in database
        
        public void EditOperationXML(OrderDetailsVM obj, List<OrderDetailsVM> Details/*,BooksVM[] deleteBookIssueDetailsID*/)
        {
            OrderDetails orderDetails = new OrderDetails();
            Orders orders = new Orders();
            OrderDetailsVM orderDetailsVM = new OrderDetailsVM();
            try
            {
                orders.OrderId = obj.OrderId;
                orders.CustomerId = obj.CustomerID;
                orders.OrderDateFrom = Convert.ToDateTime(obj.OrderDateFromStr);
                orders.DeliveryDate= Convert.ToDateTime(obj.DeliveryDateStr);
                orders.Save();

                XmlSerializer inst = new XmlSerializer(Details.GetType());
                StringWriter writer = new StringWriter();
                inst.Serialize(writer, Details);
                string strXML = writer.ToString();
                orderDetails.IssueXML = strXML;
                orderDetails.OrderId = orders.OrderId;
                orderDetails.Save();

                writer.Close();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
         
        }
    }
}