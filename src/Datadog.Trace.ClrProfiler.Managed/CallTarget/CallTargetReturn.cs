using Datadog.Trace.Logging;

namespace Datadog.Trace.ClrProfiler.CallTarget
{
    /// <summary>
    /// Call target return value
    /// </summary>
    /// <typeparam name="T">Type of the return value</typeparam>
    public readonly struct CallTargetReturn<T>
    {
        private static readonly Vendors.Serilog.ILogger Log = DatadogLogging.GetLogger(typeof(CallTargetReturn<T>));

        private readonly T _returnValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="CallTargetReturn{T}"/> struct.
        /// </summary>
        /// <param name="returnValue">Return value</param>
        public CallTargetReturn(T returnValue)
        {
            _returnValue = returnValue;
        }

        /// <summary>
        /// Gets the default call target return value (used by the native side to initialize the locals)
        /// </summary>
        /// <returns>Default call target return value</returns>
        public static CallTargetReturn<T> GetDefault()
        {
            Log.Information($"CallTargetReturn<{typeof(T).FullName}>.GetDefault() called");
            return new CallTargetReturn<T>(default);
        }

        /// <summary>
        /// Gets the return value
        /// </summary>
        /// <returns>Return value</returns>
        public T GetReturnValue() => _returnValue;
    }

    /// <summary>
    /// Call target return value
    /// </summary>
    public readonly struct CallTargetReturn
    {
        private static readonly Vendors.Serilog.ILogger Log = DatadogLogging.GetLogger(typeof(CallTargetReturn));

        /// <summary>
        /// Gets the default call target return value (used by the native side to initialize the locals)
        /// </summary>
        /// <returns>Default call target return value</returns>
        public static CallTargetReturn GetDefault()
        {
            Log.Information("CallTargetReturn.GetDefault() called");
            return default;
        }
    }
}