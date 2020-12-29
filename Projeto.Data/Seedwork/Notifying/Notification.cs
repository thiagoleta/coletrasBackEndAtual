using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Projeto.Data.Seedwork.Notifying
{
    public class Notification
    {
        private Notification()
        {
        }

        public Notification(string property, string message)
        {
            Property = property;
            Message = message;
        }      

        public string Property { get; }

        public string Message { get; }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Property))
            {
                return $"{ Property } - { Message }.";
            }

            return $"{ Message }.";
        }
    }
}