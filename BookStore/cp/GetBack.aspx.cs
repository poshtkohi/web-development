/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/



using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using bookstore;

using System.Data.SqlClient;

namespace bookstore.cp
{
    public partial class GetBack : System.Web.UI.Page
    {

        //--------------------------------------------------------------------
        private void PageSettings()
        {
            UserMenuControlLoad();
            LoginControlLoad();
        }
        //--------------------------------------------------------------------
        private void UserMenuControlLoad()
        {
            this.UserMenuControl.Controls.Add(LoadControl("UserMenuControl.ascx"));
            return;
        }
        //--------------------------------------------------------------------
        private void LoginControlLoad()
        {
            this.LoginControl.Controls.Add(LoadControl("LoginControl.ascx"));
            return;
        }
        //--------------------------------------------------------------------
        private string refrenceNumber = string.Empty;
        //private string refrenceNumber = "d4338edc7416498c8125";
        private string reservationNumber = string.Empty;
        private string transactionState = string.Empty;
        private string merchantID = constants.MarchantID; // Every merchant has a special ID & password
        private string password = constants.MarchantPassword;
        private string succeedMsg = string.Empty;
        private double result;

        string errorMsg = string.Empty;
        bool isError = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            PageSettings();
            com.sb24.acquirer.ReferencePayment SamanPaymentServices = new com.sb24.acquirer.ReferencePayment();


            // unpack and get info from request object
            if (RequestUnpack())
            {

                // ======================= << checking for transaction state  >>=======================================
                if (transactionState.Equals("OK"))            // Transaction is OK
                {
                    ///////////////////////////////////////////////////////////////////////////////////
                    //   *** IMPORTANT  ****   ATTENTION
                    // Here you should check refrenceNumber in your DataBase tp prevent double spending
                    ///////////////////////////////////////////////////////////////////////////////////

                    Session["RefNum"] = refrenceNumber; // add refrenceNumber in session object
                    /*try
                    {*/

                        result = SamanPaymentServices.verifyTransaction(refrenceNumber, merchantID);

                        //this.Response.Write("result:" + result.ToString() + "<br>");
                        if (result > 0)
                        {
                            // when result >0, its amount of transaction
                            double clientAmount = GetClientAmount();
                            if (result > clientAmount)
                            {
                                int i = ReverseTransaction(result - clientAmount);
                                if (i == 1)
                                {
                                    //refrenceNumber
                                    SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
                                    connection.Open();
                                    SqlCommand command = connection.CreateCommand();
                                    command.Connection = connection;
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.CommandText = "CommitCreditTransaction_CreditPage_proc";


                                    command.Parameters.Add("@ReservationCode", SqlDbType.BigInt);
                                    command.Parameters["@ReservationCode"].Value = Convert.ToInt64(reservationNumber);

                                    command.Parameters.Add("@amount", SqlDbType.BigInt);
                                    command.Parameters["@amount"].Value = (Int64)clientAmount;


                                    command.ExecuteNonQuery();
                                    command.Dispose();
                                    connection.Close();

                                    errorMsg = "بانک صحت رسيد ديجيتالي شما را تصديق نمود. فرايند خريد تکميل گشت";
                                    isError = false;
                                }
                                else
                                {
                                    //  reverse was not posible
                                    result = i;
                                }
                            }
                            else if (result < clientAmount)
                            {
                                int i = ReverseTransaction(result);
                                if (i == 1)
                                {
                                    errorMsg = "بروز خطا درهنگام بررسي صحت رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد";
                                    isError = true; 
                                }
                                else
                                {
                                    //  reverse was not posible
                                    result = i;
                                }
                            }
                            else if (result.Equals(clientAmount)) //Total Amount of Basket
                            {
                                isError = false;
                                succeedMsg = "بانک صحت رسيد ديجيتالي شما را تصديق نمود. فرايند خريد تکميل گشت";

                                SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
                                connection.Open();
                                SqlCommand command = connection.CreateCommand();
                                command.Connection = connection;
                                command.CommandType = CommandType.StoredProcedure;
                                command.CommandText = "CommitCreditTransaction_CreditPage_proc";


                                command.Parameters.Add("@ReservationCode", SqlDbType.BigInt);
                                command.Parameters["@ReservationCode"].Value = Convert.ToInt64(reservationNumber);

                                command.Parameters.Add("@amount", SqlDbType.BigInt);
                                command.Parameters["@amount"].Value = clientAmount;


                                command.ExecuteNonQuery();
                                command.Dispose();
                                connection.Close();

                                errorMsg = "بانک صحت رسيد ديجيتالي شما را تصديق نمود. فرايند خريد تکميل گشت";
                                isError = false;
                            }

                        }
                        TransactionChecking((int)result);
                   /* }
                    catch (Exception ex)
                    {
                        isError = true;
                        errorMsg = "سرور بانک براي تاييد رسيد ديجيتالي در دسترس نيست<br><br>" + ex.Message;
                    }
                    */
                }
            }
            else
            {
                isError = true;
                errorMsg = "متاسفانه بانک خريد شما را تاييد نکرده است";

                if (transactionState.Equals("Canceled By User") || transactionState.Equals(string.Empty))
                {
                    // Transaction was canceled by user
                    isError = true;
                    errorMsg = "تراكنش توسط خريدار كنسل شد";
                }
                else if (transactionState.Equals("Invalid Amount"))
                {
                    // Amount of revers teransaction is more than teransaction
                }
                else if (transactionState.Equals("Invalid Transaction"))
                {
                    // Can not find teransaction
                }
                else if (transactionState.Equals("Invalid Card Number"))
                {
                    // Card number is wrong
                }
                else if (transactionState.Equals("No Such Issuer"))
                {
                    // Issuer can not find
                }
                else if (transactionState.Equals("Expired Card Pick Up"))
                {
                    // The card is expired
                }
                else if (transactionState.Equals("Allowable PIN Tries Exceeded Pick Up"))
                {
                    // For third time user enter a wrong PIN so card become invalid
                }
                else if (transactionState.Equals("Incorrect PIN"))
                {
                    // Pin card is wrong
                }
                else if (transactionState.Equals("Exceeds Withdrawal Amount Limit"))
                {
                    // Exceeds withdrawal from amount limit
                }
                else if (transactionState.Equals("Transaction Cannot Be Completed"))
                {
                    // PIN and PAD are currect but Transaction Cannot Be Completed
                }
                else if (transactionState.Equals("Response Received Too Late"))
                {
                    // Timeout occur
                }
                else if (transactionState.Equals("Suspected Fraud Pick Up"))
                {
                    // User did not insert cvv2 & expiredate or they are wrong.
                }
                else if (transactionState.Equals("No Sufficient Funds"))
                {
                    // there are not suficient funds in the account
                }
                else if (transactionState.Equals("Issuer Down Slm"))
                {
                    // The bank server is down
                }
                else if (transactionState.Equals("TME Error"))
                {
                    // unknown error occur
                }

            }

