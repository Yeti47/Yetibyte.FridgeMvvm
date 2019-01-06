using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yetibyte.FridgeMvvm.DataExchange {

    public class FetchQueryResult<T> : QueryResult {

        public IEnumerable<T> Items { get; private set; }

        public override int NumberAffectedRows => Items?.Count() ?? 0;

        public FetchQueryResult(IEnumerable<T> items, string errorMessage = null) : base(errorMessage) {

            Items = items;

        }

    }

}
