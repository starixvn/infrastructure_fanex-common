using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fanex.Common.Extensions;
public partial class QueryExtensionDefault : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int defaultValue = QueryExtensions.Value<int>("valuedef");
        int tmpValue = QueryExtensions.Value<int>("value", defaultValue);
        Response.Write(tmpValue);

        
    }
}