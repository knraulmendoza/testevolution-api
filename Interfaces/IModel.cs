using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testEvolution.Interfaces
{
    public interface IModel<T> 
    {
        T Id { get; set; }
    }
}
