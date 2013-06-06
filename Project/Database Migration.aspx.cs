using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Odbc;
using System.Data;
using System.Xml;
using System.Data.SqlClient;


public partial class Database_Migration : System.Web.UI.Page
{
 

    
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedItem.Text == "MYSQL")
        {
            OdbcConnection odb = new OdbcConnection("Driver={MYSQL ODBC 5.1 Driver};Server=localhost;User=root;Password=hello;database=mdb");
            odb.Open();
            OdbcCommand cmd = new OdbcCommand("show databases", odb);
            OdbcDataReader dr = cmd.ExecuteReader();
            DropDownList2.DataSource = dr;
            DropDownList2.DataTextField = "Database";
            DropDownList2.DataValueField = "Database";
            DropDownList2.DataBind();
            ListItem li = new ListItem("select database", "0");
            DropDownList2.Items.Insert(0, li);
            odb.Close();
        }
        else
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=localhost;Initial Catalog=mdb;Integrated Security=SSPI;";
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from sys.databases;", conn);
            SqlDataReader sa = cmd.ExecuteReader();
            DropDownList2.DataSource = sa;
            DropDownList2.DataTextField = "name";
            DropDownList2.DataValueField = "name";
            DropDownList2.DataBind();
            ListItem li = new ListItem("select database", "0");
            DropDownList2.Items.Insert(0, li);

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

        }

           

    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedItem.Text == "MYSQL")
        {
            OdbcConnection odb = new OdbcConnection("Driver={MYSQL ODBC 5.1 Driver};Server=localhost;User=root;Password=hello;database=" + DropDownList2.SelectedItem.Text + ";");
            {
                odb.Open();
                OdbcCommand cmd = new OdbcCommand("show tables", odb);
                OdbcDataReader dr = cmd.ExecuteReader();
                DropDownList3.DataSource = dr;
                DropDownList3.DataTextField = "Tables_in_" + DropDownList2.SelectedItem.Text;
                DropDownList3.DataValueField = "Tables_in_" + DropDownList2.SelectedItem.Text;
                DropDownList3.DataBind();
                ListItem li = new ListItem("select database", "0");
                DropDownList3.Items.Insert(0, li);
                odb.Close();

            }
        }
        else
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=localhost;Initial Catalog="+DropDownList2.SelectedItem.Text+";Integrated Security=SSPI;";
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from sys.tables ", conn);
            SqlDataReader dr = cmd.ExecuteReader();
            DropDownList3.DataSource = dr;
            DropDownList3.DataTextField = "name";
            DropDownList3.DataValueField = "name";
            DropDownList3.DataBind();
            ListItem LI = new ListItem("SELECT TABLE", "0");
            DropDownList3.Items.Insert(0, LI);
            conn.Close();
        }
    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedItem.Text == "MYSQL")
        {
            OdbcConnection odb = new OdbcConnection("Driver={MYSQL ODBC 5.1 Driver};Server=localhost;User=root;Password=hello;database=" + DropDownList2.SelectedItem.Text + ";");
            odb.Open();
            OdbcCommand cmd = new OdbcCommand("select * from " + DropDownList3.SelectedItem.Text + "", odb);
            DataSet ds = new DataSet();
            OdbcDataAdapter oda = new OdbcDataAdapter(cmd);
            oda.Fill(ds);
            GridView1.DataSource = ds;
            GridView1.DataBind();
            GridView1.Visible = false;
            odb.Close();

        }
        else
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=localhost;Initial Catalog=" + DropDownList2.SelectedItem.Text + ";Integrated Security=SSPI;";
            conn.Open();
            SqlCommand cmd = new SqlCommand(" select * from " + DropDownList3.SelectedItem.Text + "",  conn);
            SqlDataReader dr = cmd.ExecuteReader();
            GridView1.DataSource = dr;
            GridView1.DataBind();
            GridView1.Visible = false;
            conn.Close();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GridView1.Visible = true;

        // Response.Write(GridView1.HeaderRow.ToString());
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedItem.Text == "MYSQL")
        {
             OdbcConnection con1 = new OdbcConnection("Driver={MySQL ODBC 5.1 Driver};Server=localhost;uid=root;pwd=hello;database=" + DropDownList2.SelectedItem.Text + " ;");
            con1.Open();
            OdbcCommand cmd = new OdbcCommand("select * from " + DropDownList3.SelectedItem.Text + "", con1);
            OdbcDataAdapter da = new OdbcDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            WriteDataSetToXmlFile(ds, DropDownList3.SelectedItem.Text + ".xml");
            Label1.Text = "SUCESSFULLY TABLE CONVERTED";
            Response.Redirect("xmltodb.aspx");
        }
        else
        {

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=localhost;Initial Catalog=" + DropDownList2.SelectedItem.Text + ";Integrated Security=SSPI;";
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from " +DropDownList3.SelectedItem.Text + "",  conn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            WriteDataSetToXmlFile(ds, DropDownList3.SelectedItem.Text + ".xml");
            Label1.Text = "SUCESSFULLY TABLE CONVERTED";
            Response.Redirect("xmltodb.aspx");

          //  GridView1.DataSource = dr;
           // GridView1.DataBind();


        }




    }
    private void WriteDataSetToXmlFile(DataSet ds, string filename)
    {
        if (DropDownList1.SelectedItem.Text == "MYSQL")
        {
            string absoluteFileName = Server.MapPath("~/MYSQL/" + filename);
            System.IO.FileStream myFileStream = new System.IO.FileStream(absoluteFileName, System.IO.FileMode.Create);
            System.Xml.XmlTextWriter myXmlWriter = new System.Xml.XmlTextWriter(myFileStream, System.Text.Encoding.Unicode);
            ds.WriteXml(myXmlWriter);
            myXmlWriter.Close();
        }
        else
        {
            string absoluteFileName = Server.MapPath("~/SQLSERVER/" + filename);
            System.IO.FileStream myFileStream = new System.IO.FileStream(absoluteFileName, System.IO.FileMode.Create);
            System.Xml.XmlTextWriter myXmlWriter = new System.Xml.XmlTextWriter(myFileStream, System.Text.Encoding.Unicode);
            ds.WriteXml(myXmlWriter);
            myXmlWriter.Close();
        }

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}