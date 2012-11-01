using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Data.Repository.Interfaces;
using Core.Data.Entities;
using Core.Data.Repository;
using TolokaStudio.Models;
using System.IO;
using TolokaStudio.Common;
using System.Web.Helpers;

namespace TolokaStudio.Controllers
{

    public class EmployeeController : Controller
    {
        #region readonly and const
        private readonly IRepository<Employee> EmployeeRepository;
        private readonly IRepository<User> UserRepository;
        private readonly IRepository<Product> ProductsRepository;
        private readonly IRepository<Order> OrdersRepository;
        private readonly IRepository<Store> StoreRepository;
        private const string DefaulAuthorCabinet = "\\Employee\\Cabinet";
        private const string DefaulImg = "/Content/img/_C3D9074.png";
        private const string DefaulDetailImg = "/Content/img/imgFull/Fluor/Coffe.png";
        private const string DefaulImgUnpublish = "/Content/img/q/unpublish.png";
        private const string DefaulImgAddProduct = "/Content/img/q/add.png";
        private const string DefaulImgPublish = "/Content/img/q/publish.png";
        private const string DefaulImgEditDetails = "/Content/img/q/edit.png";
        private const string _rootImagesFolderPath = "~/Content/img/Employee";

        private const string _authorTemplate = "<div class='template'>" +
                    " <div class='span8'>" +
                    " <a href='/Employee/Details?id={0}'>" +
                    " <div class='box_main_item'>" +
                    " <div class='box_main_item_img'>" +
                    "  <div class='box_main_item_img_bg'>" +
                    "     <span>Про автора</span>" +
                    "  </div>" +
                    " <img src='{1}' alt='img_box' />" +
                    " </div>" +
                    " <div class='box_main_item_text'>" +
                    "   <h3>" +
                    "       {2}</h3>" +
                    "     <span>{3}</span>" +
                    "  </div>" +
                    " </div>" +
                    " </a>" +
                    "</div>" +
                    " </div>";

        private const string _authorEdit = "<div class='template'>" +
                   " <div class='span8'>" +
                       " <div class='box_main_item'>" +
            " <a href='/Employee/Publish?id={0}'  title='Не публікувати'>" +
               "<img class='publishBtn' src='" + DefaulImgPublish + "' alt='{0}'  title='Публікавати'/>" +
                " </a>" +
                            " <a href='/Product/Create?employeeId={0}' >" +
           " <img class='addProduct'  title='Додати продукт {2}' alt='{0}'   src='" + DefaulImgAddProduct + "' />" +
                " </a>" +
                                          " <a href='/Employee/EditDetails?id={0}' >" +
           " <img class='addProduct'  title='Редагувати сторінку {2}' alt='{0}'   src='" + DefaulImgEditDetails + "' />" +
                " </a>" +
               "</div>" +
                   " <a href='/Employee/EditDetails?id={0}' title='Редагувати Сторінку {2}'>" +
                   " <div class='box_main_item'>" +
                   " <div class='box_main_item_img'>" +
                   "  <div class='box_main_item_img_bg'>" +
                   "     <span>Редагувати Сторінку</span>" +
                   "  </div>" +
                   " <img src='{1}' alt='img_box' />" +
                   " </div>" +
                   " <div class='box_main_item_text'>" +
                    "   <h3 class='name'>" +
                   "       {2}</h3>" +
                    "     <span class='email'>{3}</span>" +
                   "  </div>" +
                   " </div>" +
                   " </a>" +
                   "</div>" +
                   " </div>";

        private const string _authorEditPublished = "<div class='template'>" +
                   " <div class='span8'>" +
                       " <div class='box_main_item'>" +

            " <a href='/Employee/Unpublish?id={0}'  title='Не публікувати{2}'>" +
             "<img class='unpublishBtn' src='" + DefaulImgUnpublish + "' alt='{0}'/>" +
                " </a>" +
                            " <a href='/Product/Create?employeeId={0}' >" +
           " <img class='addProduct'  title='Додати продукт {2}' alt='{0}'   src='" + DefaulImgAddProduct + "' />" +
                " </a>" +
                                          " <a href='/Employee/EditDetails?id={0}' >" +
           " <img class='addProduct'  title='Редагувати сторінку {2}' alt='{0}'   src='" + DefaulImgEditDetails + "' />" +
                " </a>" +
               "</div>" +
                   " <a href='/Employee/EditDetails?id={0}' title='Редагувати Сторінку {2}'>" +
                   " <div class='box_main_item'>" +
                   " <div class='box_main_item_img'>" +
                   "  <div class='box_main_item_img_bg'>" +
                   "     <span>Редагувати Сторінку</span>" +
                   "  </div>" +
                   " <img src='{1}' alt='img_box' />" +
                   " </div>" +
                   " <div class='box_main_item_text'>" +
                    "   <h3 class='name'>" +
                   "       {2}</h3>" +
                    "     <span class='email'>{3}</span>" +
                   "  </div>" +
                   " </div>" +
                   " </a>" +
                   "</div>" +
                   " </div>";
        #endregion
   
