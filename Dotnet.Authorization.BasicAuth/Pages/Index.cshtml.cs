using Dotnet.Authorization.BasicAuth.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.Authorization.BasicAuth.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private const string TARGETURL = "https://httpbin.org/basic-auth/user/passwd";

        [BindProperty]
        public UserAuth UserAuth { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            HttpClient httpClient = new HttpClient();

            ////user = user && password = passwd
            var byteArray = Encoding.ASCII.GetBytes($"{UserAuth.Username}:{UserAuth.Password}");

            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            //httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", "dXNlcjpwYXNzd2Q=");

            HttpResponseMessage response = await httpClient.GetAsync(TARGETURL);
            HttpContent content = response.Content;

            int statusCode = (int)response.StatusCode;
            string result = await content.ReadAsStringAsync();


            if (statusCode == 200 && result != null)
            {
                return new ObjectResult(result);
            }

            httpClient.Dispose();

            return NotFound();
        }

    }
}
