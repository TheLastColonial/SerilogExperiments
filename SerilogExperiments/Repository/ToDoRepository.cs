namespace SerilogExperiments.Repository
{
    using System;
    using System.Threading.Tasks;
    using SerilogExperiments.Commands;
    using SerilogExperiments.Models;
    using SerilogExperiments.Services;

    public class ToDoRepository : IRepository<ToDoItem>
    {
        private readonly ISafeCallService safeCallService;
        private readonly IHttpHeaderAccessor headerAccessor;

        public ToDoRepository(ISafeCallService safeCallService, IHttpHeaderAccessor headerAccessor)
        {
            this.safeCallService = safeCallService;
            this.headerAccessor = headerAccessor;
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
            var meta = new SafeCallServiceLogMetadata(
                this.headerAccessor.CorrelationId,
                typeof(ToDoRepository),
                nameof(this.GetByIdAsync));

            var command = new SafeCallServiceQuery<ToDoItem>(
                this.SimulatedDbCall,
                meta);

            return await this.safeCallService.Call(command);
        }

        public Task<ToDoItem> UpdateAsync(int id, ToDoItem model)
        {
            throw new NotImplementedException();
        }

        public async Task<ToDoItem> SimulatedDbCall()
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
