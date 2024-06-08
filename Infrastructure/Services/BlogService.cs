using DataDemo.Common;
using Domain.Common;
using Domain.Features;
using Domain.Models.Dto.Blog;
using Infrastructure.Entities;
using Infrastructure.Reponsitories.BlogRepository;
using Infrastructure.Reponsitories.RoleReponsitories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace Infrastructure.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        public BlogService(IBlogRepository BlogRepository)
        {
            _blogRepository = BlogRepository;
        }
        public async Task<ApiResult<bool>> Create(BlogDto request)
        {
            var obj = new Infrastructure.Entities.Blog()
            {
                CreateAtBy=request.CreateAtBy,
                Status = request.Status,
                Description = request.Description,
                SubTitle = request.SubTitle,
                CreatedAt = request.CreatedAt,
                //IdBlog = request.IdBlog,
                Image = request.Image,
                Name = request.Name,
                Title = request.Title,
                UpdatedAt = request.UpdatedAt,
            };
            await _blogRepository.CreateAsync(obj);
            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            if (id > 0)
            {
                var findobj = await _blogRepository.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                await _blogRepository.DeleteAsync(findobj);
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }

        public async Task<ApiResult<PagedResult<BlogDto>>> GetAll(int? pageSize, int? pageIndex, string? search)
        {
            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            var totalRow = await _blogRepository.CountAsync();
            var query = await _blogRepository.GetAll(pageSize, pageIndex);
            if (!string.IsNullOrEmpty(search))
            {
                Expression<Func<Infrastructure.Entities.Blog, bool>> expression2 = x => x.Name.Contains(search);
                query = await _blogRepository.GetAll(pageSize, pageIndex, expression2);
                totalRow = await _blogRepository.CountAsync(expression2);
            }
            var data = query
                .Select(request => new BlogDto()
                {
                    CreateAtBy = request.CreateAtBy,
                    Status = request.Status,
                    Description = request.Description,
                    SubTitle = request.SubTitle,
                    CreatedAt = request.CreatedAt,
                    IdBlog = request.IdBlog,
                    Image = request.Image,
                    Name = request.Name,
                    Title = request.Title,
                    UpdatedAt = request.UpdatedAt,
                }).ToList();
            var pagedResult = new PagedResult<BlogDto>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data,
                Status = true
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<BlogDto>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<BlogDto>>(pagedResult);
        }

        public async Task<ApiResult<List<BlogDto>>> GetAll()
        {
            var totalRow = await _blogRepository.GetAll();
            var result = totalRow
                .Select(request => new BlogDto()
                {
                    CreateAtBy = request.CreateAtBy,
                    Status = request.Status,
                    Description = request.Description,
                    SubTitle = request.SubTitle,
                    CreatedAt = request.CreatedAt,
                    IdBlog = request.IdBlog,
                    Image = request.Image,
                    Name = request.Name,
                    Title = request.Title,
                    UpdatedAt = request.UpdatedAt,
                }).ToList();

            return new ApiSuccessResult<List<BlogDto>>(result);
        }

        public async Task<ApiResult<BlogDto>> GetById(int id)
        {
            if (id > 0)
            {
                var request = await _blogRepository.GetById(id);
                if (request == null)
                {
                    return null;
                }

                var obj = new BlogDto()
                {
                    CreateAtBy = request.CreateAtBy,
                    Status = request.Status,
                    Description = request.Description,
                    SubTitle = request.SubTitle,
                    CreatedAt = request.CreatedAt,
                    IdBlog = request.IdBlog,
                    Image = request.Image,
                    Name = request.Name,
                    Title = request.Title,
                    UpdatedAt = request.UpdatedAt,
                };
                return new ApiSuccessResult<BlogDto>(obj);
            }
            return new ApiErrorResult<BlogDto>("Lỗi tham số chuyền về null hoặc trống");
        }

        public async Task<ApiResult<bool>> Update(int id, BlogDto request)
        {
            if (id > 0)
            {
                var findobj = await _blogRepository.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                findobj.CreateAtBy = request.CreateAtBy;
                findobj.Status = request.Status;
                findobj.Description = request.Description;
                findobj.SubTitle = request.SubTitle;
                findobj.CreatedAt = request.CreatedAt;
                //findobj.IdBlog = request.IdBlog;
                findobj.Image = request.Image;
                findobj.Name = request.Name;
                findobj.Title = request.Title;
                findobj.UpdatedAt = request.UpdatedAt;
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }
    }
}
