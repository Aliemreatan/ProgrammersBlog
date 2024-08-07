using Microsoft.EntityFrameworkCore;
using MyProject.Entities.Concrete;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Shared.Data.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Data.Concrete.EntityFramework.Repository
{
    public class EfEventRepository : EfEntityRepositoryBase<Event>, IEventRepository
    {
        public EfEventRepository(DbContext context) : base(context)
        {

        }
    }
}
