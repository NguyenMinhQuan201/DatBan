using DataDemo.Common;
using Domain.Common;
using Domain.Features;
using Domain.Models.Dto.Category;
using Infrastructure.Entities;
using Infrastructure.Reponsitories.CategoryReponsitories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _CategoryRepository;
        public CategoryService(ICategoryRepository CategoryRepository)
        {
            _CategoryRepository = CategoryRepository;
        }
        public async Task<ApiResult<bool>> Create(CategoryDto request)
        {
            var obj = new Infrastructure.Entities.Category()
            {
                Status = request.Status,
                CreatedAt = request.CreatedAt,
                Icon = request.Icon,
                IdCategory = request.IdCategory,
                Name = request.Name,
                UpdatedAt = request.UpdatedAt,
            };
            await _CategoryRepository.CreateAsync(obj);
            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            if (id > 0)
            {
                var findobj = await _CategoryRepository.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                await _CategoryRepository.DeleteAsync(findobj);
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }

        public async Task<ApiResult<PagedResult<CategoryDto>>> GetAll(int? pageSize, int? pageIndex, string? search)
        {
            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            var totalRow = await _CategoryRepository.CountAsync();
            var query = await _CategoryRepository.GetAll(pageSize, pageIndex);
            if (!string.IsNullOrEmpty(search))
            {
                Expression<Func<Infrastructure.Entities.Category, bool>> expression2 = x => x.Name.Contains(search);
                query = await _CategoryRepository.GetAll(pageSize, pageIndex, expression2);
                totalRow = await _CategoryRepository.CountAsync(expression2);
            }
            var data = query
                .Select(request => new CategoryDto()
                {
                    Status = request.Status,
                    CreatedAt = request.CreatedAt,
                    Icon = request.Icon,
                    IdCategory = request.IdCategory,
                    Name = request.Name,
                    UpdatedAt = request.UpdatedAt,
                }).ToList();
            var pagedResult = new PagedResult<CategoryDto>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data,
                Status = true
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<CategoryDto>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<CategoryDto>>(pagedResult);
        }

        public async Task<ApiResult<List<CategoryDto>>> GetAll()
        {
            var totalRow = await _CategoryRepository.GetAll();
            var result = totalRow
                .Select(request => new CategoryDto()
                {
                    Status = request.Status,
                    CreatedAt = request.CreatedAt,
                    Icon = request.Icon,
                    IdCategory = request.IdCategory,
                    Name = request.Name,
                    UpdatedAt = request.UpdatedAt,
                }).ToList();

            return new ApiSuccessResult<List<CategoryDto>>(result);
        }

        public async Task<ApiResult<CategoryDto>> GetById(int id)
        {
            if (id > 0)
            {
                var request = await _CategoryRepository.GetById(id);
                if (request == null)
                {
                    return null;
                }

                var obj = new CategoryDto()
                {
                    Status = request.Status,
                    CreatedAt = request.CreatedAt,
                    Icon = request.Icon,
                    IdCategory = request.IdCategory,
                    Name = request.Name,
                    UpdatedAt = request.UpdatedAt,
                };
                return new ApiSuccessResult<CategoryDto>(obj);
            }
            return new ApiErrorResult<CategoryDto>("Lỗi tham số chuyền về null hoặc trống");
        }

        public async Task<ApiResult<bool>> Update(int id, CategoryDto request)
        {
            if (id > 0)
            {
                var findobj = await _CategoryRepository.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                findobj.Status = request.Status;
                findobj.CreatedAt = request.CreatedAt;
                findobj.Icon = request.Icon;
                //findobj.IdCategory = request.IdCategory;
                findobj.Name = request.Name;
                findobj.UpdatedAt = request.UpdatedAt;
                await _CategoryRepository.UpdateAsync(findobj);
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }
    }
}
