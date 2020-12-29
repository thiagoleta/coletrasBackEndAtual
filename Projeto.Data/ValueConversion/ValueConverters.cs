using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Projeto.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.ValueConversion
{
    internal static class ValueConverters
    {
        public static readonly ValueConverter<bool, string> BoolToString =
            new ValueConverter<bool, string>(v => v ? "S" : "N", v => string.IsNullOrEmpty(v) || v.Equals("N") ? false : true);

        
    }
}