using DataDemo.Common;
using Domain.Common;
using Domain.Features;
using Domain.Models.Dto.Blog;
using Infrastructure.Entities;
using Infrastructure.Reponsitories.RoleReponsitories;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure.Services
{
    public class AreaService : IAreaService
    {
        private readonly IAreaRepository _areaRepository;
        public AreaService(IAreaRepository areaRepository)
        {
            _areaRepository = areaRepository;
        }
        public async Task<ApiResult<bool>> Create(AreaDto request)
        {
            var obj = new Infrastructure.Entities.Area()
            {
                AreaName = request.AreaName,
                CreatedAt = request.CreatedAt,
                RestaurantID = request.RestaurantID,
                AreaID = request.AreaID,
                UpdatedAt = request.UpdatedAt,
            };
            await _areaRepository.CreateAsync(obj);
            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {

            if (id > 0)
            {
                var findobj = await _areaRepository.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                await _areaRepository.DeleteAsync(findobj);
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }

        public async Task<ApiResult<PagedResult<AreaDto>>> GetAll(int? pageSize, int? pageIndex, string? search)
        {
            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            var totalRow = await _areaRepository.CountAsync();
            var query = await _areaRepository.GetAll(pageSize, pageIndex);
            if (!string.IsNullOrEmpty(search))
            {
                Expression<Func<Infrastructure.Entities.Area, bool>> expression2 = x => x.AreaName.Contains(search);
                query = await _areaRepository.GetAll(pageSize, pageIndex, expression2);
                totalRow = await _areaRepository.CountAsync(expression2);
            }
            var data = query
                .Select(x => new AreaDto()
                {
                    AreaName = x.AreaName,
                    CreatedAt = x.CreatedAt,
                    RestaurantID = x.RestaurantID,
                    AreaID = x.AreaID,
                    UpdatedAt = x.UpdatedAt,
                }).ToList();
            var pagedResult = new PagedResult<AreaDto>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data,
                Status = true
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<AreaDto>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<AreaDto>>(pagedResult);
        }

        public async Task<ApiResult<List<AreaDto>>> GetAll()
        {
            var totalRow = await _areaRepository.GetAll();
            var result = totalRow
                .Select(x => new AreaDto()
                {
                    AreaName = x.AreaName,
                    CreatedAt = x.CreatedAt,
                    RestaurantID = x.RestaurantID,
                    AreaID = x.AreaID,
                    UpdatedAt = x.UpdatedAt,
                }).ToList();

            return new ApiSuccessResult<List<AreaDto>>(result);
        }

        public async Task<ApiResult<AreaDto>> GetById(int Id)
        {
            if (Id > 0 )
            {
                var findobj = await _areaRepository.GetById(Id);
                if (findobj == null)
                {
                    return null;
                }

                var obj = new AreaDto()
                {
                    AreaName = findobj.AreaName,
                    CreatedAt = findobj.CreatedAt,
                    RestaurantID = findobj.RestaurantID,
                    AreaID = findobj.AreaID,
                    UpdatedAt = findobj.UpdatedAt,
                };
                return new ApiSuccessResult<AreaDto>(obj);
            }
            return new ApiErrorResult<AreaDto>("Lỗi tham số chuyền về null hoặc trống");
        }

        public Task<ApiResult<bool>> Restore(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<bool>> Update(int id, AreaDto request)
        {
            if (id >0)
            {
                var findobj = await _areaRepository.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                findobj.AreaName = request.AreaName;
                findobj.CreatedAt = request.CreatedAt;
                findobj.RestaurantID = request.RestaurantID;
                //findobj.AreaID = request.AreaID;
                findobj.UpdatedAt = request.UpdatedAt;
                await _areaRepository.UpdateAsync(findobj);
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }
    }
}
