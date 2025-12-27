using Microsoft.AspNetCore.Mvc;

namespace PadocaGestor.Controllers
{
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
