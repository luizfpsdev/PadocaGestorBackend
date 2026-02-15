using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PadocaGestor.Controllers
{
    [Authorize]
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
