using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rop.Winforms9.Basic
{
    public abstract class AbsController<T> where T:FormSc
    {
        public T ParentForm { get; }
        public SynchronizationContext SynchronizationContext => ParentForm.SynchronizationContext;

        protected AbsController(T parentForm)
        {
            ParentForm = parentForm;
            parentForm.Shown += ParentForm_Shown;
        }
        private async void ParentForm_Shown(object? sender, EventArgs e)
        {
            Init();
            await InitAsync();
        }
        protected virtual void Init()
        {
        
        }
        protected virtual async ValueTask InitAsync()
        {
            await Task.CompletedTask;
        }
    }
}
