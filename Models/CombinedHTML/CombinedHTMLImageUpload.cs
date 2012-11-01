using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TolokaStudio.Models
{
    public class ImagesFolder
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
    public class CombinedHTMLImageUpload
    {
        public  CombinedHTMLImageUpload()
        {
            this.Message="";
            this.SelectLabel = "Виберіть зображення для завантаження";
            this.ImageUploaded = "/Content/img/_C3D9074.png"; 
        }
   
        public string Message { get; set; }
        public string ImageUploaded { get; set; }
        public string SelectLabel { get; set; }
      
    }
}