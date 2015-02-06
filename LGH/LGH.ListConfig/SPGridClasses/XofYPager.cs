using System;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace LGH.ListConfig
{
   public class XofYPager : ITemplate
    {

        GridView _grid;

        String _format;

        public XofYPager(string format, GridView grid)
        {
            _format = format;
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

            if (currentPage > 1)
            {

                ImageButton prevBtn = new ImageButton();

                prevBtn.ImageUrl = "~/_layouts/1025/images/prev.gif";

                prevBtn.CommandName = "Page";

                prevBtn.CommandArgument = "Prev";

                cell.Controls.Add(prevBtn);

            }

            cell.Controls.Add(new LiteralControl(String.Format(_format, currentPage, _grid.PageCount)));

            if (currentPage < _grid.PageCount)
            {

                ImageButton nextBtn = new ImageButton();

                nextBtn.ImageUrl = "~/_layouts/1025/images/next.gif";

                nextBtn.CommandName = "Page";

                nextBtn.CommandArgument = "Next";

                cell.Controls.Add(nextBtn);

            }

        }

    }

}
