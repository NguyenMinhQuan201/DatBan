using DataDemo.Common;
using Domain.Common;
using Domain.Models.Dto.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features
{
    public interface IUserOperationService
    {
        public Task<UserOperationDto> Create(UserOperationDto request);
        public Task<UserOperationDto> Update(int id, UserOperationDto request);
        public Task<bool> Delete(int id);
        public Task<UserOperationDto> GetById(int id);
        public Task<ApiResult<PagedResult<UserOperationDto>>> GetAll(int? pageSize, int? pageIndex, string search);
    }
}
