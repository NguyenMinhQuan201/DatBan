using DataDemo.Common;
using Domain.Common;
using Domain.Models.Dto.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features
{
    public interface IBlogService
    {
        public Task<ApiResult<bool>> Create(BlogDto request);
        public Task<ApiResult<bool>> Update(int id, BlogDto request);
        public Task<ApiResult<bool>> Delete(int id);
        public Task<ApiResult<BlogDto>> GetById(int id);
        public Task<ApiResult<PagedResult<BlogDto>>> GetAll(int? pageSize, int? pageIndex, string?search);
        public Task<ApiResult<List<BlogDto>>> GetAll();
    }
}
