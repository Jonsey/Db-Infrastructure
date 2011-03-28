using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Naimad.Infrastructure.Db
{
    public interface IAggregateRoot

    {
        Guid Id { get; set; }
    }
}
