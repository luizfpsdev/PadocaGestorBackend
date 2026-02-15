using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PadocaGestor.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class IngredientesController : ControllerBase
    {

        [HttpGet]
        public IEnumerable<object> Get()
        {
            return new List<object>();
        }
    }
}
