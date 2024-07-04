using AutoMapper;
using DataDemo.Common;
using Domain.Common;
using Domain.Features;
using Domain.Models.Dto.UserDto;
using Infrastructure.Entities;
using Infrastructure.Reponsitories.OperationReponsitories;
using Infrastructure.Reponsitories.UserOperationRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserOperationService : IUserOperationService
    {
        private readonly IUserOperationRepository _userOperationRepository;
        private readonly IMapper _mapper;

        public UserOperationService(IUserOperationRepository userOperationRepository, IMapper mapper)
        {
            _userOperationRepository = userOperationRepository;
            _mapper = mapper;
        }
        public async Task<UserOperationDto> Create(UserOperationDto request)
        {
            var obj = _mapper.Map<UserOperation>(request);
            await _userOperationRepository.CreateAsync(obj);
            request.Id = obj.Id;
            return request;
        }

        public async Task<bool> Delete(int id)
        {
            var obj = await _userOperationRepository.GetById(id);
            if (obj == null)
            {
                return false;
            }
            await _userOperationRepository.DeleteAsync(obj);
            return true;
        }

        public async Task<UserOperationDto> GetById(int id)
        {
            var obj = await _userOperationRepository.GetById(id);
            var map = _mapper.Map<UserOperationDto>(obj);
            return map;
        }

        public async Task<UserOperationDto> Update(int id, UserOperationDto request)
        {
            var map = _mapper.Map<UserOperation>(request);
            map.Id = id;
            await _userOperationRepository.UpdateAsync(map);
            return request;
        }
        public async Task<ApiResult<PagedResult<UserOperationDto>>> GetAll(int? pageSize, int? pageIndex, string search)
        {
            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            Expression<Func<Infrastructure.Entities.UserOperation, bool>> expression = x => x.IsAccess == true;
            var totalRow = await _userOperationRepository.CountAsync();
            var query = await _userOperationRepository.GetAll(pageSize, pageIndex, expression);
            //if (!string.IsNullOrEmpty(search))
            //{
            //    Expression<Func<Infrastructure.Entities.UserOperation, bool>> expression2 = x => x.Name.Contains(search) && x.IsAccess == true;
            //    query = await _userOperationRepository.GetAll(pageSize, pageIndex, expression2);
            //    totalRow = await _userOperationRepository.CountAsync(expression2);
            //}
            //Paging
            var data = _mapper.Map<List<UserOperationDto>>(query.ToList());
            var pagedResult = new PagedResult<UserOperationDto>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data,
                Status = true
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<UserOperationDto>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<UserOperationDto>>(pagedResult);
        }
    }
}
