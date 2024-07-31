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
    public class GroupManager : IGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GroupManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<GroupDto>> Get(int groupId)
        {
            var group = await _unitOfWork.Groups.GetAsync(g => g.Id == groupId, g => g.Users);
            if (group != null)
            {
                return new DataResult<GroupDto>(ResultStatus.Success, new GroupDto
                {
                    Group = group,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<GroupDto>(ResultStatus.Error, "Böyle bir Grup bulunamadı.", new GroupDto
            {
                Group = null,
                ResultStatus = ResultStatus.Error,
                Message = "Böyle bir Grup bulunamadı."
            });
        }

        public async Task<IDataResult<GroupUpdateDto>> GetGroupUpdateDto(int groupId)
        {
            var result = await _unitOfWork.Groups.AnyAsync(c => c.Id == groupId);
            if (result)
            {
                var group = await _unitOfWork.Groups.GetAsync(c => c.Id == groupId);
                var groupUpdateDto = _mapper.Map<GroupUpdateDto>(group);
                return new DataResult<GroupUpdateDto>(ResultStatus.Success, groupUpdateDto);
            }
            else
            {
                return new DataResult<GroupUpdateDto>(ResultStatus.Error, "Böyle bir Grup bulunamadı.", null);
            }
        }

        public async Task<IDataResult<GroupListDto>> GetAll()
        {
            var groups = await _unitOfWork.Groups.GetAllAsync(null, c => c.Users);
            if (groups.Count > -1)
            {
                return new DataResult<GroupListDto>(ResultStatus.Success, new GroupListDto
                {
                    Groups = groups,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<GroupListDto>(ResultStatus.Error, "Hiç bir Grup bulunamadı.", new GroupListDto
            {
                Groups = null,
                ResultStatus = ResultStatus.Error,
                Message = "Hiç bir Grup bulunamadı."
            });
        }

        public async Task<IDataResult<GroupListDto>> GetAllByNonDeleted()
        {
            var groups = await _unitOfWork.Groups.GetAllAsync(c => !c.IsDeleted, c => c.Users);
            if (groups.Count > -1)
            {
                return new DataResult<GroupListDto>(ResultStatus.Success, new GroupListDto
                {
                    Groups = groups,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<GroupListDto>(ResultStatus.Error, "Hiç bir Grup bulunamadı.", new GroupListDto
            {
                Groups = null,
                ResultStatus = ResultStatus.Error,
                Message = "Hiç bir Grup bulunamadı."
            });
        }

        public async Task<IDataResult<GroupListDto>> GetAllByNonDeletedAndActive()
        {
            var groups = await _unitOfWork.Groups.GetAllAsync(c => !c.IsDeleted && c.IsActive, c => c.Users);
            if (groups.Count > -1)
            {
                return new DataResult<GroupListDto>(ResultStatus.Success, new GroupListDto
                {
                    Groups = groups,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<GroupListDto>(ResultStatus.Error, "Hiç bir Grup bulunamadı.", null);
        }

        public async Task<IDataResult<GroupDto>> Add(GroupAddDto groupAddDto, string createdByName)
        {
            var group = _mapper.Map<Group>(groupAddDto);
            group.CreatedByName = createdByName;
            group.ModifiedByName = createdByName;
            var addedGroup = await _unitOfWork.Groups.AddAsync(group);
            await _unitOfWork.SaveAsync();
            return new DataResult<GroupDto>(ResultStatus.Success, $"{groupAddDto.Name} adlı Grup başarıyla eklenmiştir.", new GroupDto
            {
                Group = addedGroup,
                ResultStatus = ResultStatus.Success,
                Message = $"{groupAddDto.Name} adlı Grup başarıyla eklenmiştir."
            });
        }

        public async Task<IDataResult<GroupDto>> Update(GroupUpdateDto groupUpdateDto, string modifiedByName)
        {
            var oldGroup = await _unitOfWork.Groups.GetAsync(c =>c.Id== groupUpdateDto.Id);
            var group = _mapper.Map<GroupUpdateDto, Group>(groupUpdateDto, oldGroup);
            group.ModifiedByName = modifiedByName;
            var updatedGroup = await _unitOfWork.Groups.UpdateAsync(group);
            await _unitOfWork.SaveAsync();
            return new DataResult<GroupDto>(ResultStatus.Success, $"{groupUpdateDto.Name} adlı Grup başarıyla güncellenmiştir.", new GroupDto
            {
                Group = updatedGroup,
                ResultStatus = ResultStatus.Success,
                Message = $"{groupUpdateDto.Name} adlı Grup başarıyla güncellenmiştir."
            });
        }

        public async Task<IDataResult<GroupDto>> Delete(int groupId, string modifiedByName)
        {
            var group = await _unitOfWork.Groups.GetAsync(c => c.Id == groupId);
            if (group != null)
            {
                group.IsDeleted = true;
                group.ModifiedByName = modifiedByName;
                group.ModifiedDate = DateTime.Now;
                var deletedGroup = await _unitOfWork.Groups.UpdateAsync(group);
                await _unitOfWork.SaveAsync();
                return new DataResult<GroupDto>(ResultStatus.Success, $"{deletedGroup.Name} adlı Grup başarıyla silinmiştir.", new GroupDto
                {
                    Group = deletedGroup,
                    ResultStatus = ResultStatus.Success,
                    Message = $"{deletedGroup.Name} adlı Grup başarıyla silinmiştir."
                });
            }
            return new DataResult<GroupDto>(ResultStatus.Error, $"Böyle bir Grup bulunamadı.", new GroupDto
            {
                Group = null,
                ResultStatus = ResultStatus.Error,
                Message = $"Böyle bir Grup bulunamadı."
            });
        }

        public async Task<IResult> HardDelete(int groupId)
        {
            var group = await _unitOfWork.Groups.GetAsync(c => c.Id == groupId);
            if (group != null)
            {
                await _unitOfWork.Groups.DeleteAsync(group);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{group.Name} adlı Grup başarıyla veritabanından silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Böyle bir Grup bulunamadı.");
        }
    }
}
