using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskMaster
{
    public class TodoItemManager
    {
        IRestService restService;

        public TodoItemManager(IRestService service)
        {
            restService = service;
        }

        public Task<List<TodoItem>> GetTasksAsync()
        {
            return restService.RefreshDataAsync();
        }

    }
}
