using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Seedwork.Notifying
{
    public interface IValidatable : INotifiable
    {
        bool Valid { get; }

        bool Invalid { get; }

        void Validate();
    }
}