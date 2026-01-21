namespace Tradeflow.TradeflowApi.Application.Interfaces.Repositories;

using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Customers;
using Tradeflow.TradeflowApi.Domain.Entities;

public interface ICustomerRepository
{
    Task<List<CustomerDTO>> GetCustomers(int pageNumber, int pageSize, int sellerId);
    // Backwards-compatible overloads
    Task<List<CustomerDTO>> GetCustomers(int pageNumber, int pageSize);

    Task<Customer> GetCustomer(int id, int sellerId);
    Task<Customer> GetCustomer(int id);

    Task<Customer> AddCustomer(CustomerDTO request, int sellerId);
    Task<Customer> AddCustomer(CustomerDTO request);
}