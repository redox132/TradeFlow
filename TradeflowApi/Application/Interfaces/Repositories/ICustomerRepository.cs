namespace Tradeflow.TradeflowApi.Application.Interfaces.Repositories;

using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Customers;
using Tradeflow.TradeflowApi.Domain.Entities;

public interface ICustomerRepository
{
    Task<List<CustomerDTO>> GetCustomers(int pageNumber, int PageSize);
    Task<Customer> GetCustomer(int id);
    Task<Customer> AddCustomer(CustomerDTO request);
}