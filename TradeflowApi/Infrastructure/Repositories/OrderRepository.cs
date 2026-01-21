namespace Tradeflow.TradeflowApi.Infrastructure.Repositories;

using Tradeflow.TradeflowApi.Application.DTOs.Repositories.OrderItems;
using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Customers;
using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Addresses;
using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Countries;
using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Orders;
using Tradeflow.TradeflowApi.Application.Interfaces.Repositories;
using Tradeflow.TradeflowApi.Infrastructure.Repositories;
using Tradeflow.TradeflowApi.Infrastructure.Data;
using Tradeflow.TradeflowApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;


public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _appDbContext;

    public OrderRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<OrderDTO>> GetOrdersAsync(int sellerId, int pageNumber, int pageSize)
    {
        pageNumber = pageNumber <= 0 ? 1 : pageNumber;
        pageSize = pageSize <= 0 ? 10 : pageSize;

        return await _appDbContext.Orders
            .Where(o => o.SellerId == sellerId)
            .AsNoTracking()
            .OrderBy(o => o.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(o => new OrderDTO
            {
                Id = o.Id,
                CreatedAt = o.CreatedAt,
                UpdatedAt = o.UpdatedAt,
                Status = o.Status,

                OrderItems = o.OrderItems.Select(oi => new OrderItemsDTO
                {
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList(),

                CustomerDTO = new CustomerDTO
                {
                    First_name = o.Customer.FName,
                    Last_name = o.Customer.LName,
                    Email = o.Customer.Email,
                    Address_detaills = o.Customer.Address == null ? null : new AddressDTO
                    {
                        Street = o.Customer.Address.Street,
                        HouseNumber = o.Customer.Address.HouseNumber,
                        FlatNumber = o.Customer.Address.FlatNumber,
                        PostCode = o.Customer.Address.PostCode,
                        City = o.Customer.Address.City,
                        State = o.Customer.Address.State,
                        Country_details = o.Customer.Address.Country == null ? null : new CountryDTO
                        {
                            Name = o.Customer.Address.Country.Name,
                            Code = o.Customer.Address.Country.Code
                        }
                    }
                }
            })
            .ToListAsync();
    }

    // Backwards-compatible overload: return all orders (no seller filtering)
    public async Task<List<OrderDTO>> GetOrdersAsync(int pageNumber, int pageSize)
    {
        pageNumber = pageNumber <= 0 ? 1 : pageNumber;
        pageSize = pageSize <= 0 ? 10 : pageSize;

        return await _appDbContext.Orders
            .AsNoTracking()
            .OrderBy(o => o.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(o => new OrderDTO
            {
                Id = o.Id,
                CreatedAt = o.CreatedAt,
                UpdatedAt = o.UpdatedAt,
                Status = o.Status,

                OrderItems = o.OrderItems.Select(oi => new OrderItemsDTO
                {
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList(),

                CustomerDTO = new CustomerDTO
                {
                    First_name = o.Customer.FName,
                    Last_name = o.Customer.LName,
                    Email = o.Customer.Email,
                    Address_detaills = o.Customer.Address == null ? null : new AddressDTO
                    {
                        Street = o.Customer.Address.Street,
                        HouseNumber = o.Customer.Address.HouseNumber,
                        FlatNumber = o.Customer.Address.FlatNumber,
                        PostCode = o.Customer.Address.PostCode,
                        City = o.Customer.Address.City,
                        State = o.Customer.Address.State,
                        Country_details = o.Customer.Address.Country == null ? null : new CountryDTO
                        {
                            Name = o.Customer.Address.Country.Name,
                            Code = o.Customer.Address.Country.Code
                        }
                    }
                }
            })
            .ToListAsync();
    }



    public async Task<List<OrderDTO>> GetOrderByIdAsync(int sellerId, int id)
    {
        var order = await _appDbContext.Orders
            .Where(o => o.Id == id && o.SellerId == sellerId)
            .Select(o => new OrderDTO
            {
                Id = o.Id,
                CreatedAt = o.CreatedAt,
                Status = o.Status,

                OrderItems = o.OrderItems.Select(oi => new OrderItemsDTO
                {
                    ProductId = oi.ProductId,
                    Price = oi.Price,
                    Quantity = oi.Quantity
                }).ToList(),

                CustomerDTO = new CustomerDTO
                {
                    First_name = o.Customer.FName,
                    Last_name = o.Customer.LName,
                    Email = o.Customer.Email,

                    Address_detaills = o.Customer.Address == null ? null : new AddressDTO
                    {
                        Street = o.Customer.Address.Street,
                        HouseNumber = o.Customer.Address.HouseNumber,
                        FlatNumber = o.Customer.Address.FlatNumber,
                        PostCode = o.Customer.Address.PostCode,
                        City = o.Customer.Address.City,
                        State = o.Customer.Address.State,

                        Country_details = o.Customer.Address.Country == null ? null : new CountryDTO
                        {
                            Name = o.Customer.Address.Country.Name,
                            Code = o.Customer.Address.Country.Code
                        }
                    }
                }

            }).ToListAsync();

        return order;
    }

    // Backwards-compatible overload
    public async Task<List<OrderDTO>> GetOrderByIdAsync(int id)
    {
        var order = await _appDbContext.Orders
            .Where(o => o.Id == id)
            .Select(o => new OrderDTO
            {
                Id = o.Id,
                CreatedAt = o.CreatedAt,
                Status = o.Status,

                OrderItems = o.OrderItems.Select(oi => new OrderItemsDTO
                {
                    ProductId = oi.ProductId,
                    Price = oi.Price,
                    Quantity = oi.Quantity
                }).ToList(),

                CustomerDTO = new CustomerDTO
                {
                    First_name = o.Customer.FName,
                    Last_name = o.Customer.LName,
                    Email = o.Customer.Email,

                    Address_detaills = o.Customer.Address == null ? null : new AddressDTO
                    {
                        Street = o.Customer.Address.Street,
                        HouseNumber = o.Customer.Address.HouseNumber,
                        FlatNumber = o.Customer.Address.FlatNumber,
                        PostCode = o.Customer.Address.PostCode,
                        City = o.Customer.Address.City,
                        State = o.Customer.Address.State,

                        Country_details = o.Customer.Address.Country == null ? null : new CountryDTO
                        {
                            Name = o.Customer.Address.Country.Name,
                            Code = o.Customer.Address.Country.Code
                        }
                    }
                }

            }).ToListAsync();

        return order;
    }

    public async Task<OrderResponse> CreateOrderAsync(CreateOrderRequest request, int sellerId)
    {
        // Map DTO → entity
        var order = new Order
        {
            SellerId = sellerId,
            CustomerId = request.CustomerDTO.Id,
            CreatedAt = DateTime.UtcNow,
            OrderItems = request.OrderItems.Select(oi => new OrderItem
            {
                ProductId = oi.ProductId,
                Quantity = oi.Quantity,
                Price = oi.Price
            }).ToList()
        };

        _appDbContext.Orders.Add(order);
        await _appDbContext.SaveChangesAsync();

        // Map entity → response DTO (without Id)
        return new OrderResponse
        {
            CreatedAt = order.CreatedAt,
            OrderItems = order.OrderItems.Select(oi => new OrderItemsDTO
            {
                ProductId = oi.ProductId,
                Quantity = oi.Quantity,
                Price = oi.Price
            }).ToList(),
            CustomerDTO = request.CustomerDTO
        };
    }


    public async Task<Order> DeleteOrderAsync(int sellerId, int id)
    {
        var order = await _appDbContext.Orders.FirstOrDefaultAsync(o => o.Id == id && o.SellerId == sellerId);
        if (order != null)
        {
            _appDbContext.Orders.Remove(order);
            await _appDbContext.SaveChangesAsync();
        }
        return order;
    }

    // Backwards-compatible overloads
    public async Task<OrderResponse> CreateOrderAsync(CreateOrderRequest request)
    {
        // create without setting SellerId
        var order = new Order
        {
            CustomerId = request.CustomerDTO.Id,
            CreatedAt = DateTime.UtcNow,
            OrderItems = request.OrderItems.Select(oi => new OrderItem
            {
                ProductId = oi.ProductId,
                Quantity = oi.Quantity,
                Price = oi.Price
            }).ToList()
        };

        _appDbContext.Orders.Add(order);
        await _appDbContext.SaveChangesAsync();

        return new OrderResponse
        {
            CreatedAt = order.CreatedAt,
            OrderItems = order.OrderItems.Select(oi => new OrderItemsDTO
            {
                ProductId = oi.ProductId,
                Quantity = oi.Quantity,
                Price = oi.Price
            }).ToList(),
            CustomerDTO = request.CustomerDTO
        };
    }

    public async Task<Order> DeleteOrderAsync(int id)
    {
        var order = await _appDbContext.Orders.FindAsync(id);
        if (order != null)
        {
            _appDbContext.Orders.Remove(order);
            await _appDbContext.SaveChangesAsync();
        }
        return order;
    }
}