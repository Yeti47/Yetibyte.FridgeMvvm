using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yetibyte.FridgeMvvm.DataExchange {

    public class ActionQueryResult : QueryResult {

        private int _numberAffectedRows;

        public override int NumberAffectedRows => _numberAffectedRows;

        public ActionQueryResult(int numberAffectedRows, string errorMessage = null) : base(errorMessage) {

            _numberAffectedRows = numberAffectedRows;

        }

    }

}
