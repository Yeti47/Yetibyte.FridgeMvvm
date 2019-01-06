using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yetibyte.FridgeMvvm.Utilities
{
    public static class EnumUtil
    {
        public static TOut Convert<TOut>(object enumObj, TOut fallbackValue = default(TOut)) where TOut : struct, IConvertible {

            if (!typeof(TOut).IsEnum)
                throw new ArgumentException("The type parameter is not an enum type.");

            if (!enumObj.GetType().IsEnum)
                throw new ArgumentException(nameof(enumObj), "The type of the given object is not an enum type.");

            TOut result = (TOut)enumObj;

            if (!Enum.IsDefined(typeof(TOut), result))
                result = fallbackValue;

            return result;
            
        }

    }
}
