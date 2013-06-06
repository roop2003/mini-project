using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Data.Odbc;
using System.Xml;
using System.Text;
using System.IO;
using System.IO.MemoryMappedFiles;


public partial class xmltodb : System.Web.UI.Page
{
    OdbcConnection conn = new OdbcConnection("Driver={MYSQL ODBC 5.1 Driver};Server=localhost;User=root;Password=hello;database=mysql;");
    protected void Page_Load(object sender, EventArgs e)
    {
        
        

    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

       // if (DropDownList1.SelectedValue == "0")
        //{
          //  DropDownList2.Enabled = false;
        //}
         if (DropDownList1.SelectedItem.Text == "MYSQL")
        {

            DropDownList2.Items.Clear();
            string fol = DropDownList1.SelectedItem.Text;
            string dir_path = Server.MapPath("~/" + fol + "/");
            DirectoryInfo dr = new DirectoryInfo(dir_path);
            foreach (FileInfo files in dr.GetFiles())
            {
                DropDownList2.Items.Add(files.Name);
            }
        }
         else if (DropDownList1.SelectedItem.Text == "SQLSERVER")
         {
             DropDownList2.Items.Clear();
             string fol = DropDownList1.SelectedItem.Text;
             string dir_path = Server.MapPath("~/" + fol + "/");
             DirectoryInfo dr = new DirectoryInfo(dir_path);
             foreach (FileInfo files in dr.GetFiles())
             {
                 DropDownList2.Items.Add(files.Name);
             }
         }

        
        else
        {
            Response.Write( "SELECT THE FOLDER");
        }
        }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(GetType(), "abc", "window.open('" + DropDownList1.SelectedItem.Text + "/" + DropDownList2.SelectedItem.Text + "');", true);
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        // string path = Server.MapPath("abcd.xml");//for understanding the fun of Server.MapPath
        //Response.Write(path);
        string fol = DropDownList1.SelectedItem.Text;
        string fl = DropDownList2.SelectedItem.Text;
        ds.ReadXml(Server.MapPath("~/" + fol + "/" + fl));
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string fl = DropDownList2.SelectedItem.Text;
        string f = fl.Remove(fl.Length - 4);
        Response.Write(f);
        StringBuilder sb = new StringBuilder();
        string sf = null;
        for (int i = 0; i < GridView1.HeaderRow.Cells.Count; i++)
        {
            sb.Append(GridView1.HeaderRow.Cells[i].Text).Append("   ").Append("varchar(50),");
        }
        sf = sb.ToString();
        string tex = sf.Remove(sf.Length - 1);
        try
        {
            conn.Open();
            OdbcCommand cmd = new OdbcCommand("create table " + f + "(" + tex + ")", conn);
            cmd.ExecuteNonQuery();
            Label1.Visible = true;
            Label1.Text = "TABLE CREATED SUCESSFULLY";
        }
        catch (Exception ex)
        {
            
    

            Response.Write(ex.Message);

        }
        finally
        {
            conn.Close();
        }

        
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        StringBuilder sbb = new StringBuilder();
        int i;
        DataSet ds = new DataSet();
        string fol = DropDownList1.SelectedItem.Text;
        string fi = DropDownList2.SelectedItem.Text;
        ds.ReadXml(Server.MapPath("~/" + fol + "/" + fi));
        // ds.ReadXml(Server.MapPath("~/abcd/" + DDL_FILES.SelectedItem.Text));
        for (i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
            {

                sbb.Append("'").Append(ds.Tables[0].Rows[i].ItemArray[j].ToString()).Append("'").Append(",");



            }
            string str = sbb.ToString();
            string ext = str.Remove(str.Length - 1);
            string fl = DropDownList2.SelectedItem.Text;
            string f = fl.Remove(fl.Length - 4);
            //Response.Write(ext + "<br>");
            conn.Open();
            OdbcCommand cmd = new OdbcCommand("insert into " + f + " values (" + ext + ")", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            sbb.Remove(0, ext.Length + 1);
            str = "";
            ext = "";
            Label1.Text = "DATA INSERTED SUCESSFFULLY";
        }

    }

    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        

    }
}
