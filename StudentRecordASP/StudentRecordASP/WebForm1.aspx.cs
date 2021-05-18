using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;

namespace StudentRecordASP
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateGridview();
            }
        }

        void PopulateGridview()
        {
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("~/XML/XMLFile1.xml"));
            StudentRecord.DataSource = ds;
            StudentRecord.DataBind();
        }

        protected void StudentRecord_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "AddNew")
                {
                    XmlDocument xmlStudentDoc = new XmlDocument();
                    xmlStudentDoc.Load(Server.MapPath("~/XML/XMLFile1.xml"));
                    XmlElement ParentElement = xmlStudentDoc.CreateElement("Student");

                    XmlElement Name = xmlStudentDoc.CreateElement("Name");
                    Name.InnerText = (StudentRecord.FooterRow.FindControl("txtNameFooter") as TextBox).Text.Trim();

                    XmlElement RollNumber = xmlStudentDoc.CreateElement("RollNumber");
                    RollNumber.InnerText = (StudentRecord.FooterRow.FindControl("txtRollNumberFooter") as TextBox).Text.Trim();

                    XmlElement Age = xmlStudentDoc.CreateElement("Age");
                    Age.InnerText = (StudentRecord.FooterRow.FindControl("txtAgeFooter") as TextBox).Text.Trim();

                    XmlElement Gender = xmlStudentDoc.CreateElement("Gender");
                    Gender.InnerText = (StudentRecord.FooterRow.FindControl("txtGenderFooter") as TextBox).Text.Trim();


                    ParentElement.AppendChild(Name);
                    ParentElement.AppendChild(RollNumber);
                    ParentElement.AppendChild(Age);
                    ParentElement.AppendChild(Gender);
                    xmlStudentDoc.DocumentElement.AppendChild(ParentElement);
                    xmlStudentDoc.Save(Server.MapPath("~/XML/XMLFile1.xml"));
                    PopulateGridview();
                    lblSuccessMessage.Text = "New Record Added";
                    lblErrorMessage.Text = "";
                }
            }
            catch(Exception ex) 
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text =ex.Message ;
            }
                        
        }
        protected void StudentRecord_RowEditing(object sender, GridViewEditEventArgs e)
        {
            StudentRecord.EditIndex = e.NewEditIndex;
            PopulateGridview();
        }

        protected void StudentRecord_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            StudentRecord.EditIndex = -1;
            PopulateGridview();
        }

        protected void StudentRecord_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();

                ds.ReadXml(Server.MapPath("~/XML/XMLFile1.xml"));

                ds.Tables[0].Rows[e.RowIndex]["Name"] = (StudentRecord.Rows[e.RowIndex].FindControl("txtName") as TextBox).Text.Trim();
                ds.Tables[0].Rows[e.RowIndex]["RollNumber"] = (StudentRecord.Rows[e.RowIndex].FindControl("txtRollNumber") as TextBox).Text.Trim();
                ds.Tables[0].Rows[e.RowIndex]["Age"] = (StudentRecord.Rows[e.RowIndex].FindControl("txtAge") as TextBox).Text.Trim();
                ds.Tables[0].Rows[e.RowIndex]["Gender"] = (StudentRecord.Rows[e.RowIndex].FindControl("txtGender") as TextBox).Text.Trim();
                ds.WriteXml(Server.MapPath("~/XML/XMLFile1.xml"));
                StudentRecord.EditIndex = -1;
                lblSuccessMessage.Text = "Successfully updated";
                lblErrorMessage.Text = "";
                PopulateGridview();
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }

        }

        protected void StudentRecord_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(Server.MapPath("~/XML/XMLFile1.xml"));
                ds.Tables[0].Rows.RemoveAt(e.RowIndex);
                ds.WriteXml(Server.MapPath("~/XML/XMLFile1.xml"));
                PopulateGridview();
                lblSuccessMessage.Text = "Selected Record Deleted";
                lblErrorMessage.Text = "";
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }
    }
}