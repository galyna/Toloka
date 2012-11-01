using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace TolokaStudio.Models.CombinedHTML
{


    public enum DisplayType : byte
    {
       
        Undefined = 0,
        Detail = 1,
        Banner = 2
    }

    public class CombinedHTMLItemPart
    {
        public CombinedHTMLItem combinedHTMLItem = new CombinedHTMLItem();
        public const string PartName = "CombinedHTMLItemPart";

        public string Name
        {
            get { return Record.Name; }
            set { Record.Name = value; }
        }

        public string Css
        {
            get { return Record.Css; }
            set { Record.Css = value; }
        }

        public string Js
        {
            get { return Record.Js; }
            set { Record.Js = value; }
        }

        public string Html
        {
            get { return Record.Html; }
            set { Record.Html = value; }
        }

        public byte Type
        {
            get { return Record.Type; }
            set { Record.Type = value; }
        }
        public CombinedHTMLItem Record
        {
            get { return combinedHTMLItem; }
            set { combinedHTMLItem = value; }
        }
    }
}

