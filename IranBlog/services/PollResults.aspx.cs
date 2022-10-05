/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using System.Data.SqlClient;
using AlirezaPoshtkoohiLibrary;

namespace services
{
	/// <summary>
	/// Summary description for PollResults.
	/// </summary>
	public partial class PollResults : System.Web.UI.Page
	{
		//--------------------------------------------------------------------------------
		private class PollInfo
		{
			public int PollNumbers;
			public string ResponseText;
			public PollInfo(int PollNumbers, string ResponseText)
			{
				this.PollNumbers = PollNumbers;
				this.ResponseText = ResponseText;
			}
		}
		//--------------------------------------------------------------------------------
		protected void Page_Load(object sender, System.EventArgs e)
		{
			string question = this.Request.Form["question"];
			string response = this.Request.Form["response"];
			try { Convert.ToInt64(question); Convert.ToInt64(response); }
			catch { return ;} 
			SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
			connection.Open();
			SqlCommand command = connection.CreateCommand();
			command.Connection = connection;
			command.CommandText = String.Format("UPDATE {0} SET PollNumbers=PollNumbers+1 WHERE id={1} AND QuestionID={2}; SELECT ResponseText,PollNumbers FROM {0} WHERE QuestionID={2}", constants.SQLPollResponsesTableName, response, question);
			SqlDataReader reader = command.ExecuteReader();
			this.LabelOutput.Text = String.Format("<TABLE dir=ltr style='BORDER-COLLAPSE: collapse' borderColor='#0066cc' cellPadding=2 width='100%' border=1><TBODY><TR><TD style='BACKGROUND-COLOR: #c4c4ff' colSpan=2><P dir=rtl>{0}</P></TD></TR>", this.Request.Form["QuestionText"]);
			ArrayList alPollNumbers = new ArrayList();
			ArrayList alResponseTexts = new ArrayList();
			while(reader.Read())
			{
				alPollNumbers.Add((int)reader["PollNumbers"]);
				alResponseTexts.Add((string)reader["ResponseText"]);
			}
			reader.Close();
			//connection.Close();
			command.Dispose();
			int TotalPollNumber = 0;
			if(alPollNumbers.Count > 0)
			{												
				for(int i = 0 ; i < alPollNumbers.Count ; i++)
					TotalPollNumber += (int)alPollNumbers[i];
				bool ccolor = true;
				string color = "";
				for(int i = 0, j = 0 ; i < alResponseTexts.Count ; i++, j++)
				{
					if(ccolor == true)
					{
						color = "FFFFFF";
						ccolor = false;
					}
					else
					{
						color = "efeff8";
						ccolor = true;
					}
					if(j == 0)
						this.LabelOutput.Text += String.Format("<TR><TD class='_t2d_' width='20%' bgColor='#{2}'>{0}</TD><TD class='_t2d_' dir=ltr width='64%' bgColor='#{2}'><DIV align=left><IMG height=18 src='/images/poll/X_R_1_2.gif' width='{1}%'><IMG src='/images/poll/X_R_1_1.gif'></DIV></TD></TR>", (string)alResponseTexts[i], (int)((((double)(int)alPollNumbers[i])/TotalPollNumber)*100), color);
					if(j == 1)
						this.LabelOutput.Text += String.Format("<TR><TD class='_t2d_' width='20%' bgColor='#{2}'>{0}</TD><TD class='_t2d_' dir=ltr width='64%' bgColor='#{2}'><DIV align=left><IMG height=18 src='/images/poll/X_R_2_2.gif' width='{1}%'><IMG src='/images/poll/X_R_2_1.gif'></DIV></TD></TR>", (string)alResponseTexts[i], (int)((((double)(int)alPollNumbers[i])/TotalPollNumber)*100), color);
					if(j == 2)
						this.LabelOutput.Text += String.Format("<TR><TD class='_t2d_' width='20%' bgColor='#{2}'>{0}</TD><TD class='_t2d_' dir=ltr width='64%' bgColor='#{2}'><DIV align=left><IMG height=18 src='/images/poll/X_R_3_2.gif' width='{1}%'><IMG src='/images/poll/X_R_3_1.gif'></DIV></TD></TR>", (string)alResponseTexts[i], (int)((((double)(int)alPollNumbers[i])/TotalPollNumber)*100), color);
					if(j == 3)
					{
						this.LabelOutput.Text += String.Format("<TR><TD class='_t2d_' width='20%' bgColor='#{2}'>{0}</TD><TD class='_t2d_' dir=ltr width='64%' bgColor='#{2}'><DIV align=left><IMG height=18 src='/images/poll/X_R_4_2.gif' width='{1}%'><IMG src='/images/poll/X_R_4_1.gif'></DIV></TD></TR>", (string)alResponseTexts[i], (int)((((double)(int)alPollNumbers[i])/TotalPollNumber)*100), color);
			            j = -1;
					}
				}
			}
			this.LabelOutput.Text += String.Format("<TR><TD style='BACKGROUND-COLOR: #e6e6ff' colSpan=2><P align=center>در كل <FONT color=#ff0000>{0}</FONT> تا نظر براي <FONT color=#ff0000>{1}</FONT> تا انتخاب </P></TD></TR></TBODY></TABLE>", TotalPollNumber, alPollNumbers.Count);
		}
		//--------------------------------------------------------------------------------
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
