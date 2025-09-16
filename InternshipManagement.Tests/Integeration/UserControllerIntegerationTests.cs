﻿using InternshipManagement.Tests.Integration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace InternshipManagement.Tests.Integration
{
    public class UsersControllerIntegrationTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public UsersControllerIntegrationTests(TestWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Register_And_Login_ReturnsToken()
        {
            // Arrange - register
            var dto = new
            {
                FullName = "Integration User",
                Email = "integration@example.com",
                Password = "password123",
                Role = "Student"
            };

            var registerContent = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var registerResponse = await _client.PostAsync("/api/users/register", registerContent);
            registerResponse.EnsureSuccessStatusCode();

            // Act - login
            var loginDto = new { Email = dto.Email, Password = dto.Password };
            var loginContent = new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.UTF8, "application/json");
            var loginResponse = await _client.PostAsync("/api/users/login", loginContent);
            loginResponse.EnsureSuccessStatusCode();

            var loginBody = await loginResponse.Content.ReadAsStringAsync();
            Assert.Contains("token", loginBody);
        }
    }
}