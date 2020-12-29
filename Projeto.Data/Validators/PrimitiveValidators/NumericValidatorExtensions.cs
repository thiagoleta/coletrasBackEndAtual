using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Validators.PrimitiveValidators
{
    public static class NumericValidatorExtensions
    {
        public static bool HasMaxLength(this int value, int length)
        {
            return length >= value.ToString().Length;
        }
    }
}
