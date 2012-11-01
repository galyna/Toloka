﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Data.Entities;
using TolokaStudio.Models;
using Core.Data.Repository.Interfaces;
using Core.Data.Repository;
using System.Web.Security;
using System.IO;
using System.Web.Helpers;

namespace TolokaStudio.Controllers
{

    public class ProductController : Controller
    {
        #region readonly and const
        private readonly IRepository<Store> StoreRepository;
        private readonly IRepository<Product> ProductsRepository;
        private readonly IRepository<Employee> EmployeeRepository;
        private readonly IRepository<User> UserRepository;

        private const string DefaulImgName = "Coffe.png";
        private const string DefaulImg = "/Content/img/imgThumbs/Fluor/" + DefaulImgName;
        private const string DefaulDetailImg = "/Content/img/imgFull/Fluor/" + DefaulImgName;

        private const string DefaulImgBascet = "/Content/img/q/shopping_cart_1.gif";
        private const string DefaulImgBascetDelete = "/Content/img/q/delete.png";
        private const string DefaulImgBascetOrder = "/Content/img/q/order.png";
        private const string DefaulImgEditDetails = "/Content/img/q/edit.png";
        private const string _rootImagesFolderPath = "~/Content/img/Product";
        private const string DefaulImgUnpublish = "/Content/img/q/unpublish.png";
        private const string DefaulImgPublish = "/Content/img/q/publish.png";
       

        #region order
        private const string _productBenner = "<div class='template order{0}'>" +
                   " <div class='span8'>" +
                " <div class='box_main_item'>" +
            " <img class='orderBtn' title='Додати в кошик'  alt='{0}'  src='" + DefaulImgBascet + "' />" +
                  "  </div>" +
                   " <a href='/Product/Details?id={0}'>" +
                   " <div class='box_main_item'>" +
                   " <div class='box_main_item_img'>" +
                   "  <div class='box_main_item_img_bg'>" +
                   "     <span>Детальніше</span>" +
                   "  </div>" +
                    " <img src='{1}' />" +
                  " </div>" +
                  " <div class='box_main_item_text'>" +
                  "   <h3>{2}</h3>" +
                  "     <span>{3}</span>" +
                  "  </div>" +
                  " </div>" +
                  " </a>" +
                  "</div>" +
                  " </div>";


        private const string _productBennerOrder = "<div class='template order{0}'>" + " <div class='span8'>" +
                " <div class='box_main_item'>" +
                  " <img class='orderBtn' title='Додати в кошик Назва'  alt='{0}'  src='" + DefaulImgBascet + "' />" +
           " <img class='makeOrder'  title='Замовити Назва' alt='{0}'  src='" + DefaulImgBascetOrder + "' />" +
           " <img class='deleteBtn' alt='{0}'  title='Видалити з кошика Назва' src='" + DefaulImgBascetDelete + "'/>" +
               "  </div>" +
               " <a href='/Product/Details?id={0}'>" +
                  " <div class='box_main_item'>" +
                  " <div class='box_main_item_img'>" +
                  "  <div class='box_main_item_img_bg'>" +
                  "     <span>Детальніше</span>" +
                  "  </div>" +
                 " <img src='{1}' />" +
                  " </div>" +
                  " <div class='box_main_item_text'>" +
                  "   <h3>{2}</h3>" +
                  "     <span>{3}</span>" +
                  "  </div>" +
                  " </div>" +
                  " </a>" +
                  "</div>" +
                  " </div>";
        #endregion
        #region edit
        private const string _productBennerTemplate = "<div class='template order{0}'>" +
                   " <div class='span8'>" +
                " <div class='box_main_item'>" +
                  " <a href='/Product/Publish?Id={0}'>" +
              "<img  src='" + DefaulImgPublish + "' alt='{0}'  title='Публікавати'/>" +
               " </a>" +
                " <a href='/Product/EditDetails?id={0}' >" +
           " <img   title='Редагувати сторінку' alt='{0}'   src='" + DefaulImgEditDetails + "' />" +
                " </a>" +
                  "  </div>" +
                   " <a href='/Product/Edit?id={0}'>" +
                   " <div class='box_main_item'>" +
                   " <div class='box_main_item_img'>" +
                   "  <div class='box_main_item_img_bg'>" +
                   "     <span>Редагувати</span>" +
                   "  </div>" +
                    " <img src='{1}' />" +
                  " </div>" +
                  " <div class='box_main_item_text'>" +
                  "   <h3>{2}</h3>" +
                  "     <span>{3}</span>" +
                  "  </div>" +
                  " </div>" +
                  " </a>" +
                  "</div>" +
                  " </div>";
        private const string _productBennerTemplatePublished = "<div class='template order{0}'>" +
               " <div class='span8'>" +
            " <div class='box_main_item'>" +
              " <a href='/Product/Unpublish?Id={0}'>" +
             "<img  src='" + DefaulImgUnpublish + "' alt='{0}' title='Не публікувати'/>" +
               " </a>" +
                 " <a href='/Product/EditDetails?id={0}' >" +
           " <img   title='Редагувати сторінку' alt='{0}'   src='" + DefaulImgEditDetails + "' />" +
                " </a>" +
              "  </div>" +
               " <a href='/Product/Edit?id={0}'>" +
                   " <div class='box_main_item'>" +
                   " <div class='box_main_item_img'>" +
                   "  <div class='box_main_item_img_bg'>" +
                   "     <span>Редагувати</span>" +
                   "  </div>" +
                    " <img src='{1}' />" +
                  " </div>" +
              " <div class='box_main_item_text'>" +
              "   <h3>{2}</h3>" +
              "     <span>{3}</span>" +
              "  </div>" +
              " </div>" +
              " </a>" +
              "</div>" +
              " </div>";
        #endregion


