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

    public async Task<List<CustomerDTO>> GetCustomers(int pageNumber, int pageSize)
    {
        return await _appDbContext.Customers
            .AsNoTracking()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(c => new CustomerDTO
            {
                FName = c.FName,
                LName = c.LName,
                Email = c.Email,
                AddressDTO = c.Address == null ? null : new AddressDTO
                {
                    Street = c.Address.Street,
                    HouseNumber = c.Address.HouseNumber,
                    FlatNumber = c.Address.FlatNumber,
                    PostCode = c.Address.PostCode,
                    City = c.Address.City,
                    State = c.Address.State,
                    CountryDTO = c.Address.Country == null ? null : new CountryDTO
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
        return await _appDbContext.Customers.
            Where(c => c.Id == id)
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
            FName = request.FName,
            LName = request.LName,
            Email = request.Email,
            Address = new Address
            {
                Street = request.AddressDTO.Street,
                HouseNumber = request.AddressDTO.HouseNumber,
                FlatNumber = request.AddressDTO.FlatNumber,
                PostCode = request.AddressDTO.PostCode,
                City = request.AddressDTO.City,
                State = request.AddressDTO.State,
                Country = new Country
                {
                    Name = request.AddressDTO.CountryDTO.Name,
                    Code = request.AddressDTO.CountryDTO.Code
                }
            }
        };

        _appDbContext.Customers.Add(customer); // ✅ ENTITY
        await _appDbContext.SaveChangesAsync();

        return customer; // ✅ ENTITY
    }

}