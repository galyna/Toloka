using System;
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
        private const string DefaulImgBigBanner = "/Content/img/Product/imgBigBanner/" + DefaulImgName;
        private const string DefaulImgSmallBanner = "/Content/img/Product/imgSmallBanner/" + DefaulImgName;
        private const string DefaulDetailImg = "/Content/img/Product/imgFull/" + DefaulImgName;

        private const string DefaulImgBascet = "/Content/img/q/shopping_cart_1.gif";
        private const string DefaulImgBascetDelete = "/Content/img/q/delete.png";
        private const string DefaulImgBascetOrder = "/Content/img/q/order.png";
        private const string DefaulImgEditDetails = "/Content/img/q/edit.png";
        private const string _rootImagesFolderPath = "~/Content/img/Product";
        private const string DefaulImgUnpublish = "/Content/img/q/unpublish.png";
        private const string DefaulImgPublish = "/Content/img/q/publish.png";
        private const string DefaulImgUpload = "/Content/img/q/image_upload.png";
        private const string DefaulImgDelete = "/Content/img/q/delete.png";


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
        private const string _productSmallBenner =
                  " <div class='span3'>" +
                  " <a href='/Bascet'>" +
                   " <img  src='{0}' />" +
                 " </a>" +
                 " </div>";


        private const string _productBennerOrder = "<div class='template order{0}'>" + " <div class='span8'>" +
                " <div class='box_main_item'>" +
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


        private const string employeeDetails = @"<a href='/Employee/Details?id={0}'>Автор:{1} {2}</a></div>";
        private const string orderCreate = @" <a href='/Order/Create?Id=@Model.Id'> <img src='../../Content/img/q/shopping_cart_1.gif' title='Додати в кошик' />    </a>";



        #endregion
        #region edit
        private const string _productBennerTemplate = "<div class='template order{0}'>" +
                   " <div class='span8'>" +
                " <div class='box_main_item'>" +

           " <img class='addBannerImg'  title='Додати продукт {2}' alt='{0}'   src='" + DefaulImgUpload + "' />" +


           " <img class='addSmallImg'  title='Додати продукт {2}' alt='{0}'   src='" + DefaulImgUpload + "' />" +
             
                  " <a href='/Product/Publish?Id={0}'>" +
              "<img  src='" + DefaulImgPublish + "' alt='{0}'  title='Публікавати'/>" +
               " </a>" +
                " <a href='/Product/EditDetails?id={0}' >" +
           " <img   title='Редагувати сторінку' alt='{0}'   src='" + DefaulImgEditDetails + "' />" +
                " </a>" +
                  " <a href='/Product/Delete?Id={0}'>" +
              "<img  src='" + DefaulImgDelete + "' alt='{0}'  title='Видалити'/>" +
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
           " <img   title='Створити сторінку' alt='{0}'   src='" + DefaulImgEditDetails + "' />" +
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
                Product p = CreateProduct(employeeId);
                return RedirectToAction("Edit", new { Id = p.Id });
            }
            catch
            {
                return View();
            }
        }

        private Product CreateProduct(int employeeId)
        {

            Product product = new Product() { Name = "Назва", Price = 100, ImagePath = DefaulImgBigBanner, ImageSmallPath = DefaulImgSmallBanner, ImageMediumPath = DefaulImgBigBanner ,};
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
                Product product = ProductsRepository.Get(s => s.Id == id).SingleOrDefault();

                return View(product);
            }

            return null;
        }

        //
        // POST: /Employee/Edit/5

        [HttpPost]
        public ActionResult EditDetails(ProductDetail model)
        {
            User user = UserRepository.Get(u => u.UserName == User.Identity.Name).SingleOrDefault();
            if (user != null && user.Role.IsAdmin || user.Role.IsAuthor || !String.IsNullOrEmpty(model.HtmlDetail))
            {

                try
                {
                    Product product = ProductsRepository.Get(s => s.Id.Equals(model.Id)).SingleOrDefault();
                    product.HtmlDetail = Server.HtmlEncode(model.HtmlDetail);
                    ProductsRepository.SaveOrUpdate(product);

                    return Json("\\Product\\Details?id=" + model.Id);
                }
                catch
                {
                    return View(model);
                }
            }
            return View(model);
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
                return RedirectToAction("Edit", "Employee", new { id = product.Employee.Id });
            }
            return RedirectToAction("Index");
        }
        #endregion
        #region Image


        public ActionResult ImageUploadMediumBanner()
        {
            return PartialView("ImageUploadMediumBanner", new CombinedHTMLImageUpload());
        }

        [HttpPost, ActionName("ImageUploadMediumBanner")]
        public ActionResult ImageUploadMediumBanner(HttpPostedFileBase fileUpload)
        {

            var viewModel = new CombinedHTMLImageUpload();
            var image = WebImage.GetImageFromRequest();
            try
            {

                if (image==null)
                {
                    viewModel.Message = string.Format("Не вдалось завантажити зображення.");
                    Console.WriteLine(viewModel.Message);
                    return PartialView("ImageUploadMediumBanner", viewModel);
                }
      
                ImagesSaveHelper.FullImageSave(image);
                string fileName = ImagesSaveHelper.BannerMediumImageSave(image);

                viewModel.ImageUploaded = "<IMG id='ImageBannerUploaded' src=" + fileName + " style='float: left;'/>";
                viewModel.Message = string.Format("Зображення {0} було успішно завантажено.{1}", fileName, Server.MapPath(fileName));
            }
            catch (Exception)
            {

                Console.WriteLine(viewModel.Message);
                return PartialView("ImageUploadMediumBanner", viewModel);
            }

            return PartialView(viewModel);
        }

        public ActionResult ImageUploadSmallBanner()
        {
            return PartialView("ImageUploadSmallBanner", new CombinedHTMLImageUpload());
        }

        [HttpPost, ActionName("ImageUploadSmallBanner")]
        public ActionResult ImageUploadSmallBanner(HttpPostedFileBase fileUpload)
        {
            var fileUploaded = (fileUpload != null && fileUpload.ContentLength > 0) ? true : false;
            var viewModel = new CombinedHTMLImageUpload();

            try
            {

                if (!fileUploaded)
                {
                    viewModel.Message = string.Format("Не вдалось завантажити зображення.");
                    Console.WriteLine(viewModel.Message);
                    return PartialView("ImageUploadSmallBanner", viewModel);
                }
                var image = WebImage.GetImageFromRequest();
                ImagesSaveHelper.FullImageSave(image);
                string fileName = ImagesSaveHelper.BannerSmallImageSave(image);

                viewModel.ImageUploaded = "<IMG id='ImageUploadSmallBanner' src=" + fileName + " style='float: left;'/>";
                viewModel.Message = string.Format("Зображення {0} було успішно завантажено.{1}", fileName, Server.MapPath(fileName));
            }
            catch (Exception)
            {

                Console.WriteLine(viewModel.Message);
                return PartialView("ImageUploadSmallBanner", viewModel);
            }

            return PartialView(viewModel);
        }

        public ActionResult ImageUploadSmallDetail()
        {
            return PartialView("ImageUploadSmallDetail", new CombinedHTMLImageUpload());
        }


        [HttpPost, ActionName("ImageUploadSmallDetail")]
        public ActionResult ImageUploadSmallDetail(HttpPostedFileBase fileUpload)
        {
            var fileUploaded = (fileUpload != null && fileUpload.ContentLength > 0) ? true : false;
            var viewModel = new CombinedHTMLImageUpload();

            try
            {

                if (!fileUploaded)
                {
                    viewModel.Message = string.Format("Не вдалось завантажити зображення.");
                    Console.WriteLine(viewModel.Message);
                    return PartialView("ImageUploadSmallDetail", viewModel);
                }


                var image = WebImage.GetImageFromRequest();
                ImagesSaveHelper.FullImageSave(image);
                string fileName = ImagesSaveHelper.SmallDetailImageSave(image);


                viewModel.ImageUploaded = "<IMG id='ImageUploadSmallDetail' src=" + fileName + " style='float: left;'/>";
                viewModel.Message = string.Format("Зображення {0} було успішно завантажено.{1}", fileName, Server.MapPath(fileName));
            }
            catch (Exception)
            {

                Console.WriteLine(viewModel.Message);
                return PartialView("ImageUploadSmallDetail", viewModel);
            }

            return PartialView(viewModel);
        }

        public ActionResult ImageUploadBigDetail()
        {
            return PartialView("ImageUploadBigDetail", new CombinedHTMLImageUpload());
        }
        [HttpPost, ActionName("ImageUploadBigDetail")]
        public ActionResult ImageUploadBigDetail(HttpPostedFileBase fileUpload)
        {
            var fileUploaded = (fileUpload != null && fileUpload.ContentLength > 0) ? true : false;
            var viewModel = new CombinedHTMLImageUpload();

            try
            {

                if (!fileUploaded)
                {
                    viewModel.Message = string.Format("Не вдалось завантажити зображення.");
                    Console.WriteLine(viewModel.Message);
                    return PartialView("ImageUploadBigDetail", viewModel);
                }


                var image = WebImage.GetImageFromRequest();
                ImagesSaveHelper.FullImageSave(image);
                string fileName = ImagesSaveHelper.BigDetailImageSave(image);

                viewModel.ImageUploaded = "<IMG id='ImageUploadBigDetail' src=" + fileName + " style='float: left;'/>";
                viewModel.Message = string.Format("Зображення {0} було успішно завантажено.{1}", fileName, Server.MapPath(fileName));
            }
            catch (Exception)
            {

                Console.WriteLine(viewModel.Message);
                return PartialView("ImageUploadBigDetail", viewModel);
            }

            return PartialView(viewModel);
        }

        #endregion
        #region private
        private Product EditModelToProduct(ProductEditModel productEditModel)
        {
            Product product = ProductsRepository.Get(s => s.Id.Equals(productEditModel.Id)).SingleOrDefault();
            product.Name = productEditModel.Name;
            product.Price = productEditModel.Price;
            product.ImageMediumPath = productEditModel.ImageMediumPath;
            product.ImageSmallPath= productEditModel.ImageSmallPath;
            product.ImagePath = productEditModel.ImageMediumPath;
            CreateHtml(ref product);

            return product;
        }

        private ProductEditModel ProductToEditModel(int Id)
        {
            Product product = ProductsRepository.Get(s => s.Id.Equals(Id)).SingleOrDefault();
            CreateHtml(ref product);
            ProductEditModel ProductEditModel = new ProductEditModel()
            {
                ImageMediumPath = product.ImageMediumPath,
                ImageSmallPath=product.ImageSmallPath,
                Name = product.Name,
                Price = product.Price,
                Id = Id,
                HtmlBannerEdit = product.HtmlBannerEdit,
                HtmlDetail = product.HtmlDetail,
                HtmlBanner = product.HtmlBanner,
                HtmlSmallBanner = product.HtmlSmallBanner
            };
            return ProductEditModel;
        }
        private void CreateHtml(ref Product product)
        {
            var HtmlBanner = string.Format(_productBennerTemplate, product.Id, !string.IsNullOrEmpty(product.ImageMediumPath) ? product.ImagePath : DefaulImgBigBanner, product.Name, product.Price + " грн.");
            var HtmlBannerOrderedNot = string.Format(_productBenner, product.Id, !string.IsNullOrEmpty(product.ImageMediumPath) ? product.ImagePath : DefaulImgBigBanner, product.Name, product.Price + " грн.");
            var HtmlBannerOrdered = string.Format(_productBennerOrder, product.Id, !string.IsNullOrEmpty(product.ImageMediumPath) ? product.ImagePath : DefaulImgBigBanner, product.Name, product.Price + " грн.");
            var HtmlBannerEdit = "";
            if (product.IsPublished)
            {
                HtmlBannerEdit = string.Format(_productBennerTemplatePublished, product.Id,!string.IsNullOrEmpty(product.ImageMediumPath)?product.ImagePath:DefaulImgBigBanner, product.Name, product.Price + " грн.");
            }
            else
            {
                HtmlBannerEdit = string.Format(_productBennerTemplate, product.Id, !string.IsNullOrEmpty(product.ImageMediumPath) ? product.ImageMediumPath : DefaulImgBigBanner, product.Name, product.Price + " грн.");
            }

            var HtmlSmallBanner = string.Format(_productSmallBenner, !string.IsNullOrEmpty(product.ImageSmallPath) ? product.ImageSmallPath : DefaulImgSmallBanner);
            product.HtmlBanner = Server.HtmlEncode(HtmlBanner);
            product.HtmlBannerOrderedNot = Server.HtmlEncode(HtmlBannerOrderedNot);
            product.HtmlBannerOrdered = Server.HtmlEncode(HtmlBannerOrdered);
            product.HtmlBannerEdit = Server.HtmlEncode(HtmlBannerEdit);
            product.HtmlSmallBanner = Server.HtmlEncode(HtmlSmallBanner);


        }
        #endregion


    }
}

