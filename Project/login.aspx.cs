using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Odbc;
using System.Data;

public partial class login : System.Web.UI.Page
{
    OdbcConnection con = new OdbcConnection("Driver={MYSQL ODBC 5.1 Driver};Server=localhost;User=root;Password=hello;database=mdb");
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        con.Open();
        
        OdbcCommand cmd=new OdbcCommand("select count(*) from user_det where uid='"+TextBox1.Text+"'and pass='"+TextBox2.Text+"'",con);
        int i = Convert.ToInt32(cmd.ExecuteScalar().ToString());
        if (i == 1)
        {
            Response.Redirect("Database Migration.aspx");
            Session["uid"] = TextBox1.Text;
           // HttpApplicationState hp = new HttpApplicationState();
            //hp.Add("uid", TextBox1.Text);
            //SessionPageStatePersister oh = new SessionPageStatePersister();
            
            
        }
        else
        {
            Response.Redirect("Invalid.aspx");
        }


            
    }
}