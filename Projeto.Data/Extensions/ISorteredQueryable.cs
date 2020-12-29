using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projeto.Data.Extensions
{
    /// <summary>
    /// Represents the result of a sorting operation.
    /// </summary>
    /// <typeparam name="T">The type of the content of the data source.</typeparam>
    public interface ISorteredQueryable<out T> : IOrderedQueryable<T>
    {
    }
}
