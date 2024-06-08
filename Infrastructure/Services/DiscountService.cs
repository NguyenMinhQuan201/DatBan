using DataDemo.Common;
using Domain.Common;
using Domain.Features;
using Domain.Models.Dto.Discount;
using Infrastructure.Reponsitories.DiscountReponsitories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _DiscountRepository;
        public DiscountService(IDiscountRepository DiscountRepository)
        {
            _DiscountRepository = DiscountRepository;
        }
        public async Task<ApiResult<bool>> Create(DiscountDto request)
        {
            var obj = new Infrastructure.Entities.Discount()
            {
                Status = request.Status,
                CreatedAt = request.CreatedAt,
                DiscountName=request.DiscountName,
                UpdatedAt = request.UpdatedAt,
            };
            await _DiscountRepository.CreateAsync(obj);
            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            if (id > 0)
            {
                var findobj = await _DiscountRepository.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                await _DiscountRepository.DeleteAsync(findobj);
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }

        public async Task<ApiResult<PagedResult<DiscountDto>>> GetAll(int? pageSize, int? pageIndex, string? search)
        {
            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            var totalRow = await _DiscountRepository.CountAsync();
            var query = await _DiscountRepository.GetAll(pageSize, pageIndex);
            if (!string.IsNullOrEmpty(search))
            {
                Expression<Func<Infrastructure.Entities.Discount, bool>> expression2 = x => x.DiscountName.Contains(search);
                query = await _DiscountRepository.GetAll(pageSize, pageIndex, expression2);
                totalRow = await _DiscountRepository.CountAsync(expression2);
            }
            var data = query
                .Select(request => new DiscountDto()
                {
                    Status = request.Status,
                    CreatedAt = request.CreatedAt,
                    DiscountName = request.DiscountName,
                    UpdatedAt = request.UpdatedAt,
                }).ToList();
            var pagedResult = new PagedResult<DiscountDto>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data,
                Status = true
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<DiscountDto>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<DiscountDto>>(pagedResult);
        }

        public async Task<ApiResult<List<DiscountDto>>> GetAll()
        {
            var totalRow = await _DiscountRepository.GetAll();
            var result = totalRow
                .Select(request => new DiscountDto()
                {
                    Status = request.Status,
                    CreatedAt = request.CreatedAt,
                    DiscountName = request.DiscountName,
                    UpdatedAt = request.UpdatedAt,
                }).ToList();

            return new ApiSuccessResult<List<DiscountDto>>(result);
        }

        public async Task<ApiResult<DiscountDto>> GetById(int id)
        {
            if (id > 0)
            {
                var request = await _DiscountRepository.GetById(id);
                if (request == null)
                {
                    return null;
                }

                var obj = new DiscountDto()
                {
                    Status = request.Status,
                    CreatedAt = request.CreatedAt,
                    DiscountName = request.DiscountName,
                    UpdatedAt = request.UpdatedAt,
                };
                return new ApiSuccessResult<DiscountDto>(obj);
            }
            return new ApiErrorResult<DiscountDto>("Lỗi tham số chuyền về null hoặc trống");
        }

        public async Task<ApiResult<bool>> Update(int id, DiscountDto request)
        {
            if (id > 0)
            {
                var findobj = await _DiscountRepository.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                findobj.Status = request.Status;
                findobj.CreatedAt = request.CreatedAt;
                findobj.DiscountName = request.DiscountName;
                findobj.UpdatedAt = request.UpdatedAt;
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }
    }
}
