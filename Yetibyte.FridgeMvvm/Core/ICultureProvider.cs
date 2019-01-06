using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Yetibyte.FridgeMvvm.Core
{
    public interface ICultureProvider {

        CultureInfo Culture { get; }

    }
}
