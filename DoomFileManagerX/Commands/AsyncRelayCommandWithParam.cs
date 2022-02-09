using AsyncCommands.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoomFileManagerX.Commands
{
    public class AsyncRelayCommandWithParam : AsyncCommandBase
    {        
        private readonly Func<object, Task> _callback;                        
        public AsyncRelayCommandWithParam(Func<object, Task> callback, Action<Exception> onException) : base(onException)
        {
            _callback = callback;
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            await _callback(parameter);
        }
    }
}
