using System;
using System.Threading;

namespace Synchronization.Core
{
    /*
     * Implement very simple wrapper around Mutex or Semaphore (remember both implement WaitHandle) to
     * provide a exclusive region created by `using` clause.
     *
     * Created region may be system-wide or not, depending on the constructor parameter.
     *
     * Any try to get a second systemwide scope should throw an `System.InvalidOperationException` with `Unable to get a global lock {name}.`
     */
    public class NamedExclusiveScope : IDisposable
    {
        private Mutex _mutex;

        public NamedExclusiveScope(string name, bool isSystemWide)
        {
            _mutex = new Mutex(initiallyOwned: isSystemWide, name: name, out var created);
            if(!created)
            {
                throw new InvalidOperationException($"Unable to get a global lock {name}.");
            }
            _mutex.WaitOne();
        }

        public void Dispose()
        {
            _mutex.ReleaseMutex();
        }
    }
}
