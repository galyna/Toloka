using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.IO;
using System.Security.Policy;

namespace TolokaStudio.Controllers
{
    public static class ImagesSaveHelper
    {

        private const string DefaulDetailImg = "~/Content/img/Product/imgDetailBig/";
        private const string DefaulImgBigBanner = "~/Content/img/Product/imgBigBanner/";
        private const string DefaulImgSmallBanner = "~/Content/img/Product/imgSmallBanner/";
        private const string DefaulFull = "~/Content/img/Product/imgFull/";
        private const string DefaulDetailSmall = "~/Content/img/Product/imgDetailSmall/";

        public static string BannerMediumImageSave(WebImage image, int width = 310, int height = 310)
        {
            if (image != null)
            {
                if (image.Width > width)
                {
                    image.Resize(width, ((height * image.Height) / image.Width));
                }

                var filename = Path.GetFileName(image.FileName);
                var filepath = Path.Combine(DefaulImgBigBanner, filename);
                image.Save(filepath);

                return filepath.TrimStart('~');

            }
            return "";
        }

        public static string BannerSmallImageSave(WebImage image, int width = 80, int height = 80)
        {
            if (image != null)
            {
                if (image.Width > width)
                {
                    image.Resize(width, ((height * image.Height) / image.Width));
                }

                var filename = Path.GetFileName(image.FileName);
                var filepath = Path.Combine(DefaulImgSmallBanner, filename);
                image.Save(filepath);

                return filepath.TrimStart('~');

            }
            return "";
        }

        public static string BigDetailImageSave(WebImage image, int width = 600, int height = 600)
        {
            if (image != null)
            {
                if (image.Width > width)
                {
                    image.Resize(width, ((height * image.Height) / image.Width));
                }

                var filename = Path.GetFileName(image.FileName);
                var filepath = Path.Combine(DefaulDetailImg, filename);
                image.Save(filepath);

                return filepath.TrimStart('~');

            }
            return "";
        }

        public static string SmallDetailImageSave(WebImage image, int widthR = 100, int heightR = 100)
        {
            if (image != null)
            {
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

                if (image.Width > widthR)
                {
                    image.Resize(widthR, ((heightR * image.Height) / image.Width));
                }

                var filename = Path.GetFileName(image.FileName);
                var filepath = Path.Combine(DefaulDetailImg, filename);
                image.Save(filepath);

                return filepath;

            }
            return "";
        }

        public static string SaveSmallImageDetail(WebImage image, int widthRequired = 100, int heightRequired = 100)
        {
            if (image != null)
            {
                var width = image.Width;
                var height = image.Height;
                var filename = Path.GetFileName(image.FileName);
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

                if (image.Width > widthRequired)
                {
                    image.Resize(widthRequired, ((heightRequired * image.Height) / image.Width));
                }

                var filepath = Path.Combine(DefaulDetailImg, filename);
                image.Save(filepath);
                return filepath.TrimStart('~');

            }
            return "";
        }

        public static void FullImageSave(WebImage image)
        {
            if (image != null)
            {
                var filename = Path.GetFileName(image.FileName);
                var filepath = Path.Combine(DefaulFull, filename);
                image.Save(filepath);

            }
        }
    }
}