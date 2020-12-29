using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Seedwork.Notifying
{
    public abstract class Validatable : Notifiable, IValidatable
    {
        public bool Valid => !HasNotifications;

        public bool Invalid => !Valid;

        public abstract void Validate();
    }
}
