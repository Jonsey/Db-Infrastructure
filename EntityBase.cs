using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naimad.Infrastructure.Db
{
    public abstract class EntityBase 
    {
        public virtual Guid Id { get; set; }

        protected EntityBase()
        {
            Id = Guid.NewGuid();
        }
    }
}
