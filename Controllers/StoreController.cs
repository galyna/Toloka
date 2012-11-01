using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TolokaStudio;
using Core.Data.Entities;
using Core.Data.Repository;
using TolokaStudio.Models;
using Core.Data.Repository.Interfaces;
using System.IO;
using TolokaStudio.Common;

namespace TolokaStudio.Controllers
{

    public class StoreController : Controller
    {
        #region
        private readonly IRepository<Store> StoreRepository;
        private readonly IRepository<WebTemplate> WebTemplateRepository;
        private readonly IRepository<Product> ProductsRepository;
        private readonly IRepository<User> UserRepository;
        private readonly IRepository<Employee> EmployeeRepository;
        private const string DefaultImg = "/Content/img/imgThumbs/Fluor/Tiger.png";
        private const string _rootImagesFolderPath = "/Content/img/Store/";
        private const string _storeTemplate = "<div class='template'>" +
                    " <div class='span8'>" +
                    " <a href='/Store/Details?id={0}'>" +
                    " <div class='box_main_item'>" +
                    " <div class='box_main_item_img'>" +
                    "  <div class='box_main_item_img_bg'>" +
                    "     <span>Товари</span>" +
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
        private const string _storeEdit = "<div class='template'>" +
                   " <div class='span8'>" +
                   " <a href='/Store/Edit?id={0}'>" +
                   " <div class='box_main_item'>" +
                   " <div class='box_main_item_img'>" +
                   "  <div class='box_main_item_img_bg'>" +
                   "     <span>Редагувати</span>" +
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
        #endregion
        public StoreController()
        {
            StoreRepository = new Repository<Store>();
            WebTemplateRepository = new Repository<WebTemplate>();
            UserRepository = new Repository<User>();
            ProductsRepository = new Repository<Product>();
            EmployeeRepository = new Repository<Employee>();
        }

        public ActionResult Index()
        {
            User user = UserRepository.Get(u => u.UserName == User.Identity.Name).SingleOrDefault();
            if (user != null && user.Role.IsAdmin)
            {

                StoreIndexModel storeIndexModel = new StoreIndexModel();
                storeIndexModel.Stores = StoreRepository.GetAll().ToList();
                storeIndexModel.Users = UserRepository.GetAll().ToList();
                storeIndexModel.Employees = EmployeeRepository.GetAll().ToList();
                return View(storeIndexModel);
            }

            return null;
        }
        //
        // GET: /Storage/Details/5

        public ActionResult Details(int id)
        {
            Store store = StoreRepository.Get(s => s.Id.Equals(id)).SingleOrDefault();
            return View(store);
        }

        //
        // GET: /Storage/Create

        public ActionResult Create()
        {

            User user = UserRepository.Get(u => u.UserName == User.Identity.Name).SingleOrDefault();
            if (user != null && user.Role.IsAdmin)
            {
                return View(new StoreCreateModel()
                {
                    Name = "Назва розділу",
                    Description = "Опис",
                    ImagePath = DefaultImg,
                    HtmlBanner = string.Format(_storeTemplate, '0', DefaultImg, "Назва розділу", "Опис"),
                });
            }

            return null;

        }

        //
        // POST: /Storage/Create

