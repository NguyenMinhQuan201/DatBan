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
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
                CreatedAt = DateTime.Now,
                Phone = request.Phone,
                UpdatedAt = DateTime.Now,
                Description = request.Description,
                DiscountID = request.DiscountID,
                NumberOfCustomer = request.NumberOfCustomer,
                VAT = request.VAT,
                Payment = request.Payment,
                PriceTotal = request.PriceTotal,
                UserName = request.UserName,
                TableID = request.TableID,
                OrderID = request.OrderID,
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
                        Payment = orderDetail.Payment,
                        TableID = orderDetail.TableID,
                        UpdatedAt = DateTime.Now,
                        UserID = orderDetail.UserID,
                    };
                    //sửa sản phẩm trong kho
                    temp.Add(_productOrder);
                }
                order.OrderDetails = (ICollection<OrderDetail>)temp;
            }
            try
            {
                var result = await _orderReponsitory.CreateAsyncFLByOrder(order);
                request.OrderID = result.OrderID;
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<OrderDto>(ex.ToString());
            }
            return new ApiSuccessResult<OrderDto>(request);
        }

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
            var data = query
                .Select(request => new OrderDto()
                {
                    CreatedAt = DateTime.Now,
                    Phone = request.Phone,
                    UpdatedAt = DateTime.Now,
                    Description = request.Description,
                    DiscountID = request.DiscountID,
                    NumberOfCustomer = request.NumberOfCustomer,
                    VAT = request.VAT,
                    Payment = request.Payment,
                    PriceTotal = request.PriceTotal,
                    UserName = request.UserName,
                    TableID = request.TableID,
                    OrderID = request.OrderID,
                }).ToList();
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
            var queryTable = await _tableRepository.GetAll();
            var queryTableOrder = await _orderTableRepository.GetAll();
            var data = (from orderDetailtbl in query
                        join tble in queryTable on orderDetailtbl.TableID equals tble.TableID
                        select new OrderDetailDto()
                        {
                            CreatedAt = orderDetailtbl.CreatedAt,
                            Price = orderDetailtbl.Price,
                            Description = orderDetailtbl.Description,
                            DishID = orderDetailtbl.DishID,
                            NumberOfCustomer = orderDetailtbl.NumberOfCustomer,
                            OrderID = orderDetailtbl.OrderID,
                            Payment = orderDetailtbl.Payment,
                            TableID = orderDetailtbl.TableID,
                            UpdatedAt = orderDetailtbl.UpdatedAt,
                            UserID = orderDetailtbl.UserID,
                            TableNumber = tble.TableNumber
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
                findobj.UpdatedAt = DateTime.Now;
                findobj.Description = request.Description;
                findobj.DiscountID = request.DiscountID;
                findobj.NumberOfCustomer = request.NumberOfCustomer;
                findobj.VAT = request.VAT;
                findobj.Payment = request.Payment;
                findobj.PriceTotal = request.PriceTotal;
                findobj.UserName = request.UserName;
                findobj.TableID = request.TableID;
                findobj.OrderID = request.OrderID;
                
                await _orderReponsitory.UpdateAsync(findobj);
                var orderDetail = await _orderDetailReponsitory.GetByCondition(x => x.OrderID == id);
                //foreach (var item in orderDetail)
                //{
                //    var pro = await _productReponsitory.GetByProductID(item.IdProduct);
                //    pro.Quantity = pro.Quantity - item.Quantity;
                //    await _productReponsitory.UpdateAsync(pro);
                //}
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
                    DiscountID = request.DiscountID,
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

        public async Task<ApiResult<List<OrderDetailDto>>> GetAll()
        {
            var query = await _orderDetailReponsitory.GetAll();
            var queryTable = await _tableRepository.GetAll();
            var queryTableOrder = await _orderTableRepository.GetAll();
            var data = (from orderDetailtbl in query
                        join tble in queryTable on orderDetailtbl.TableID equals tble.TableID
                        select new OrderDetailDto()
                        {
                            CreatedAt = orderDetailtbl.CreatedAt,
                            Price = orderDetailtbl.Price,
                            Description = orderDetailtbl.Description,
                            DishID = orderDetailtbl.DishID,
                            NumberOfCustomer = orderDetailtbl.NumberOfCustomer,
                            OrderID = orderDetailtbl.OrderID,
                            Payment = orderDetailtbl.Payment,
                            TableID = orderDetailtbl.TableID,
                            UpdatedAt = orderDetailtbl.UpdatedAt,
                            UserID = orderDetailtbl.UserID,
                            TableNumber = tble.TableNumber
                        }).ToList();
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
                        join table in queryTable on orderDetail.TableID equals table.TableID
                        select new OrderDetailDto()
                        {
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now,
                            Description = orderDetail.Description,
                            NumberOfCustomer = orderDetail.NumberOfCustomer,
                            Payment = orderDetail.Payment,
                            TableID = orderDetail.TableID,
                            OrderID = orderDetail.OrderID,
                            DishID = orderDetail.DishID,
                            Id = orderDetail.Id,
                            Price = orderDetail.Price,
                            TableNumber = table.TableNumber,
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
    }
}
