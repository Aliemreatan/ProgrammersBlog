using Microsoft.EntityFrameworkCore;
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
    public class EfGroupRepository : EfEntityRepositoryBase<Group>, IGroupRepository
    {
        public EfGroupRepository(DbContext context) : base(context)
        {

        }
    }
}
