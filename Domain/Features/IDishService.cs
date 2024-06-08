using DataDemo.Common;
using Domain.Common;
using Domain.Models.Dto.Discount;
using Domain.Models.Dto.ImportInvoice;
using Domain.Models.Dto.ImportInvoiceDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features
{
    public interface IDishService
    {
        public Task<ApiResult<bool>> Create(DishDto request);
        public Task<ApiResult<bool>> Update(int id, DishDto request);
        public Task<ApiResult<bool>> Delete(int id);
        public Task<ApiResult<PagedResult<DishDto>>> GetAll(int? pageSize, int? pageIndex, string? name);
        public Task<ApiResult<DishDto>> GetById(int Id);
        public Task<ApiResult<List<DishDto>>> GetAll();
    }
}
