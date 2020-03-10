using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testEvolution.Interfaces;

namespace testEvolution.Models.Base
{
    public abstract class BaseModel
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public abstract class Model<T> : IModel<int>
    {
        public virtual int Id { get; set; }
    }
}
