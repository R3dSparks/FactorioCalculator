using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorio.Entities
{
    public static class DiagnosticEvents
    {
        /// <summary>
        /// Base error code for this application
        /// </summary>
        internal static int Base = 1000;


        #region Data Access Layer

        public static int Dal = Base + 1;
        public static int DalXmlRead = Base + 2;
        public static int DalXmlReadAttribute = Base + 3;

        #endregion

        #region Factorio Logic

        public static int ItemDoesNotExist = Base + 4;
        public static int ItemAlreadyExists = Base + 5;

        #endregion
    }
}
