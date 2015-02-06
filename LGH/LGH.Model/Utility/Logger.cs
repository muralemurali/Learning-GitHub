using System.Collections.Generic;
using Microsoft.SharePoint.Administration;

namespace LGH.Model.Utility
{
    public class Logger : SPDiagnosticsServiceBase
    {
        #region Variable Declaration

        private const string LGHDiagnosticAreaName = "LGH";
        private static Logger _current;

        #endregion

        #region Properties

        /// <summary>
        /// Return the object. Singleton Pattern
        /// </summary>
        public static Logger Current
        {
            get { return _current ?? (_current = new Logger()); }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        private Logger() : base("Logger Service", SPFarm.Local)
        {

        }

        #endregion

        #region Override

        /// <summary>
        /// Implement ProvideAreas Override
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<SPDiagnosticsArea> ProvideAreas()
        {
            var areas = new List<SPDiagnosticsArea>
            {
                new SPDiagnosticsArea(LGHDiagnosticAreaName, new List<SPDiagnosticsCategory>
                {
                    new SPDiagnosticsCategory("LGH WebParts", TraceSeverity.Unexpected, EventSeverity.Error)
                })
            };

            return areas;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Log the error message in Logs folder
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="errorMessage"></param>
        public static void LogError(string categoryName, string errorMessage)
        {
            var category = Current.Areas[LGHDiagnosticAreaName].Categories[categoryName];
            if (category != null)
            {
                Current.WriteTrace(0, category, TraceSeverity.Unexpected, errorMessage);
            }
            else
            {
                var diagonosticCategory = new SPDiagnosticsCategory("LGH WebParts", TraceSeverity.Unexpected, EventSeverity.Error);
                Current.WriteTrace(0, diagonosticCategory, TraceSeverity.Unexpected, errorMessage);
            }
        }

        #endregion
    }
}