            /////////////////////////////////////////////////////////////////////////////////////////////////////////
            //                      Page initialize
            /////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (isError)
            {
                lblMsg.Text = errorMsg;
                lblMsg.Visible = true;
            }
            else
            {
                lblMsg.Text = succeedMsg;
                lblMsg.Visible = true;
                //btnReverse.Visible = true;
                //lblText.Visible = true;
                //txtReverseAmount.Visible = true;
            }

        }

        // Unpack info sent by Bank Server from request object

        private bool RequestUnpack()
        {
            if (RequestFeildIsEmpity()) return false;

            refrenceNumber = Request.Form["RefNum"].ToString();
            reservationNumber = Request.Form["ResNum"].ToString();
            transactionState = Request.Form["state"].ToString();

            //this.Response.Write(String.Format("{refrenceNumber:{0} reservationNumber:{1} transactionState:{2}", refrenceNumber, reservationNumber, transactionState));
            /*this.Response.Write("refrenceNumber:" + refrenceNumber + "<br>");
            this.Response.Write("reservationNumber:" + reservationNumber + "<br>");
            this.Response.Write("transactionState:" + transactionState + "<br>");*/

            return true;
        }
        private bool RequestFeildIsEmpity()
        {
            if (Request.Form["RefNum"] == null)
            {
                isError = true;
                errorMsg = "فرايند انتقال وجه با موفقيت انجام شده است اما فرايند تاييد رسيد ديجيتالي با خطا مواجه گشت";
                return true;
            }
            
            if (Request.Form["RefNum"].ToString().Equals(string.Empty))
            {
                isError = true;
                errorMsg = "فرايند انتقال وجه با موفقيت انجام شده است اما فرايند تاييد رسيد ديجيتالي با خطا مواجه گشت";
                return true;
            }
            if (Request.Form["state"] == null)
            {
                isError = true;
                errorMsg = "خريد شما توسط بانک تاييد شده است اما رسيد ديجيتالي شما تاييد نگشت! مشکلي در فرايند رزرو خريد شما پيش آمده است";
                return true;
            }
            if (Request.Form["state"].ToString().Equals(string.Empty))
            {
                isError = true;
                errorMsg = "خريد شما توسط بانک تاييد شده است اما رسيد ديجيتالي شما تاييد نگشت! مشکلي در فرايند رزرو خريد شما پيش آمده است";
                return true;
            }
            if (Request.Form["ResNum"] == null)
            {
                isError = true;
                errorMsg = "خطا در برقرار ارتباط با بانک";
                return true;
            }
            if (Request.Form["ResNum"].ToString().Equals(string.Empty))
            {
                isError = true;
                errorMsg = "خطا در برقرار ارتباط با بانک";
                return true;
            }
            return false;
        }


