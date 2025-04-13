using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace APIClinica.Services
{
    public class EmailService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public EmailService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["Resend:ApiKey"]; // Agregalo a appsettings.json

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _apiKey);
        }

        public async Task<bool> SendAppointmentEmailAsync(string to, string subject, string html)
        {
            var payload = new
            {
                from = "Clinica Salud Integral <onboarding@resend.dev>",
                to,
                subject,
                html
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://api.resend.com/emails", content);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SendEmailWithAttachmentAsync(string to, string subject, string html, byte[] attachment, string fileName)
        {
            var base64File = Convert.ToBase64String(attachment);

            var payload = new
            {
                from = "Clinica Salud Integral <onboarding@resend.dev>",
                to,
                subject,
                html,
                attachments = new[]
                {
            new {
                filename = fileName,
                content = base64File
            }
        }
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://api.resend.com/emails", content);

            return response.IsSuccessStatusCode;
        }

    }
}
