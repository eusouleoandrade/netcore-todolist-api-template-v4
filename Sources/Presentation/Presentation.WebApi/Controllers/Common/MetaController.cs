using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebApi.Controllers.Common
{
    [ExcludeFromCodeCoverage]
    [ApiController]
    [Route("api/v{version:apiVersion}")]
    public class MetaController : ControllerBase
    {
        [HttpGet("info")]
        public ActionResult<string> Info()
        {
            var assembly = typeof(Program).Assembly;
            var lastUpdate = System.IO.File.GetLastWriteTime(assembly.Location);
            var version = FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion;

            return Ok($"Version: {version}, Last Updated: {lastUpdate}");
        }
    }
}