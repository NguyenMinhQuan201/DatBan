using DataDemo.Common;
using Domain.Common;
using Domain.Models.Dto.Category;
using Domain.Models.Dto.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features
{
    public interface IDiscountService
    {
        public Task<ApiResult<bool>> Create(DiscountDto request);
        public Task<ApiResult<bool>> Update(int id, DiscountDto request);
        public Task<ApiResult<bool>> Delete(int id);
        public Task<ApiResult<DiscountDto>> GetById(int id);
        public Task<ApiResult<PagedResult<DiscountDto>>> GetAll(int? pageSize, int? pageIndex, string search);
        public Task<ApiResult<List<DiscountDto>>> GetAll();
    }
}
