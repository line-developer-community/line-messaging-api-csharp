using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace LineDC.Messaging.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LineBotController : ControllerBase
    {
        private readonly ILoggableWebhookApplication app;
        private readonly ILogger logger;

        public LineBotController( ILoggableWebhookApplication app , ILogger<LineBotController> logger)
        {
            this.logger = logger;
            this.app = app;
            this.app.Logger = logger;
        }

        // POST api/LineBot
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var reader = new StreamReader(Request.Body);
            var body = await reader.ReadToEndAsync();
            var xLineSignature = Request.Headers["x-line-signature"];
            try
            {
                logger?.LogTrace($"RequestBody: {body}");
                await app.RunAsync(xLineSignature, body);
            }
            catch (Exception e)
            {
                logger?.LogError(e.Message);
            }
            return Ok();
        }
    }
}
