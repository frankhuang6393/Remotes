using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Remotes.Services
{
    public interface IDaoService<T>
    {
        public T Query(string procedureName, T model);

        public IEnumerable<T> QueryItems(string procedureName);

        public object Excute(string procedureName, T model);
    }
}
