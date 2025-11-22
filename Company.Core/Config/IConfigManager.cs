using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Core.Config
{
    public interface IConfigManager
    {
        T Read<T>(ValueType key);


        void Write<T>(ValueType key, T value);
    }
}
