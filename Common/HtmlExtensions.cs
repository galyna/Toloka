using System.Linq;
using System.Web;
using System.Web.Mvc;
using HtmlAgilityPack;

namespace TolokaStudio.Common.Extensions
{
  public static class HtmlExtensions
  {
    public static MvcHtmlString Menu(this HtmlHelper htmlHelper, string menuHtml, byte level = 0)
    {
      string requestPath = ConvertUrl(HttpContext.Current.Request.Path);
      string requestRawUrl = ConvertUrl(HttpContext.Current.Request.RawUrl);

      var htmlDoc = new HtmlDocument();
      htmlDoc.LoadHtml(menuHtml);

      foreach (HtmlNode menuItem in htmlDoc.DocumentNode.SelectNodes("//li"))
      {
        var link = menuItem.ChildNodes.Where(n => n.Name == "a").FirstOrDefault();
        if (link != null)
        {
          string linkUrl = ConvertUrl(link.Attributes["href"].Value);

          int index = linkUrl.IndexOf("?");
          if (index > 0)
            linkUrl = linkUrl.Remove(index);

          string linkUrlPart = linkUrl;

          if (level == 1)
          {
            index = linkUrlPart.LastIndexOf('/');
            if (index < linkUrlPart.Length - 1 && index > 0)
              linkUrlPart = linkUrlPart.Remove(index).Trim('/');
          }

          bool isMatched = false;

          switch (level)
          {
            case 1:
              isMatched = (!string.IsNullOrEmpty(linkUrlPart) && requestRawUrl.Contains(linkUrlPart)) || requestPath == linkUrl;
              break;
            case 3:
              isMatched = requestPath == linkUrl;
              break;
            default:
              isMatched = (!string.IsNullOrEmpty(linkUrl) && requestPath.StartsWith(linkUrl)) || requestPath == linkUrl;
              break;
          }

          if (isMatched)
          {
            if (menuItem.Attributes["class"] == null)
              menuItem.Attributes.Add("class", "active");
            else
              menuItem.Attributes["class"].Value = string.Format("{0} active", menuItem.Attributes["class"].Value);
            if (level == 3)
              link.InnerHtml = string.Format("{0}<span></span>", link.InnerHtml);
          }
        }
      }
      return new MvcHtmlString(htmlDoc.DocumentNode.OuterHtml);
    }

    private static string ConvertUrl(string url)
    {
      if (url.StartsWith(HttpContext.Current.Request.ApplicationPath))
        url = url.Remove(0, HttpContext.Current.Request.ApplicationPath.Length);
      return url.TrimEnd('/').ToLowerInvariant();
    }
  }
}