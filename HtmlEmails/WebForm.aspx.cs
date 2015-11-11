using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace HtmlEmails
{
    public partial class WebForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                //UserName.Text = "submitted";
                string newFileBody = PopulateBody(UserName.Text, "", "", "");
                using (StreamWriter writer = 
                    new StreamWriter(Server.MapPath("EmailOutput.htm"), false))
                {
                    writer.Write(newFileBody);
                }
                Response.Redirect("EmailOutput.htm");
            }
        }

        private string PopulateBody(string userName, string title, string url, string description)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplate.htm")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", userName);
            //body = body.Replace("{Title}", title);
            //body = body.Replace("{Url}", url);
            //body = body.Replace("{Description}", description);
            return body;
        }
    }
}