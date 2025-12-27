using Microsoft.AspNetCore.Mvc;

namespace PadocaGestor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FornecedoresController : ControllerBase
    {

        [HttpGet(Name = "Fornecedores")]
        public IEnumerable<object> Get()
        {
            return new List<object>();
        }
    }
}
