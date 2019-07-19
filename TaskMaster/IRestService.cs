using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskMaster
{
    public interface IRestService
    {
        Task<List<TodoItem>> RefreshDataAsync();

    }
}
