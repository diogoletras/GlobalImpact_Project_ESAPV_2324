using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;

namespace GlobalImpactIntegrationTest
{
    public class AccountControllerIntegrationTest
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public AccountControllerIntegrationTest()
        {
            _factory = new WebApplicationFactory<Program>();
            _client = _factory.CreateClient(
                new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false
                }
            );
        }

        [Fact]
        public async Task RegisterTask()
        {
            var response = await _client.GetAsync("/Account/Register");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Register", responseString);
        }

        [Fact]
        public async Task LoginTask()
        {
            var response = await _client.GetAsync("/Account/Login");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Login", responseString);
        }

        [Fact]
        public async Task ForgotPasswordTask()
        {
            var response = await _client.GetAsync("/Account/ForgotPassword");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Forgot your password?", responseString);
        }

        [Fact]
        public async Task ForgotPasswordConfirmationTask()
        {
            var response = await _client.GetAsync("/Account/ForgotPasswordConfirmation");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Forgot Password Confirmation", responseString);
        }

        [Fact]
        public async Task ResetPasswordConfirmationTask()
        {
            var response = await _client.GetAsync("/Account/ResetPasswordConfirmation");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Reset Password Confirm", responseString);
        }

        [Fact]
        public async Task AccessDeniedTask()
        {
            var response = await _client.GetAsync("/Account/UserPage");
            Assert.Contains("302", response.ToString());
        }
    }
}
