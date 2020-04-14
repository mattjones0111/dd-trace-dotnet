using System;
using System.Threading;
using System.Threading.Tasks;

namespace Datadog.RuntimeMetrics
{
    // https://github.com/dotnet/runtime/blob/master/src/libraries/Microsoft.Extensions.Hosting.Abstractions/src/BackgroundService.cs

    public abstract class BackgroundService : IDisposable
    {
        private Task? _executingTask;
        private CancellationTokenSource? _stoppingCts;

        /// <summary>
        /// This method is called when the <see cref="IBackgroundService"/> starts. The implementation should return a task that represents
        /// the lifetime of the long running operation(s) being performed.
        /// </summary>
        /// <param name="stoppingToken">Triggered when <see cref="IBackgroundService.StopAsync(CancellationToken)"/> is called.</param>
        /// <returns>A <see cref="Task"/> that represents the long running operations.</returns>
        protected abstract Task ExecuteAsync(CancellationToken stoppingToken);

        /// <summary>
        /// Triggered when the application host is ready to start the service.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            // Create linked token to allow cancelling executing task from provided token
            _stoppingCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            // Store the task we're executing
            _executingTask = ExecuteAsync(_stoppingCts.Token);

            // If the task is completed then return it, this will bubble cancellation and failure to the caller
            if (_executingTask.IsCompleted)
            {
                return _executingTask;
            }

            // Otherwise it's running
#if NET451
            return Task.FromResult<object?>(null);
#else
            return Task.CompletedTask;
#endif
        }

        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the shutdown process should no longer be graceful.</param>
        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            // Stop called without start
            if (_executingTask == null)
            {
                return;
            }

            try
            {
                // Signal cancellation to the executing method
                _stoppingCts?.Cancel();
            }
            finally
            {
                // Wait until the task completes or the stop token triggers
                await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite, cancellationToken));
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _executingTask?.Dispose();
                _stoppingCts?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}