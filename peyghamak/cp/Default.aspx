<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Peyghamak.cp.MainCp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<asp:PlaceHolder ID="MetaCopyrightSection" runat="server"></asp:PlaceHolder>
<title>صفحه اصلی تنظیمات</title>
<link href="style.css" rel="stylesheet" type="text/css" />
</head>

<body>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td align="center" valign="Top" class="tewst">
    <table width="916" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="700"><table width="700" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td height="25">&nbsp;</td>
            <td></td>
            <td>&nbsp;</td>
          </tr>
          <tr>
            <td height="60" colspan="3" class="menuBar">
          <!--Menubar-->
            <td height="60" colspan="3" align="center" valign="middle">
			</td>		
          <!--End Menubar-->            
            </tr>
          <tr>
            <td width="25" height="25" class="td_mainborderTop-left">&nbsp;</td>
            <td width="650" class="td_mainborderTop-fill">&nbsp;</td>
            <td width="25" class="td_mainborderTop-right">&nbsp;</td>
          </tr>
          <tr>
            <td class="td_mainborderLeft-fill">&nbsp;</td>
            <td valign="Top" class="td_mainborderFill" width="650" align="center">
            <p>
            <table width="560" border="0" cellpadding="0" cellspacing="0">
              <tr>
                <td><div id="setting_plate" onclick="window.location='password.aspx'" style="cursor:pointer" title="تغییر کلمه عبور">
                    <div  class="setting_password">
                      <div class="setting_text">برای حفاظت از اطلاعات شخصیتان ما پیشنهاد میکنیم حداقل هر شش ماه کلمه عبور خود را تغییر دهید.</div>
                    </div>
                </div></td>
                <td width="10">&nbsp;</td>
                <td><div id="setting_plate" onclick="window.location='account.aspx'" style="cursor:pointer" title="مشخصات">
                    <div class="setting_profile">
                      <div class="setting_text">اگر بعد از ثبت نام بخشی از مشخصاتی که در هنگام ثبت نام وارد کرده بودید تغییر کرده است، تغییرات را اعمال کنید.</div>
                    </div>
                </div></td>
              </tr>
              <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
              </tr>
              <tr>
                <td><div id="setting_plate" onclick="window.location='picture.aspx'" style="cursor:pointer" title="آپلود عکس خود">
                    <div class="setting_picture">
                      <div class="setting_text">اگر تمایل دارید می توانید عکسی را برای نمایش به عنوان تصویرتان بار گذاری کنید.</div>
                    </div>
                </div></td>
                <td>&nbsp;</td>
                <td><div id="setting_plate" onclick="window.location='mobile.aspx'" style="cursor:pointer" title="تنظیمات موبایل">
                    <div class="setting_mobile">
                      <div class="setting_text">برای استفاده از خدمات پیام کوتاه سایت موبایل خود را تنظیم کنید.</div>
                    </div>
                </div></td>
              </tr>
              <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td><div id="setting_plate" onclick="window.location='messenger.aspx'" style="cursor:pointer" title="تنظیمات یاهو مسنجر">
                    <div class="setting_messenger">
                      <div class="setting_text">برای استفاده از خدمات مسنجر سایت تنظیمات لازم را انجام دهید.</div>
                    </div>
                </div></td>
              </tr>
            </table>
            </p>	
		    <p align="right" class="home_text">
            <asp:PlaceHolder ID="SiteFooterSection" runat="server"></asp:PlaceHolder> 
            </p></td>
            <td class="td_mainborderRight-fill">&nbsp;</td>
          </tr>
          <tr>
            <td class="td_mainborderButtom-left">&nbsp;</td>
            <td class="td_mainborderButtom-fill">&nbsp;</td>
            <td width="25" height="25" class="td_mainborderButtom-right">&nbsp;</td>
          </tr>
        </table></td>
        <td width="4">&nbsp;</td>
        <td width="212" valign="Top">
        <table width="250" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td height="25">&nbsp;</td>
    <td>&nbsp;</td>
    <td height="25">&nbsp;</td>
  </tr>
  <tr>
    <td height="25">&nbsp;</td>
    <td height="60">&nbsp;</td>
    <td height="25">&nbsp;</td>
  </tr>
  <tr>
    <td width="25" height="25" class="td_mainborderTop-left">&nbsp;</td>
    <td width="200" class="td_mainborderTop-fill">&nbsp;</td>
    <td width="25" height="25" class="td_mainborderTop-right">&nbsp;</td>
  </tr>
  <tr>
    <td class="td_mainborderLeft-fill">&nbsp;</td>
    <td valign="top" class="td_mainborderFill"><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td height="42" align="right" class="sidebar_titlebar">منوی اصلی </td>
      </tr>
      <tr>
        <td align="right" class="setting_links"><a href="/my.aspx">پیغامک خودم</a></td>
      </tr>
      <tr>
        <td align="right" class="setting_links"><a href="/signout.aspx">خروج از سایت</a></td>
      </tr>
    </table>
    <td class="td_mainborderRight-fill">&nbsp;</td>
  </tr>
  <tr>
    <td class="td_mainborderLeft-fill">&nbsp;</td>
    <td valign="top" class="td_mainborderFill"><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td height="42" align="right" class="sidebar_titlebar">منوی تنظیمات</td>
      </tr>
      <tr>
        <td align="right" class="setting_links"><a href="../cp/">صفحه اصلی تنظیمات</a></td>
      </tr>
      <tr>
        <td align="right" class="setting_links"><a href="account.aspx">مشخصات</a></td>
      </tr>
      <tr>
        <td align="right" class="setting_links"><a href="password.aspx">تغییر کلمه عبور</a></td>
      </tr>
      <tr>
        <td align="right" class="setting_links"><a href="picture.aspx">آپلود عکس خود</a></td>
      </tr>
      <tr>
        <td align="right" class="setting_links"><a href="mobile.aspx">تنظیمات موبایل</a></td>
      </tr>
      <tr>
        <td align="right" class="setting_links"><a href="messenger.aspx">تنظیمات یاهو مسنجر</a></td>
      </tr>
    </table>
    <td class="td_mainborderRight-fill">&nbsp;</td>
  </tr>
  <tr>
    <td width="25" height="25" class="td_mainborderButtom-left">&nbsp;</td>
    <td class="td_mainborderButtom-fill">&nbsp;</td>
    <td width="25" height="25" class="td_mainborderButtom-right">&nbsp;</td>
  </tr>
</table>

        </td>
      </tr>
      
    </table></td>
  </tr>
  <tr>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
  </tr>
</table>

</body>
</html>