using System.Web.UI.WebControls;
using System.Web.UI;

namespace LGH.ListConfig
{

    public class SmartPager : ITemplate
    {

        GridView _grid;

        int _additionalpages;

        public SmartPager(int additionalpages, GridView grid)
        {
            _additionalpages = additionalpages;

            _grid = grid;
        }

        public void InstantiateIn(Control container)
        {

            Table tbl = new Table();

            container.Controls.Add(tbl);

            tbl.Width = Unit.Percentage(100);

            TableRow row = new TableRow();

            tbl.Rows.Add(row);

            TableCell cell = new TableCell();

            row.Cells.Add(cell);

            cell.HorizontalAlign = HorizontalAlign.Center;



            int currentPage = _grid.PageIndex + 1;

            if (_grid.PageCount < (5 + _additionalpages * 2) + 1)
            {

                AddPages(cell, 1, _grid.PageCount, currentPage);

            }

            else
            {

                if (currentPage < 4 + _additionalpages)
                {

                    AddPages(cell, 1, 3 + _additionalpages * 2, currentPage);

                    cell.Controls.Add(new LiteralControl("..."));

                    AddPages(cell, _grid.PageCount, _grid.PageCount, currentPage);

                }

                else
                {

                    AddPages(cell, 1, 1, currentPage);

                    cell.Controls.Add(new LiteralControl("..."));

                    if (currentPage > _grid.PageCount - 3 - _additionalpages)

                        AddPages(cell, _grid.PageCount - 2 - _additionalpages * 2, _grid.PageCount, currentPage);

                    else
                    {

                        AddPages(cell, currentPage - _additionalpages, currentPage + _additionalpages, currentPage);

                        cell.Controls.Add(new LiteralControl("..."));

                        AddPages(cell, _grid.PageCount, _grid.PageCount, currentPage);

                    }
                }

            }

        }



        void AddPages(Control container, int from, int to, int current)
        {

            for (int i = from; i <= to; i++)
            {

                LinkButton link = new LinkButton();

                link.CommandName = "Page";

                link.CommandArgument = i.ToString();

                link.Text = i.ToString();

                if (i == current)

                    link.Style.Add(HtmlTextWriterStyle.FontWeight, "700");

                container.Controls.Add(link);

                if (i < to)

                    container.Controls.Add(new LiteralControl("|"));

            }

        }

    }

}
