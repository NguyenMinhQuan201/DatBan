using DataDemo.Common;
using Domain.Common;
using Domain.Models.Dto.ImportInvoiceDto;
using Domain.Models.Dto.Notifi;
using Domain.Models.Dto.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Features
{
    public interface IOrderService
    {
        public Task<ApiResult<OrderDto>> Create(OrderDto request);
        public Task<ApiResult<bool>> Update(int id, OrderDto request);
        public Task<ApiResult<bool>> Delete(int id);
        public Task<ApiResult<OrderDto>> GetById(int id);
        public Task<ApiResult<PagedResult<OrderDto>>> GetAll(int? pageSize, int? pageIndex, string search);
        public Task<ApiResult<List<OrderDetailDto>>> GetAllOrderDetail(int id);
        public Task<ApiResult<List<DishDto>>> GetAll();
    }
}