        #endregion

        public ProductController()
        {
            StoreRepository = new Repository<Store>();
            ProductsRepository = new Repository<Product>();
            EmployeeRepository = new Repository<Employee>();
            UserRepository = new Repository<User>();
        }

        #region Index Details
        public ActionResult Index()
        {
            IList<Product> products = ProductsRepository.GetAll().ToList();
            ProductList model = new ProductList();
            model.Products = products.ToList<Product>();
            return View(model);
        }

        //
        // GET: /Product/Details/5

        public ActionResult Details(int id)
        {
            Product product = ProductsRepository.Get(s => s.Id.Equals(id)).SingleOrDefault();
            return View(product);
        }
        //
        // GET: /Product/Create/5
        #endregion

        #region Create Edit
        public ActionResult Create(int employeeId)
        {
            try
            {
              Product p=  CreateProduct(employeeId);
              return RedirectToAction("Edit", new { Id = p.Id });
            }
            catch
            {
                return View();
            }
        }

        private Product CreateProduct(int employeeId)
        {

            Product product = new Product() { Name = "Назва", Price = 100, ImagePath = DefaulImg };
            product.Employee = EmployeeRepository.Get(e => e.Id == employeeId).SingleOrDefault();
            product.Store = StoreRepository.GetAll().First();

            Product productSaved = ProductsRepository.SaveOrUpdate(product);
            product.Store.AddProduct(productSaved);

            productSaved = ProductsRepository.SaveOrUpdate(productSaved);
            return productSaved;

        }

        public ActionResult Edit(int Id)
        {
            User user = UserRepository.Get(u => u.UserName == User.Identity.Name).SingleOrDefault();
            if (user != null && user.Role.IsAdmin || user.Role.IsAuthor)
            {
                ProductEditModel ProductEditModel = ProductToEditModel(Id);
                return View(ProductEditModel);
            }
            return null;
        }

        //
        // POST: /Product/Edit/5

        [HttpPost]
        public ActionResult Edit(ProductEditModel productEditModel)
        {
            User user = UserRepository.Get(u => u.UserName == User.Identity.Name).SingleOrDefault();

            if (ModelState.IsValid && user != null && user.Role.IsAdmin || user.Role.IsAuthor)
            {
                try
                {
                    SaveProductImage(productEditModel);
                    Product product = EditModelToProduct(productEditModel);
                    ProductsRepository.SaveOrUpdate(product);
                    return RedirectToAction("Edit", "Employee", new { id = product.Employee.Id });
                }
                catch
                {
                    return View(productEditModel);
                }
            }
            else
            {
                return View(productEditModel);
            }
        }
        #endregion

        #region EditDetails
        public ActionResult EditDetails(int id)
        {
            User user = UserRepository.Get(u => u.UserName == User.Identity.Name).SingleOrDefault();
            if (user != null && user.Role.IsAdmin || user.Role.IsAuthor)
            {
                string html = HttpUtility.HtmlDecode(ProductsRepository.Get(s => s.Id == id).SingleOrDefault().HtmlDetail);
                DetailsModel model = new DetailsModel();
                model.Id = id;
                model.HtmlDetail = html != null ? html : "";
                return View(model);
            }

            return null;
        }

