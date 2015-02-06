using Microsoft.SharePoint.WebControls;
using System.Web.UI.WebControls;
using System.Data;
using System;
using System.Web.UI.WebControls.WebParts;

namespace LGH.ListConfig
{
    public class SPGridViewWebpartClass : WebPart
    {
        private SPGridView oGrid;

        private DataTable sourceDataTable;
        private DataTableWrapper myDataTable;

        protected override void CreateChildControls()
        {
            sourceDataTable = new DataTable();
            sourceDataTable.Columns.Add("Title");
            sourceDataTable.Columns.Add("Status");
            sourceDataTable.Columns.Add("Description");

            DataRow dr;

            dr = CreateDataTable("a");
            dr = CreateDataTable("b");
            dr = CreateDataTable("c");
            dr = CreateDataTable("d");
            dr = CreateDataTable("e");
            dr = CreateDataTable("f");
            dr = CreateDataTable("g");
            dr = CreateDataTable("h");
            dr = CreateDataTable("i");

            myDataTable = new DataTableWrapper(sourceDataTable);
            Type t = myDataTable.GetType();

            ObjectDataSource ds = new ObjectDataSource();
            ds.ID = "myDataSource";
            ds.TypeName = t.AssemblyQualifiedName;
            ds.SelectMethod = "GetTable";
            ds.ObjectCreating += new ObjectDataSourceObjectEventHandler(ds_ObjectCreating);
            this.Controls.Add(ds);

            this.oGrid = new SPGridView();
            this.oGrid.ID = "oGridId";
            this.oGrid.AutoGenerateColumns = false;
            
            #region "Sorting"

            oGrid.AllowSorting = true;

            BoundField colTitle = new BoundField();
            colTitle.DataField = "Title";
            colTitle.HeaderText = "Title";
            colTitle.SortExpression = "Title";
            this.oGrid.Columns.Add(colTitle);

            BoundField colStatus = new BoundField();
            colStatus.DataField = "Status";
            colStatus.HeaderText = "Status";
            colStatus.SortExpression = "Status";
            this.oGrid.Columns.Add(colStatus);

            BoundField colDescriptione = new BoundField();
            colDescriptione.DataField = "Description";
            colDescriptione.HeaderText = "Description";
            colDescriptione.SortExpression = "Description";
            this.oGrid.Columns.Add(colDescriptione);

            #endregion
                        
            
            #region "Filtering"

            oGrid.AllowFiltering = true;

            oGrid.FilterDataFields = "Title";
            oGrid.FilteredDataSourcePropertyName = "FilterExpression";
            oGrid.FilteredDataSourcePropertyFormat = "{1} like '{0}'";

            oGrid.FilterDataFields = "Status";
            oGrid.FilteredDataSourcePropertyName = "FilterExpression";
            oGrid.FilteredDataSourcePropertyFormat = "{1} like '{0}'";

            oGrid.FilterDataFields = "Description";
            oGrid.FilteredDataSourcePropertyName = "FilterExpression";
            oGrid.FilteredDataSourcePropertyFormat = "{1} like '{0}'";

            oGrid.DataSourceID = "myDataSource";
            this.Controls.Add(oGrid);

            #endregion
          
            
            #region "Pagination"
            //Note:Along with default pagination three other pagination models are given.
            //Uncomment each pagination code one by one and check the different pagination models given. 

            oGrid.PageSize = 5;
            oGrid.AllowPaging = true;

            //Default Pagination
            oGrid.PageIndexChanging += new GridViewPageEventHandler(oGrid_PageIndexChanging);
            oGrid.PagerTemplate = null;

            //First model of Pagination
            //XofYPager xofy = new XofYPager("{0} of {1}", oGrid);
            //oGrid.PagerTemplate = xofy;

            //Second model of Pagination
            //SmartPager smartpager = new SmartPager(1, oGrid);
            //oGrid.PagerTemplate = smartpager;

            //Third model of Pagination
            //SPGridViewPager pager = new SPGridViewPager();
            //pager.ID = "mypager";
            //pager.GridViewId = "oGridId";
            //base.Controls.Add(pager);
            
            #endregion
           
            oGrid.DataBind();

            base.CreateChildControls();

        }

        void oGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            oGrid.PageIndex = e.NewPageIndex;
            oGrid.DataBind();
        }


        private DataRow CreateDataTable(string letter)
        {
            DataRow dr;
            dr = sourceDataTable.NewRow();
            dr["Title"] = "Title" + letter;
            dr["Status"] = "Status" + letter;
            dr["Description"] = "Description" + letter;
            sourceDataTable.Rows.Add(dr);
            return dr;
        }

        void ds_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            myDataTable = new DataTableWrapper(sourceDataTable);
            e.ObjectInstance = myDataTable;
        }
    }
}

