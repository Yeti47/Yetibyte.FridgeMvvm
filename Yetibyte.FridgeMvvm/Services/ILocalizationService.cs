using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yetibyte.FridgeMvvm.Services {

    public interface ILocalizationService : IService {

        string this[string key] { get; }

        CultureInfo CurrentCulture { get; set; }

        string GetLocalizedValue(string key);



    }

}
