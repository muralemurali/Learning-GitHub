<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LGHCalendar.ascx.cs" Inherits="LGH.GENERAL.Webparts.LGHCalendar.LGHCalendar" %>



<script type="text/javascript">

    $(document).ready(function () {

        $(".day_viw").click(function () {
            MoveView('day');
        });

        $(".week_viw").click(function () {
            MoveView('week');
        });

        $(".month_viw").click(function () {
            MoveView('month')
        });

        $("body").on("mousedown mouseup dblclick mousemove", ".ms-acal-rootdiv td", false);
    });

    _spOriginalFormAction = document.forms[0].action;
    _spSuppressFormOnSubmitWrapper = true;

    //$(document).ready(function () { $('.ms-cal-dayitem').each(function () { $(this).removeAttr('href'); $(this).removeAttr('onclick'); $(this).closest("td").removeAttr('href'); $(this).closest("td").removeAttr('onclick'); }); $('.ms-cal-defaultbgcolor').each(function () { $(this).removeAttr('href'); $(this).removeAttr('onclick'); $(this).children("a").removeAttr('href'); $(this).children("a").removeAttr('onclick'); }); });


    //$(".ms-acal-rootdiv").bind("click dblclick", function () {
    //    return false;
    //});

    //setInterval(function () {
    //    $(".ms-acal-item [bricked!='1']").each(function () {
    //        $(this).attr("bricked", "1").bind("click dblclick", function () {
    //            return false;
    //        });
    //    });
    //    // Single day events
    //    $(".ms-acal-sdiv a").each(function () {
    //        var text = $(this).text();
    //        $(this).before(text);
    //        $(this).remove();

    //    });
    //    // Events spanning multiple days
    //    $(".ms-acal-mdiv a").each(function () {
    //        var text = $(this).text();
    //        $(this).before(text);
    //        $(this).remove();

    //    });

    //}, 500);



    //var tables = document.getElementsByTagName("TABLE");
    //for (var i = 0; i < tables.length; i++) {

    //    if (tables[i].summary == "Monthly graphical Calendar View") {

    //        var TDs = tables[i].getElementsByTagName("TD");
    //        for (var j = 0; j < TDs.length; j++) {

    //            if (TDs[j].className == "ms-cal-monthitem") {
    //                var As = TDs[j].getElementsByTagName("A");
    //                if (As.length > 0) {
    //                    TDs[j].innerText = As[0].innerText;
    //                }
    //            }
    //        }
    //        break;
    //    }
    //}

    //ExecuteOrDelayUntilScriptLoaded(function () {

    //    //Disable calendar events
    //    $('.ms-acal-rootdiv td').on("mousedown mouseup dblclick mousemove", false);

    //}, 'SP.UI.ApplicationPages.Calendar.js');



    // load our function to the delayed load list
    ////_spBodyOnLoadFunctionNames.push('hideCalendarEventLinkIntercept');

    ////// hook into the existing SharePoint calendar load function
    ////function hideCalendarEventLinkIntercept() {
    ////    var OldCalendarNotify4a = SP.UI.ApplicationPages.CalendarNotify.$4b;
    ////    SP.UI.ApplicationPages.CalendarNotify.$4b = function () {
    ////        OldCalendarNotify4a();
    ////        hideCalendarEventLinks();
    ////    }
    ////}

    ////// hide the hyperlinks
    ////function hideCalendarEventLinks() {

    ////    // find all DIVs
    ////    var divs = document.getElementsByTagName("DIV");
    ////    for (var i = 0; i < divs.length; i++) {
    ////        // find calendar item DIVs
    ////        if (divs[i].className.toLowerCase() == "ms-acal-item") {
    ////            // find the hyperlink
    ////            var links = divs[i].getElementsByTagName("A");
    ////            if (links.length == 1) {
    ////                // replace the hyperlink with text
    ////                links[0].parentNode.innerHTML = links[0].innerHTML
    ////            }
    ////        }

    ////        // find "x more items" links and re-remove links on Expand/Contract
    ////        if (divs[i].className.toLowerCase() == "ms-acal-ctrlitem") {
    ////            var links = divs[i].getElementsByTagName("A");
    ////            if (links.length == 1) {
    ////                links[0].href = "javascript:hideCalendarEventLinks();void(0);"
    ////            }
    ////        }

    ////    }
    ////}


