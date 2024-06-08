using DataDemo.Common;
using Domain.Common;
using Domain.Features.Supplier.Dto;
using Domain.Models.Dto.Product;
using Domain.Models.Dto.Rating;
using Domain.Models.Dto.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features
{
    public interface ITableService
    {
        public Task<ApiResult<bool>> Create(TableDto request);
        public Task<ApiResult<bool>> Update(int id, TableDto request);
        public Task<ApiResult<bool>> Delete(int id);
        public Task<ApiResult<PagedResult<TableDto>>> GetByName(int? pageSize, int? pageIndex, string? search);
        public Task<ApiResult<TableDto>> GetById(int Id);
        public Task<ApiResult<List<TableDto>>> GetAll();
    }
}
