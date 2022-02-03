using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AsyncCommands.Commands
{
    public class AsyncRelayCommand : AsyncCommandBase
    {
        private readonly Func<Task> _callback;
        private Func<object, Task> _paramcallback;
        public AsyncRelayCommand(Func<Task> callback, Action<Exception> onException) : base(onException)
        {
            _callback = callback;
        }

        public AsyncRelayCommand(Func<object, Task> paramcallback, Action<Exception> p) : base(p)
        {
            this._paramcallback = paramcallback;            
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            await _callback();
        }
    }
}
