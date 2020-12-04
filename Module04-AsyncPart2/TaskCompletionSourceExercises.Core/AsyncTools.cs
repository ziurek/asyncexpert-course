using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TaskCompletionSourceExercises.Core
{
    public class AsyncTools
    {
        public static Task<string> RunProgramAsync(string path, string args = "")
        {
            var taskCompletionSource = new TaskCompletionSource<string>();

            var process = new Process();
            process.EnableRaisingEvents = true;
            process.StartInfo = new ProcessStartInfo(path)
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                Arguments = args
            };
            process.Exited += (sender, eventArgs) =>
            {
                var senderProcess = sender as Process;
                if (senderProcess?.ExitCode == 0)
                {
                    taskCompletionSource.TrySetResult(senderProcess?.StandardOutput.ReadToEnd());
                }
                else
                {
                    taskCompletionSource.TrySetException(new Exception(senderProcess?.StandardError.ReadToEnd()));
                }
                senderProcess?.Dispose();
            };
            process.Start();

            return taskCompletionSource.Task;
        }
    }
}
