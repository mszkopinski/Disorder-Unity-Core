using System.Threading.Tasks;

namespace UnityCore
{
    public static class TaskExtensions
    {
        public static async void RunTaskAsync(this Task task)
        {
            await task;
        }
    }
}