using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fanex.Common.Extensions;
using Fanex.Common;
public partial class _Default : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        var s = Cryptography.CfsEnCrypt("asdfasdfasdf");

    }
    public object BB()
    {
        return 100;
    }

    protected void btnAddSession_Click(object sender, EventArgs e)
    {
        Session.Add(txtKeySession.Text, txtValueSession.Text);
    }

    protected void btnGetSession_Click(object sender, EventArgs e)
    {
        string tmpValue = Session.GetObject<string>(txtKeyGet.Text);

        string tmpValueUpCreat = Session.GetObject<string>(txtValueGet.Text, UpCreate);

        if (tmpValue == string.Empty)
            txtValueGet.Text = tmpValueUpCreat;
        else
            txtValueGet.Text = tmpValue;
    }

    public object UpCreate()
    {
        return "Empty";
    }

    public object SetDefault()
    {
        Test tmp = new Test();
        tmp.value = -1;
        return tmp;
    }

    protected void btnGetSessionDefault_Click(object sender, EventArgs e)
    {
        Test tmp = new Test();
        tmp.value = 100;

        Session.Add("KEY", tmp);

        Test tmpValue = Session.GetValue<Test>("KEY", SetDefault);

        txtValueDefault.Text = tmpValue.value.ToString();
    }

    struct Test
    {
        public int value;
    }
}