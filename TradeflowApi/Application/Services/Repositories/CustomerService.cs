namespace Tradeflow.TradeflowApi.Application.Services.Repositories;
using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Customers;

using Tradeflow.TradeflowApi.Application.Interfaces.Repositories;
using Tradeflow.TradeflowApi.Domain.Entities;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<IEnumerable<CustomerDTO>> GetCustomers(int pageNumber, int PageSize, int sellerId)
    {
        return await _customerRepository.GetCustomers(pageNumber, PageSize, sellerId);
    }

    
    public async Task<IEnumerable<CustomerDTO>> GetCustomers(int pageNumber, int PageSize)
    {
        return await _customerRepository.GetCustomers(pageNumber, PageSize);
    }
    public async Task<Customer> GetCustomer(int id, int sellerId)
    {
        return await _customerRepository.GetCustomer(id, sellerId);
    }

    public async Task<Customer> GetCustomer(int id)
    {
        return await _customerRepository.GetCustomer(id);
    }

    public async Task<Customer> AddCustomer(CustomerDTO customer, int sellerId)
    {
        return await _customerRepository.AddCustomer(customer, sellerId);
    }
    
    public async Task<Customer> AddCustomer(CustomerDTO customer)
    {
        return await _customerRepository.AddCustomer(customer);
    }
}
