<%@ Page Language="C#" CodeFile="signup.aspx.cs" Inherits="Peyghamak.signup"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link href="http://www.peyghamak.com/theme/skeleton.css" rel="stylesheet" type="text/css" />
<link href="http://www.peyghamak.com/theme/green.css" rel="stylesheet" type="text/css" />
<link href="http://www.peyghamak.com/theme/direction.css" rel="stylesheet" type="text/css" />
<SCRIPT language="javascript" src="/js/farsi.js"></SCRIPT>
<asp:PlaceHolder ID="MetaCopyrightSection" runat="server"></asp:PlaceHolder>
<title>ثبت نام در جامعه پیغامک</title>
</head>

<body>
<!--body-->
<div id="body">

<!--menu inner-->
<div id="menu">
    <div id="menu_inner"><asp:Panel ID="UnloginedPanel" runat="server" EnableViewState="False">
            <a href="http://www.peyghamak.com/signin.aspx" target="_self">ورود</a>
        </asp:Panel>
</div>
    <div class="menu_message">
    	
    </div>
</div>
<!--/menu inner-->

<!--left-->
<div id="left">

	<!--top left-->
	<div id="left_top">
    
    </div>
    <!--/top left-->
    
    <!--left fill-->
	<form id="form1" name="form1" method="post" runat="server">
      <div id="left_fill">
        <p> </p>
        <div id="signup_flowers">
          <div id="singup">
            <table width="400" border="0" cellspacing="4" cellpadding="2">
              <tr>
                <td width="20">&nbsp;</td>
                <td>&nbsp;</td>
                <td width="10">&nbsp;</td>
                <td width="120">&nbsp;</td>
              </tr>
              <tr>
                <td class="validation" align="right" dir="rtl" width="200px">
				<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="name"
                                  ErrorMessage="*" Display="Dynamic"  setfocusonerror="True"></asp:RequiredFieldValidator>
                </td>
                <td><div align="right">
                    <asp:TextBox CssClass="right_align" ID="name" MaxLength="30" runat="server" style="text-align:right" lang="fa"></asp:TextBox>
                </div></td>
                <td>&nbsp;</td>
                <td>نام و نام خانوادگی</td>
              </tr>
              <tr>
                <td class="validation"><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="username"
                                  ErrorMessage="*" Display="Static"  setfocusonerror="True"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="username"
                                ErrorMessage="*" Display="Static" setfocusonerror="True" ValidationExpression="^[\-0-9a-zA-Z]{1,}$"></asp:RegularExpressionValidator></td>
                <td><div align="right">
                    <asp:TextBox ID="username" MaxLength="30" runat="server"></asp:TextBox>
                </div></td>
                <td>&nbsp;</td>
                <td>نام کاربری</td>
              </tr>
              <tr>
                <td class="validation"><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="password"
                                  ErrorMessage="*" Display="Static"  setfocusonerror="True"></asp:RequiredFieldValidator>
                </td>
                <td><div align="right">
                    <asp:TextBox ID="password" MaxLength="30" runat="server" TextMode="Password"></asp:TextBox>
                </div></td>
                <td>&nbsp;</td>
                <td> کلمه عبور</td>
              </tr>
              <tr>
                <td class="validation"><asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="confirmPasword"
                               ErrorMessage="*" Display="Static" setfocusonerror="True"></asp:RequiredFieldValidator>
                    <span class="error">
                      <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="password"
                                  ControlToValidate="confirmPasword" ErrorMessage="*" Display="Static"  setfocusonerror="True"></asp:CompareValidator>
                    </span></td>
                <td><div align="right">
                    <asp:TextBox ID="confirmPasword" MaxLength="30" runat="server" TextMode="Password" Wrap="false"></asp:TextBox>
                </div></td>
                <td>&nbsp;</td>
                <td>تکرار کلمه عبور</td>
              </tr>
              <tr>
                <td class="validation"><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="email"
                               ErrorMessage="*" Display="Static" setfocusonerror="True"></asp:RequiredFieldValidator>
                    <span class="error">
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="email"
                                      ErrorMessage="*" Display="Static" setfocusonerror="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </span></td>
                <td><div align="right">
                    <asp:TextBox ID="email" MaxLength="30" runat="server"></asp:TextBox>
                </div></td>
                <td>&nbsp;</td>
                <td>آدرس ایمیل</td>
              </tr>
              <tr>
                <td class="validation"><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="BirthYear"
                                  ErrorMessage="*" Display="Static" InitialValue="none"  setfocusonerror="True"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                  ControlToValidate="BirthMonth" ErrorMessage="*" Display="Static" InitialValue="none"  setfocusonerror="True"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                  ControlToValidate="BirthDay" ErrorMessage="*" Display="Static" InitialValue="none"  setfocusonerror="True"></asp:RequiredFieldValidator>
                </td>
                <td><div align="right"><asp:DropDownList ID="BirthYear" runat="server"  tooltip="سال تولد">
                          <asp:ListItem Value="none">سال</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="BirthMonth" runat="server"  tooltip="ماه تولد">
                          <asp:ListItem Value="none">ماه</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList CssClass="i_list" ID="BirthDay" runat="server" tooltip="روز تولد">
                          <asp:ListItem Value="none">روز</asp:ListItem>
                        </asp:DropDownList>
                </div></td>
                <td>&nbsp;</td>
                <td>تاریخ تولد</td>
              </tr>
              <tr>
                <td class="validation"><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="sex"
                                  ErrorMessage="*" Display="Static" InitialValue="none"  setfocusonerror="True"></asp:RequiredFieldValidator>
                </td>
                <td><div align="right">
                    <asp:DropDownList CssClass="i_list" ID="sex" runat="server">
                      <asp:ListItem Value="none">.انتخاب کنید</asp:ListItem>
                      <asp:ListItem Value="false">مرد</asp:ListItem>
                      <asp:ListItem Value="true">زن</asp:ListItem>
                    </asp:DropDownList>
                </div></td>
                <td>&nbsp;</td>
                <td>جنس </td>
              </tr>
              <tr>
                <td class="validation"><asp:RequiredFieldValidator ID="RequiredFieldValidator300" runat="server" ControlToValidate="CountryKey"
                                  ErrorMessage="*" Display="Static" InitialValue="none"  setfocusonerror="True"></asp:RequiredFieldValidator>
                </td>
                <td><div align="right">
                    <asp:DropDownList ID="CountryKey" runat="server"  style="width:150px">
                      <asp:ListItem Value="none">.انتخاب کنید</asp:ListItem>
                      <asp:ListItem Value="afg">افغانستان</asp:ListItem>
                      <asp:ListItem Value="alb">Albania</asp:ListItem>
                      <asp:ListItem Value="dza">Algeria</asp:ListItem>
                      <asp:ListItem Value="asm">American samoa</asp:ListItem>
                      <asp:ListItem Value="and">Andorra</asp:ListItem>
                      <asp:ListItem Value="ago">Angola</asp:ListItem>
                      <asp:ListItem Value="aia">Anguilla</asp:ListItem>
                      <asp:ListItem Value="atg">Antigua and barbuda</asp:ListItem>
                      <asp:ListItem Value="arg">Argentina</asp:ListItem>
                      <asp:ListItem Value="arm">Armenia</asp:ListItem>
                      <asp:ListItem Value="abw">Aruba</asp:ListItem>
                      <asp:ListItem Value="aus">Australia</asp:ListItem>
                      <asp:ListItem Value="aut">Austria</asp:ListItem>
                      <asp:ListItem Value="aze">Azerbaijan</asp:ListItem>
                      <asp:ListItem Value="bhs">Bahamas</asp:ListItem>
                      <asp:ListItem Value="bhr">Bahrain</asp:ListItem>
                      <asp:ListItem Value="bgd">Bangladesh</asp:ListItem>
                      <asp:ListItem Value="brb">Barbados</asp:ListItem>
                      <asp:ListItem Value="blr">Belarus</asp:ListItem>
                      <asp:ListItem Value="bel">Belgium</asp:ListItem>
                      <asp:ListItem Value="blz">Belize</asp:ListItem>
                      <asp:ListItem Value="ben">Benin</asp:ListItem>
                      <asp:ListItem Value="bmu">Bermuda</asp:ListItem>
                      <asp:ListItem Value="btn">Bhutan</asp:ListItem>
                      <asp:ListItem Value="bol">Bolivia</asp:ListItem>
                      <asp:ListItem Value="bih">Bosnia and herzegovina</asp:ListItem>
                      <asp:ListItem Value="bwa">Botswana</asp:ListItem>
                      <asp:ListItem Value="bra">Brazil</asp:ListItem>
                      <asp:ListItem Value="brn">Brunei darussalam</asp:ListItem>
                      <asp:ListItem Value="bgr">Bulgaria</asp:ListItem>
                      <asp:ListItem Value="bfa">Burkina faso</asp:ListItem>
                      <asp:ListItem Value="bdi">Burundi</asp:ListItem>
                      <asp:ListItem Value="khm">Cambodia</asp:ListItem>
                      <asp:ListItem Value="cmr">Cameroon</asp:ListItem>
                      <asp:ListItem Value="can">Canada</asp:ListItem>
                      <asp:ListItem Value="cpv">Cape verde</asp:ListItem>
                      <asp:ListItem Value="cym">Cayman islands</asp:ListItem>
                      <asp:ListItem Value="caf">Central african republic</asp:ListItem>
                      <asp:ListItem Value="tcd">Chad</asp:ListItem>
                      <asp:ListItem Value="chl">Chile</asp:ListItem>
                      <asp:ListItem Value="chn">China</asp:ListItem>
                      <asp:ListItem Value="cxr">Christmas island</asp:ListItem>
                      <asp:ListItem Value="cck">Cocos (keeling)</asp:ListItem>
                      <asp:ListItem Value="col">Colombia</asp:ListItem>
                      <asp:ListItem Value="com">Comoros</asp:ListItem>
                      <asp:ListItem Value="cod">Congo</asp:ListItem>
                      <asp:ListItem Value="cog">Congo - dem. rep.</asp:ListItem>
                      <asp:ListItem Value="cok">Cook islands</asp:ListItem>
                      <asp:ListItem Value="cri">Costa rica</asp:ListItem>
                      <asp:ListItem Value="civ">Cote divoire</asp:ListItem>
                      <asp:ListItem Value="hrv">Croatia</asp:ListItem>
                      <asp:ListItem Value="cub">Cuba</asp:ListItem>
                      <asp:ListItem Value="cyp">Cyprus</asp:ListItem>
                      <asp:ListItem Value="cze">Czech republic</asp:ListItem>
                      <asp:ListItem Value="dnk">Denmark</asp:ListItem>
                      <asp:ListItem Value="dji">Djibouti</asp:ListItem>
                      <asp:ListItem Value="dma">Dominica</asp:ListItem>
                      <asp:ListItem Value="dom">Dominican republic</asp:ListItem>
                      <asp:ListItem Value="ecu">Ecuador</asp:ListItem>
                      <asp:ListItem Value="egy">Egypt</asp:ListItem>
                      <asp:ListItem Value="slv">El salvador</asp:ListItem>
                      <asp:ListItem Value="gnq">Equatorial guinea</asp:ListItem>
                      <asp:ListItem Value="eri">Eritrea</asp:ListItem>
                      <asp:ListItem Value="est">Estonia</asp:ListItem>
                      <asp:ListItem Value="eth">Ethiopia</asp:ListItem>
                      <asp:ListItem Value="flk">Falkland islands (malvinas)</asp:ListItem>
                      <asp:ListItem Value="fro">Faroe islands</asp:ListItem>
                      <asp:ListItem Value="fji">Fiji</asp:ListItem>
                      <asp:ListItem Value="fin">Finland</asp:ListItem>
                      <asp:ListItem Value="fra">France</asp:ListItem>
                      <asp:ListItem Value="guf">French guiana</asp:ListItem>
                      <asp:ListItem Value="pyf">French polynesia</asp:ListItem>
                      <asp:ListItem Value="gab">Gabon</asp:ListItem>
                      <asp:ListItem Value="gmb">Gambia</asp:ListItem>
                      <asp:ListItem Value="geo">Georgia</asp:ListItem>
                      <asp:ListItem Value="deu">Germany</asp:ListItem>
                      <asp:ListItem Value="gha">Ghana</asp:ListItem>
                      <asp:ListItem Value="gib">Gibraltar</asp:ListItem>
                      <asp:ListItem Value="grc">Greece</asp:ListItem>
                      <asp:ListItem Value="grl">Greenland</asp:ListItem>
                      <asp:ListItem Value="grd">Grenada</asp:ListItem>
                      <asp:ListItem Value="glp">Guadeloupe</asp:ListItem>
                      <asp:ListItem Value="gum">Guam</asp:ListItem>
                      <asp:ListItem Value="gtm">Guatemala</asp:ListItem>
                      <asp:ListItem Value="gin">Guinea</asp:ListItem>
                      <asp:ListItem Value="gnb">Guinea-bissau</asp:ListItem>
                      <asp:ListItem Value="guy">Guyana</asp:ListItem>
                      <asp:ListItem Value="hti">Haiti</asp:ListItem>
                      <asp:ListItem Value="vat">Holy see</asp:ListItem>
                      <asp:ListItem Value="hnd">Honduras</asp:ListItem>
                      <asp:ListItem Value="hkg">Hong kong</asp:ListItem>
                      <asp:ListItem Value="hun">Hungary</asp:ListItem>
                      <asp:ListItem Value="isl">Iceland</asp:ListItem>
                      <asp:ListItem Value="ind">India</asp:ListItem>
                      <asp:ListItem Value="idn">Indonesia</asp:ListItem>
                      <asp:ListItem Value="irn" Selected="True">ایران</asp:ListItem>
                      <asp:ListItem Value="irq">Iraq</asp:ListItem>
                      <asp:ListItem Value="irl">Ireland</asp:ListItem>
                      <asp:ListItem Value="isr">Israel</asp:ListItem>
                      <asp:ListItem Value="ita">Italy</asp:ListItem>
                      <asp:ListItem Value="jam">Jamaica</asp:ListItem>
                      <asp:ListItem Value="jpn">Japan</asp:ListItem>
                      <asp:ListItem Value="jor">Jordan</asp:ListItem>
                      <asp:ListItem Value="kaz">Kazakhstan</asp:ListItem>
                      <asp:ListItem Value="ken">Kenya</asp:ListItem>
                      <asp:ListItem Value="kir">Kiribati</asp:ListItem>
                      <asp:ListItem Value="prk">Korea - dem. peoples rep.</asp:ListItem>
                      <asp:ListItem Value="kor">Korea - republic of</asp:ListItem>
                      <asp:ListItem Value="kwt">Kuwait</asp:ListItem>
                      <asp:ListItem Value="kgz">Kyrgyzstan</asp:ListItem>
                      <asp:ListItem Value="lao">Lao - peoples dem. rep.</asp:ListItem>
                      <asp:ListItem Value="lva">Latvia</asp:ListItem>
                      <asp:ListItem Value="lbn">Lebanon</asp:ListItem>
                      <asp:ListItem Value="lso">Lesotho</asp:ListItem>
                      <asp:ListItem Value="lbr">Liberia</asp:ListItem>
                      <asp:ListItem Value="lie">Liechtenstein</asp:ListItem>
                      <asp:ListItem Value="ltu">Lithuania</asp:ListItem>
                      <asp:ListItem Value="lux">Luxembourg</asp:ListItem>
                      <asp:ListItem Value="mac">Macao</asp:ListItem>
                      <asp:ListItem Value="mkd">Macedonia</asp:ListItem>
                      <asp:ListItem Value="mdg">Madagascar</asp:ListItem>
                      <asp:ListItem Value="mwi">Malawi</asp:ListItem>
                      <asp:ListItem Value="mys">Malaysia</asp:ListItem>
                      <asp:ListItem Value="mdv">Maldives</asp:ListItem>
                      <asp:ListItem Value="mli">Mali</asp:ListItem>
                      <asp:ListItem Value="mlt">Malta</asp:ListItem>
                      <asp:ListItem Value="mnp">Mariana islands, n.</asp:ListItem>
                      <asp:ListItem Value="mhl">Marshall islands</asp:ListItem>
                      <asp:ListItem Value="mtq">Martinique</asp:ListItem>
                      <asp:ListItem Value="mrt">Mauritania</asp:ListItem>
                      <asp:ListItem Value="mus">Mauritius</asp:ListItem>
                      <asp:ListItem Value="myt">Mayotte</asp:ListItem>
                      <asp:ListItem Value="mex">Mexico</asp:ListItem>
                      <asp:ListItem Value="fsm">Micronesia</asp:ListItem>
                      <asp:ListItem Value="mda">Moldova</asp:ListItem>
                      <asp:ListItem Value="mco">Monaco</asp:ListItem>
                      <asp:ListItem Value="mng">Mongolia</asp:ListItem>
                      <asp:ListItem Value="msr">Montserrat</asp:ListItem>
                      <asp:ListItem Value="mar">Morocco</asp:ListItem>
                      <asp:ListItem Value="moz">Mozambique</asp:ListItem>
                      <asp:ListItem Value="mmr">Myanmar</asp:ListItem>
                      <asp:ListItem Value="nam">Namibia</asp:ListItem>
                      <asp:ListItem Value="nru">Nauru</asp:ListItem>
                      <asp:ListItem Value="npl">Nepal</asp:ListItem>
                      <asp:ListItem Value="nld">Netherlands</asp:ListItem>
                      <asp:ListItem Value="ant">Netherlands antilles</asp:ListItem>
                      <asp:ListItem Value="ncl">New caledonia</asp:ListItem>
                      <asp:ListItem Value="nzl">New zealand</asp:ListItem>
                      <asp:ListItem Value="nic">Nicaragua</asp:ListItem>
                      <asp:ListItem Value="ner">Niger</asp:ListItem>
                      <asp:ListItem Value="nga">Nigeria</asp:ListItem>
                      <asp:ListItem Value="niu">Niue</asp:ListItem>
                      <asp:ListItem Value="nfk">Norfolk island</asp:ListItem>
                      <asp:ListItem Value="nor">Norway</asp:ListItem>
                      <asp:ListItem Value="omn">Oman</asp:ListItem>
                      <asp:ListItem Value="pak">Pakistan</asp:ListItem>
                      <asp:ListItem Value="plw">Palau</asp:ListItem>
                      <asp:ListItem Value="pse">Palestinia</asp:ListItem>
                      <asp:ListItem Value="pan">Panama</asp:ListItem>
                      <asp:ListItem Value="png">Papua new guinea</asp:ListItem>
                      <asp:ListItem Value="pry">Paraguay</asp:ListItem>
                      <asp:ListItem Value="per">Peru</asp:ListItem>
                      <asp:ListItem Value="phl">Philippines</asp:ListItem>
                      <asp:ListItem Value="pcn">Pitcairn</asp:ListItem>
                      <asp:ListItem Value="pol">Poland</asp:ListItem>
                      <asp:ListItem Value="prt">Portugal</asp:ListItem>
                      <asp:ListItem Value="pri">Puerto rico</asp:ListItem>
                      <asp:ListItem Value="qat">Qatar</asp:ListItem>
                      <asp:ListItem Value="reu">Reunion</asp:ListItem>
                      <asp:ListItem Value="rou">Romania</asp:ListItem>
                      <asp:ListItem Value="rus">Russia</asp:ListItem>
                      <asp:ListItem Value="rwa">Rwanda</asp:ListItem>
                      <asp:ListItem Value="shn">Saint helena</asp:ListItem>
                      <asp:ListItem Value="kna">Saint kitts</asp:ListItem>
                      <asp:ListItem Value="lca">Saint lucia</asp:ListItem>
                      <asp:ListItem Value="spm">Saint pierre and miquelon</asp:ListItem>
                      <asp:ListItem Value="vct">Saint vincent and the grenadines</asp:ListItem>
                      <asp:ListItem Value="wsm">Samoa</asp:ListItem>
                      <asp:ListItem Value="smr">San marino</asp:ListItem>
                      <asp:ListItem Value="stp">Sao tome and principe</asp:ListItem>
                      <asp:ListItem Value="sau">Saudi arabia</asp:ListItem>
                      <asp:ListItem Value="sen">Senegal</asp:ListItem>
                      <asp:ListItem Value="syc">Seychelles</asp:ListItem>
                      <asp:ListItem Value="sle">Sierra leone</asp:ListItem>
                      <asp:ListItem Value="sgp">Singapore</asp:ListItem>
                      <asp:ListItem Value="svk">Slovakia</asp:ListItem>
                      <asp:ListItem Value="svn">Slovenia</asp:ListItem>
                      <asp:ListItem Value="slb">Solomon islands</asp:ListItem>
                      <asp:ListItem Value="som">Somalia</asp:ListItem>
                      <asp:ListItem Value="zaf">South africa</asp:ListItem>
                      <asp:ListItem Value="esp">Spain</asp:ListItem>
                      <asp:ListItem Value="lka">Sri lanka</asp:ListItem>
                      <asp:ListItem Value="sdn">Sudan</asp:ListItem>
                      <asp:ListItem Value="sur">Suriname</asp:ListItem>
                      <asp:ListItem Value="swz">Swaziland</asp:ListItem>
                      <asp:ListItem Value="swe">Sweden</asp:ListItem>
                      <asp:ListItem Value="che">Switzerland</asp:ListItem>
                      <asp:ListItem Value="syr">Syrian arab republic</asp:ListItem>
                      <asp:ListItem Value="twn">Taiwan</asp:ListItem>
                      <asp:ListItem Value="tjk">تاجیکستان</asp:ListItem>
                      <asp:ListItem Value="tza">Tanzania</asp:ListItem>
                      <asp:ListItem Value="tha">Thailand</asp:ListItem>
                      <asp:ListItem Value="tls">Timor-leste</asp:ListItem>
                      <asp:ListItem Value="tgo">Togo</asp:ListItem>
                      <asp:ListItem Value="tkl">Tokelau</asp:ListItem>
                      <asp:ListItem Value="ton">Tonga</asp:ListItem>
                      <asp:ListItem Value="tto">Trinidad and tobago</asp:ListItem>
                      <asp:ListItem Value="tun">Tunisia</asp:ListItem>
                      <asp:ListItem Value="tur">Turkey</asp:ListItem>
                      <asp:ListItem Value="tkm">Turkmenistan</asp:ListItem>
                      <asp:ListItem Value="tca">Turks and caicos islands</asp:ListItem>
                      <asp:ListItem Value="tuv">Tuvalu</asp:ListItem>
                      <asp:ListItem Value="uga">Uganda</asp:ListItem>
                      <asp:ListItem Value="ukr">Ukraine</asp:ListItem>
                      <asp:ListItem Value="are">امارات متحده عربي</asp:ListItem>
                      <asp:ListItem Value="gbr">انگلیس</asp:ListItem>
                      <asp:ListItem Value="usa">آمریکا</asp:ListItem>
                      <asp:ListItem Value="ury">Uruguay</asp:ListItem>
                      <asp:ListItem Value="uzb">Uzbekistan</asp:ListItem>
                      <asp:ListItem Value="vut">Vanuatu</asp:ListItem>
                      <asp:ListItem Value="ven">Venezuela</asp:ListItem>
                      <asp:ListItem Value="vnm">Viet nam</asp:ListItem>
                      <asp:ListItem Value="vgb">Virgin islands - british</asp:ListItem>
                      <asp:ListItem Value="vir">Virgin islands - u.s.</asp:ListItem>
                      <asp:ListItem Value="wlf">Wallis and futuna</asp:ListItem>
                      <asp:ListItem Value="yem">Yemen</asp:ListItem>
                      <asp:ListItem Value="scg">Serbia and montenegro</asp:ListItem>
                      <asp:ListItem Value="zmb">Zambia</asp:ListItem>
                      <asp:ListItem Value="zwe">Zimbabwe</asp:ListItem>
                    </asp:DropDownList>
                </div></td>
                <td>&nbsp;</td>
                <td>کشور </td>
              </tr>
              <tr>
                <td class="validation"><asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ProvinceKey"
                                  ErrorMessage="*" Display="Static" InitialValue="none"  setfocusonerror="True"></asp:RequiredFieldValidator>
                </td>
                <td><div align="right">
                    <asp:DropDownList ID="ProvinceKey" runat="server" style="width:150px">
                                <asp:ListItem Value="none" Selected="true">.انتخاب کنید</asp:ListItem>
                                <asp:ListItem Value="1">آذربايجان شرقي</asp:ListItem>
                                <asp:ListItem Value="2">آذربايجان غربي</asp:ListItem>
                                <asp:ListItem Value="3">اردبيل</asp:ListItem>
                                <asp:ListItem Value="4">اصفهان</asp:ListItem>
                                <asp:ListItem Value="5">ايلام</asp:ListItem>
                                <asp:ListItem Value="6">بوشهر</asp:ListItem>
                                <asp:ListItem Value="7">تهران</asp:ListItem>
                                <asp:ListItem Value="8">چهال محال و بختياري</asp:ListItem>
                                <asp:ListItem Value="9">خراسان</asp:ListItem>
                                <asp:ListItem Value="10">خوزستان</asp:ListItem>
                                <asp:ListItem Value="11">زنجان</asp:ListItem>
                                <asp:ListItem Value="12">سمنان</asp:ListItem>
                                <asp:ListItem Value="13">سيستان و بلوچستان</asp:ListItem>
                                <asp:ListItem Value="14">فارس</asp:ListItem>
                                <asp:ListItem Value="15">قزوين</asp:ListItem>
                                <asp:ListItem Value="16">قم</asp:ListItem>
                                <asp:ListItem Value="17">کردستان</asp:ListItem>
                                <asp:ListItem Value="18">کرمان</asp:ListItem>
                                <asp:ListItem Value="19">کرمانشاه</asp:ListItem>
                                <asp:ListItem Value="20">کهگيلويه و بوير احمد</asp:ListItem>
                                <asp:ListItem Value="21">گلستان</asp:ListItem>
                                <asp:ListItem Value="22">گيلان</asp:ListItem>
                                <asp:ListItem Value="23">لرستان</asp:ListItem>
                                <asp:ListItem Value="24">مازندران</asp:ListItem>
                                <asp:ListItem Value="25">مرکزي</asp:ListItem>
                                <asp:ListItem Value="26">هرمزگان</asp:ListItem>
                                <asp:ListItem Value="27">همدان</asp:ListItem>
                                <asp:ListItem Value="28">يزد</asp:ListItem>
                     <asp:ListItem Value="0">کشورم ایران نیست</asp:ListItem>
                      </asp:DropDownList>
                </div></td>
                <td>&nbsp;</td>
                <td>استان </td>
              </tr>
              <tr>
                <td class="validation"><asp:RequiredFieldValidator ID="RequiredFieldValidator6565" runat="server" ControlToValidate="city"
                                  ErrorMessage="*" Display="Static"  setfocusonerror="True"></asp:RequiredFieldValidator>
                </td>
                <td><div align="right">
                    <asp:TextBox ID="city" MaxLength="30" runat="server" style="text-align:right" lang="fa"></asp:TextBox>
                </div></td>
                <td>&nbsp;</td>
                <td>شهر </td>
              </tr>
              <tr>
                <td class="validation">&nbsp;</td>
                <td><div align="right">
                  <asp:Image ID="Image1" runat="server" Height="40px"  Width="130px" ImageUrl="RandomImage.aspx" />      
                </div></td>
                <td>&nbsp;</td>
                <td>کد امنیتی</td>
              </tr>
              <tr>
                <td class="validation"><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="SecurityCode"
                                  ErrorMessage="*" Display="Static"  setfocusonerror="True"></asp:RequiredFieldValidator>
                </td>
                <td><div align="right">
                    <asp:TextBox ID="SecurityCode" MaxLength="30" runat="server"></asp:TextBox>
                </div></td>
                <td>&nbsp;</td>
                <td>کد امنیتی را وارد کنید</td>
              </tr>
              <tr>
                <td class="validation">&nbsp;</td>
                <td><div align="right">
                    <asp:Button ID="save" runat="server" Text="ثبت نام" OnClick="save_Click"/>      
                </div></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
              </tr>
              <tr>
                <td colspan="2" class="validation" dir="rtl"><asp:Label ID="message" runat="server"></asp:Label></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
              </tr>
            </table>
          </div>
        </div>
        <p></p>
      </div>
    </form>
<!--/left fill-->
   <div id="left_bottom">
   	<span class="bottom_menus">
	   <asp:PlaceHolder ID="SiteFooterSection" runat="server"></asp:PlaceHolder>
    </span>
   </div>    
</div>
<!--/left-->

<!--right-->
<div id="right">

<div class="right_sidebar_top">
<div class="right_sidebar_text">

</div>
</div>
<div class="right_sidebar_fill">
    <p>&nbsp;</p>
  	<form action="signin.aspx?mode=h" method="post">
    <div id="login_compact">
    نام کاربری:
    <input type="text" name="username" id="username"  class="login_txtbox"/>
    کلمه عبور:
    <input type="password" name="password" id="username" class="login_txtbox" />
    <input type="submit" value="ورود" class="login_btn"/>
    </div>
    </form>
</div>

<div class="right_sidebar_btm">&nbsp;&nbsp;&nbsp;&nbsp;</div>
</div>
<!--/right-->


</div>
<!--/body-->

</body>
</html>
