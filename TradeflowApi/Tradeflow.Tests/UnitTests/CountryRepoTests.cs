using Tradeflow.TradeflowApi.Application.Interfaces.Services.Repositories;
using Tradeflow.TradeflowApi.Application.Interfaces.Repositories;
using Tradeflow.TradeflowApi.Domain.Entities;
using Tradeflow.TradeflowApi.Application.Services.Repositories;
using Xunit;
using Moq;


namespace Tradeflow.TradeflowApi.Tradeflow.Tests.UnitTests
{
    public class CountryRepoTests
    {
        private readonly ICountryService _countryService;
        private readonly Mock<ICountryRepository> _countryRepositoryMock;

        public CountryRepoTests()
        {
            _countryRepositoryMock = new Mock<ICountryRepository>();
            _countryService = new CountryService(_countryRepositoryMock.Object);
        }

        // Add your unit tests here
        [Fact]
        public async Task GetCountriesAsync_ReturnsCountries()
        {
            // Given
            var countries = new List<Country>
            {
                new Country { Id = 1, Name = "Country1" },
                new Country { Id = 2, Name = "Country2" }
            };
            _countryRepositoryMock.Setup(repo => repo.GetCountriesAsync()).ReturnsAsync(countries);

            // When
            var result = await _countryService.GetCountriesAsync();

            // Then
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("Country1", result.First().Name);
        }
    }
}
