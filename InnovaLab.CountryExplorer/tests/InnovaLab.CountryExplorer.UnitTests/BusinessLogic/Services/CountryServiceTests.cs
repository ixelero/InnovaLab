using AutoFixture;
using FluentAssertions;
using InnovaLab.CountryExplorer.BusinessLogic.Clients;
using InnovaLab.CountryExplorer.BusinessLogic.DTOs;
using InnovaLab.CountryExplorer.BusinessLogic.Services;
using InnovaLab.CountryExplorer.BusinessLogic.Services.Contracts;
using InnovaLab.CountryExplorer.Domain.Models;
using Moq;
using Xunit;

namespace InnovaLab.CountryExplorer.UnitTests.BusinessLogic.Services;

public class CountryServiceTests
{
    private readonly Mock<IRestCountriesApi> _mockRestCountriesApi;

    private readonly ICountryService _countryService;

    private readonly Fixture _fixture;

    public CountryServiceTests()
    {
        _mockRestCountriesApi = new Mock<IRestCountriesApi>();

        _countryService = new CountryService(_mockRestCountriesApi.Object);

        _fixture = new Fixture();
    }

    [Fact]
    public async Task GetAllCountriesAsync_3rdApiResponded_Successful()
    {
        // Arrange
        IEnumerable<Country> countries = _fixture.CreateMany<Country>();
        _mockRestCountriesApi.Setup(api => api.GetAllCountriesAsync(It.IsAny<IEnumerable<string>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(countries);

        // Act
        IEnumerable<CountryDTO> result = await _countryService.GetAllCountriesAsync(It.IsAny<CancellationToken>());

        // Assert
        result.Should().NotBeNullOrEmpty();
        result.Should().BeEquivalentTo(countries, options => options
            .Excluding(field => field.Name)
            .ExcludingMissingMembers());
        _mockRestCountriesApi.Verify(api => api.GetAllCountriesAsync(It.IsAny<IEnumerable<string>>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GetAllCountriesAsync_3rdApiNotResponded_Failed()
    {
        // Arrange
        IEnumerable<Country> countries = _fixture.CreateMany<Country>();
        _mockRestCountriesApi.Setup(api => api.GetAllCountriesAsync(It.IsAny<IEnumerable<string>>(), It.IsAny<CancellationToken>()))
            .Throws<TimeoutException>();

        // Act
        Exception exception = await Record.ExceptionAsync(() =>
            _countryService.GetAllCountriesAsync(It.IsAny<CancellationToken>()));

        // Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<TimeoutException>();
        _mockRestCountriesApi.Verify(api => api.GetAllCountriesAsync(It.IsAny<IEnumerable<string>>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public void GetFlagMapsInfoAync_3rdApiResponded_Successful()
    {
        //Not applicable as ApiResponse is internal class in Refit library
    }

    [Fact]
    public async Task GetFlagMapsInfoAync_3rdApiNotResponded_Failed()
    {
        // Arrange
        _mockRestCountriesApi
            .Setup(api => api.GetFlagMapsInfoAync(It.IsAny<string>(), It.IsAny<IEnumerable<string>>(), It.IsAny<CancellationToken>()))
            .Throws<TimeoutException>();

        // Act
        Exception exception = await Record.ExceptionAsync(() =>
            _countryService.GetFlagMapsInfoAync(It.IsAny<string>(), It.IsAny<CancellationToken>()));

        // Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<TimeoutException>();
        _mockRestCountriesApi.Verify(api => api.GetFlagMapsInfoAync(It.IsAny<string>(), It.IsAny<IEnumerable<string>>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }
}

