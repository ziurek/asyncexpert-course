using System;
using System.Runtime.CompilerServices;

namespace AwaitableExercises.Core
{
    public static class BoolExtensions
    {
        public static BoolAwaiter GetAwaiter(this bool boolValue)
        {
            return new BoolAwaiter(boolValue);
        }
    }

    public class BoolAwaiter : INotifyCompletion
    {
        private readonly bool _value;

        public BoolAwaiter(bool value)
        {
            _value = value;
        }

        public bool IsCompleted => false;

        public bool GetResult()
        {
            return _value;
        }

        public void OnCompleted(Action continuation)
        {
            continuation();
            return;
        }
    }
}
