using AutoMapper;
using DataDemo.Common;
using Domain.Common;
using Domain.Models.Dto.ImportInvoiceDto;
using Domain.Models.Dto.Notifi;
using Domain.Models.Dto.Order;
using Domain.Models.Dto.Product;
using Infrastructure.Entities;
using Infrastructure.Reponsitories.OrderDetailReponsitory;
using Infrastructure.Reponsitories.OrderReponsitory;
using Infrastructure.Reponsitories.OrderTableRepository;
using Infrastructure.Reponsitories.TableReponsitory;
using Library.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Domain.Features.Order
{
    public class OrderService : IOrderService
    {
        private readonly IHubContext<NotiHub> _hubContext;
        private readonly IOrderDetailRepository _orderDetailReponsitory;
        private readonly IOrderRepository _orderReponsitory;
        private readonly IMapper _mapper;
        private readonly ITableRepository _tableRepository;
        private readonly IOrderTableRepository _orderTableRepository;

        public OrderService(IOrderTableRepository orderTableRepository, ITableRepository tableRepository, IHubContext<NotiHub> hubContext, IOrderRepository orderReponsitory, IOrderDetailRepository orderDetailReponsitory, IMapper mapper)
        {
            _mapper = mapper;
            _orderDetailReponsitory = orderDetailReponsitory;
            _orderReponsitory = orderReponsitory;
            _hubContext = hubContext;
            _mapper = mapper;
            _tableRepository = tableRepository;
            _orderTableRepository = orderTableRepository;
        }
        public void SendNotification()
        {
            _hubContext.Clients.All.SendAsync("ReceiveNotification", "Hello from ASP.NET Core!");
        }
        public async Task<ApiResult<OrderDto>> Create(OrderDto request)
        {
            var order = new Infrastructure.Entities.Order()
            {
                RestaurantID = request.RestaurantID,
                CreatedAt = DateTime.Now,
                Phone = request.Phone,
                UpdatedAt = DateTime.Now,
                Description = request.Description,
                Status = (int)EnumOrder.Cho,
                NumberOfCustomer = request.NumberOfCustomer,
                VAT = (double)VATORDER.Default,
                Payment = request.Payment,
                PriceTotal = request.PriceTotal,
                UserName = request.UserName,
                TableID = request.TableID,
                OrderID = request.OrderID,
                From =request.From,
                To =request.To,
            };
            var temp = new List<OrderDetail>();
            if (request.OrderDetailDtos != null)
            {
                foreach (var orderDetail in request.OrderDetailDtos)
                {
                    var _productOrder = new OrderDetail()
                    {
                        CreatedAt = DateTime.Now,
                        Price = orderDetail.Price,
                        Description = orderDetail.Description,
                        DishID = orderDetail.DishID,
                        NumberOfCustomer = orderDetail.NumberOfCustomer,
                        OrderID = orderDetail.OrderID,
                        UpdatedAt = DateTime.Now,
                    };
                    //sửa sản phẩm trong kho
                    temp.Add(_productOrder);
                }
                order.OrderDetails = (ICollection<OrderDetail>)temp;
            }
            try
            {
                //var tabel = _tableRepository.GetById(order.TableID);
                var result = await _orderReponsitory.CreateAsyncFLByOrder(order);
                request.OrderID = result.OrderID;
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<OrderDto>(ex.ToString());
            }
            return new ApiSuccessResult<OrderDto>(request);
        }
        public async Task<ApiResult<OrderDetailDto>> CreateDetail(OrderDetailDto request)
        {
            var order = new Infrastructure.Entities.OrderDetail()
            {
                CreatedAt = DateTime.Now,
                Price = request.Price,
                Description = request.Description,
                DishID = request.DishID,
                NumberOfCustomer = request.NumberOfCustomer,
                OrderID = request.OrderID,
                UpdatedAt = DateTime.Now,
            };
            try
            {
                await _orderDetailReponsitory.CreateAsync(order);
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<OrderDetailDto>(ex.ToString());
            }
            return new ApiSuccessResult<OrderDetailDto>(request);
        }
        [AllowAnonymous]
        public async Task<ApiResult<bool>> Delete(int id)
        {
            if (id > 0)
            {
                var findobj = await _orderReponsitory.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                await _orderReponsitory.DeleteAsync(findobj);
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }
        public async Task<ApiResult<bool>> DeleteDetail(int id)
        {
            if (id > 0)
            {
                var findobj = await _orderDetailReponsitory.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                await _orderDetailReponsitory.DeleteAsync(findobj);
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }
        public async Task<ApiResult<PagedResult<OrderDto>>> GetAll(int? pageSize, int? pageIndex, string search)
        {
            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            var totalRow = await _orderReponsitory.CountAsync();
            var query = await _orderReponsitory.GetAll(pageSize, pageIndex);
            if (!string.IsNullOrEmpty(search))
            {
                Expression<Func<Infrastructure.Entities.Order, bool>> expression2 = x => x.UserName.Contains(search);
                query = await _orderReponsitory.GetAll(pageSize, pageIndex, expression2);
                totalRow = await _orderReponsitory.CountAsync(expression2);
            }
            var data = _mapper.Map<List<OrderDto>>(query.ToList());
            var pagedResult = new PagedResult<OrderDto>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data,
                Status = true
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<OrderDto>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<OrderDto>>(pagedResult);
        }
        public async Task<ApiResult<List<OrderDetailDto>>> GetAllOrderDetail(int id)
        {
            Expression<Func<Infrastructure.Entities.OrderDetail, bool>> expression = x => x.OrderID == id;
            var query = await _orderDetailReponsitory.GetByCondition(expression);
            var data = (from orderDetailtbl in query
                        select new OrderDetailDto()
                        {
                            CreatedAt = orderDetailtbl.CreatedAt,
                            Price = orderDetailtbl.Price,
                            Description = orderDetailtbl.Description,
                            DishID = orderDetailtbl.DishID,
                            NumberOfCustomer = orderDetailtbl.NumberOfCustomer,
                            OrderID = orderDetailtbl.OrderID,
                            UpdatedAt = orderDetailtbl.UpdatedAt,
                        }).ToList();
            var temp = new List<OrderDetailDto>();
            if (data == null)
            {
                return new ApiErrorResult<List<OrderDetailDto>>("Khong co gi ca");
            }
            return new ApiSuccessResult<List<OrderDetailDto>>(data);
        }
        public async Task<ApiResult<bool>> Update(int id, OrderDto request)
        {
            if (id != null)
            {
                var findobj = await _orderReponsitory.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                findobj.Phone = request.Phone;
                findobj.RestaurantID = request.RestaurantID;
                findobj.UpdatedAt = DateTime.Now;
                findobj.Description = request.Description;
                findobj.Status = request.Status;
                findobj.NumberOfCustomer = request.NumberOfCustomer;
                //findobj.VAT = request.VAT;
                findobj.Payment = request.Payment;
                findobj.PriceTotal = request.PriceTotal;
                findobj.UserName = request.UserName;
                findobj.TableID = request.TableID;
                // lay ra ban
                //var tabel = await _tableRepository.GetById(request.TableID);
                //tabel.Status = 1;
                await _orderReponsitory.UpdateAsync(findobj);
                var getTable = await _tableRepository.GetById(request.TableID);
                //if (getTable != null)
                //{
                //    getTable.Status = 
                //    await _tableRepository.UpdateAsync(getTable);
                //}
                var orderDetail = await _orderDetailReponsitory.GetByCondition(x => x.OrderID == id);
                
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }
        public async Task<ApiResult<bool>> UpdateDetail(int id, OrderDetailDto request)
        {
            if (id != null)
            {
                var findobj = await _orderDetailReponsitory.GetById(id);
                if (findobj == null)
                {
                    return new ApiErrorResult<bool>("Không tìm thấy đối tượng");
                }
                findobj.OrderID = request.OrderID;
                findobj.Price = request.Price;
                findobj.Description = request.Description;
                findobj.DishID = request.OrderID;
                findobj.NumberOfCustomer = request.NumberOfCustomer;
                findobj.Quantity = request.Quantity;
                await _orderDetailReponsitory.UpdateAsync(findobj);
                var orderDetail = await _orderDetailReponsitory.GetByCondition(x => x.OrderID == id);
                return new ApiSuccessResult<bool>(true);
            }
            return new ApiErrorResult<bool>("Lỗi tham số chuyền về null hoặc trống");
        }
        public async Task<ApiResult<OrderDto>> GetById(int id)
        {
            
            try
            {
                var request = await _orderReponsitory.GetById(id);
                if (request == null)
                {
                    return null;
                }

                var obj = new OrderDto()
                {
                    CreatedAt = request.CreatedAt,
                    Phone = request.Phone,
                    UpdatedAt = request.UpdatedAt,
                    Description = request.Description,
                    Status = request.Status,
                    NumberOfCustomer = request.NumberOfCustomer,
                    VAT = request.VAT,
                    Payment = request.Payment,
                    PriceTotal = request.PriceTotal,
                    UserName = request.UserName,
                    TableID = request.TableID,
                    OrderID = request.OrderID,
                };
                return new ApiSuccessResult<OrderDto>(obj);
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<OrderDto>(ex.ToString());
            }
            
        }
        public async Task<ApiResult<OrderDetailDto>> GetByIdDetail(int id)
        {
            try
            {
                var request = await _orderDetailReponsitory.GetById(id);
                if (request == null)
                {
                    return null;
                }

                var obj = new OrderDetailDto()
                {
                    CreatedAt = request.CreatedAt,
                    Price = request.Price,
                    Description = request.Description,
                    DishID = request.DishID,
                    NumberOfCustomer = request.NumberOfCustomer,
                    OrderID = request.OrderID,
                    UpdatedAt = request.UpdatedAt,
                };
                return new ApiSuccessResult<OrderDetailDto>(obj);
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<OrderDetailDto>(ex.ToString());
            }

        }
        public async Task<ApiResult<List<OrderDetailDto>>> GetAll()
        {
            var query = await _orderDetailReponsitory.GetAll();
            var data = _mapper.Map<List<OrderDetailDto>>(query.ToList());
            var temp = new List<OrderDetailDto>();
            if (data == null)
            {
                return new ApiErrorResult<List<OrderDetailDto>>("Khong co gi ca");
            }
            return new ApiSuccessResult<List<OrderDetailDto>>(data);
        }
        public async Task<ApiResult<PagedResult<OrderDetailDto>>> GetAllOrderDetaill(int? pageSize, int? pageIndex, string search)
        {
            if (pageSize != null)
            {
                pageSize = pageSize.Value;
            }
            if (pageIndex != null)
            {
                pageIndex = pageIndex.Value;
            }
            var totalRow = await _orderDetailReponsitory.CountAsync();
            var query = await _orderDetailReponsitory.GetAll(pageSize, pageIndex);
            if (!string.IsNullOrEmpty(search))
            {
                Expression<Func<Infrastructure.Entities.OrderDetail, bool>> expression2 = x => x.Description.Contains(search);
                query = await _orderDetailReponsitory.GetAll(pageSize, pageIndex, expression2);
                totalRow = await _orderDetailReponsitory.CountAsync(expression2);
            }
            var queryTable = await _tableRepository.GetAllAsQueryable();
            var data = (from orderDetail in query
                        select new OrderDetailDto()
                        {
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now,
                            Description = orderDetail.Description,
                            NumberOfCustomer = orderDetail.NumberOfCustomer,
                            OrderID = orderDetail.OrderID,
                            DishID = orderDetail.DishID,
                            Id = orderDetail.Id,
                            Price = orderDetail.Price,
                        }).ToList();
            var pagedResult = new PagedResult<OrderDetailDto>()
            {
                TotalRecord = totalRow,
                PageSize = pageSize.Value,
                PageIndex = pageIndex.Value,
                Items = data,
                Status = true
            };
            if (pagedResult == null)
            {
                return new ApiErrorResult<PagedResult<OrderDetailDto>>("Khong co gi ca");
            }
            return new ApiSuccessResult<PagedResult<OrderDetailDto>>(pagedResult);
        }

        public async Task<ApiResult<List<OrderDto>>> GetAllOrder()
        {
            var query = await _orderReponsitory.GetAll();
            var data = _mapper.Map<List<OrderDto>>(query.ToList());
            return new ApiSuccessResult<List<OrderDto>>(data);
        }
    }
}
