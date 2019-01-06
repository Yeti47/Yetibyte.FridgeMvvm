using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Yetibyte.FridgeMvvm.Utilities {

    public static class ReflectionUtil {

        public static IEnumerable<FieldInfo> FindFieldsWithAttribute<TAttribute>(Type type, BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static) {

            if (type == null)
                throw new ArgumentNullException(nameof(type));


            return type.GetFields(bindingFlags).Where(fi => fi.IsDefined(typeof(TAttribute), false));


        }

        public static IEnumerable<FieldInfo> FindFieldsWithAttribute<TAttribute>(object obj, BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static) {

            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            return FindFieldsWithAttribute<TAttribute>(obj.GetType(), bindingFlags);

        }

    }

}
