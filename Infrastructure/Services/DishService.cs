using DataDemo.Common;
using Domain.Common;
using Domain.Features;
using Domain.Models.Dto.ImportInvoiceDto;
using Infrastructure.Entities;
using Infrastructure.Reponsitories.CategoryReponsitories;
using Infrastructure.Reponsitories.DishReponsitories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Infrastructure.Services
{
    public class DishService : IDishService
    {
        private readonly IDishRepository _DishRepository;
        private readonly ICategoryRepository _categoryRepository;
        public DishService(IDishRepository DishRepository, ICategoryRepository categoryRepository)
        {
            _DishRepository = DishRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<ApiResult<bool>> Create(DishDto request)
        {
            var obj = new Infrastructure.Entities.Dish()
            {
                CreatedAt = request.CreatedAt,
                UpdatedAt = request.UpdatedAt,
                Description = request.Description,
                CategoryID = request.CategoryID,
                //Discount = request.Discount,
                Name = request.Name,
                Price = request.Price,
            };
            await _DishRepository.CreateAsync(obj);
            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            if (id > 0)
            {
                var findobj = await _DishRepository.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                await _DishRepository.DeleteAsync(findobj);
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }

        public async Task<ApiResult<PagedResult<DishDto>>> GetAll(int? pageSize, int? pageIndex, string? search)
        {
            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            var totalRow = await _DishRepository.CountAsync();
            var query = await _DishRepository.GetAll(pageSize, pageIndex);
            if (!string.IsNullOrEmpty(search))
            {
                Expression<Func<Infrastructure.Entities.Dish, bool>> expression2 = x => x.Name.Contains(search);
                query = await _DishRepository.GetAll(pageSize, pageIndex, expression2);
                totalRow = await _DishRepository.CountAsync(expression2);
            }
            var data = query
                .Select(request => new DishDto()
                {
                    CreatedAt = request.CreatedAt,
                    UpdatedAt = request.UpdatedAt,
                    Description = request.Description,
                    CategoryID = request.CategoryID,
                    //Discount = request.Discount,
                    Name = request.Name,
                    Price = request.Price,
                }).ToList();
            var pagedResult = new PagedResult<DishDto>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data,
                Status = true
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<DishDto>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<DishDto>>(pagedResult);
        }

        public async Task<ApiResult<List<DishDto>>> GetAll()
        {
            var totalRow = await _DishRepository.GetAll();
            var nameCategories = _categoryRepository.GetAllAsQueryable().Result.ToList();
            var result = totalRow
                .Select(request => new DishDto()
                {
                    CreatedAt = request.CreatedAt,
                    UpdatedAt = request.UpdatedAt,
                    Description = request.Description,
                    CategoryID = request.CategoryID,
                    //Discount = request.Discount,
                    Name = request.Name,
                    Price = request.Price,
                    DishId = request.DishId,
                    NameCate = nameCategories.Where(x=>x.IdCategory == request.CategoryID).FirstOrDefault()?.Name ?? "Không tên"
                }).ToList();

            return new ApiSuccessResult<List<DishDto>>(result);
        }

        public async Task<ApiResult<DishDto>> GetById(int id)
        {
            if (id > 0)
            {
                var request = await _DishRepository.GetById(id);
                if (request == null)
                {
                    return null;
                }

                var obj = new DishDto()
                {
                    CreatedAt = request.CreatedAt,
                    UpdatedAt = request.UpdatedAt,
                    Description = request.Description,
                    CategoryID = request.CategoryID,
                    //Discount = request.Discount,
                    Name = request.Name,
                    Price = request.Price,
                };
                return new ApiSuccessResult<DishDto>(obj);
            }
            return new ApiErrorResult<DishDto>("Lỗi tham số chuyền về null hoặc trống");
        }

        public async Task<ApiResult<bool>> Update(int id, DishDto request)
        {
            if (id > 0)
            {
                var findobj = await _DishRepository.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                findobj.CreatedAt = request.CreatedAt;
                findobj.UpdatedAt = request.UpdatedAt;
                findobj.Description = request.Description;
                findobj.CategoryID = request.CategoryID;
                //findobj.Discount = request.Discount;
                findobj.Name = request.Name;
                findobj.Price = request.Price;
                await _DishRepository.UpdateAsync(findobj);
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }
    }
}
