using DataDemo.Common;
using Domain.Common;
using Domain.Models.Dto.Category;
using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features
{
    public interface ICategoryService
    {
        public Task<ApiResult<bool>> Create(CategoryDto request);
        public Task<ApiResult<bool>> Update(int id, CategoryDto request);
        public Task<ApiResult<bool>> Delete(int id);
        public Task<ApiResult<CategoryDto>> GetById(int id);
        public Task<ApiResult<PagedResult<CategoryDto>>> GetAll(int? pageSize, int? pageIndex, string search);
        public Task<ApiResult<List<CategoryDto>>> GetAll();
    }
}
