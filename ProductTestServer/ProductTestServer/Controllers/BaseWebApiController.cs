using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductTestServer.Web.ViewModels;

namespace ProductTestServer.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseWebApiController : ControllerBase
    {
        protected BaseWebApiController() { }

        public IActionResult Success(string message = null, object value = null)
        {
            ResponseContent contentResult = new ResponseContent { errorMessage = "", message = message, result = value, succeded = true };
            var objectResult = JsonConvert.SerializeObject(contentResult);
            return new ContentResult
            {
                Content = objectResult,
                ContentType = "application/json",
                StatusCode = 200
            };
        }

        public IActionResult Error(string message = null)
        {
            ResponseContent content = new ResponseContent { errorMessage = message, message = "", result = null, succeded = false };
            var result = JsonConvert.SerializeObject(content);

            return new ContentResult
            {
                Content = result,
                ContentType = "application/json",
                StatusCode = 400
            };      
        }

        public IActionResult ServerError()
        {
            ResponseContent content = new ResponseContent
            {
                errorMessage = "Something went wrong. Try again later.",
                message = "",
                result = null,
                succeded = false
            };
            var result = JsonConvert.SerializeObject(content);

            return new ContentResult
            {
                Content = result,
                ContentType = "application/json",
                StatusCode = 500
            };
        }

    }
}