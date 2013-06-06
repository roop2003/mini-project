using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Odbc;
using System.Data;
public partial class register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        OdbcConnection odb = new OdbcConnection("Driver={MYSQL ODBC 5.1 Driver};Server=localhost;User=root;Password=hello;database=mdb");
        odb.Open(); //OdbcCommand od = new OdbcCommand("select count(*) from login where uid='" + TextBox1.Text + "' and pass='" + TextBox2.Text + "'", odb);
        OdbcCommand cmd = new OdbcCommand("insert into user_det values('" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "')", odb);
        cmd.ExecuteScalar();
         Response.Write("successfully registrd");


    }
}