        [HttpPost]
        public ActionResult Create(StoreCreateModel store)
        {
            User user = UserRepository.Get(u => u.UserName == User.Identity.Name).SingleOrDefault();
            if (ModelState.IsValid && user != null && user.Role.IsAdmin)
            {
                try
                {
                    Store savedStore = StoreRepository.SaveOrUpdate(new Store() { Name = store.Name, Description = store.Description, ImagePath = store.ImagePath });
                    var htmlBanner = string.Format(_storeTemplate, savedStore.Id, store.ImagePath, savedStore.Name, savedStore.Description);
                    savedStore.HtmlBanner = Server.HtmlEncode(htmlBanner);
                    Store savedStoreWithBanner = StoreRepository.SaveOrUpdate(savedStore);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }

        //
        // GET: /Storage/Edit/5

        public ActionResult Edit(int id)
        {
            User user = UserRepository.Get(u => u.UserName == User.Identity.Name).SingleOrDefault();
            if (user != null && user.Role.IsAdmin)
            {
                StoreEditModel model = StoreToEditModel(id);
                return View(model);
            }

            return null;
        }


        //
        // POST: /Storage/Edit/5

        [HttpPost]
        public ActionResult Edit(StoreEditModel model)
        {
            User user = UserRepository.Get(u => u.UserName == User.Identity.Name).SingleOrDefault();
            if (ModelState.IsValid && user != null && user.Role.IsAdmin)
            {

                try
                {
                    Store store = EditModelToStore(model);

                    StoreRepository.SaveOrUpdate(store);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }

        public ActionResult UnPublish(int Id)
        {
            User user = UserRepository.Get(u => u.UserName == User.Identity.Name).SingleOrDefault();
            if (user != null && user.Role.IsAdmin )
            {
                Store store = StoreRepository.Get(s => s.Id == Id).SingleOrDefault();
                store.IsPublished = false;
                StoreRepository.SaveOrUpdate(store);
            }

            return RedirectToAction("Index", "Store");
        }
        public ActionResult Publish(int Id)
        {
            User user = UserRepository.Get(u => u.UserName == User.Identity.Name).SingleOrDefault();
            if (user != null && user.Role.IsAdmin || user.Role.IsAuthor)
            {

                Store store = StoreRepository.Get(s => s.Id == Id).SingleOrDefault();
                store.IsPublished = true;
                StoreRepository.SaveOrUpdate(store);
            }

            return RedirectToAction("Index", "Store");
        }

        public ActionResult Delete(int id)
        {
            User user = UserRepository.Get(u => u.UserName == User.Identity.Name).SingleOrDefault();
            if (user != null && user.Role.IsAdmin)
            {
                return View(StoreRepository.Get(s => s.Id.Equals(id)).SingleOrDefault());

            }

            return null;
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            User user = UserRepository.Get(u => u.UserName == User.Identity.Name).SingleOrDefault();
            if (user != null && user.Role.IsAdmin)
            {
                try
                {
                    StoreRepository.Delete(StoreRepository.Get(s => s.Id.Equals(id)).SingleOrDefault());
                    return RedirectToAction("Index");
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }


        public ActionResult EditProduct(int id)
        {
            return RedirectToAction("Edit", "Product", new { id = id });
        }

        public ActionResult AddProduct(int id)
        {
            return RedirectToAction("Create", "Product", new { id = id });
        }

        public ActionResult DeleteProduct(int id)
        {
            return RedirectToAction("Delete", "Product", new { id = id });
        }

        public ActionResult EditEmployee(int id)
        {
            return RedirectToAction("Edit", "Employee", new { id = id });
        }

        public ActionResult AddEmployee(int id)
        {
            return RedirectToAction("Create", "Employee", new { id = id });
        }

        public ActionResult DeleteEmployee(int id)
        {
            return RedirectToAction("Delete", "Employee", new { id = id });
        }
               public ActionResult Admin(int id)
        {

            User user = UserRepository.Get(u => u.UserName == User.Identity.Name).SingleOrDefault();
            if (user != null && user.Role.IsAdmin)
            {
                if (!user.Role.IsAdmin)
                {
                    user.Role.IsAdmin = true;
                    UserRepository.SaveOrUpdate(user);

                }

                return RedirectToAction("Index", "Home");
            }

            return null;

        }
        public ActionResult SetAsAdmin(int id)
        {

            User user = UserRepository.Get(u => u.UserName == User.Identity.Name).SingleOrDefault();
            if (user != null && user.Role.IsAdmin && id !=null)
            {
                User userAdmin = UserRepository.Get(u => u.Id == id).SingleOrDefault();
                userAdmin.Role.IsAdmin = true;
                UserRepository.SaveOrUpdate(userAdmin);
                return RedirectToAction("Index");
            }

            return null;
           
        }

        public ActionResult SetAsAuthor(int id)
        {
            
            User user = UserRepository.Get(u => u.UserName == User.Identity.Name).SingleOrDefault();
            if (user != null && user.Role.IsAdmin)
            {

                return RedirectToAction("Create", "Employee", new { userId = id });

            }

            return null;
        }

        public ActionResult ImageUpload()
        {
            return PartialView("ImageUpload", new CombinedHTMLImageUpload());
        }


        [HttpPost, ActionName("ImageUpload")]
        public ActionResult ImageUpload(HttpPostedFileBase fileUpload)
        {
            var fileUploaded = (fileUpload != null && fileUpload.ContentLength > 0) ? true : false;
            var viewModel = new CombinedHTMLImageUpload();

            try
            {

                if (!fileUploaded)
                {
                    viewModel.Message = string.Format("Не вдалось завантажити зображення.");
                    Console.WriteLine(viewModel.Message);
                    return PartialView("ImageUpload", viewModel);
                }

                string fileName = Path.GetFileName(fileUpload.FileName);
                string saveLocation = Path.Combine(Server.MapPath(_rootImagesFolderPath), fileName);
                // Try to save image.
                fileUpload.SaveAs(saveLocation);
                viewModel.ImageUploaded = "<IMG id='ImageUploaded' src=" + Path.Combine(_rootImagesFolderPath, fileName) + " style='float: left;'/>";
                viewModel.Message = string.Format("Зображення {0} було успішно завантажено.{1}", fileName, Server.MapPath(_rootImagesFolderPath));
            }
            catch (Exception)
            {

                Console.WriteLine(viewModel.Message);
                return PartialView("ImageUpload", viewModel);
            }

            return PartialView(viewModel);
        }


        private StoreEditModel StoreToEditModel(int id)
        {
            Store store = StoreRepository.Get(s => s.Id.Equals(id)).SingleOrDefault();
            StoreEditModel model = new StoreEditModel()
            {
                Id = store.Id,
                ImagePath = store.ImagePath,
                Name = store.Name,
                HtmlBanner = store.HtmlBanner,
                Description = store.Description,
                Products = store.Products
            };
            return model;
        }
        private Store EditModelToStore(StoreEditModel model)
        {
            Store store = StoreRepository.Get(s => s.Id.Equals(model.Id)).SingleOrDefault();
            if (store != null)
            {

                store.ImagePath = model.ImagePath;
                store.Name = model.Name;
                store.HtmlBanner = Server.HtmlEncode(string.Format(_storeTemplate, model.Id, model.ImagePath, model.Name, model.Description));
                store.Description = model.Description;
                store.Products = model.Products;
            }
            return store;
        }
    }
}
