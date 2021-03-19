namespace SerilogExperiments.Controllers
{
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using SerilogExperiments.Models;
    using SerilogExperiments.Repository;

    /// <summary>
    /// Controller for To do list creation
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class ToDoController : Controller
    {
        private readonly IRepository<ToDoItem> repository;

        public ToDoController(IRepository<ToDoItem> repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Gets a <see cref="ToDoItem"/> by Id
        /// </summary>
        /// <param name="id">Item Id</param>
        /// <returns>ToDo Item</returns>
        [HttpGet("{id:int}")]
        [ProducesDefaultResponseType(typeof(ToDoItem))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetById([FromRoute]int id)
        {
            if (id <= 0)
            {
                return this.BadRequest();
            }

            var item = this.repository.GetById(id);

            if (item == null)
            {
                return this.NotFound(new { id });
            }

            return this.Ok(item);
        }
    }
}
