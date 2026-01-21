namespace Tradeflow.TradeflowApi.Application.Interfaces.Repositories;

using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Customers;
using Tradeflow.TradeflowApi.Domain.Entities;

public interface ICustomerService
{
    Task<IEnumerable<CustomerDTO>> GetCustomers(int pageNumber, int PageSize, int sellerId);
    // Backwards-compatible overloads
    Task<IEnumerable<CustomerDTO>> GetCustomers(int pageNumber, int PageSize);

    Task<Customer> GetCustomer(int id, int sellerId);
    Task<Customer> GetCustomer(int id);

    Task<Customer> AddCustomer(CustomerDTO customer, int sellerId);
    Task<Customer> AddCustomer(CustomerDTO customer);
}