using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DAL
{
    public interface IDbTrigger
    {
        Type ApplyOn { get; }
        void BeforeInsert();
        void AfterInsert();

        void BeforeUpdate();
        void AfterUpdate();
    }
}
