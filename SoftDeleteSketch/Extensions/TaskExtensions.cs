namespace SoftDeleteSketch.Extensions {
    internal static class TaskExtensions {

        public static T EnsureCompleted<T>(this Task<T> task) {
            if (!task.IsCompleted) {
#if DEBUG
                throw new InvalidOperationException("Task is not completed");
#else
      Task.Run(() => task).GetAwaiter().GetResult(); // Wait for task completion on a separate thread (should NOT happen) 
#endif
            }
            return task.Result;
        }

        public static void EnsureCompleted(this Task task) {
            if (!task.IsCompleted) {
#if DEBUG
                throw new InvalidOperationException("Task is not completed");
#else
      Task.Run(() => task).GetAwaiter().GetResult(); // Wait for task completion on a separate thread (should NOT happen) 
#endif
            }
        }
    }
}