</script>

<style type="text/css">
    /*.ms-acal-rootdiv A[title="Add"] {
        display: none;
    }*/
    table.ms-acal-vcont tbody tr td a {display:none !important;}
</style>

<div>
    <%--<asp:UpdatePanel runat="server" ID="updPanel">
        <ContentTemplate>--%>
            <div class="cal_sidebar">
			    <%--<div class="location">
			        <div class="loctn_icon">
                        <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control">
                        </asp:DropDownList>
			        </div>
			    </div>--%>
			    <div class="cal_view">
                    <ul class="cal_ico">
						<li><span class="day_viw">Day</span></li>
						<li><span class="week_viw">Week</span></li>
						<li><span class="month_viw">Month</span></li>
					</ul>
		        </div>
                <div id="DatePickerDiv" style="display:block  ">
                    <div class="ms-datepickerouter"> 
                        <div class="ms-quickLaunch">
                            <div class="ms-picker-header"><table cellpadding="0" cellspacing="0" border="0" class="ms-picker-table"><tbody><tr> <td align="center"><a href="javascript:MoveToDate('12\u002f18\u002f2013');" accesskey="<" title="Previous Year 2013" class="ms-pagearrow-left"><img border="0" alt="Previous Year 2013" src="/_layouts/15/images/spcommon.png?rev=23" class="ms-pagearrow-left-icon"></a></td>
                                <td class="ms-picker-month" nowrap="nowrap" align="center">&nbsp;2014&nbsp;</td>
                                <td align="center"><a href="javascript:MoveToDate('12\u002f18\u002f2015');" accesskey=">" title="Next Year 2015" class="ms-pagearrow-right"><img border="0" alt="Next Year 2015" src="/_layouts/15/images/spcommon.png?rev=23" class="ms-pagearrow-right-icon"></a></td>
                                </tr></tbody></table>
                            </div>
                            <div class="ms-picker-body"><table cellpadding="0" cellspacing="0" border="0" class="ms-picker-table">
                                <tbody><tr><td colspan="3" class="ms-picker-line"></td></tr>
                                <tr>
                                <td class="ms-picker-monthcenter" onmouseover="this.className='ms-picker-monthcenterOn';" onmouseout="this.className='ms-picker-monthcenter';" onclick="javascript:ClickDay('1\u002f18\u002f2014')" "january"=""><a href="javascript:ClickDay('1\u002f18\u002f2014')" id="20140118"><span class="ms-accessible"></span>Jan<span class="ms-accessible">2014</span></a></td>
                                <td class="ms-picker-monthcenter" onmouseover="this.className='ms-picker-monthcenterOn';" onmouseout="this.className='ms-picker-monthcenter';" onclick="javascript:ClickDay('2\u002f18\u002f2014')" "february"=""><a href="javascript:ClickDay('2\u002f18\u002f2014')" id="20140218"><span class="ms-accessible"></span>Feb<span class="ms-accessible">2014</span></a></td>
                                <td class="ms-picker-monthcenter" onmouseover="this.className='ms-picker-monthcenterOn';" onmouseout="this.className='ms-picker-monthcenter';" onclick="javascript:ClickDay('3\u002f18\u002f2014')" "march"=""><a href="javascript:ClickDay('3\u002f18\u002f2014')" id="20140318"><span class="ms-accessible"></span>Mar<span class="ms-accessible">2014</span></a></td>
                                </tr>
                                <tr>
                                <td class="ms-picker-monthcenter" onmouseover="this.className='ms-picker-monthcenterOn';" onmouseout="this.className='ms-picker-monthcenter';" onclick="javascript:ClickDay('4\u002f18\u002f2014')" "april"=""><a href="javascript:ClickDay('4\u002f18\u002f2014')" id="20140418"><span class="ms-accessible"></span>Apr<span class="ms-accessible">2014</span></a></td>
                                <td class="ms-picker-monthcenter" onmouseover="this.className='ms-picker-monthcenterOn';" onmouseout="this.className='ms-picker-monthcenter';" onclick="javascript:ClickDay('5\u002f18\u002f2014')" "may"=""><a href="javascript:ClickDay('5\u002f18\u002f2014')" id="20140518"><span class="ms-accessible"></span>May<span class="ms-accessible">2014</span></a></td>
                                <td class="ms-picker-monthcenter" onmouseover="this.className='ms-picker-monthcenterOn';" onmouseout="this.className='ms-picker-monthcenter';" onclick="javascript:ClickDay('6\u002f18\u002f2014')" "june"=""><a href="javascript:ClickDay('6\u002f18\u002f2014')" id="20140618"><span class="ms-accessible"></span>Jun<span class="ms-accessible">2014</span></a></td>
                                </tr>
                                <tr>
                                <td class="ms-picker-monthcenter" onmouseover="this.className='ms-picker-monthcenterOn';" onmouseout="this.className='ms-picker-monthcenter';" onclick="javascript:ClickDay('7\u002f18\u002f2014')" "july"=""><a href="javascript:ClickDay('7\u002f18\u002f2014')" id="20140718"><span class="ms-accessible"></span>Jul<span class="ms-accessible">2014</span></a></td>
                                <td class="ms-picker-monthcenter" onmouseover="this.className='ms-picker-monthcenterOn';" onmouseout="this.className='ms-picker-monthcenter';" onclick="javascript:ClickDay('8\u002f18\u002f2014')" "august"=""><a href="javascript:ClickDay('8\u002f18\u002f2014')" id="20140818"><span class="ms-accessible"></span>Aug<span class="ms-accessible">2014</span></a></td>
                                <td class="ms-picker-monthcenter" onmouseover="this.className='ms-picker-monthcenterOn';" onmouseout="this.className='ms-picker-monthcenter';" onclick="javascript:ClickDay('9\u002f18\u002f2014')" "september"=""><a href="javascript:ClickDay('9\u002f18\u002f2014')" id="20140918"><span class="ms-accessible"></span>Sep<span class="ms-accessible">2014</span></a></td>
                                </tr>
                                <tr>
                                <td class="ms-picker-monthcenter" onmouseover="this.className='ms-picker-monthcenterOn';" onmouseout="this.className='ms-picker-monthcenter';" onclick="javascript:ClickDay('10\u002f18\u002f2014')" "october"=""><a href="javascript:ClickDay('10\u002f18\u002f2014')" id="20141018"><span class="ms-accessible"></span>Oct<span class="ms-accessible">2014</span></a></td>
                                <td class="ms-picker-monthcenter" onmouseover="this.className='ms-picker-monthcenterOn';" onmouseout="this.className='ms-picker-monthcenter';" onclick="javascript:ClickDay('11\u002f18\u002f2014')" "november"=""><a href="javascript:ClickDay('11\u002f18\u002f2014')" id="20141118"><span class="ms-accessible"></span>Nov<span class="ms-accessible">2014</span></a></td>
                                <td class="ms-picker-monthselected" onmouseover="this.className='ms-picker-monthselected';" onmouseout="this.className='ms-picker-monthselected';" onclick="javascript:ClickDay('12\u002f18\u002f2014')" "december"=""><a href="javascript:ClickDay('12\u002f18\u002f2014')" id="20141218"><span class="ms-accessible"></span>Dec<span class="ms-accessible">2014</span></a></td>
                                </tr>
                                <tr><td colspan="3" class="ms-picker-footer ms-textSmall" dir="ltr"><div>Today is <a href="#" onclick="var ifrm = GetIframe(); if (ifrm != null) ifrm.firstUp = true; MoveToDate('12\u002f18\u002f2014'); return false;"><div id="divDate" runat="server"></div></a></div></td></tr></tbody></table>
                            </div>
                        </div>
                    </div>
				</div>
                
		    </div>
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</div>