        #region EmployeeController
        public EmployeeController()
        {
            EmployeeRepository = new Repository<Employee>();
            UserRepository = new Repository<User>();
            ProductsRepository = new Repository<Product>();
            OrdersRepository = new Repository<Order>();
            StoreRepository = new Repository<Store>();
        }
        #endregion

        #region Index Details Cabinet
        public ActionResult Index()
        {
            IList<Employee> employees = EmployeeRepository.GetAll().ToList();
            return View(employees);
        }

        public ActionResult Details(int id)
        {
            Employee employee = EmployeeRepository.Get(s => s.Id.Equals(id)).SingleOrDefault();

            return View(employee);
        }

        public ActionResult Cabinet()
        {
            User user = UserRepository.Get(u => u.UserName == User.Identity.Name).SingleOrDefault();
            Employee IsAuthor = EmployeeRepository.Get(u => u.Id == user.Employee.Id).SingleOrDefault();
            if (user != null && IsAuthor != null && user.Role.IsAdmin || user.Role.IsAuthor)
            {
                EmployeeEditModel model = EmployeeToEditModel(IsAuthor);
                return View("Edit", model);
            }

            return null;
        }

        #endregion

        #region Create Edit
        public ActionResult Create(int userId)
        {
            User user = UserRepository.Get(u => u.UserName == User.Identity.Name).SingleOrDefault();
            User userauthor = UserRepository.Get(u => u.Id == userId).SingleOrDefault();
            if (user != null && user.Role.IsAdmin && userauthor != null)
            {


                userauthor.Role.IsAuthor = true;
                Employee employee = new Employee();
                employee.Email = user.Email;
                employee.FirstName = "Ім'я";
                employee.LastName = "Прізвище";
                employee.ImagePath = DefaulImg;
                Employee employeeSaved = EmployeeRepository.SaveOrUpdate(employee);
                employee.HtmlBannerEdit = Server.HtmlEncode(string.Format(_authorEdit, employeeSaved.Id, DefaulImg, employee.FirstName, employee.LastName, employee.Email));
                employee.HtmlBanner = Server.HtmlEncode(string.Format(_authorTemplate, employeeSaved.Id, DefaulImg, employee.FirstName, employee.LastName, user.Email));

                userauthor.Employee = employeeSaved;
                UserRepository.SaveOrUpdate(userauthor);

                return RedirectToAction("Edit", "Employee", new { id = employeeSaved.Id });

            }

            return null;
        }

        public ActionResult Edit(int id)
        {
            User user = UserRepository.Get(u => u.UserName == User.Identity.Name).SingleOrDefault();
            Employee IsAuthor = EmployeeRepository.Get(u => u.Id == id).SingleOrDefault();
            if (user != null && IsAuthor != null && user.Role.IsAdmin || user.Role.IsAuthor)
            {
                EmployeeEditModel model = EmployeeToEditModel(IsAuthor);
                return View(model);
            }

            return null;
        }

        [HttpPost]
        public ActionResult Edit(EmployeeEditModel model)
        {
            User user = UserRepository.Get(u => u.UserName == User.Identity.Name).SingleOrDefault();
            if (user != null && user.Role.IsAdmin || user.Role.IsAuthor)
            {
                SaveImage(model);
                Employee employee = EditModelToEmployee(model);
                if (ModelState.IsValid)
                {
                    EmployeeRepository.SaveOrUpdate(employee);
                    return RedirectToAction("Edit", "Employee", new { id = model.Id });
                }
                else
                {
                    model = EmployeeToEditModel(employee);
                    return View(model);
                }


            }

            return null;

        }

        private void SaveImage(EmployeeEditModel model)
        {
            var image = WebImage.GetImageFromRequest();
            if (image != null)
            {
                if (image.Width > 310)
                {
                    image.Resize(310, ((310 * image.Height) / image.Width));
                }

                var filename = Path.GetFileName(image.FileName);
                image.Save(Path.Combine(_rootImagesFolderPath, filename));
                filename = Path.Combine(_rootImagesFolderPath, filename);

                model.ImagePath = Url.Content(filename);

            }
        }
        #endregion

        #region EditDetails
        public ActionResult EditDetails(int id)
        {
            User user = UserRepository.Get(u => u.UserName == User.Identity.Name).SingleOrDefault();
            if (user != null && user.Role.IsAdmin || user.Role.IsAuthor)
            {
                string html = HttpUtility.HtmlDecode(EmployeeRepository.Get(s => s.Id == id).SingleOrDefault().HtmlDetail);
                DetailsModel model = new DetailsModel();
                model.Id = id;
                model.HtmlDetail = html != null ? html : "";
                return View(model);
            }

            return null;
        }

        [HttpPost]
        public ActionResult EditDetails(DetailsModel model)
        {
            User user = UserRepository.Get(u => u.UserName == User.Identity.Name).SingleOrDefault();
            if (user != null && user.Role.IsAdmin || user.Role.IsAuthor)
            {

                try
                {
                    Employee employee = EmployeeRepository.Get(s => s.Id.Equals(model.Id)).SingleOrDefault();
                    employee.HtmlDetail = Server.HtmlEncode(model.HtmlDetail);
                    EmployeeRepository.SaveOrUpdate(employee);

                    return RedirectToAction("Edit", "Employee", new { id = model.Id });
                }
                catch
                {
                    return View(model);
                }
            }

            return null;

        }
        #endregion

