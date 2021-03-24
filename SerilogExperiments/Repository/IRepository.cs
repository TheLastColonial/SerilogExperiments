namespace SerilogExperiments.Repository
{
    using System.Threading.Tasks;

    /// <summary>
    /// Simple Respository
    /// </summary>
    /// <typeparam name="TModel">DTO model stored</typeparam>
    public interface IRepository<TModel> where TModel : class
    {
        Task<int> CreateAsync(TModel model);
        Task<TModel> GetByIdAsync(int id);
        Task<TModel> UpdateAsync(int id, TModel model);
        Task<bool> DeleteByIdAsync(int id);
    }
}
