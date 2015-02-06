using System.Data;

namespace LGH.ListConfig
{
    public class DataTableWrapper
    {
        private DataTable _dt = new DataTable();

        public DataTableWrapper(DataTable dt)
        {
            _dt = dt;
        }

        public DataTable GetTable()
        {
            return _dt;
        }
    }
}