        #region Delete
        public ActionResult Delete(int Id)
        {
            User user = UserRepository.Get(u => u.UserName == User.Identity.Name).SingleOrDefault();
            if (user != null && user.Role.IsAdmin || user.Role.IsAuthor)
            {
                if (!OrdersRepository.Get(p => p.Product.Employee.Id == Id).Any())
                {
                    List<Product> products = ProductsRepository.Get(p => p.Employee.Id == Id).ToList();

                    foreach (var item in products)
                    {
                        Store store = StoreRepository.Get(p => p.Products.Contains(item)).SingleOrDefault();
                        store.DeleteProduct(item);
                        ProductsRepository.Delete(item);

                    }
                }
                User userAuthor = UserRepository.Get(u => u.Employee.Id == Id).SingleOrDefault();
                if (userAuthor != null)
                {
                    userAuthor.Employee = null;
                    UserRepository.SaveOrUpdate(userAuthor);
                }

                Employee employee = EmployeeRepository.Delete(EmployeeRepository.Get(s => s.Id == Id).SingleOrDefault());
            }

            return RedirectToAction("Index", "Store");
        }
        #endregion

        #region Publish
        public ActionResult Unpublish(int Id)
        {
            User user = UserRepository.Get(u => u.UserName == User.Identity.Name).SingleOrDefault();
            if (user != null && user.Role.IsAdmin || user.Role.IsAuthor)
            {
                Employee employee = EmployeeRepository.Get(s => s.Id == Id).SingleOrDefault();
                employee.IsPublished = false;
                employee.HtmlBannerEdit = Server.HtmlEncode(string.Format(_authorEdit, employee.Id, employee.ImagePath, employee.FirstName + " " + employee.LastName, employee.Email));
                EmployeeRepository.SaveOrUpdate(employee);
                return RedirectToAction("Edit", "Employee", new { id = Id });
            }

            return RedirectToAction("Index");
        }
        public ActionResult Publish(int Id)
        {
            User user = UserRepository.Get(u => u.UserName == User.Identity.Name).SingleOrDefault();
            if (user != null && user.Role.IsAdmin || user.Role.IsAuthor)
            {
                Employee employee = EmployeeRepository.Get(s => s.Id == Id).SingleOrDefault();
                employee.IsPublished = true;
                employee.HtmlBannerEdit = Server.HtmlEncode(string.Format(_authorEditPublished, employee.Id, employee.ImagePath, employee.FirstName + " " + employee.LastName, employee.Email));
                EmployeeRepository.SaveOrUpdate(employee);
                return RedirectToAction("Edit", "Employee", new { id = Id });
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region private
        private EmployeeEditModel EmployeeToEditModel(Employee employee)
        {
            EmployeeEditModel employeeEditModel = new EmployeeEditModel();
            employeeEditModel.ImagePath = employee.ImagePath;
            employeeEditModel.FirstName = employee.FirstName;
            employeeEditModel.LastName = employee.LastName;
            employeeEditModel.HtmlBanner = HttpUtility.HtmlDecode(employee.HtmlBanner);
            employeeEditModel.HtmlDetail = HttpUtility.HtmlDecode(employee.HtmlDetail);
            employeeEditModel.HtmlBannerEdit = HttpUtility.HtmlDecode(employee.HtmlBannerEdit);
            employeeEditModel.Email = employee.Email;
            employeeEditModel.Id = employee.Id;
            employeeEditModel.Products = ProductsRepository.Get(p => p.Employee.Id == employee.Id).ToList();


            return employeeEditModel;
        }

        private Employee EditModelToEmployee(EmployeeEditModel employeeEditModel)
        {
            Employee employee = EmployeeRepository.Get(s => s.Id.Equals(employeeEditModel.Id)).SingleOrDefault();
            employee.FirstName = employeeEditModel.FirstName;
            employee.LastName = employeeEditModel.LastName;
            employee.ImagePath = employeeEditModel.ImagePath;
            employee.Email = employeeEditModel.Email;
            if (employee.IsPublished)
            {
                employee.HtmlBannerEdit = Server.HtmlEncode(string.Format(_authorEditPublished, employeeEditModel.Id, employeeEditModel.ImagePath, employeeEditModel.FirstName + " " + employeeEditModel.LastName, employee.Email));
            }
            else
            {
                employee.HtmlBannerEdit = Server.HtmlEncode(string.Format(_authorEdit, employeeEditModel.Id, employeeEditModel.ImagePath, employeeEditModel.FirstName + " " + employeeEditModel.LastName, employee.Email));
            }

            employee.HtmlBanner = Server.HtmlEncode(string.Format(_authorTemplate, employeeEditModel.Id, employeeEditModel.ImagePath, employeeEditModel.FirstName + " " + employeeEditModel.LastName, employee.Email));
            employee.HtmlDetail = Server.HtmlEncode(employeeEditModel.HtmlDetail);
            return employee;
        }
        #endregion
    }
}
