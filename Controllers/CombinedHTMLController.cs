
using System.Web.Mvc;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.IO;
using System;
using System.Linq;
using System.Net;

using Core.Data.Entities;
using SquishIt.Framework;
using TolokaStudio.Models;
using Core.Data.Repository;
using Core.Data.Repository.Interfaces;

namespace TolokaStudio.Controllers
{

    public class CombinedHTMLController : Controller
    {
        #region Private firlds
        private string _getTemplateQuery = "CombinedHTML/GetTemplate?id={0}";
        private const string _rootImagesFolder = "Root";
        private const string _rootImagesFolderPath = "Content/img/";
        private readonly IRepository<WebTemplate> WebTemplateRepository;
        #endregion
        public CombinedHTMLController()
        {
            WebTemplateRepository = new Repository<WebTemplate>();
        }

        #region Templates Init
        public JsonResult GetTinyMceInitSettings()
        {
            var model = new TinymceInitSettingsModel();
            model.templates = GetTemplatesList();
            model.scripts = GetTinyMceExternallScripts();
            model.rootImagesFolderPath = Path.Combine(Request.Url.Scheme + "://" + Request.Url.DnsSafeHost + ":" + Request.Url.Port + Request.ApplicationPath, _rootImagesFolderPath);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetHtmlFromPage(string pageUrl)
        {
            WebClient wc = new WebClient();
            byte[] raw = wc.DownloadData(pageUrl);

            string webData = System.Text.Encoding.UTF8.GetString(raw);
            var template =
                 new TemplateModel
                 {
                     title = "t",
                     src = webData,
                     description = "d"
                 };
            return Json(template, JsonRequestBehavior.AllowGet);
        }

        private List<TemplateModel> GetTemplatesList()
        {
            IList<WebTemplate> templatesDB = WebTemplateRepository.GetAll().ToList();
            List<TemplateModel> templates = new List<TemplateModel>();
            foreach (var item in templatesDB)
            {
                var template =
                     new TemplateModel
                     {
                         title = item.Name,
                         src = string.Format(_getTemplateQuery, item.Id),
                         description = item.Name
                     };
                templates.Add(template);
            }
            return templates;
        }

        public ActionResult GetTemplate(int id)
        {
            WebTemplate template = WebTemplateRepository.Get(s => s.Id.Equals(id)).SingleOrDefault();
            return Content(Server.HtmlDecode(template.Html));
        }

        public ActionResult GetTemplateCss()
        {
            var file = File("~/Content/style/toloka_content.css", "text/css", Server.UrlEncode("toloka_content.css"));
            return file;
        }

        private List<string> GetTinyMceExternallScripts()
        {
            List<string> scripts = new List<string>();
            //SquishIt
            var squishIt = Bundle.JavaScript()
           .Add("~/Content/assets/js/jquery.js")
           .Add("~/Content/assets/js/google-code-prettify/prettify.js")
           .Add("~/Content/js/events_init.js")
           .Add("~/Content/js/scroller_load_img.js")
           .Add("~/Content/js/menu_full_width.js")
           .Add("~/Content/assets/js/google-code-prettify/prettify.js")
           .ForceRelease()
           .Render("~/Content/js/combined_.js");
            var squishItVersion = squishIt.Substring(squishIt.IndexOf("combined_") + 9, 32);
            scripts.Add(Path.Combine(Request.Url.Scheme + "://" + Request.Url.DnsSafeHost + ":" + Request.Url.Port + Request.ApplicationPath, "Content/js/combined_.js"));
            return scripts;
        }
        #endregion
        #region Templates Images
        public ActionResult GetImagesList(string selectedFolder)
        {
            List<ImageModel> imageList =  FillImagesList(selectedFolder);
            return Json(imageList, JsonRequestBehavior.AllowGet);
        }

        private List<ImageModel> FillImagesList(string sourceDir)
        {
            List<ImageModel> imageList = new List<ImageModel>();
            string[] files = Directory.GetFiles(sourceDir);
            var urlPrefix = Path.Combine(Request.Url.Scheme + "://" + Request.Url.DnsSafeHost + ":" + Request.Url.Port + Request.ApplicationPath + _rootImagesFolderPath, new DirectoryInfo(sourceDir).Name);
            if (IsRootFolder(sourceDir))
            {
                urlPrefix = Request.ApplicationPath + _rootImagesFolderPath;
            }

            foreach (string filePath in files)
            {
                string fullName = Path.GetFileName(filePath);
                string extention = Path.GetExtension(filePath);

                if (extention.Equals(".png") || extention.Equals(".jpg") || extention.Equals(".jpeg") || extention.Equals(".gif"))
                {
                    imageList.Add(new ImageModel() { name = fullName, src = Path.Combine(urlPrefix, fullName) });
                }
            }
            return imageList;
        }
        public ActionResult GetLinksList()
        {
            var pages = new List<string>();// _pagePropertiesService.GetPageURLs();
            Dictionary<string, string> tinyMCELinkList = new Dictionary<string, string>();
            foreach (var page in pages)
            {
                var value = "";//Request.ApplicationPath + page.Key;
                string key = "";// page.Value + "(" + page.Key + ")";
                tinyMCELinkList.Add(key, value);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("var tinyMCELinkList = new Array(");
            foreach (var link in tinyMCELinkList)
            {
                sb.Append("[" + "'" + link.Key + "'" + "," + "'" + link.Value + "'" + "],");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(");");
            return JavaScript(sb.ToString());
        }

        public ActionResult GetImage(string filename)
        {
            string[] nameDotSplited = filename.Split('.');

            var file = File(Request.ApplicationPath + _rootImagesFolderPath + filename, "image/" + nameDotSplited[1], Server.UrlEncode(filename));
            return file;

        }

      
        
        #endregion

        #region Templates GetImageFoldrs

        private bool IsRootFolder(string path)
        {
            var rootPhysicalPath = Server.MapPath("~/" + _rootImagesFolderPath);
            if (rootPhysicalPath.Equals(path))
            {
                return true;
            }
            return false;
        }

        private List<ImagesFolder> GetImageFoldrs()
        {
            List<ImagesFolder> imgFolders = new List<ImagesFolder>();
            // Process the list of files found in the directory. 
            string rootPath = Server.MapPath("~/" + _rootImagesFolderPath);
            string[] fileEntries = Directory.GetDirectories(rootPath);
            foreach (string fileName in fileEntries)
            {
                ImagesFolder imagesFolder = new ImagesFolder() { Name = new DirectoryInfo(fileName).Name, Url = Path.Combine(rootPath, fileName) };
                imgFolders.Add(imagesFolder);
            }
            //addd root
            ImagesFolder imagesFolderRoot = new ImagesFolder() { Name = _rootImagesFolder, Url = rootPath };
            imgFolders.Add(imagesFolderRoot);

            return imgFolders;
        }

        #endregion



        #region Templates Save

        public ActionResult SaveWebTemplate(string html, string name)
        {
            WebTemplate item = new WebTemplate();
            item.Html = html;
            item.Name= name;
            WebTemplateRepository.SaveOrUpdate(item);
            var model = new TinymceInitSettingsModel();
            model.templates = GetTemplatesList();
            return Json(model, JsonRequestBehavior.AllowGet);

        }

        #endregion

    }
    #region JavaScript models
    public class TemplateModel
    {
        public string title { get; set; }
        public string src { get; set; }
        public string description { get; set; }
    }
    public class ImageModel
    {
        public string name { get; set; }
        public string src { get; set; }
        public string description { get; set; }
    }

    public class TinymceInitSettingsModel
    {
        public List<TemplateModel> templates { get; set; }
        public List<string> scripts { get; set; }
        public string rootImagesFolderPath { get; set; }

    }
    #endregion


}
