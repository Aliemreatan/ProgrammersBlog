using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using ProgrammersBlog.Shared.Utilities.Results.Concrete;

namespace ProgrammersBlog.Services.Concrete
{
    public class ArticleManager : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ArticleManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<ArticleDto>> Add(ArticleAddDto articleAddDto, string createdByName)
        {
            var article = _mapper.Map<Article>(articleAddDto);
            article.CreatedByName = createdByName;
            article.ModifiedByName = createdByName;
            var addedArticle = await _unitOfWork.Articles.AddAsync(article);
            await _unitOfWork.SaveAsync();
            return new DataResult<ArticleDto>(ResultStatus.Success, $"{articleAddDto.Title} adlı makale başarıyla eklenmiştir.", new ArticleDto
            {
                Article = addedArticle,
                ResultStatus = ResultStatus.Success,
                Message = $"{articleAddDto.Title} adlı makale başarıyla eklenmiştir."
            });
        }

        public async Task<IDataResult<ArticleDto>> Delete(int articleId, string modifiedByName)
        {
            var article = await _unitOfWork.Articles.GetAsync(c => c.Id == articleId);
            if (article != null)
            {
                article.IsDeleted = true;
                article.ModifiedByName = modifiedByName;
                article.ModifiedDate = DateTime.Now;
                var deletedArticle = await _unitOfWork.Articles.UpdateAsync(article);
                await _unitOfWork.SaveAsync();
                return new DataResult<ArticleDto>(ResultStatus.Success, $"{deletedArticle.Title} adlı makale başarıyla silinmiştir.", new ArticleDto
                {
                    Article = deletedArticle,
                    ResultStatus = ResultStatus.Success,
                    Message = $"{deletedArticle.Title} adlı makale başarıyla silinmiştir."
                });
            }
            return new DataResult<ArticleDto>(ResultStatus.Error, $"Böyle bir makale bulunamadı.", new ArticleDto
            {
                Article = null,
                ResultStatus = ResultStatus.Error,
                Message = $"Böyle bir makale bulunamadı."
            });
        }

        public async Task<IDataResult<ArticleDto>> Get(int articleId)
        {
            var article = await _unitOfWork.Articles.GetAsync(a => a.Id == articleId, a => a.Category);
            if (article != null)
            {
                return new DataResult<ArticleDto>(ResultStatus.Success, new ArticleDto
                {
                    Article = article,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ArticleDto>(ResultStatus.Error,"Böyle bir makale bulunmadı.", new ArticleDto
            {
                Article = null,
                ResultStatus = ResultStatus.Error,
                Message= "Böyle bir makale bulunmadı."
            });
        }
        

        public async Task<IDataResult<ArticleListDto>> GetAll()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(null, a => a.Category);
            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, "Hiç bir makale bulunamadı.", new ArticleListDto
            {
                Articles = null,
                ResultStatus = ResultStatus.Error,
                Message = "Hiç bir makale bulunamadı."
            });
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByNonDeleted()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(a => !a.IsDeleted, a => a.Category);
            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, "Hiç bir makale bulunamadı.", new ArticleListDto
            {
                Articles = null,
                ResultStatus = ResultStatus.Error,
                Message = "Hiç bir makale bulunamadı."
            });
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByNonDeletedAndActive()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(a => !a.IsDeleted && a.IsActive, a => a.Category);
            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, "Hiç bir makale bulunamadı.", null);
        }

        public async Task<IDataResult<ArticleUpdateDto>> GetArticleUpdateDto(int articleId)
        {
            var result = await _unitOfWork.Articles.AnyAsync(a => a.Id == articleId);
            if (result)
            {
                var article = await _unitOfWork.Articles.GetAsync(c => c.Id == articleId);
                var articleUpdateDto = _mapper.Map<ArticleUpdateDto>(article);
                return new DataResult<ArticleUpdateDto>(ResultStatus.Success, articleUpdateDto);
            }
            else
            {
                return new DataResult<ArticleUpdateDto>(ResultStatus.Error, "Böyle bir makale bulunamadı.", null);
            }
        }

        public async Task<IResult> HardDelete(int articleId)
        {
            var article = await _unitOfWork.Articles.GetAsync(a => a.Id == articleId);
            if (article != null)
            {
                await _unitOfWork.Articles.DeleteAsync(article);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{article.Title} adlı makale başarıyla veritabanından silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Böyle bir makale bulunamadı.");
        }

        public  async Task<IDataResult<ArticleDto>> Update(ArticleUpdateDto articleUpdateDto, string modifiedByName)
        {
            var oldArticle = await _unitOfWork.Articles.GetAsync(a => a.Id == articleUpdateDto.Id);
            var article = _mapper.Map<ArticleUpdateDto, Article>(articleUpdateDto, oldArticle);
            article.ModifiedByName = modifiedByName;
            var updatedArticle = await _unitOfWork.Articles.UpdateAsync(article);
            await _unitOfWork.SaveAsync();
            return new DataResult<ArticleDto>(ResultStatus.Success, $"{articleUpdateDto.Title} adlı makale başarıyla güncellenmiştir.", new ArticleDto
            {
                Article = updatedArticle,
                ResultStatus = ResultStatus.Success,
                Message = $"{articleUpdateDto.Title} adlı makale başarıyla güncellenmiştir."
            });
        }
    }
}
