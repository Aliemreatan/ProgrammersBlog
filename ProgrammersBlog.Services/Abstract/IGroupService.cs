using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;

namespace ProgrammersBlog.Services.Abstract
{
    public interface IGroupService
    {
        Task<IDataResult<GroupDto>> Get(int groupId);

        Task<IDataResult<GroupUpdateDto>> GetGroupUpdateDto(int groupId);
        Task<IDataResult<GroupListDto>> GetAll();
        Task<IDataResult<GroupListDto>> GetAllByNonDeleted();
        Task<IDataResult<GroupListDto>> GetAllByNonDeletedAndActive();
        Task<IDataResult<GroupDto>> Add(GroupAddDto groupAddDto, string createdByName);
        Task<IDataResult<GroupDto>> Update(GroupUpdateDto groupUpdateDto, string modifiedByName);
        Task<IDataResult<GroupDto>> Delete(int groupId, string modifiedByName);
        Task<IResult> HardDelete(int groupId);
    }
}
