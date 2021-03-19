namespace SerilogExperiments.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Serilog;
    using SerilogExperiments.Models;

    public class ToDoRepository : IRepository<ToDoItem>
    {
        private readonly ILogger log;
        public ToDoRepository(ILogger logger)
        {
            this.log = logger?.ForContext<ToDoRepository>();
        }

        public int Create(ToDoItem model)
        {
            throw new NotImplementedException();
        }

        public bool DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public ToDoItem GetById(int id)
        {
            log.Information("Get By Id");
            return new ToDoItem() { Id = 1, Description = "This is an important item", Completed = true };
        }

        public ToDoItem Update(int id, ToDoItem model)
        {
            throw new NotImplementedException();
        }
    }
}
