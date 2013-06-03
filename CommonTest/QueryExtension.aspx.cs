using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fanex.Common.Extensions;
public partial class QueryExtensioni : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int tmpValue = QueryExtensions.Value<int>("value");
        Response.Write(tmpValue);
    }
}