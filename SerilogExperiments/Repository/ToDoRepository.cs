namespace SerilogExperiments.Repository
{
    using System;
    using System.Threading.Tasks;
    using Serilog;
    using SerilogExperiments.Models;
    using SerilogExperiments.Services;

    public class ToDoRepository : IRepository<ToDoItem>
    {
        private readonly ISafeCallService safeCallService;

        public ToDoRepository(ISafeCallService safeCallService)
        {
            this.safeCallService = safeCallService;
        }

        public Task<int> CreateAsync(ToDoItem model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ToDoItem> GetByIdAsync(int id)
        {
            var result = await this.safeCallService.Call(                
                this.SimulatedRequest,
                //() =>
                //{
                //    Task.Delay(1000);
                //    return Task.FromResult(new ToDoItem()
                //    {
                //        Id = 1,
                //        Description = "This is an important item",
                //        Completed = true
                //    });
                //},
                Guid.NewGuid(),
                typeof(ToDoRepository));

            return result;
        }

        public Task<ToDoItem> UpdateAsync(int id, ToDoItem model)
        {
            throw new NotImplementedException();
        }

        public async Task<ToDoItem> SimulatedRequest()
        {
            await Task.Delay(1000);
            return new ToDoItem()
            {
                Id = 1,
                Description = "This is an important item",
                Completed = true
            };
        }
    }
}
