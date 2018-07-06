using System.Threading.Tasks;

namespace Disorder.Unity.Core.Extensions.Await
{
    public static class UnityAwaitExtensions
    {
        public static async void RunTaskAsync(this Task task)
        {
            await task;
        }
    }
}