using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yetibyte.FridgeMvvm.DataExchange {

    public abstract class QueryResult {

        #region Properties

        public abstract int NumberAffectedRows { get; }

        public virtual string ErrorMessage { get; protected set; }

        public bool HasError => ErrorMessage != null;

        #endregion

        #region Constructors

        protected QueryResult(string errorMessage = null) {

            ErrorMessage = errorMessage;

        }

        #endregion

    }

}
