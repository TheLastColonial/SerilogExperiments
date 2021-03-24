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
            var command = new SafeCallServiceQuery<ToDoItem>(
                this.SimulatedDbCall,
                new SafeCallServiceLogMetadata(
                    Guid.NewGuid(),
                    typeof(ToDoRepository),
                    nameof(this.GetByIdAsync)));

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
