using Projeto.Data.Seedwork.Notifying;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projeto.Data.Extensions
{
    public static class NotificationsExtensions
    {
        public static string ToNotificationsString(this IEnumerable<Notification> notifications)
        {
            return string.Join("\n", notifications.Select(x => x.ToString()));
        }
    }
}