using DataDemo.Common;
using Domain.Common;
using Domain.Features;
using Domain.Features.Supplier.Dto;
using Domain.Models.Dto.ImportInvoiceDto;
using Infrastructure.Entities;
using Infrastructure.Reponsitories.TableReponsitory;
using Library.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class TableService : ITableService
    {
        private readonly ITableRepository _TableRepository;
        public TableService(ITableRepository TableRepository)
        {
            _TableRepository = TableRepository;
        }
        public async Task<ApiResult<bool>> Create(TableDto request)
        {
            var obj = new Infrastructure.Entities.Table()
            {
                CreatedAt = request.CreatedAt,
                UpdatedAt = request.UpdatedAt,
                TableNumber = request.TableNumber,
                Status = (int)EnumTable.Trong,
                AreaID = request.AreaID,
            };
            await _TableRepository.CreateAsync(obj);
            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            if (id > 0)
            {
                var findobj = await _TableRepository.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                await _TableRepository.DeleteAsync(findobj);
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }

        public async Task<ApiResult<PagedResult<TableDto>>> GetAll(int? pageSize, int? pageIndex, string? search)
        {
            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            var totalRow = await _TableRepository.CountAsync();
            var query = await _TableRepository.GetAll(pageSize, pageIndex);
            //if (!string.IsNullOrEmpty(search))
            //{
            //    Expression<Func<Infrastructure.Entities.Table, bool>> expression2 = x => x.TableNumber.Contains(search);
            //    query = await _TableRepository.GetAll(pageSize, pageIndex, expression2);
            //    totalRow = await _TableRepository.CountAsync(expression2);
            //}
            var data = query
                .Select(request => new TableDto()
                {
                    CreatedAt = request.CreatedAt,
                    UpdatedAt = request.UpdatedAt,
                    TableNumber = request.TableNumber,
                    Status = request.Status,
                    AreaID = request.AreaID,
                    TableID = request.TableID,

                }).ToList();
            var pagedResult = new PagedResult<TableDto>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data,
                Status = true
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<TableDto>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<TableDto>>(pagedResult);
        }

        public async Task<ApiResult<List<TableDto>>> GetAll()
        {
            var totalRow = await _TableRepository.GetAll();
            var result = totalRow
                .Select(request => new TableDto()
                {
                    CreatedAt = request.CreatedAt,
                    UpdatedAt = request.UpdatedAt,
                    TableNumber = request.TableNumber,
                    Status = request.Status,
                    AreaID = request.AreaID,
                    TableID = request.TableID,
                }).ToList();

            return new ApiSuccessResult<List<TableDto>>(result);
        }

        public async Task<ApiResult<TableDto>> GetById(int id)
        {
            if (id > 0)
            {
                var request = await _TableRepository.GetById(id);
                if (request == null)
                {
                    return null;
                }

                var obj = new TableDto()
                {
                    CreatedAt = request.CreatedAt,
                    UpdatedAt = request.UpdatedAt,
                    TableNumber = request.TableNumber,
                    Status = request.Status,
                    AreaID = request.AreaID,
                };
                return new ApiSuccessResult<TableDto>(obj);
            }
            return new ApiErrorResult<TableDto>("Lỗi tham số chuyền về null hoặc trống");
        }

        public async Task<ApiResult<bool>> Update(int id, TableDto request)
        {
            if (id > 0)
            {
                var findobj = await _TableRepository.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                findobj.CreatedAt = request.CreatedAt;
                findobj.UpdatedAt = request.UpdatedAt;
                findobj.TableNumber = request.TableNumber;
                findobj.Status = request.Status;
                findobj.AreaID = request.AreaID;
                await _TableRepository.UpdateAsync(findobj);
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }
    }
}
