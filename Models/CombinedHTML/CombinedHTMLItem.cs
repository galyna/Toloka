using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TolokaStudio.Models.CombinedHTML
{
    public class CombinedHTMLItem
    {
        public virtual string Name { get; set; }

        public virtual string Css { get; set; }

        public virtual string Js { get; set; }

        public virtual string Html { get; set; }

        public virtual byte Type { get; set; }

        //public virtual PagePropertiesRecord PagePropertiesRecord { get; set; }
    }
}