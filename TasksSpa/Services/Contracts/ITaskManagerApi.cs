using System.Threading.Tasks;
using TasksSpa.Model;

namespace TasksSpa.Services.Contracts
{
    interface ITaskManagerApi
    {
        Task ChangeStatus(TaskDto changedTask, string token);
        Task<int> CreateTask(TaskDto newTask, string token);
        Task<CommentDto[]> GetComments(int taskId);
        Task<TaskDto> GetTask(int taskId, string token);
        Task<TaskDto[]> GetTasks(string token);
        Task RemoveTask(int taskId, string token);
        Task UpdateTask(TaskDto updatedTask, string token);
    }
}
