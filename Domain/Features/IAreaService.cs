using DataDemo.Common;
using Domain.Common;
using Domain.Features.Supplier.Dto;
using Domain.Models.Dto.Blog;
using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Domain.Features
{
    public interface IAreaService
    {
        public Task<ApiResult<bool>> Create(AreaDto request);
        public Task<ApiResult<bool>> Update(int id, AreaDto request);
        public Task<ApiResult<bool>> Restore(int id);
        public Task<ApiResult<bool>> Delete(int id);
        public Task<ApiResult<PagedResult<AreaDto>>> GetAll(int? pageSize, int? pageIndex, string? search);
        public Task<ApiResult<AreaDto>> GetById(int Id);
        public Task<ApiResult<IEnumerable<AreaDto>>> GetAll();
        /*public Task<UserVmDto> GetById(Guid id);*/
    }
}
