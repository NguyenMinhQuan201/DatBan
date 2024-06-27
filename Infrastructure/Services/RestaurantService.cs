using DataDemo.Common;
using Domain.Common;
using Domain.Features.Supplier.Dto;
using Domain.Features;
using Infrastructure.Reponsitories.RestaurantReponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Dto.Product;

namespace Infrastructure.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _RestaurantRepository;
        public RestaurantService(IRestaurantRepository RestaurantRepository)
        {
            _RestaurantRepository = RestaurantRepository;
        }
        public async Task<ApiResult<bool>> Create(RestaurantDto request)
        {
            var obj = new Infrastructure.Entities.Restaurant()
            {
                CreatedAt = request.CreatedAt,
                UpdatedAt = request.UpdatedAt,
                Address = request.Address,
                Status = request.Status
            };
            await _RestaurantRepository.CreateAsync(obj);
            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            if (id > 0)
            {
                var findobj = await _RestaurantRepository.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                await _RestaurantRepository.DeleteAsync(findobj);
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }

        public async Task<ApiResult<PagedResult<RestaurantDto>>> GetByName(int? pageSize, int? pageIndex, string? search)
        {
            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            var totalRow = await _RestaurantRepository.CountAsync();
            var query = await _RestaurantRepository.GetAll(pageSize, pageIndex);
            //if (!string.IsNullOrEmpty(search))
            //{
            //    Expression<Func<Infrastructure.Entities.Restaurant, bool>> expression2 = x => x.RestaurantNumber.Contains(search);
            //    query = await _RestaurantRepository.GetAll(pageSize, pageIndex, expression2);
            //    totalRow = await _RestaurantRepository.CountAsync(expression2);
            //}
            var data = query
                .Select(request => new RestaurantDto()
                {
                    CreatedAt = request.CreatedAt,
                    UpdatedAt = request.UpdatedAt,
                    Address = request.Address,
                    Status = request.Status,
                    RestaurantID = request.RestaurantID,
                    //Areas = (List<Entities.AreaDto>)request.Areas
                }).ToList();
            var pagedResult = new PagedResult<RestaurantDto>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data,
                Status = true
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<RestaurantDto>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<RestaurantDto>>(pagedResult);
        }

        public async Task<ApiResult<List<RestaurantDto>>> GetAll()
        {
            var totalRow = await _RestaurantRepository.GetAll();
            var result = totalRow
                .Select(request => new RestaurantDto()
                {
                    CreatedAt = request.CreatedAt,
                    UpdatedAt = request.UpdatedAt,
                    Address = request.Address,
                    Status = request.Status,
                    RestaurantID = request.RestaurantID,
                    //Areas = (List<Entities.AreaDto>)request.Areas
                }).ToList();

            return new ApiSuccessResult<List<RestaurantDto>>(result);
        }

        public async Task<ApiResult<RestaurantDto>> GetById(int id)
        {
            if (id > 0)
            {
                var request = await _RestaurantRepository.GetById(id);
                if (request == null)
                {
                    return null;
                }

                var obj = new RestaurantDto()
                {
                    CreatedAt = request.CreatedAt,
                    UpdatedAt = request.UpdatedAt,
                    Address = request.Address,
                    Status = request.Status,
                    RestaurantID = request.RestaurantID,
                    //Areas = (List<Entities.AreaDto>)request.Areas
                };
                return new ApiSuccessResult<RestaurantDto>(obj);
            }
            return new ApiErrorResult<RestaurantDto>("Lỗi tham số chuyền về null hoặc trống");
        }
        public async Task<ApiResult<bool>> Update(int id, RestaurantDto request)
        {
            if (id > 0)
            {
                var findobj = await _RestaurantRepository.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                findobj.CreatedAt = request.CreatedAt;
                findobj.UpdatedAt = request.UpdatedAt;
                findobj.Address = request.Address;
                findobj.Status = request.Status;
                await _RestaurantRepository.UpdateAsync(findobj);
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }
    }
}
