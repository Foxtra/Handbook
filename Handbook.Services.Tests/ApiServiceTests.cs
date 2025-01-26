using Handbook.Services.DTO;
using Handbook.Services.Implementation;
using RichardSzalay.MockHttp;
using System.Net;
using System.Text.Json;

namespace Handbook.Services.Tests
{
    public sealed class ApiServiceTests
    {
        [Fact(DisplayName = "Положительный сценарий загрузки цилиндров")]
        public async Task LoadCylinders_ShouldReturn()
        {
            // Arrange
            var mockHttp = new MockHttpMessageHandler();
            var expectedResponse = new List<CylinderDto>
            {
                new CylinderDto { Id = 1, Name = "Cylinder1" },
                new CylinderDto { Id = 2, Name = "Cylinder2" }
            };
            mockHttp.When(Constants.Url.Cylinders)
                    .Respond("application/json", JsonSerializer.Serialize(expectedResponse));

            var httpClient = new HttpClient(mockHttp);
            var apiService = new ApiService(httpClient);

            // Act
            var result = await apiService.LoadCylinders();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Cylinder1", result.First().Name);
        }

        [Fact(DisplayName = "Пустой массив в ответе при загрузке цилиндров")]
        public async Task LoadCylinders_ShouldHandleEmptyResponse()
        {
            // Arrange
            var mockHttp = new MockHttpMessageHandler();
            var emptyResponse = "[]"; 

            mockHttp.When(Constants.Url.Cylinders)
                    .Respond("application/json", emptyResponse);

            var httpClient = new HttpClient(mockHttp);
            var apiService = new ApiService(httpClient);

            // Act
            var result = await apiService.LoadCylinders();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result); 
        }

        [Fact (DisplayName = "Ошибка 404 метода загрузки цилиндров")]
        public async Task LoadCylinders_NotFound_ShouldThrow()
        {
            // Arrange
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When(Constants.Url.Cylinders)
                    .Respond(HttpStatusCode.NotFound); 

            var httpClient = new HttpClient(mockHttp);
            var apiService = new ApiService(httpClient);

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(async () => await apiService.LoadCylinders());
        }

        [Fact(DisplayName = "Ошибка некорректный JSON")]
        public async Task LoadCylinders_JsonInvalid_ShouldThrow()
        {
            // Arrange
            var mockHttp = new MockHttpMessageHandler();
            var invalidJson = "{ invalid json "; 

            mockHttp.When(Constants.Url.Cylinders)
                    .Respond("application/json", invalidJson);

            var httpClient = new HttpClient(mockHttp);
            var apiService = new ApiService(httpClient);

            // Act & Assert
            await Assert.ThrowsAsync<JsonException>(async () => await apiService.LoadCylinders());
        }

        [Fact(DisplayName = "Тестирование обработки ответа большого объёма")]
        public async Task LoadCylinders_ShouldHandleLargeDataSet()
        {
            // Arrange
            var mockHttp = new MockHttpMessageHandler();
            var largeResponse = new List<CylinderDto>();

            for (int i = 0; i < 100000; i++)
            {
                largeResponse.Add(new CylinderDto { Id = i, Name = $"Cylinder{i}" });
            }

            mockHttp.When(Constants.Url.Cylinders)
                    .Respond("application/json", JsonSerializer.Serialize(largeResponse));

            var httpClient = new HttpClient(mockHttp);
            var apiService = new ApiService(httpClient);

            // Act
            var result = await apiService.LoadCylinders();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(100000, result.Count);
        }
    }
}