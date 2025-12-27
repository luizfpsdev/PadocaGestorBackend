using Microsoft.AspNetCore.Mvc;

namespace PadocaGestor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {

        [HttpGet]
        public IEnumerable<object> Get()
        {
            return new List<object>();
        }
    }
}
