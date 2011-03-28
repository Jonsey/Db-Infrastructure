using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naimad.Infrastructure.Db
{
    public abstract class ValueObject
    {
        public virtual Guid Id { get; set; }

        protected ValueObject()
        {
            Id = Guid.NewGuid();
        }
    }
}
