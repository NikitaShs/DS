using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Exseption
{
    public abstract class Entity<TId>
    {
        protected Entity(TId id) => Id = id;

        public TId Id { get; private set; }
    }
}
