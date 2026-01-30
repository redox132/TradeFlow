namespace Tradeflow.TradeflowApi.Infrastructure.Repositories;

using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Addresses;
using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Customers;
using Tradeflow.TradeflowApi.Application.DTOs.Repositories.Countries;
using Tradeflow.TradeflowApi.Application.Interfaces.Repositories;
using Tradeflow.TradeflowApi.Infrastructure.Data;
using Tradeflow.TradeflowApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _appDbContext;

    public CustomerRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    // Seller-scoped paged query
    public async Task<List<CustomerDTO>> GetCustomers(int pageNumber, int pageSize, int sellerId)
    {
        return await _appDbContext.Customers
            .Where(c => c.SellerId == sellerId)
            .AsNoTracking()
            .OrderBy(c => c.Id) 
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(c => new CustomerDTO
            {
                First_name = c.FName,
                Last_name = c.LName,
                Email = c.Email,
                Address_detaills = c.Address == null ? null : new AddressDTO
                {
                    Street = c.Address.Street,
                    HouseNumber = c.Address.HouseNumber,
                    FlatNumber = c.Address.FlatNumber,
                    PostCode = c.Address.PostCode,
                    City = c.Address.City,
                    State = c.Address.State,
                    Country_details = c.Address.Country == null ? null : new CountryDTO
                    {
                        Name = c.Address.Country.Name,
                        Code = c.Address.Country.Code
                    }
                }
            })
            .ToListAsync();
    }

    public async Task<Customer> GetCustomer(int id, int sellerId)
    {
        return await _appDbContext.Customers
            .Where(c => c.Id == id && c.SellerId == sellerId)
            .Select(c => new Customer
            {
                FName = c.FName,
                LName = c.LName,
                Email = c.Email,
            })
            .FirstOrDefaultAsync();
    }

    public async Task<Customer> AddCustomer(CustomerDTO request, int sellerId)
    {
        var customer = new Customer
        {
            SellerId = sellerId,
            FName = request.First_name,
            LName = request.Last_name,
            Email = request.Email,
            Address = new Address
            {
                Street = request.Address_detaills.Street,
                HouseNumber = request.Address_detaills.HouseNumber,
                FlatNumber = request.Address_detaills.FlatNumber,
                PostCode = request.Address_detaills.PostCode,
                City = request.Address_detaills.City,
                State = request.Address_detaills.State,
                Country = new Country
                {
                    Name = request.Address_detaills.Country_details.Name,
                    Code = request.Address_detaills.Country_details.Code
                }
            }
        };

        _appDbContext.Customers.Add(customer);
        await _appDbContext.SaveChangesAsync();

        return customer;
    }

    // Non-seller-scoped paged query
    public async Task<List<CustomerDTO>> GetCustomers(int pageNumber, int pageSize)
    {
        return await _appDbContext.Customers
            .AsNoTracking()
            .OrderBy(c => c.Id) 
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(c => new CustomerDTO
            {
                First_name = c.FName,
                Last_name = c.LName,
                Email = c.Email,
                Address_detaills = c.Address == null ? null : new AddressDTO
                {
                    Street = c.Address.Street,
                    HouseNumber = c.Address.HouseNumber,
                    FlatNumber = c.Address.FlatNumber,
                    PostCode = c.Address.PostCode,
                    City = c.Address.City,
                    State = c.Address.State,
                    Country_details = c.Address.Country == null ? null : new CountryDTO
                    {
                        Name = c.Address.Country.Name,
                        Code = c.Address.Country.Code
                    }
                }
            })
            .ToListAsync();
    }

    public async Task<Customer> GetCustomer(int id)
    {
        return await _appDbContext.Customers
            .Where(c => c.Id == id)
            .Select(c => new Customer
            {
                FName = c.FName,
                LName = c.LName,
                Email = c.Email,
            })
            .FirstOrDefaultAsync();
    }

    public async Task<Customer> AddCustomer(CustomerDTO request)
    {
        var customer = new Customer
        {
            FName = request.First_name,
            LName = request.Last_name,
            Email = request.Email,
            Address = new Address
            {
                Street = request.Address_detaills.Street,
                HouseNumber = request.Address_detaills.HouseNumber,
                FlatNumber = request.Address_detaills.FlatNumber,
                PostCode = request.Address_detaills.PostCode,
                City = request.Address_detaills.City,
                State = request.Address_detaills.State,
                Country = new Country
                {
                    Name = request.Address_detaills.Country_details.Name,
                    Code = request.Address_detaills.Country_details.Code
                }
            }
        };

        _appDbContext.Customers.Add(customer);
        await _appDbContext.SaveChangesAsync();

        return customer;
    }
}