        //
        // POST: /Employee/Edit/5

        [HttpPost]
        public ActionResult EditDetails(DetailsModel model)
        {
            User user = UserRepository.Get(u => u.UserName == User.Identity.Name).SingleOrDefault();
            if (user != null && user.Role.IsAdmin || user.Role.IsAuthor)
            {

                try
                {
                    Product product = ProductsRepository.Get(s => s.Id.Equals(model.Id)).SingleOrDefault();
                    product.HtmlDetail = Server.HtmlEncode(model.HtmlDetail);
                    ProductsRepository.SaveOrUpdate(product);

                    return RedirectToAction("Edit", "Product", new { Id = model.Id });
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
                return View(ProductsRepository.Get(s => s.Id.Equals(Id)).SingleOrDefault());
            }
            return null;
        }

        //
        // POST: /Product/Delete/5
     
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            User user = UserRepository.Get(u => u.UserName == User.Identity.Name).SingleOrDefault();
            if (user != null && user.Role.IsAdmin || user.Role.IsAuthor)
            {
                try
                {
                    ProductsRepository.Delete(ProductsRepository.Get(s => s.Id.Equals(id)).SingleOrDefault());
                    return RedirectToAction("Edit", "Employee");
                }
                catch
                {
                    return View();
                }
            }
            return null;
        }
        #endregion

        #region Publish
        public ActionResult Unpublish(int Id)
        {
            User user = UserRepository.Get(u => u.UserName == User.Identity.Name).SingleOrDefault();
            if (user != null && user.Role.IsAdmin || user.Role.IsAuthor)
            {
                Product product = ProductsRepository.Get(s => s.Id == Id).SingleOrDefault();
                product.IsPublished = false;
                product.HtmlBannerEdit = Server.HtmlEncode(string.Format(_productBennerTemplate, product.Id, product.ImagePath, product.Name, product.Price + " грн."));
                ProductsRepository.SaveOrUpdate(product);
                return RedirectToAction("Edit", "Employee", new { id = product.Employee.Id });
            }

            return RedirectToAction("Index");
        }

        public ActionResult Publish(int Id)
        {
            User user = UserRepository.Get(u => u.UserName == User.Identity.Name).SingleOrDefault();
            if (user != null && user.Role.IsAdmin || user.Role.IsAuthor)
            {
                Product product = ProductsRepository.Get(s => s.Id == Id).SingleOrDefault();
                product.IsPublished = true;
                product.HtmlBannerEdit = Server.HtmlEncode(string.Format(_productBennerTemplatePublished, product.Id, product.ImagePath, product.Name, product.Price + " грн."));
                ProductsRepository.SaveOrUpdate(product);
                return RedirectToAction("Edit", "Employee", new { id = product.Employee.Id});
            }
            return RedirectToAction("Index");
        }
        #endregion
        #region Image

        private void SaveProductImage(ProductEditModel model)
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

                string fileName = SaveImage();

                viewModel.ImageUploaded = "<IMG id='ImageUploaded' src=" + fileName + " style='float: left;'/>";
                viewModel.Message = string.Format("Зображення {0} було успішно завантажено.{1}", fileName, Server.MapPath(fileName));
            }
            catch (Exception)
            {

                Console.WriteLine(viewModel.Message);
                return PartialView("ImageUpload", viewModel);
            }

