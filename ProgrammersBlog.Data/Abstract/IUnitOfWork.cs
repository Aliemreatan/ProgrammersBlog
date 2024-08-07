using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Data.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IArticleRepository Articles { get; } // unitofwork.Articles
        ICategoryRepository Categories { get; }

        IEventRepository Events { get; }

        IGroupRepository Groups { get; }
        ICommentRepository Comments { get; }
        
        // _unitOfWork.Categories.AddAsync();
        // _unitOfWork.Categories.AddAsync(category);
        //_unitOfWork.Users.AddAsync(user);
        //_unitOfWork.SaveAsync();
        Task<int> SaveAsync();
    }
}
