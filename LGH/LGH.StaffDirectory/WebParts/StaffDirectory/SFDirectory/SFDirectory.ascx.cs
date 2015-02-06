using LGH.StaffDirectory.StaffDirectory.StaffDirectory;
using Microsoft.Office.Server.UserProfiles;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace LGH.StaffDirectory.WebParts.StaffDirectory.SFDirectory
{
    [ToolboxItemAttribute(false)]
    public partial class SFDirectory : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public SFDirectory()
        {
             this.ExportMode = WebPartExportMode.All;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

      

        private char[] _sep = { ',' };
        private string[] _ssep = { "AND" };
        protected SPGridView gridView;
        private ObjectDataSource gridDS;
       // private TextBox txtdummy;


        string FilterExpression
        {
            get
            {
                if (ViewState["FilterExpression"] == null)
                { ViewState["FilterExpression"] = ""; }

                return (string)ViewState["FilterExpression"];
            }
            set
            {
                string thisFilterExpression = "(" + value.ToString() + ")";
                List<string> fullFilterExpression = new List<string>();
               
                if (ViewState["FilterExpression"] != null)
                {
                    string[] fullFilterExp = ViewState["FilterExpression"].ToString().Split(_ssep, StringSplitOptions.RemoveEmptyEntries);
                    fullFilterExpression.AddRange(fullFilterExp);
                   
                    //if the filter is gone expression already exist?
                    int index = fullFilterExpression.FindIndex(s => s.Contains(thisFilterExpression));
                    if (index == -1)
                    { fullFilterExpression.Add(thisFilterExpression); }
                }
                else
                {
                    fullFilterExpression.Add(thisFilterExpression);
                }
                //loop through the list<T> and serialize to string
                string filterExp = string.Empty;
                fullFilterExpression.ForEach(s => filterExp += s + " AND ");
                filterExp = filterExp.Remove(filterExp.LastIndexOf(" AND "));
                if (!filterExp.EndsWith("))") && filterExp.Contains("AND"))
                { filterExp = "(" + filterExp + ")"; }
                ViewState["FilterExpression"] = filterExp; 
            }
        }


        string SortExpression
        {
            get
            {
                if (ViewState["SortExpression"] == null)
                { ViewState["SortExpression"] = ""; }

                return (string)ViewState["SortExpression"];
            }
            set
            {
                string[] thisSE = value.ToString().Split(' ');
                string thisSortExpression = thisSE[0];
                List<string> fullSortExpression = new List<string>();

                if (ViewState["SortExpression"] != null)
                {
                    string[] fullSortExp = ViewState["SortExpression"].ToString().Split(_sep);
                    fullSortExpression.AddRange(fullSortExp);

                    //does the sort expression already exist?
                    int index = fullSortExpression.FindIndex(s => s.Contains(thisSortExpression));
                    if (index >= 0)
                    {
                        string s = string.Empty;
                        if (value.ToString().Contains("DESC"))
                        { s = value.ToString(); }
                        else
                        {
                            s = fullSortExpression[index];
                            if (s.Contains("ASC"))
                            { s = s.Replace("ASC", "DESC"); }
                            else
                            { s = s.Replace("DESC", "ASC"); }
                        }
                        //reset the sort direction
                        fullSortExpression[index] = s;
                    }
                    else
                    {
                        if (value.ToString().Contains("DESC"))
                        { fullSortExpression.Add(value.ToString()); }
                        else
                        { fullSortExpression.Add(thisSortExpression + " ASC"); }
                    }
                }
                else
                {
                    if (value.ToString().Contains("DESC"))
                    { fullSortExpression.Add(value.ToString()); }
                    else
                    { fullSortExpression.Add(thisSortExpression + " ASC"); }
                }
                //loop through the list<T> and serialize to string
                string sortExp = string.Empty;
                fullSortExpression.ForEach(s => sortExp += s);
                sortExp = sortExp.Replace(" ASC", " ASC,");
                sortExp = sortExp.Replace(" DESC", " DESC,");
                ViewState["SortExpression"] = sortExp.Remove(sortExp.LastIndexOf(',')); 
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Page.IsPostBack)
            //{
            //    ViewState["FilterExpression"] = "";
            //    gridDS.FilterExpression = "Name LIKE '%" + string.Empty + "%' AND Department LIKE '%" + string.Empty + "%' AND Title LIKE '%" + string.Empty + "%'";

            //}
            //if (!Page.IsPostBack)
            //{
            //    gridView.DataBind();
            //    // ClearTextBoxControls();
            //}

            ////make sure the header images are set
            //buildFilterView(gridDS.FilterExpression);
        }
       
        
        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);

            if (Context.Request.Form["__EVENTARGUMENT"] != null &&
                 Context.Request.Form["__EVENTARGUMENT"].Contains("__ClearFilter__"))
            {
                // Clear FilterExpression
                ViewState.Remove("FilterExpression");
            }
        }


        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            try
            {

                //txtdummy.Text = "Navigation";
                
                //build the datasource
                gridDS = new ObjectDataSource();
                gridDS.ID = "gridDS";
                gridDS.SelectMethod = "SelectData";
                gridDS.TypeName = this.GetType().AssemblyQualifiedName;
                gridDS.EnableViewState = false;
                
                //pass the SortExpression to the select method
                gridDS.SortParameterName = "SortExpression";

                //this resets the dropdown options for other columns after a filter is selected
                gridDS.FilterExpression = FilterExpression;
                //add the data source
                Controls.Add(gridDS);

                //build the gridview
                gridView = new SPGridView();
                gridView.AutoGenerateColumns = false;
                gridView.EnableViewState = false;
                gridView.ID = "gridView";
                
                //sorting
                gridView.AllowSorting = true;
                
                //filtering 
                gridView.AllowFiltering = true;
                gridView.FilterDataFields = ",Name,Title,Department,Email,WorkPhone,CellPhone";
                gridView.FilteredDataSourcePropertyName = "FilterExpression";
                gridView.FilteredDataSourcePropertyFormat = "{1} = '{0}'";
                
                //set header icons for sorting an filtering
                gridView.RowDataBound += new GridViewRowEventHandler(gridView_RowDataBound);
                gridView.AllowPaging = true;
                gridView.PageSize = 7;
                
                //sorting
                gridView.Sorting += new GridViewSortEventHandler(gridView_Sorting);
                gridView.PageIndexChanging += new GridViewPageEventHandler(gridView_PageIndexChanging);
               // gridView.PagerStyle.HorizontalAlign = HorizontalAlign.Center;
                
                //paging
                
                
                
                //create gridView columns
                BuildColumns();
               SmartPager smartpager = new SmartPager(1, gridView);
               gridView.PagerTemplate = smartpager;
                //Controls.Add(smartpager);

                //set the id and add the control
                gridView.DataSourceID = gridDS.ID;

              //  gridView.DataBind();
                Controls.Add(gridView);

                //SPGridViewPager pager = new SPGridViewPager();
                //pager.GridViewId = gridView.ID;
                //Controls.Add(pager);
                gridView.PagerTemplate = smartpager;

            }
            catch (Exception ex)
            {
                //To Do Log it
            }
        }

        public void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridView.PageIndex = e.NewPageIndex;
            gridView.DataBind();

        }


        protected override void OnPreRender(EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                gridView.DataBind();
              //  gridView.CssClass = "sseGridStyles";
                // ClearTextBoxControls();
            }

            //make sure the header images are set
            buildFilterView(gridDS.FilterExpression);
        }


        public DataTable SelectData(string SortExpression)
        {
            DataTable dataSource = new DataTable();

            //dataSource.Columns.Add("ID");
            //dataSource.Columns.Add("LastName");
            //dataSource.Columns.Add("FirstName");
            //dataSource.Columns.Add("Department");
            //dataSource.Columns.Add("Country");
            //dataSource.Columns.Add("Salary", typeof(double));

            //dataSource.Rows.Add(1, "Smith", "Laura", "Sales", "IreLand", 150000);
            //dataSource.Rows.Add(2, "Jones", "Ed", "Marketing", "IreLand", 75000);
            //dataSource.Rows.Add(3, "Jefferson", "Bill", "Security", "Britian", 87000);
            //dataSource.Rows.Add(4, "Washington", "George", "PMO", "France", 110000);
            //dataSource.Rows.Add(5, "Bush", "Laura", "Accounting", "USA", 44000);
            //dataSource.Rows.Add(6, "Clinton", "Hillory", "Human Resources", "USA", 121000);
            //dataSource.Rows.Add(7, "Ford", "Jack", "IT", "France", 150000);
            //dataSource.Rows.Add(8, "Hailey", "Tom", "Networking", "Canada", 72000);
            //dataSource.Rows.Add(9, "Raul", "Mike", "Accounting", "Canada", 97000);
            //dataSource.Rows.Add(10, "Shyu", "Danny", "Sales", "Britian", 89000);
            //dataSource.Rows.Add(11, "Hanny", "Susan", "Sales", "USA", 275000);



            dataSource.Columns.Add("Picture");

            dataSource.Columns.Add("Name");
            dataSource.Columns.Add("Title");
            dataSource.Columns.Add("Department");
            dataSource.Columns.Add("Email");
            dataSource.Columns.Add("WorkPhone");
            dataSource.Columns.Add("CellPhone");

            DataRow dataRow;

            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.ID))
                {
                    SPServiceContext serviceContext = SPServiceContext.GetContext(site);
                    UserProfileManager profileManager = new UserProfileManager(serviceContext);
                    List<UserProfileContents> employees = new List<UserProfileContents>();

                    foreach (UserProfile profile in profileManager)
                    {

                        if (profile != null)
                        {
                            UserProfileContents userInformation = new UserProfileContents();
                            userInformation.Name = (String)profile[PropertyConstants.PreferredName].Value;
                            userInformation.LastName = (String)profile[PropertyConstants.LastName].Value;
                            if (profile.PublicUrl != null)
                            {
                                userInformation.MySiteUri = (String)profile.PublicUrl.AbsoluteUri;
                            }
                            userInformation.FirstName = (String)profile[PropertyConstants.FirstName].Value;
                            userInformation.PictureURL = (String)profile[PropertyConstants.PictureUrl].Value;
                            userInformation.Title = (String)profile[PropertyConstants.Title].Value;
                            userInformation.Department = (String)profile[PropertyConstants.Department].Value;
                            userInformation.WorkEmail = (String)profile[PropertyConstants.WorkEmail].Value;
                            userInformation.WorkPhone = (String)profile[PropertyConstants.WorkPhone].Value;
                            userInformation.CellPhone = (String)profile[PropertyConstants.CellPhone].Value;
                            employees.Add(userInformation);
                        }

                    }

                    foreach (UserProfileContents employee in employees)
                    {
                        dataRow = dataSource.NewRow();
                        if (string.IsNullOrEmpty(employee.PictureURL))
                        {
                            dataRow["Picture"] = "~/_layouts/15/LGH.StaffDirectory/Images/empty_user.jpg";
                        }
                        else
                        {
                            dataRow["Picture"] = employee.PictureURL;
                        }
                        //dataRow["Picture"] = "http://devsp13:13498/Style%20Library/newpic.jpg";
                        dataRow["Name"] = employee.Name != null ? employee.Name : string.Empty;
                        //dr["Uri"] = employee.MySiteUri!=null?employee.MySiteUri:string.Empty;
                        dataRow["Title"] = employee.Title != null ? employee.Title : string.Empty;
                        dataRow["Department"] = employee.Department != null ? employee.Department : string.Empty;
                        dataRow["Email"] = employee.WorkEmail != null ? employee.WorkEmail : string.Empty;
                        dataRow["WorkPhone"] = employee.WorkPhone != null ? employee.WorkPhone : string.Empty;
                        dataRow["CellPhone"] = employee.CellPhone != null ? employee.CellPhone : string.Empty;
                        
                        dataSource.Rows.Add(dataRow);
                    }
                }
            });


           // clean up the sort expression if needed - the sort descending 
            //menu item causes the double in some cases 
            if (SortExpression.ToLowerInvariant().EndsWith("desc desc"))
                SortExpression = SortExpression.Substring(0, SortExpression.Length - 5);

            //need to handle the actual sorting of the data
            if (!string.IsNullOrEmpty(SortExpression))
            {
                DataView view = new DataView(dataSource);
                view.Sort = SortExpression;
                DataTable newTable = view.ToTable();
                dataSource.Clear();
                dataSource = newTable;
            }

            return dataSource; 
        }


        private void BuildColumns()
        {
            ImageField colPicture = new ImageField();

            colPicture.DataImageUrlField = "Picture";
            colPicture.HeaderText = "Picture";
            colPicture.ControlStyle.Width = Unit.Pixel(90);
            colPicture.ControlStyle.Height = Unit.Pixel(110);
            colPicture.Visible = true;
            gridView.Columns.Add(colPicture);



            BoundField column = new BoundField();
            column.DataField = "Name";
            column.SortExpression = "Name";
            column.HeaderText = "NAME";
            column.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
           
            gridView.Columns.Add(column);

            column = new BoundField();
            column.DataField = "Title";
            column.SortExpression = "Title";
            column.HeaderText = "TITLE";
            gridView.Columns.Add(column);

            column = new BoundField();
            column.DataField = "Department";
            column.SortExpression = "Department";
            column.HeaderText = "DEPARTMENT";
            gridView.Columns.Add(column);

            column = new BoundField();
            column.DataField = "Email";
            column.SortExpression = "Email";
            column.HeaderText = "EMAIL";
            gridView.Columns.Add(column);

            column = new BoundField();
            column.DataField = "WorkPhone";
            column.SortExpression = "WorkPhone";
            column.HeaderText = "WORKPHONE";
            gridView.Columns.Add(column);

            column = new BoundField();
            column.DataField = "CellPhone";
            column.SortExpression = "CellPhone";
            column.HeaderText = "CELLPHONE";
            gridView.Columns.Add(column);
        }


        private void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (sender == null || e.Row.RowType != DataControlRowType.Header)
            { return; }
 
            SPGridView grid = sender as SPGridView;

            // Show icon on filtered and sorted columns 
            for (int i = 1; i < grid.Columns.Count; i++)
            {
                DataControlField field = grid.Columns[i];

                if (FilterExpression.Contains(field.SortExpression) && 
                    !string.IsNullOrEmpty(FilterExpression))
                {
                    PlaceHolder panel = HeaderImages(field, "/_layouts/images/filter.gif");
                    e.Row.Cells[i].Controls[0].Controls.Add(panel);
                }
                else if(SortExpression.Contains(field.SortExpression)) 
                {
                    
                        string url = sortImage(field);
                        PlaceHolder panel = HeaderImages(field, url);
                        e.Row.Cells[i].Controls[0].Controls.Add(panel);
                    
                }          
            }
        }


        private string sortImage(DataControlField field)
        {
            string url = string.Empty;
            string[] fullSortExp = SortExpression.Split(_sep);
            List<string> fullSortExpression = new List<string>();
            fullSortExpression.AddRange(fullSortExp);

            //does the sort expression already exist?
            int index = fullSortExpression.FindIndex(s => s.Contains(field.SortExpression));
            if (index >= 0)
            {
                string s = fullSortExpression[index];
                if (s.Contains("ASC"))
                { url = "_layouts/images/sortup.gif"; }
                else
                { url = "_layouts/images/sortdown.gif"; }
            }
            return url;
        }


        private PlaceHolder HeaderImages(DataControlField field, string imageUrl)
        {
            Image filterIcon = new Image();
            filterIcon.ImageUrl = imageUrl;
            filterIcon.Style[HtmlTextWriterStyle.MarginLeft] = "2px";

            Literal headerText = new Literal();
            headerText.Text = field.HeaderText;

            PlaceHolder panel = new PlaceHolder();
            panel.Controls.Add(headerText);

            //add the sort icon if needed
            if (FilterExpression.Contains(field.SortExpression) &&
                SortExpression.Contains(field.SortExpression)) 
            {
                string url = sortImage(field);
                Image sortIcon = new Image();
                sortIcon.ImageUrl = url;
                sortIcon.Style[HtmlTextWriterStyle.MarginLeft] = "1px";
                panel.Controls.Add(sortIcon);
                //change the left margin to 1
                filterIcon.Style[HtmlTextWriterStyle.MarginLeft] = "1px";
            }

            panel.Controls.Add(filterIcon);
            return panel;
        }


        void gridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sDir = e.SortDirection.ToString();
            sDir = sDir == "Descending" ? " DESC" : "";

            SortExpression = e.SortExpression + sDir;
            e.SortExpression = SortExpression;

            //keep the filter
            if (!string.IsNullOrEmpty(FilterExpression))
            { gridDS.FilterExpression = FilterExpression; }

        }
        

        void buildFilterView(string filterExp)
        {
            string lastExp = filterExp;
            if (lastExp.Contains("AND"))
            {
                if (lastExp.Length < lastExp.LastIndexOf("AND") + 4)
                { lastExp = lastExp.Substring(lastExp.LastIndexOf("AND") + 4); }
                else
                { lastExp = string.Empty; }
            }
            
            //update the filter
            if (!string.IsNullOrEmpty(lastExp))
            { FilterExpression = lastExp; }

            //reset object dataset filter
            if (!string.IsNullOrEmpty(FilterExpression))
            { gridDS.FilterExpression = FilterExpression; }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            gridDS.FilterExpression = "Name LIKE '%" + txtname.Text + "%' AND Department LIKE '%" + txtdepartment.Text + "%' AND Title LIKE '%" + txttitle.Text + "%'";

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ViewState["FilterExpression"] = "";
            gridDS.FilterExpression = "Name LIKE '%" + string.Empty + "%' AND Department LIKE '%" + string.Empty + "%' AND Title LIKE '%" + string.Empty + "%'";
            ClearTextBoxControls();

        }

        private void ClearTextBoxControls()
        {
            txtname.Text = string.Empty;
            txttitle.Text = string.Empty;
            txtdepartment.Text = string.Empty;
        }
        
    }
}
