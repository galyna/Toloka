using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Data.Entities
{
    public class OrderStatus
    {
        public virtual int Id { get; protected set; }
        public virtual bool IsInProgress { get; set; }
        public virtual bool IsDone{ get; set; }
    }
}
