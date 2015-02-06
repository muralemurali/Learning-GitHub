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




namespace LGH.StaffDirectory.StaffDirectory.StaffDirectory
{
    [ToolboxItemAttribute(false)]
    public partial class StaffDirectory : WebPart
    {

        private ObjectDataSource ds;
        const string GRIDID = "grid";
        const string DATASOURCEID = "gridDS";
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public StaffDirectory()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadGrid();
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ds.FilterExpression = "Name LIKE '%" + txtName1.Text + "%' AND Department LIKE '%" + txtDepartment1.Text + "%' AND Title LIKE '%" + txtTitle1.Text + "%'";
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtTitle1.Text = string.Empty;
            txtDepartment1.Text = string.Empty;
            txtName1.Text = string.Empty;
            LoadGrid();

        }

        #region GridViewFilters&Search


    

      

  

       

       
        #endregion

        public void LoadGrid()
        {
            DataTable employeetable = SelectData();

            if (employeetable.Rows.Count != 0)
            {

                _gridView.DataSource = employeetable.DefaultView;


                


              //  employeetable.DefaultView.Sort = "Name";
              //  employeetable.DefaultView.Sort = "Name";


                _gridView.PageSize = 5;
                _gridView.AllowPaging = true;
                _gridView.Width = new Unit(30, UnitType.Pixel);
             //   _gridView.AllowGrouping = true;
                _gridView.EnableViewState = false;
              //  _gridView.GroupField = "Department";
               // _gridView.GroupFieldDisplayName = "DEPARTMENT";
              //  _gridView.AllowGroupCollapse = true;

                _gridView.AllowFiltering = true;

                _gridView.FilterDataFields = "Name,Title,Department";
                _gridView.FilteredDataSourcePropertyName = "FilterExpression";
                _gridView.FilteredDataSourcePropertyFormat = "{1} = '{0}'";

                //_gridView.FilterDataFields = "Name";
                //_gridView.FilteredDataSourcePropertyName = "FilterExpression";
                //_gridView.FilteredDataSourcePropertyFormat = "{1} like '{0}'";

                //_gridView.FilterDataFields = "Department";
                //_gridView.FilteredDataSourcePropertyName = "FilterExpression";
                //_gridView.FilteredDataSourcePropertyFormat = "{1} like '{0}'";

                //_gridView.FilterDataFields = "Description";
                //_gridView.FilteredDataSourcePropertyName = "FilterExpression";
                //_gridView.FilteredDataSourcePropertyFormat = "{1} like '{0}'";



                _gridView.PageIndexChanging += new GridViewPageEventHandler(_gridView_PageIndexChanging);
              //  LGH.ListConfig.XofYPager xofy = new XofYPager("{0} of {1}", _gridView);
                _gridView.PagerTemplate = null;


                this._gridView.AllowSorting = true;

                //ImageField colPicture = new ImageField();

                //colPicture.DataImageUrlField = "Picture";
                //colPicture.HeaderText = "Picture";
                //colPicture.ControlStyle.Width = Unit.Pixel(50);
                //colPicture.ControlStyle.Height = Unit.Pixel(50);
                //colPicture.Visible = true;


                //this._gridView.Columns.Add(colPicture);

               

               // _gridView.Columns.Add(new SPBoundField {  = "Picture", HeaderText = "PICTURE", });
                _gridView.Columns.Add(new SPBoundField { DataField = "Name", HeaderText = "NAME",SortExpression="Name" });
                //_gridView.Columns.Add(new SPBoundField { DataField = "Uri", HeaderText = "URI" });
                _gridView.Columns.Add(new SPBoundField { DataField = "Title", HeaderText = "TITLE" });
                _gridView.Columns.Add(new SPBoundField { DataField = "Department", HeaderText = "DEPARTMENT", SortExpression = "Department" });
                _gridView.Columns.Add(new SPBoundField { DataField = "Email", HeaderText = "EMAIL" });
                _gridView.Columns.Add(new SPBoundField { DataField = "WorkPhone", HeaderText = "WORKPHONE" });
                _gridView.Columns.Add(new SPBoundField { DataField = "CellPhone", HeaderText = "CELLPHONE" });
                _gridView.DataBind();

                
            

                this.Controls.Add(_gridView);




            }
        }

        private void _gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            _gridView.PageIndex = e.NewPageIndex;
            LoadGrid();

        }

        public DataTable SelectData()
        {
            try
            {
                DataTable dt = new DataTable();
               // dt.Columns.Add("Picture");
                dt.Columns.Add("Name");
                dt.Columns.Add("Title");
                dt.Columns.Add("Department");
                dt.Columns.Add("Email");
                dt.Columns.Add("WorkPhone");
                dt.Columns.Add("CellPhone");

                DataRow dr;

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
                                UserProfileContents emp = new UserProfileContents();
                                emp.Name = (String)profile[PropertyConstants.PreferredName].Value;
                                emp.LastName = (String)profile[PropertyConstants.LastName].Value;
                                if (profile.PublicUrl != null)
                                {
                                    emp.MySiteUri = (String)profile.PublicUrl.AbsoluteUri;
                                }
                                emp.FirstName = (String)profile[PropertyConstants.FirstName].Value;
                                emp.PictureURL = (String)profile[PropertyConstants.PictureUrl].Value;
                                emp.Title = (String)profile[PropertyConstants.Title].Value;
                                emp.Department = (String)profile[PropertyConstants.Department].Value;
                                emp.WorkEmail = (String)profile[PropertyConstants.WorkEmail].Value;
                                emp.WorkPhone = (String)profile[PropertyConstants.WorkPhone].Value;
                                emp.CellPhone = (String)profile[PropertyConstants.CellPhone].Value;
                                employees.Add(emp);
                            }

                        }

                        foreach (UserProfileContents employee in employees)
                        {
                            dr = dt.NewRow();
                            //if (string.IsNullOrEmpty(employee.PictureURL))
                            //{
                            //    dr["Picture"] = "~/_layouts/Images/LGH.StaffDirectory/no_image.gif";
                            //}
                            //else
                            //{
                            //    dr["Picture"] = employee.PictureURL;
                            //}
                            dr["Name"] = employee.Name != null ? employee.Name : string.Empty ;
                            //dr["Uri"] = employee.MySiteUri!=null?employee.MySiteUri:string.Empty;
                            dr["Title"] = employee.Title != null ? employee.Title : string.Empty;
                            dr["Department"] = employee.Department != null ? employee.Department : string.Empty;
                            dr["Email"] = employee.WorkEmail != null ? employee.WorkEmail : string.Empty;
                            dr["WorkPhone"] = employee.WorkPhone != null ? employee.WorkPhone : string.Empty;
                            dr["CellPhone"] = employee.CellPhone != null ? employee.CellPhone : string.Empty;
                            dt.Rows.Add(dr);
                        }
                    }
                });

                return dt;

                //DataView v = dt.DefaultView;
                //v.Sort = "Name ASC";
                //dt = v.ToTable();
                //return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }        
    }
}
