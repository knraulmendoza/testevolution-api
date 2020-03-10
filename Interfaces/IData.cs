using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testEvolution.Interfaces
{
    public interface IData<T>
    {
        public T Find(int id);
        public IList<T> GetAll();
        public T Add(T model);
        public T Edit(int id, T model);
    }
}
