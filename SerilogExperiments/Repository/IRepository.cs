namespace SerilogExperiments.Repository
{
    /// <summary>
    /// Simple Respository
    /// </summary>
    /// <typeparam name="TModel">DTO model stored</typeparam>
    public interface IRepository<TModel> where TModel : class
    {
        int Create(TModel model);
        TModel GetById(int id);
        TModel Update(int id, TModel model);
        bool DeleteById(int id);
    }
}
