using DataDemo.Common;
using Domain.Common;
using Domain.Models.Dto.ImportInvoiceDto;
using Domain.Models.Dto.Product;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features
{
    public interface IRestaurantService
    {
        public Task<ApiResult<bool>> Create(RestaurantDto request);
        public Task<ApiResult<bool>> Update(int id, RestaurantDto request);
        public Task<ApiResult<bool>> Delete(int id);
        public Task<ApiResult<PagedResult<RestaurantDto>>> GetByName(int? pageSize, int? pageIndex, string? search);
        public Task<ApiResult<RestaurantDto>> GetById(int Id);
        public Task<ApiResult<IEnumerable<RestaurantDto>>> GetAll();
    }
}