        private double GetClientAmount()
        {
            //return 1.0;
            ////////////////////////////////////////////////////
            /// Atrtention                  توجه !
            /// شما بايد قيمت خريد را از قبل در بانك اطلاعات يا 
            ///  Session or Request
            /// ذخيره نموده باشيد. حال آنرا به روشي كه مي دانيد بازيابي كنيد
            /// 
            /// مثال 
            // double.Parse(Session["Amount"].ToString());

            SqlConnection connection = new SqlConnection(constants.ConnectionStringSQLDatabase);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "GetAmountCreditTransaction_CreditPage_proc";


            command.Parameters.Add("@ReservationCode", SqlDbType.BigInt);
            command.Parameters["@ReservationCode"].Value = Convert.ToInt64(reservationNumber);

            command.Parameters.Add("@amount", SqlDbType.Int);
            command.Parameters["@amount"].Direction = ParameterDirection.Output;


            command.ExecuteNonQuery();
            int amount = (int)command.Parameters["@amount"].Value;
            command.Dispose();
            connection.Close();
            return (double)amount;


        }
        /*protected void btnReverse_Click(object sender, EventArgs e)
        {
            double reverseAmount = 0;
            if (txtReverseAmount.Text.Equals(string.Empty)) reverseAmount = result;
            else reverseAmount = double.Parse(txtReverseAmount.Text);
            ReverseTransaction(reverseAmount);
        }*/

        private int ReverseTransaction(double revAmount)
        {
            com.sb24.acquirer.ReferencePayment SamanPaymentServices = new com.sb24.acquirer.ReferencePayment();
            int res = SamanPaymentServices.reverseTransaction(refrenceNumber, merchantID, password, revAmount);
            return res;
        }

        private void TransactionChecking(int i)
        {

            switch (i)
            {

                case -1:		//TP_ERROR
                    isError = true;
                    errorMsg = "بروز خطا درهنگام بررسي صحت رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد";
                    break;
                case -2:		//ACCOUNTS_DONT_MATCH
                    isError = true;
                    errorMsg = "بروز خطا در هنگام تاييد رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد";
                    break;
                case -3:		//BAD_INPUT
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد";
                    break;
                case -4:		//INVALID_PASSWORD_OR_ACCOUNT
                    isError = true;
                    errorMsg = "خطاي دروني سيستم درهنگام بررسي صحت رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد";
                    break;
                case -5:		//DATABASE_EXCEPTION
                    isError = true;
                    errorMsg = "خطاي دروني سيستم درهنگام بررسي صحت رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد";
                    break;
                case -7:		//ERROR_STR_NULL
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد";
                    break;
                case -8:		//ERROR_STR_TOO_LONG
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد";
                    break;
                case -9:		//ERROR_STR_NOT_AL_NUM
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد";
                    break;
                case -10:	//ERROR_STR_NOT_BASE64
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد";
                    break;
                case -11:	//ERROR_STR_TOO_SHORT
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد";
                    break;
                case -12:		//ERROR_STR_NULL
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد";
                    break;
                case -13:		//ERROR IN AMOUNT OF REVERS TRANSACTION AMOUNT
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد";
                    break;
                case -14:	//INVALID TRANSACTION
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد";
                    break;
                case -15:	//RETERNED AMOUNT IS WRONG
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد";
                    break;
                case -16:	//INTERNAL ERROR
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد";
                    break;
                case -17:	// REVERS TRANSACTIN FROM OTHER BANK
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد";
                    break;
                case -18:	//INVALID IP
                    isError = true;
                    errorMsg = "خطا در پردازش رسيد ديجيتالي در نتيجه خريد شما تاييد نگرييد";
                    break;

            }
        }

    }
}