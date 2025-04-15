using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beyond.Domain.Base
{
    /// <summary>
    /// Entity interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface IEntity<T>
    {
        public T Id { get; }
    }
}
