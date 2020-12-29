using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Seedwork.Notifying
{
    public interface INotifiable
    {
        IEnumerable<Notification> Notifications { get; }

        bool HasNotifications { get; }
    }
}