            return PartialView(viewModel);
        }

        private string SaveImage()
        {
            var image = WebImage.GetImageFromRequest();
            if (image != null)
            {
                if (image.Width > 600)
                {
                    image.Resize(600, ((600 * image.Height) / image.Width));
                }

                var filename = Path.GetFileName(image.FileName);
                image.Save(Path.Combine("~/Content/img/imgFull", filename));
                var filepath = Path.Combine("~/Content/img/imgFull", filename);
                var width = image.Width;
                var height = image.Height;

                if (width > height)
                {
                    var leftRightCrop = (width - height) / 2;
                    image.Crop(0, leftRightCrop, 0, leftRightCrop);
                }
                else if (height > width)
                {
                    var topBottomCrop = (height - width) / 2;
                    image.Crop(topBottomCrop, 0, topBottomCrop, 0);
                }

                if (image.Width > 100)
                {
                    image.Resize(100, ((100 * image.Height) / image.Width));

                }
                image.Save(Path.Combine("~/Content/img/imgThumbs", filename));
                return Url.Content(filepath);

            }
            return "";
        }

        public ActionResult ImageUploadSmall()
        {
            return PartialView("ImageUploadSmall", new CombinedHTMLImageUpload());
        }


        [HttpPost, ActionName("ImageUploadSmall")]
        public ActionResult ImageUploadSmall(HttpPostedFileBase fileUpload)
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


                string fileName = SaveSmallImage();

                viewModel.ImageUploaded = "<IMG id='ImageUploaded' src=" + fileName + " style='float: left;'/>";
                viewModel.Message = string.Format("Зображення {0} було успішно завантажено.{1}", fileName, Server.MapPath(fileName));
            }
            catch (Exception)
            {

                Console.WriteLine(viewModel.Message);
                return PartialView("ImageUploadSmall", viewModel);
            }

            return PartialView(viewModel);
        }

        private string SaveSmallImage()
        {
            var image = WebImage.GetImageFromRequest();
            if (image != null)
            {
                if (image.Width > 600)
                {
                    image.Resize(600, ((600 * image.Height) / image.Width));
                }

                var filename = Path.GetFileName(image.FileName);
                image.Save(Path.Combine("~/Content/img/imgFull", filename));
                var width = image.Width;
                var height = image.Height;

                if (width > height)
                {
                    var leftRightCrop = (width - height) / 2;
                    image.Crop(0, leftRightCrop, 0, leftRightCrop);
                }
                else if (height > width)
                {
                    var topBottomCrop = (height - width) / 2;
                    image.Crop(topBottomCrop, 0, topBottomCrop, 0);
                }

                if (image.Width > 100)
                {
                    image.Resize(100, ((100 * image.Height) / image.Width));

                }
                image.Save(Path.Combine("~/Content/img/imgThumbs", filename));
                filename = Path.Combine("~/Content/img/imgThumbs", filename);

                return Url.Content(filename);

            }
            return "";
        }
        #endregion
        #region private
        private Product EditModelToProduct(ProductEditModel productEditModel)
        {
            Product product = ProductsRepository.Get(s => s.Id.Equals(productEditModel.Id)).SingleOrDefault();
            product.Name = productEditModel.Name;
            product.Price = productEditModel.Price;
            product.ImagePath = productEditModel.ImagePath;

            CreateHtml(ref product);

            return product;
        }

        private ProductEditModel ProductToEditModel(int Id)
        {
            Product product = ProductsRepository.Get(s => s.Id.Equals(Id)).SingleOrDefault();
            CreateHtml(ref product);
            ProductEditModel ProductEditModel = new ProductEditModel()
            {
                ImagePath = product.ImagePath,
                Name = product.Name,
                Price = product.Price,
                Id = Id,
                HtmlBannerEdit = product.HtmlBannerEdit,
                HtmlDetail = product.HtmlDetail,
                HtmlBanner = product.HtmlBanner,
                
            };
            return ProductEditModel;
        }
        private void CreateHtml(ref Product product)
        {
            var HtmlBanner = string.Format(_productBennerTemplate, product.Id, product.ImagePath, product.Name, product.Price + " грн.");
            var HtmlBannerOrderedNot = string.Format(_productBenner, product.Id, product.ImagePath, product.Name, product.Price + " грн.");
            var HtmlBannerOrdered = string.Format(_productBennerOrder, product.Id, product.ImagePath, product.Name, product.Price + " грн.");
            var HtmlBannerEdit = "";
            if (product.IsPublished)
            {
                HtmlBannerEdit = string.Format(_productBennerTemplatePublished, product.Id, product.ImagePath, product.Name, product.Price + " грн.");
            }
            else
            {
                HtmlBannerEdit = string.Format(_productBennerTemplate, product.Id, product.ImagePath, product.Name, product.Price + " грн.");
            }

            product.HtmlBanner = Server.HtmlEncode(HtmlBanner);
            product.HtmlBannerOrderedNot = Server.HtmlEncode(HtmlBannerOrderedNot);
            product.HtmlBannerOrdered = Server.HtmlEncode(HtmlBannerOrdered);
            product.HtmlBannerEdit = Server.HtmlEncode(HtmlBannerEdit);


        }
        #endregion
       
       
    }
}
