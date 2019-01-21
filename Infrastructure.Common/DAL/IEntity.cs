using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.DAL
{
    public interface IEntity
    {
        int Id { get; set; }
    }

    public interface IEntityLong
    {
        long Id { get; set; }
    }
}
