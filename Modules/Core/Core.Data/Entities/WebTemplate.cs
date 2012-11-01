using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Entities
{
    public class WebTemplate
    {
        public virtual int Id { get; protected set; }
        public virtual string Name { get; set; }       
        public virtual string Html{ get; set; }   
    }
}
