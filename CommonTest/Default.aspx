<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        code
        {
            color: Blue;
        }
        pre
        {
            white-space: pre-wrap;
            white-space: -moz-pre-wrap;
            white-space: -o-pre-wrap;
            background: #faf8f0;
            font-size: 15px;
            color: Teal;
        }
        #content #pre-stylish code
        {
            display: block;
            padding: 0.5em 1em;
            border: 1px solid #bebab0;
        }
    </style>
    <script type="text/javascript" src="jquery.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            HideContent();
        });

        function HideContent() {
            $("#queryExtension").hide();
            // $("#sessionExtension").hide();

        }

        function LoadExample() {
            HideContent();
            $("#sessionExtension").hide();
            var exampletype = $("#extensiontype").val();
            if (exampletype == 1) {
                $("#sessionExtension").show();
            }
            if (exampletype == 2) {
                $("#queryExtension").show();
            }
        }

        function LoadQuery() {
            var tmp = $("#txtValueQuery").val();
            $.ajax({
                type: "GET",
                url: "QueryExtension.aspx",
                data: { value: tmp },
                success: function (html) {
                    $("#contentQuery").html(html);
                }
            });
        }

        function LoadQuerySet() {
            var tmp = $("#txtValueQuerySet").val();
            var def = $("#txtValueQueryDefault").val();
            $.ajax({
                type: "GET",
                url: "QueryExtensionDefault.aspx",
                data: { value: tmp, valuedef: def },
                success: function (html) {
                    $("#contentQuerySet").html(html);
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <select id="extensiontype">
            <option value="1">Session Extension</option>
            <option value="2">Query Extension</option>
        </select>
        <button id="btnExample" type="button" onclick="LoadExample()">
            Example</button>
        <p>
            <p>
            </p>
        </p>
        <div id="queryExtension">
            <h1>
                Function</h1>
            <p>
                <code>public static T Value<t>(string parameterName)</code>
            </p>
            <p>
                <code>public static T Value<t>(string parameterName, T defaultValue)</code>
            </p>
            <h1>
                Example</h1>
            <div>
                Normal</div>
            <p>
            </p>
            <hr />
            Value
            <input id="txtValueQuery" />
            <button id="btnLoadQuery" type="button" onclick="LoadQuery()">
                GET</button>
            <div id="contentQuery">
            </div>
            <script type="text/javascript">
                function ShowCodeQuery() {
                    $("#QueryCode").show();
                }
            </script>
            <a href="javascript:void(0);" onclick="ShowCodeQuery()">Show Code</a>
            <div id="QueryCode" style="display: none;">
                <pre>
                  int tmpValue = QueryExtension.Value< int>("value");                  
                </pre>
            </div>
            <p>
            </p>
            <div>
                Set Default Value</div>
            <p>
            </p>
            <hr />
            Value
            <input id="txtValueQuerySet" />
            Default Value
            <input id="txtValueQueryDefault" />
            <button id="btnLoadQuerySet" type="button" onclick="LoadQuerySet()">
                GET</button>
            <div id="contentQuerySet">
            </div>
            <script type="text/javascript">
                function ShowCodeQuerySet() {
                    $("#QueryCodeSet").show();
                }
            </script>
            <a href="javascript:void(0);" onclick="ShowCodeQuerySet()">Show Code</a>
            <div id="QueryCodeSet" style="display: none;">
                <pre>
                  int tmpValue = QueryExtension.Value< int>("value", defaultValue);
                </pre>
            </div>
        </div>
        <div id="sessionExtension">
            <h1>
                Function</h1>
            <p>
                <code>public static void AddSession(HttpSessionState sessionState, string key, object
                    value)</code>
            </p>
            <p>
                <code>public static object GetSession(HttpSessionState sessionState, string key)</code>
            </p>
            <p>
                <h5>
                    T is Struct</h5>
                <code>public static T GetSessionValue< T>(this HttpSessionState session, string key,UponCreate
                    uponCreate)</code>
            </p>
            <p>
                <h5>
                    T is Class</h5>
                <code>public static T GetSessionClass< T>(this HttpSessionState session,string key,UponCreate
                    uponCreate)</code>
            </p>
            <h1>
                Example
            </h1>
            <div>
                <h4>
                    Add Session</h4>
                Key
                <asp:TextBox ID="txtKeySession" runat="server"></asp:TextBox>
                Value
                <asp:TextBox ID="txtValueSession" runat="server"></asp:TextBox>
                <asp:Button ID="btnAddSession" runat="server" Text="Add Session" OnClick="btnAddSession_Click" /><br />
                <script type="text/javascript">
                    function ShowCodeAddSession() {
                        $("#AddSessionCode").show();
                    }
                </script>
                <a href="javascript:void(0);" onclick="ShowCodeAddSession()">Show Code</a>
                <div id="AddSessionCode" style="display: none;">
                    <pre>
                  Session.Add(txtKeySession.Text, txtValueSession.Text);
                </pre>
                </div>
            </div>
            <hr />
            <div>
                <h4>
                    Get Session Normal</h4>
                Key
                <asp:TextBox ID="txtKeyGet" runat="server"></asp:TextBox>
                Value
                <asp:TextBox ID="txtValueGet" runat="server" Enabled="false"></asp:TextBox>
                <asp:Button ID="btnGetSession" runat="server" Text="Get Session" OnClick="btnGetSession_Click" /><br />
                <script type="text/javascript">
                    function ShowCodeGetSession() {
                        $("#GetSessionCode").show();
                    }
                </script>
                <a href="javascript:void(0);" onclick="ShowCodeGetSession()">Show Code</a>
                <div id="GetSessionCode" style="display: none;">
                    <pre>
                 string tmpValue = Session.GetSessionClass<string>(txtKeyGet.Text, null);
                </pre>
                </div>
            </div>
            <hr />
            <div>
                <h4>
                    Get Session With Default Value</h4>
                Key
                <asp:TextBox ID="txtKeyDefault" runat="server" Enabled="false">KEY</asp:TextBox>
                Value
                <asp:TextBox ID="txtValueDefault" runat="server"></asp:TextBox>
                <asp:Button ID="btnGetSessionDefault" runat="server" Text="Get Session" OnClick="btnGetSessionDefault_Click" /><br />
                <script type="text/javascript">
                    function ShowCodeGetSessionDef() {
                        $("#GetSessionDefCode").show();
                    }
                </script>
                <a href="javascript:void(0);" onclick="ShowCodeGetSessionDef()">Show Code</a>
                <div id="GetSessionDefCode" style="display: none;">
                    <pre>
                    protected void btnGetSessionDefault_Click(object sender, EventArgs e)
                    {
                        Test tmp = new Test();
                        tmp.value = 100;

                        Session.Add("KEY", tmp);

                        Test tmpValue = Session.GetSessionValue<Test>("KEY",SetDefault);

                        txtValueDefault.Text = tmpValue.value.ToString();
                    }
                    /////////////
                    struct Test
                    {
                        public int value;
                    }
                </pre>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
