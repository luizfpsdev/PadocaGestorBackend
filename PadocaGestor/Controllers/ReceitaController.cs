using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PadocaGestor.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ReceitaController : ControllerBase
    {

        [HttpGet]
        public IEnumerable<object> Get()
        {
            return new List<object>();
        }
    }
}
