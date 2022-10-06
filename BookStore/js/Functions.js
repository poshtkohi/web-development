/*
	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
	Email: arp@poshtkohi.info
	Website: http://www.poshtkohi.info
*/

//Functions.js
//Version: 2.1
//This script is created by Alireza Poshtkohi. Do not remove, modify, or hide the author information. keep it intact.
//Mail: alireza.poshtkohi@gmail.com 

//-----------------------------global variables---------------------------------------------------------------------
var _isInUpdateMode = false;
var _isBoolDetailsLayerFirstLoad = true;
var _SiteLanguageIsPersian = true;
var _updateID = -1;
var _editorIsDefined = false;
var _listBooksByCategory = '';
var _listBooksByCategoryLanguage = 0;
var _signInURL = '/signin.aspx';
var _tinyMCECompare = '<p style="font-family: tahoma;font-size: 10pt" dir="rtl">&nbsp;</p>';

var langFarsi = 1;
var farsikey = [  
	0x0020, 0x0021, 0x061B, 0x066B, 0x00A4, 0x066A, 0x060C, 0x06AF,      
	0x0029, 0x0028, 0x002A, 0x002B, 0x0648, 0x002D, 0x002E, 0x002F,      
	0x06F0, 0x06F1, 0x06F2, 0x06F3, 0x06F4, 0x06F5, 0x06F6, 0x06F7,      
	0x06F8, 0x06F9, 0x003A, 0x06A9, 0x003E, 0x003D, 0x003C, 0x061F,      
	0x066C, 0x0624, 0x200C, 0x0698, 0x064A, 0x064D, 0x0625, 0x0623,      
	0x0622, 0x0651, 0x0629, 0x00BB, 0x00AB, 0x0621, 0x004E, 0x005D,      
	0x005B, 0x0652, 0x064B, 0x0626, 0x064F, 0x064E, 0x0056, 0x064C,            
	0x0058, 0x0650, 0x0643, 0x062C, 0x0698, 0x0686, 0x00D7, 0x0640,            
	0x067E, 0x0634, 0x0630, 0x0632, 0x06CC, 0x062B, 0x0628, 0x0644,            
	0x0627, 0x0647, 0x062A, 0x0646, 0x0645, 0x0626, 0x062F, 0x062E,            
	0x062D, 0x0636, 0x0642, 0x0633, 0x0641, 0x0639, 0x0631, 0x0635,            
	0x0637, 0x063A, 0x0638, 0x007D, 0x007C, 0x007B, 0x007E            
];
//------------------------------------------------------------------------------------------------------------------
function SearchBoxCleaner()
{
	if(document.getElementById('searchbox').value == 'جستجو')
		document.getElementById('searchbox').value = '';
}
//------------------------------------------------------------------------------------------------------------------
function scroll(){
//    window.scrollTo(1000,1000);alert("hi");
//	window.frames[0].scrollTo(0,0);
	document.getElementById('message').focus();
  }
function startCallback()
{
	var _filename = document.getElementById('file').value.toLowerCase();
	if(_filename == '')
	{
		alert('.یک عکس انتحاب کنید');
		return false;
	}
	if(_filename.indexOf('.jpg') <= 0 && _filename.indexOf('.png') <= 0 && _filename.indexOf('.gif') <= 0)
	{
		alert('.فرمت فایل تصویر نامعتبر است، فرمت فایل بایستی از نوع تصویر باشد');
		return false;
	}
	document.getElementById('message').innerHTML = loaderUI;
	document.getElementById('message').style.display = 'block';
	dynaframe();
//	document.getElementById('post').disabled=true;
	return true;
}
function completeCallback(response)
{
	document.getElementById('message').innerHTML = '';
	document.getElementById('message').style.display = 'none';
//	document.getElementById('post').disabled=true;
	document.getElementById('file').value = '';
	dynaframe();
	alert('.عکس شما با موفقیت در سایت ایران بلاگ به روز رسانی شد');
	return;
}
//------------------------------------------------------------------------------------------------------------------
function DoPost(_mode)
{
	var serverPage='';
	switch(_mode)
	{
		case 'NewsAdmin':
			serverPage='/Admin/NewsAdmin.aspx';
			var NewsTitle = document.getElementById('NewsTitle').value;
			var NewsContent = tinyMCE.get('NewsContent').getContent();	
			var NewsLanguage = document.getElementById('NewsLanguage').value;
			var IsTopNews = document.getElementById('IsTopNews').value;
			if(NewsTitle == '')
			{
				alert('.عنوان خبر خالی است');
				return ;
			}
			if(NewsContent == '' || NewsContent == _tinyMCECompare)
			{
				alert('.متن خبر خالی است');
				return ;
			}
			break;
		case 'PagesAdmin':
			serverPage='/Admin/PagesAdmin.aspx';
			var PageTitle = document.getElementById('PageTitle').value;
			var PageContent = tinyMCE.get('PageContent').getContent();
			var PageLanguage = document.getElementById('PageLanguage').value;
			if(PageTitle == '')
			{
				alert('.عنوان صفحه خالی است');
				return ;
			}
			if(PageContent == '' || PageContent == _tinyMCECompare)
			{
				alert('.محتوی صفحه خالی است');
				return ;
			}
			break;
		case 'CategoryAdmin':
			serverPage='/Admin/CategoryAdmin.aspx';
			var EnglishCategory = document.getElementById('EnglishCategory').value;
			var PersianCategory = document.getElementById('PersianCategory').value;
			if (EnglishCategory == "" && PersianCategory == "")
            {
				alert('.مقوله کتاب خالی است');
				return ;
            }
			break;
		case 'BooksAdmin':
			serverPage='/Admin/BooksAdmin.aspx';
			var EnglishCategory = document.getElementById('EnglishCategory').value;
			var PersianCategory = document.getElementById('PersianCategory').value;
			var Title = document.getElementById('Title').value;
			var Writer = document.getElementById('Writer').value;
			var Translator = document.getElementById('Translator').value;
			var Publisher = document.getElementById('Publisher').value;
			var PublishDate = document.getElementById('PublishDate').value;
			var Pages = document.getElementById('Pages').value;
			var ISBN = document.getElementById('ISBN').value;
			var FileType = document.getElementById('FileType').value;
			var FileSize = document.getElementById('FileSize').value;
			var Price = document.getElementById('Price').value;
			var Abstract = document.getElementById('Abstract').value;
			var filename = document.getElementById('filename').value;
			var Language = document.getElementById('Language').value;
			if (Title == "" && Title == "")
            {
				alert('.عنوان کتاب خالی است');
				return ;
            }
			break;
		case 'CustomersAdmin':
			serverPage='/Admin/CustomersAdmin.aspx';
			var PersianCustomerName = document.getElementById('PersianCustomerName').value;
			var EnglishCustomerName = document.getElementById('EnglishCustomerName').value;
			var CustomerLink = document.getElementById('CustomerLink').value;
            if (PersianCustomerName == "")
            {
                alert(".نام مشتری خالی است");
                return;
            }

            if (CustomerLink == "")
            {
                alert(".لینک مشتری خالی است");
                return;
            }
			var re = /^(http):\/\/\S+\.\S/;
			if(!re.test(CustomerLink))
			{
			   alert(".لینک مشتری نامعتبر است");
			   return ;
			}
			break;
		case 'AddToShoppingCart':
			serverPage='/';
			break;
		case 'DoPurchase':
			if (confirm('آیا شما مطمئن هستید؟'))
			{
				serverPage='/cp/ShoppingCart.aspx';
				break;
			}
			else
				return;
		case 'PurchaseVerify':
			serverPage='/Admin/PurchasesAdmin.aspx';
			break;
		default:
			return;
	}
	switch(_mode)
	{
		case 'AddToShoppingCart':
			document.getElementById('loaderImg_'+_mode).style.display = 'block';
			break;
		case 'PurchaseVerify':
			document.getElementById('loaderImg_ShowUnverifiedPurchases').style.display = 'block';
			break;
		case 'DoPurchase':
			document.getElementById('loaderImg_'+_mode).style.display = 'block';
			document.getElementById('PurchaseButton').disabled=true;
			break;
		default:
			//document.getElementById('post').disabled=true;
			document.getElementById('message').innerHTML = loaderUI;
			document.getElementById('message').style.display = 'block';
			dynaframe();
			break;
	}
	
	var xmlHttp = null;
	if((xmlHttp = createAjaxObject()) != null){
		xmlHttp.onreadystatechange=function()
		{
			if(xmlHttp.readyState==4)
			{
				if(xmlHttp.responseText == 'Logouted')
				{
					switch(_mode)
					{
						case 'AddToShoppingCart':
							window.location = _signInURL;
							break;
						case 'DoPurchase':
							window.location = _signInURL;
							break;
						default:
							window.location = 'Logouted.aspx';
							break;
					}
					return;
				}
				//document.getElementById('post').disabled=false;
				
				switch(_mode)
				{
					case 'AddToShoppingCart':
						document.getElementById('loaderImg_'+_mode).style.display = 'none';
						alert(xmlHttp.responseText);
						return;
					case 'PurchaseVerify':
						document.getElementById('loaderImg_ShowUnverifiedPurchases').style.display = 'none';
						alert(xmlHttp.responseText);
						ShowItems('1', 'ShowUnverifiedPurchases');
						ShowItems('1', 'ShowVerifiedPurchases');
						return;
					case 'DoPurchase':
						document.getElementById('loaderImg_'+_mode).style.display = 'none';
						document.getElementById('resultText_'+_mode).style.display = 'block';
						document.getElementById('resultText_'+_mode).innerHTML=xmlHttp.responseText;
						document.getElementById('resultText_ShowShoppingCart').disabled=true;
						for(var _i=0;;_i++)
						{
							if(document.getElementById('delete'+_i) == null)
								break;
							else
								document.getElementById('delete'+_i).style.visibility='hidden';
						}
						return ;
					default:
						document.getElementById('message').innerHTML='';
						document.getElementById('message').style.display = 'none';
						alert(xmlHttp.responseText);
						break;
				}

				switch(_mode)
				{
					case 'NewsAdmin':
						ShowItems('1', 'ShowNewsAdmin');
						break;
					case 'PagesAdmin':
						ShowItems('1', 'ShowPagesAdmin');
						break;
					case 'CategoryAdmin':
						ShowItems('1', 'ShowCategoryAdmin');
						Cancel();
						return;
					case 'BooksAdmin':
						ShowItems('1', 'ShowBooksAdmin');
						Cancel();
						return;
					case 'CustomersAdmin':
						ShowItems('1', 'ShowCustomersAdmin');
						Cancel();
						return;
					case '':
						ShowItems('1', '');
						Cancel();
						return;
					case '':
						ShowItems('1', '');
						Cancel();
						return;
					case '':
						ShowItems('1', '');
						Cancel();
						return;
					case '':
						ShowItems('1', '');
						Cancel();
						return;
					default:
						return;
				}
				if(_isInUpdateMode || _editorIsDefined)
					Cancel();
				else
					if(!_editorIsDefined)
						New(_mode);

				return;
			}
		}
		var __mode = 'mode=post';
		if(_isInUpdateMode)
		{
			switch(_mode)
			{
				/*case 'NewsAdminForNewsGroupsMode':
					__mode = 'mode=UpdateForNewsGroupsMode&id=' + _updateID;
					break;*/
				default:
					__mode = 'mode=update&id=' + _updateID;
					break;
			}
		}
		var parameters = '';
		switch(_mode)
		{
			case 'NewsAdmin':
				parameters = __mode + '&NewsTitle='+encodeURIComponent(NewsTitle)+'&NewsContent='+encodeURIComponent(NewsContent)+'&NewsLanguage='+encodeURIComponent(NewsLanguage)+'&IsTopNews='+encodeURIComponent(IsTopNews);
				break;
			case 'PagesAdmin':
				parameters = __mode + '&PageTitle='+encodeURIComponent(PageTitle)+'&PageContent='+encodeURIComponent(PageContent)+'&PageLanguage='+encodeURIComponent(PageLanguage);
				break;
			case 'CategoryAdmin':
				parameters = __mode + '&EnglishCategory='+encodeURIComponent(EnglishCategory)+'&PersianCategory='+encodeURIComponent(PersianCategory);
				break;
			case 'BooksAdmin':
				parameters = __mode +'&EnglishCategory='+encodeURIComponent(EnglishCategory)+'&PersianCategory='+encodeURIComponent(PersianCategory)+'&Title='+encodeURIComponent(Title)+'&Writer='+encodeURIComponent(Writer)+'&Translator='+encodeURIComponent(Translator)+'&Publisher='+encodeURIComponent(Publisher)+'&PublishDate='+encodeURIComponent(PublishDate)+'&Pages='+encodeURIComponent(Pages)+'&ISBN='+encodeURIComponent(ISBN)+'&FileType='+encodeURIComponent(FileType)+'&FileSize='+encodeURIComponent(FileSize)+'&Price='+encodeURIComponent(Price)+'&Abstract='+encodeURIComponent(Abstract)+'&filename='+encodeURIComponent(filename)+'&Language='+encodeURIComponent(Language);
				break;
			case 'CustomersAdmin':
				parameters = __mode + '&PersianCustomerName='+encodeURIComponent(PersianCustomerName)+ '&EnglishCustomerName='+encodeURIComponent(EnglishCustomerName)+'&CustomerLink='+encodeURIComponent(CustomerLink);
				break;
			case 'AddToShoppingCart':
				parameters = 'mode=AddToShoppingCart&BookID='+_updateID;
				break;
			case 'DoPurchase':
				parameters = 'mode=DoPurchase';
				break;
			case 'PurchaseVerify':
				parameters = 'mode=PurchaseVerify&PurchaseID='+_updateID;
				break;
			case '':
				parameters = __mode + '&NewsTitle='+encodeURIComponent(NewsTitle)+'&NewsContent='+encodeURIComponent(NewsContent);
				break;
			case '':
				parameters = __mode + '&NewsTitle='+encodeURIComponent(NewsTitle)+'&NewsContent='+encodeURIComponent(NewsContent);
				break;
			case '':
				parameters = __mode + '&NewsTitle='+encodeURIComponent(NewsTitle)+'&NewsContent='+encodeURIComponent(NewsContent);
				break;
			case '':
				parameters = __mode + '&NewsTitle='+encodeURIComponent(NewsTitle)+'&NewsContent='+encodeURIComponent(NewsContent);
				break;
			case '':
				parameters = __mode + '&NewsTitle='+encodeURIComponent(NewsTitle)+'&NewsContent='+encodeURIComponent(NewsContent);
				break;
			default:
				break;
		}
		xmlHttp.open("POST",serverPage,true);
 		xmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=UTF-8");
      	xmlHttp.setRequestHeader("Content-length", parameters.length);
	    xmlHttp.send(parameters);

	}
}
//------------------------------------------------------------------------------------------------------------------
function ShowItems(_page, _mode)
{
	var serverPage='';
	switch(_mode)
	{
		case 'ShowNewsAdmin':
			serverPage='/Admin/NewsAdmin.aspx';
			break;
		case 'ShowPagesAdmin':
			serverPage='/Admin/PagesAdmin.aspx';
			break;
		case 'ShowCategoryAdmin':
			serverPage='/Admin/CategoryAdmin.aspx';
			break;
		case 'ShowBooksAdmin':
			serverPage='/Admin/BooksAdmin.aspx';
			break;
		case 'ShowNewsHomePage':
			serverPage='/';
			break;
		case 'ShowNewsDetails':
			serverPage='/';
			break;
		case 'ShowAllBooks':
			serverPage='/';
			break;
		case 'ListBooksByCategory':
			serverPage='/';
			break;
		case 'ShowMainPageCustomers':
			serverPage='/';
			break;
		case 'ShowMainPageSitePages':
			serverPage='/';
			break;
		case 'ShowMainPageSitePageDetailes':
			serverPage='/';
			break;
		case 'ShowTopNews':
			serverPage='/';
			break;
		case 'ShowSiteStat':
			serverPage='/';
			break;
		case 'ShowCustomersAdmin':
			serverPage='/Admin/CustomersAdmin.aspx';
			break;
		case 'ShowByBookSearch':
			serverPage='/search.aspx';
			break;
		case 'ShowShoppingCart':
			serverPage='/cp/ShoppingCart.aspx';
			break;
		case 'ShowVerifiedPurchases':
			serverPage='/Admin/PurchasesAdmin.aspx';
			break;
		case 'ShowUnverifiedPurchases':
			serverPage='/Admin/PurchasesAdmin.aspx';
			break;
		case 'ShowPurchaseDetails':
			serverPage='/Admin/PurchaseDetails.aspx';
			break;
		case 'ShowUsers':
			serverPage='/Admin/UsersAdmin.aspx';
			break;
		case 'ShowUserDetails':
			serverPage='/Admin/UserDetails.aspx';
			break;
		case 'ShowTransactions':
			serverPage='/Admin/TransactionsAdmin.aspx';
			break;
		case 'ShowTotalAmount':
			serverPage='/Admin/TransactionsAdmin.aspx';
			break;
		default:
			return;
	}
	switch(_mode)
	{
		case 'ShowTransactions':
			document.getElementById('loaderImg_'+_mode).style.display = 'block';
			break;
		case 'ShowTotalAmount':
			document.getElementById('loaderImg_'+_mode).style.display = 'block';
			break;
		case 'ShowAllBooks':
			document.getElementById('loaderImg_'+_mode).style.display = 'block';
			break;
		case 'ShowUserDetails':
			document.getElementById('loaderImg_'+_mode).style.display = 'block';
			break;
		case 'ShowUsers':
			document.getElementById('loaderImg_'+_mode).style.display = 'block';
			break;
		case 'ShowVerifiedPurchases':
			document.getElementById('loaderImg_'+_mode).style.display = 'block';
			break;
		case 'ShowUnverifiedPurchases':
			document.getElementById('loaderImg_'+_mode).style.display = 'block';
			break;
		case 'ShowPurchaseDetails':
			document.getElementById('loaderImg_'+_mode).style.display = 'block';
			break;
		case 'ShowMainPageCustomers':
			document.getElementById('loaderImg_'+_mode).style.display = 'block';
			break;
		case 'ShowSiteStat':
			document.getElementById('loaderImg_'+_mode).style.display = 'block';
			break;
		case 'ShowMainPageSitePageDetailes':
			document.getElementById('loaderImg_'+_mode).style.display = 'block';
			break;
		case 'ShowTopNews':
			document.getElementById('loaderImg_'+_mode).style.display = 'block';
			break;
		case 'ShowMainPageSitePages':
			document.getElementById('loaderImg_'+_mode).style.display = 'block';
			break;
		case 'ShowShoppingCart':
			document.getElementById('loaderImg_'+_mode).style.display = 'block';
			break;
		case 'ListBooksByCategory':
			ShowBoolDetailsLayer(false, '-1');
			document.getElementById('loaderImg_ShowAllBooks').style.display = 'block';
			break;
		default:
			document.getElementById('loaderImg').style.display = 'block';
			break;
	}
	
	var xmlHttp = null;
	if((xmlHttp = createAjaxObject()) != null){
		xmlHttp.onreadystatechange=function()
		{
			if(xmlHttp.readyState==4)
			{
				if(xmlHttp.responseText == 'Logouted')
				{
					switch(_mode)
					{
						case 'ShowShoppingCart':
							window.location = _signInURL;
							break;
						default:
							window.location = 'Logouted.aspx';
							break;
					}
					return;
				}
				if(xmlHttp.responseText == 'DoRefresh')
				{
					ShowItems('1', _mode);
					return;
				}
				switch(_mode)
				{
					case 'ShowTransactions':
						document.getElementById('loaderImg_'+_mode).style.display = 'none';
						break;
					case 'ShowTotalAmount':
						document.getElementById('loaderImg_'+_mode).style.display = 'none';
						break;
					case 'ShowTopNews':
						document.getElementById('loaderImg_'+_mode).style.display = 'none';
						break;
					case 'ShowUserDetails':
						document.getElementById('loaderImg_'+_mode).style.display = 'none';
						break;
					case 'ShowUsers':
						document.getElementById('loaderImg_'+_mode).style.display = 'none';
						break;
					case 'ShowAllBooks':
						document.getElementById('loaderImg_'+_mode).style.display = 'none';
						break;
					case 'ShowVerifiedPurchases':
						document.getElementById('loaderImg_'+_mode).style.display = 'none';
						break;
					case 'ShowUnverifiedPurchases':
						document.getElementById('loaderImg_'+_mode).style.display = 'none';
						break;
					case 'ShowPurchaseDetails':
						document.getElementById('loaderImg_'+_mode).style.display = 'none';
						break;
					case 'ShowMainPageCustomers':
						document.getElementById('loaderImg_'+_mode).style.display = 'none';
						break;
					case 'ShowMainPageSitePages':
						document.getElementById('loaderImg_'+_mode).style.display = 'none';
						break;
					case 'ShowSiteStat':
						document.getElementById('loaderImg_'+_mode).style.display = 'none';
						break;
					case 'ShowMainPageSitePageDetailes':
						document.getElementById('loaderImg_'+_mode).style.display = 'none';
						break;
					case 'ShowShoppingCart':
						document.getElementById('loaderImg_'+_mode).style.display = 'none';
						break;
					default:
						document.getElementById('loaderImg').style.display = 'none';
						break;
				}
				if(xmlHttp.responseText == 'NoFoundPost')
				{
					switch(_mode)
					{
						case 'ShowTransactions':
							document.getElementById('resultText_'+_mode).innerHTML = "<br><div class='message'>تراکنشی وجود ندارد.</div>";
							return;
						case 'ShowTotalAmount':
							document.getElementById('resultText_'+_mode).innerHTML = "<br><div class='message'>اعتباری وجود ندارد.</div>";
							return;
						case 'ShowNewsAdmin':
							document.getElementById('resultText').innerHTML = "<br><div class='message'>خبری وجود ندارد.</div>";
							return;
						case 'ShowUserDetails':
							document.getElementById('resultText_'+_mode).innerHTML = "<br><div class='message'>کاربری وجود ندارد.</div>";
							return;
						case 'ShowUsers':
							document.getElementById('resultText_'+_mode).innerHTML = "<br><div class='message'>کاربری وجود ندارد.</div>";
							break;
						case 'ShowVerifiedPurchases':
							document.getElementById('resultText_'+_mode).innerHTML = "<br><div class='message'>خریدی وجود ندارد.</div>";
							break;
						case 'ShowUnverifiedPurchases':
							document.getElementById('resultText_'+_mode).innerHTML = "<br><div class='message'>خریدی وجود ندارد.</div>";
							break;
						case 'ShowPurchaseDetails':
							document.getElementById('resultText_'+_mode).innerHTML = "<br><div class='message'>آیتمی وجود ندارد.</div>";
							break;
						case 'ShowPagesAdmin':
							document.getElementById('resultText').innerHTML = "<br><div class='message'>صفحه ایی وجود ندارد.</div>";
							return;
						case 'ShowCategoryAdmin':
							document.getElementById('resultText').innerHTML = "<br><div class='message'>مقوله کتابی وجود ندارد.</div>";
							return;
						case 'ShowBooksAdmin':
							document.getElementById('resultText').innerHTML = "<br><div class='message'>کتابی وجود ندارد.</div>";
							return;
						case 'ShowNewsHomePage':
							document.getElementById('resultText').innerHTML = "<br><div class='message'>خبری وجود ندارد.</div>";
							return;
						case 'ShowNewsDetails':
							document.getElementById('NewsDetails').innerHTML = "<br><div class='message' align=center>این خبر وجود ندارد.</div>";
							CloseNewsPanel(true);
							return;
						case 'ShowAllBooks':
							if(_SiteLanguageIsPersian)
								document.getElementById('resultText_ShowAllBooks').innerHTML = "<br><div class='message'>مقوله کتابی وجود ندارد.</div>";
							else
								document.getElementById('resultText_ShowAllBooks').innerHTML = "<br><div class='message' dir=ltr>There is no book items.</div>";
							return;
						case 'ListBooksByCategory':
							if(_SiteLanguageIsPersian)
								alert('.کتابی با موضوع انتخاب شده وجود ندارد');
							else
								alert('There is not any items based on the seleted subject.');
							ShowBoolDetailsLayer(true, '-1');
							return;
						case 'ShowCustomersAdmin':
							document.getElementById('resultText').innerHTML = "<br><div class='message'>مشتری وجود ندارد.</div>";
							return;
						case 'ShowMainPageCustomers':
							if(_SiteLanguageIsPersian)
								document.getElementById('resultText_'+_mode).innerHTML = "<br><div class='message'>مشتری وجود ندارد.</div>";
							else
								document.getElementById('resultText_'+_mode).innerHTML = "<br><div class='message'>Customers is empty.</div>";
							return;
						case 'ShowMainPageSitePages':
							if(_SiteLanguageIsPersian)
								document.getElementById('resultText_'+_mode).innerHTML = "<br><div class='message'>صفحه ایی وجود ندارد.</div>";
							else
								document.getElementById('resultText_'+_mode).innerHTML = "<br><div class='message'>Pages is empty.</div>";
							return;
						case 'ShowMainPageSitePageDetailes':
							if(_SiteLanguageIsPersian)
								document.getElementById('center-s1').innerHTML = "<br><div class='message'>چنین صفحه ایی وجود ندارد.</div>";
							else
								document.getElementById('center-s1').innerHTML = "<br><div class='message'>There is not the seleted page.</div>";
							return;
						case 'ShowByBookSearch':
							if(_SiteLanguageIsPersian)
								document.getElementById('resultText').innerHTML = "<br><div class='message'>کتابی وجود ندارد.</div>";
							else
								document.getElementById('resultText').innerHTML = "<br><div class='message'>There is not any found book.</div>";
							return;
						case 'ShowTopNews':
							if(_SiteLanguageIsPersian)
								document.getElementById('resultText').innerHTML = "<br><div class='message'>خبر ویژه ایی وجود ندارد.</div>";
							else
								document.getElementById('resultText').innerHTML = "<br><div class='message'>There is not top news.</div>";
							return;
						case 'ShowShoppingCart':
							document.getElementById('PurchaseSection').style.display = 'none';
							if(_SiteLanguageIsPersian)
								document.getElementById('resultText_'+_mode).innerHTML = "<br><div class='message'>آیتمی وجود ندارد.</div>";
							else
								document.getElementById('resultText_'+_mode).innerHTML = "<br><div class='message'>Shopping cart is empty.</div>";
							return;
						case '':
							document.getElementById('resultText').innerHTML = "<br><div class='message'>کتابی وجود ندارد.</div>";
							return;
						case '':
							document.getElementById('resultText').innerHTML = "<br><div class='message'>کتابی وجود ندارد.</div>";
							return;
						case '':
							document.getElementById('resultText').innerHTML = "<br><div class='message'>کتابی وجود ندارد.</div>";
							return;
						case '':
							document.getElementById('resultText').innerHTML = "<br><div class='message'>کتابی وجود ندارد.</div>";
							return;
						case '':
							document.getElementById('resultText').innerHTML = "<br><div class='message'>کتابی وجود ندارد.</div>";
							return;
						case '':
							document.getElementById('resultText').innerHTML = "<br><div class='message'>کتابی وجود ندارد.</div>";
							return;
						case '':
							document.getElementById('resultText').innerHTML = "<br><div class='message'>کتابی وجود ندارد.</div>";
							return;
						case '':
							document.getElementById('resultText').innerHTML = "<br><div class='message'>کتابی وجود ندارد.</div>";
							return;
						case '':
							document.getElementById('resultText').innerHTML = "<br><div class='message'>کتابی وجود ندارد.</div>";
							return;
						default:
							return;
					}
					return;
				}
				switch(_mode)
				{
					case 'ShowTransactions':
						document.getElementById('resultText_'+_mode).innerHTML=xmlHttp.responseText;
						break;
					case 'ShowTotalAmount':
						document.getElementById('resultText_'+_mode).innerHTML=xmlHttp.responseText;
						break;
					case 'ShowNewsHomePage':
						document.getElementById('resultText').innerHTML=xmlHttp.responseText;
						break;
					case 'ShowUserDetails':
						document.getElementById('resultText_'+_mode).innerHTML=xmlHttp.responseText;
						break;
					case 'ShowUsers':
						document.getElementById('resultText_'+_mode).innerHTML=xmlHttp.responseText;
						break;
					case 'ShowByBookSearch':
						document.getElementById('resultText').innerHTML=xmlHttp.responseText;
						break;
					case 'ShowMainPageCustomers':
						document.getElementById('resultText_'+_mode).innerHTML=xmlHttp.responseText;
						break;
					case 'ShowVerifiedPurchases':
						document.getElementById('resultText_'+_mode).innerHTML=xmlHttp.responseText;
						break;
					case 'ShowUnverifiedPurchases':
						document.getElementById('resultText_'+_mode).innerHTML=xmlHttp.responseText;
						break;
					case 'ShowPurchaseDetails':
						document.getElementById('resultText_'+_mode).innerHTML=xmlHttp.responseText;
						break;
					case 'ShowMainPageSitePages':
						document.getElementById('resultText_'+_mode).innerHTML=xmlHttp.responseText;
						break;
					case 'ShowAllBooks':
						document.getElementById('resultText_'+_mode).innerHTML=xmlHttp.responseText;
						break;
					case 'ShowTopNews':
						document.getElementById('resultText_'+_mode).innerHTML=xmlHttp.responseText;
						break;
					case 'ShowSiteStat':
						document.getElementById('resultText_'+_mode).innerHTML=xmlHttp.responseText;
						break;
					case 'ShowShoppingCart':
						document.getElementById('resultText_'+_mode).innerHTML=xmlHttp.responseText;
						document.getElementById('PurchaseSection').style.display = 'block';
						break;
					case 'ShowNewsDetails':
						document.getElementById('NewsDetails').innerHTML=xmlHttp.responseText;
						CloseNewsPanel(false);
						break;
					case 'ShowMainPageSitePageDetailes':
						document.getElementById('center-s1').innerHTML=xmlHttp.responseText;
						break;
					default:
						document.getElementById('resultText').innerHTML=xmlHttp.responseText;
						dynaframe();
						break;
				}
			}
		}
		xmlHttp.open("POST",serverPage,true);
		xmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=UTF-8");
		switch(_mode)
		{
			case 'ShowByBookSearch':
				xmlHttp.send('mode='+_mode+'&page=' + _page+'&title='+encodeURIComponent(document.getElementById('title').value)+'&writer='+encodeURIComponent(document.getElementById('writer').value)+'&publisher='+encodeURIComponent(document.getElementById('publisher').value)+'&date='+encodeURIComponent(document.getElementById('date').value));
				break;
			default:
				xmlHttp.send('mode=' + _mode + '&page=' + _page);
				break;
		}
	}
}
//------------------------------------------------------------------------------------------------------------------
function ItemDelete(_mode,id)
{
	if (confirm('آیا شما مطمئن هستید؟'))
	{
		var serverPage='';
		switch(_mode)
		{
			case 'NewsAdmin':
				serverPage='/Admin/NewsAdmin.aspx';
				break;
			case 'PagesAdmin':
				serverPage='/Admin/PagesAdmin.aspx';
				break;
			case 'CategoryAdmin':
				serverPage='/Admin/CategoryAdmin.aspx';
				break;
			case 'BooksAdmin':
				serverPage='/Admin/BooksAdmin.aspx';
				break;
			case 'CustomersAdmin':
				serverPage='/Admin/CustomersAdmin.aspx';
				break;
			case 'ShoppingCart':
				serverPage='/cp/ShoppingCart.aspx';
				break;
			case 'PurchasesAdmin':
				serverPage='/Admin/PurchasesAdmin.aspx';
				break;
			case 'UsersAdmin':
				serverPage='/Admin/UsersAdmin.aspx';
				break;
			case '':
				serverPage='/Admin/.aspx';
				break;
			case '':
				serverPage='/Admin/.aspx';
				break;
			case '':
				serverPage='/Admin/.aspx';
				break;
			default:
				return;
		}
     	//document.getElementById('loaderImg').innerHTML = loaderUI;
		
		switch(_mode)
		{
			case 'ShoppingCart':
				document.getElementById('loaderImg_'+_mode).style.display = 'block';
				break;
			case 'PurchasesAdmin':
				document.getElementById('loaderImg_ShowVerifiedPurchases').style.display = 'block';
				break;
			case 'UsersAdmin':
				document.getElementById('loaderImg_ShowUsers').style.display = 'block';
				break;
			default:
				document.getElementById('loaderImg').style.display = 'block';
				break;
		}
		
		var xmlHttp = null;
		if((xmlHttp = createAjaxObject()) != null){
			xmlHttp.onreadystatechange=function()
			{
				if(xmlHttp.readyState==4)
				{
					if(xmlHttp.responseText == 'Logouted')
					{
						switch(_mode)
						{
							case 'ShoppingCart':
								window.location = _signInURL;
								break;
							default:
								window.location = 'Logouted.aspx';
								break;
						}
						return;
					}
					switch(_mode)
					{
						case 'ShoppingCart':
							document.getElementById('loaderImg_'+_mode).style.display = 'none';
							break;
						case 'PurchasesAdmin':
							document.getElementById('loaderImg_ShowVerifiedPurchases').style.display = 'none';
							break;
						case 'UsersAdmin':
							document.getElementById('loaderImg_ShowUsers').style.display = 'none';
							break;
						default:
							document.getElementById('loaderImg').style.display = 'none';
							break;
					}
					if(xmlHttp.responseText == 'Success')
					{
						alert('.عملیات حذف با موفقیت انجام شد');
						var _m = '';
						switch(_mode)
						{
							case 'NewsAdmin':
								_m='ShowNewsAdmin';
								break;
							case 'PagesAdmin':
								_m='ShowPagesAdmin';
								break;
							case 'CategoryAdmin':
								_m='ShowCategoryAdmin';
								break;
							case 'BooksAdmin':
								_m='ShowBooksAdmin';
								break;
							case 'CustomersAdmin':
								_m='ShowCustomersAdmin';
								break;
							case 'ShoppingCart':
								_m='ShowShoppingCart';
								break;
							case 'PurchasesAdmin':
								_m='ShowVerifiedPurchases';
								break;
							case 'UsersAdmin':
								_m='ShowUsers';
								break;
							case '':
								_m='';
								break;
							case '':
								_m='';
								break;
							case '':
								_m='';
								break;
							default:
								return;
						}
						ShowItems('1', _m);
						return;
					}
					else
					{
						//document.getElementById('resultText').innerHTML=xmlHttp.responseText;
						alert(xmlHttp.responseText);
						//alert('.خطایی در عملیات حذف انتخاب شده رخ داد');
						return;
					}
				}
			}
			xmlHttp.open("POST",serverPage,true);
			xmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=UTF-8");
			switch(_mode)
			{
				/*case 'NewsAdminForNewsGroupsMode':
					xmlHttp.send('mode=DeleteForNewsGroupsMode&DeleteID='+id);
					return;*/
				default:
					xmlHttp.send('mode=delete&DeleteID=' + id);
					return;
			}
		}
	}
}
//------------------------------------------------------------------------------------------------------------------
function ItemLoad(_mode,id)
{
	var serverPage='';
	switch(_mode)
	{
		case 'NewsAdmin':
			serverPage='/Admin/NewsAdmin.aspx';
			break;
		case 'PagesAdmin':
			serverPage='/Admin/PagesAdmin.aspx';
			break;
		case 'CategoryAdmin':
			serverPage='/Admin/CategoryAdmin.aspx';
			break;
		case 'BooksAdmin':
			serverPage='/Admin/BooksAdmin.aspx';
			break;
		case 'CustomersAdmin':
			serverPage='/Admin/CustomersAdmin.aspx';
			break;
		case 'ShowRandomBooks':
			serverPage='/';
			break;
		case '':
			serverPage='/Admin/.aspx';
			break;
		case '':
			serverPage='/Admin/.aspx';
			break;
		case '':
			serverPage='/Admin/.aspx';
			break;
		case '':
			serverPage='/Admin/.aspx';
			break;
		case '':
			serverPage='/Admin/.aspx';
			break;
		default:
			return;
	}	
	switch(_mode)
	{
		case 'ShowRandomBooks':
			document.getElementById('loaderImg_RandomImages').style.display = 'block';
			document.getElementById('resultText_RandomImages').style.display = 'none';
			break;
		default:
			document.getElementById('message').innerHTML = loaderUI;
			document.getElementById('message').style.display = 'block';
			break;
	}
	

	var xmlHttp = null;
	if((xmlHttp = createAjaxObject()) != null){
		xmlHttp.onreadystatechange=function()
		{
			if(xmlHttp.readyState==4)
			{
				if(xmlHttp.responseText == 'Logouted')
				{
					window.location = 'Logouted.aspx';
					return;
				}
				
				switch(_mode)
				{
					case 'ShowRandomBooks':
//						document.getElementById('loaderImg_RandomImages').style.display = 'block';
	//					document.getElementById('resultText_RandomImages').style.display = 'none';
						break;
					default:
						document.getElementById('message').style.display = 'block';
						break;
				}
				if(xmlHttp.responseText == 'NoFoundPost')
				{
					switch(_mode)
					{
						case 'ShowRandomBooks':
							alert('.کتابی یافت نشد');
							break;
						case 'NewsAdmin':
							document.getElementById('message').style.display = 'block';
							document.getElementById('message').innerHTML = '';
							alert('.این خبر قبلا حذف شده است');
							break;
						case 'PagesAdmin':
				    		document.getElementById('message').style.display = 'block';
							document.getElementById('message').innerHTML = '';
							alert('.این صفحه قبلا حذف شده است');
							break;
						case 'CategoryAdmin':
							document.getElementById('message').style.display = 'block';
							document.getElementById('message').innerHTML = '';
							alert('.این مقوله کتاب قبلا حذف شده است');
							break;
						case 'BooksAdmin':
			     			document.getElementById('message').style.display = 'block';
							document.getElementById('message').innerHTML = '';
							alert('.این کتاب قبلا حذف شده است');
							break;
						case 'CustomersAdmin':
							document.getElementById('message').style.display = 'block';
							document.getElementById('message').innerHTML = '';
							alert('.این مشتری قبلا حذف شده است');
							break;
						case '':
							document.getElementById('message').style.display = 'block';
							document.getElementById('message').innerHTML = '';
							alert('.این مشتری قبلا حذف شده است');
							break;
						default:
							break;
					}	
					return;
				}
				switch(_mode)
				{
					case 'ShowRandomBooks':
						break;
					default:
						_isInUpdateMode = true;
						_updateID = id;
						document.getElementById('up').style.display = "block";
						document.getElementById('cancel').style.display = "block";
						document.getElementById('new').style.display = "none";
						document.getElementById('message').innerHTML='هم اکنون می توانید به ویرایش آیتم انتخاب شده بپردازید.';
						document.getElementById('message').style.display = 'block';
						dynaframe();
						break;
				}
				
				var str = xmlHttp.responseText;
				//str += ',';
				var ret = str.split(',');
				switch(_mode)
				{
					case 'NewsAdmin':
						/*  
							NewsTitle
							NewsContent
							NewsLanguage
							IsTopNews
						*/
						document.getElementById('NewsTitle').value = Base64.decode(ret[0]);
						if(_editorIsDefined)
						{
							tinyMCE.get('NewsContent').setContent(Base64.decode(ret[1]));
						}
						document.getElementById('NewsLanguage').selectedIndex = GetListBoxIndexByValue('NewsLanguage', Base64.decode(ret[2]));
						document.getElementById('IsTopNews').selectedIndex = GetListBoxIndexByValue('IsTopNews', Base64.decode(ret[3]));
						scroll();
						break;
					case 'PagesAdmin':
						/*  
							PageTitle
							PageContent
							PageLanguage
						*/
						document.getElementById('PageTitle').value = Base64.decode(ret[0]);
						if(_editorIsDefined)
						{
							tinyMCE.get('PageContent').setContent(Base64.decode(ret[1]));
						}
						document.getElementById('PageLanguage').selectedIndex = GetListBoxIndexByValue('PageLanguage', Base64.decode(ret[2]));
						scroll();
						break;
					case 'CategoryAdmin':
						/*  
							EnglishCategory
							PersianCategory
						*/
						document.getElementById('EnglishCategory').value = Base64.decode(ret[0]);
						document.getElementById('PersianCategory').value = Base64.decode(ret[1]);
						scroll();
						break;
					case 'BooksAdmin':
						/*  
							EnglishCategory
							PersianCategory
							Title
							Writer
							Translator
							Publisher
							PublishDate
							Pages
							ISBN
							FileType
							FileSize
							Price
							Abstract
							filename
							Language
						*/
						document.getElementById('EnglishCategory').value = Base64.decode(ret[0]);
						document.getElementById('PersianCategory').value = Base64.decode(ret[1]);
						document.getElementById('Title').value = Base64.decode(ret[2]);
						document.getElementById('Writer').value = Base64.decode(ret[3]);
						document.getElementById('Translator').value = Base64.decode(ret[4]);
						document.getElementById('Publisher').value = Base64.decode(ret[5]);
						document.getElementById('PublishDate').value = Base64.decode(ret[6]);
						document.getElementById('Pages').value = Base64.decode(ret[7]);
						document.getElementById('ISBN').value = Base64.decode(ret[8]);
						document.getElementById('FileType').selectedIndex = GetListBoxIndexByValue('FileType', Base64.decode(ret[9]));
						document.getElementById('FileSize').value = Base64.decode(ret[10]);
						document.getElementById('Price').value = Base64.decode(ret[11]);
						document.getElementById('Abstract').value = Base64.decode(ret[12]);
						document.getElementById('filename').value = Base64.decode(ret[13]);
						document.getElementById('Language').selectedIndex = GetListBoxIndexByValue('Language', Base64.decode(ret[14]));
						scroll();
						break;
					case 'CustomersAdmin':
						/*  
							PersianCustomerName
							EnglishCustomerName
							CustomerLink
						*/
						document.getElementById('PersianCustomerName').value = Base64.decode(ret[0]);
						document.getElementById('EnglishCustomerName').value = Base64.decode(ret[1]);
						document.getElementById('CustomerLink').value = Base64.decode(ret[2]);
						scroll();
						break;
					case 'ShowRandomBooks':
						/*  
							images
							ids
						*/
						BookSlides(Base64.decode(ret[0]), Base64.decode(ret[1]));
//						alert(Base64.decode(ret[0]));
						break;
					case '':
						/*  
							xx
							xx
						*/
						document.getElementById('xx').value = Base64.decode(ret[0]);
						document.getElementById('xx').value = Base64.decode(ret[1]);
						scroll();
						break;
					default:
						return;
				}	
				return;
			}
		}
		xmlHttp.open("POST",serverPage,true);
		xmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=UTF-8");
		switch(_mode)
		{
			case 'ShowRandomBooks':
				xmlHttp.send('mode=ShowRandomBooks');
				return;
			default:
				xmlHttp.send('mode=load&id=' + id);
				return;
		}
	}
}
//------------------------------------------------------------------------------------------------------------------
function Cancel()
{
	document.getElementById('message').style.display = 'none';
	document.getElementById('message').innerHTML = '';
	document.getElementById('up').style.display = "none";
	document.getElementById('cancel').style.display = "none";
	document.getElementById('new').style.display = "block";
	dynaframe();
/*	if(_currentNewsGroupID == '-1')
		document.getElementById('new').style.display = "block";*/
	return ;
}
//------------------------------------------------------------------------------------------------------------------
function New(_mode)
{
	_isInUpdateMode = false;
	_updateID = -1;
	if(!document.getElementById )
		return;  
	var f = document.getElementById('form');
	for( var i = 0; i < f.elements.length; i++ )  
	{   
		f.elements[i].disabled = false;
		if(f.elements[i].type == 'select-one')
			f.elements[i].selectedIndex = 0;
		else
			f.elements[i].value = '';
	} 

	document.getElementById('up').style.display = "block";
	document.getElementById('cancel').style.display = "block";
	document.getElementById('new').style.display = "none";
	switch(_mode)
	{
		case 'NewsAdmin':
			if(_editorIsDefined)
			{
				tinyMCE.get('NewsContent').setContent(_tinyMCECompare);
//				tinyMCE.get('ContinuedNewsContent').setContent(_tinyMCECompare);
			}
//			AddContinuedSection(false);
			break;
		case 'PagesAdmin':
			if(_editorIsDefined)
			{
				tinyMCE.get('PageContent').setContent(_tinyMCECompare);
//				tinyMCE.get('ContinuedNewsContent').setContent(_tinyMCECompare);
			}
//			AddContinuedSection(false);
			break;//PagesAdmin
		default:
			if(_editorIsDefined)
				tinyMCE.get('content').setContent(_tinyMCECompare);
			break;
	}
	dynaframe();
	return ;
}
//------------------------------------------------------------------------------------------------------------------
function tinyMCEInit()
{	       
	_editorIsDefined = true;
	tinyMCE_GZ.init({
    theme : "advanced",
	skin : "o2k7",
	plugins : "safari,pagebreak,layer,advhr,advimage,advlink,emotions,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template,inlinepopups",
	languages : 'en',
	disk_cache : true,
	debug : false
	});
	tinyMCE.init({
	// O2k7 skin
    mode : "textareas",
    theme : "advanced",
	skin : "o2k7",
	plugins : "safari,pagebreak,layer,advhr,advimage,advlink,emotions,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,inlinepopups",

	// Theme options
	theme_advanced_buttons1 : "bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,formatselect,fontselect,fontsizeselect,language",
	theme_advanced_buttons2 : "bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,anchor,image,code,|,preview,|,forecolor,backcolor",
	theme_advanced_buttons3 : "hr,removeformat,visualaid,|,sub,sup,|,charmap,emotions,iespell,media,advhr,|,print,|,ltr,rtl,|,fullscreen",
	theme_advanced_buttons4 : "insertlayer,moveforward,movebackward,absolute,|,styleprops,|,cite,abbr,acronym,del,ins,attribs,|,visualchars,nonbreaking,pagebreak",
	theme_advanced_toolbar_location : "top",
	theme_advanced_toolbar_align : "left",
	theme_advanced_statusbar_location : "bottom",
	theme_advanced_resizing : false,
	
    setup : function(ed) {	
	
	
	ed.onKeyPress.add(function(ed, e){		   
				   var key = ed.getWin().event.keyCode;
				
				  if (key == 13) { ed.getWin().event.keyCode = 13; return true; }
				
				   if (langFarsi==1) { // If Farsi
					 if (key == 0x0020 && ed.getWin().event.shiftKey) // Shift-space -> ZWNJ
					   ed.getWin().event.keyCode = 0x200C;
					 else
					   ed.getWin().event.keyCode = farsikey[key - 0x0020];
					 if (farsikey[key - 0x0020] == 92) {
						ed.getWin().event.keyCode = 0x0698;
					 }
					 if (farsikey[key - 0x0020] == 8205) {
						ed.getWin().event.keyCode = 0x067E;
					 }
				   }
				   return true;

	        });
	
			ed.onKeyDown.add(function(ed, e){		   
				 var key = ed.getWin().event.keyCode;
				 if (key == 145){
					if (langFarsi==0) {
					  langFarsi = 1;
					  return true;
					}
					else {
					  langFarsi = 0;
					  return true;
					}
				
				}

	        });
			
		
		
		    ed.addButton('language', {
				title : 'انتخاب زبان',
				image : '/js/jscripts/tiny_mce/plugins/example/img/example.gif',
				onclick : function() {
				   changeLang();
				}
        	});			
    }
});

}
function changeLang() {
    if (langFarsi==0) {
    langFarsi = 1;
    return true;
  }
  else {
    langFarsi = 0;
    return true;
  }
}
//------------------------------------------------------------------------------------------------------------------
function AddContinuedSection(_isAdd)
{
	if(_isAdd)
	{
		document.getElementById('ContinuedPostSection').style.display = "block";
		document.getElementById('removetb').style.display = "block";
		document.getElementById('addtb').style.display = "none";
	}
	else
	{
		document.getElementById('ContinuedPostSection').style.display = "none";
		document.getElementById('removetb').style.display = "none";
		document.getElementById('addtb').style.display = "block";
	}
	if(_editorIsDefined)
		tinyMCE.get('ContinuedNewsContent').setContent(_tinyMCECompare);
	dynaframe();
}
//------------------------------------------------------------------------------------------------------------------
function GetListBoxIndexByValue(_linkBoxName, _value)
{
	var _index = -1;
	var obj = document.getElementById(_linkBoxName);
	for(var i = 0 ; obj.length ; i++)
	{
		if(obj[i].value == _value)
		{
			_index = i;
			break;
		}
	}
	return _index;
}
//------------------------------------------------------------------------------------------------------------------
AIM = {

    frame : function(c) {
        var n = 'f' + Math.floor(Math.random() * 99999);
        var d = document.createElement('DIV');
        d.innerHTML = '<iframe style="display:none" src="about:blank" id="'+n+'" name="'+n+'" onload="AIM.loaded(\''+n+'\')"></iframe>';
        document.body.appendChild(d);

        var i = document.getElementById(n);
        if (c && typeof(c.onComplete) == 'function') {
            i.onComplete = c.onComplete;
        }

        return n;
    },

    form : function(f, name) {
        f.setAttribute('target', name);
    },

    submit : function(f, c) {
        AIM.form(f, AIM.frame(c));
        if (c && typeof(c.onStart) == 'function') {
            return c.onStart();
        } else {
            return true;
        }
    },

    loaded : function(id) {
        var i = document.getElementById(id);
        if (i.contentDocument) {
            var d = i.contentDocument;
        } else if (i.contentWindow) {
            var d = i.contentWindow.document;
        } else {
            var d = window.frames[id].document;
        }
        if (d.location.href == "about:blank") {
            return;
        }

        if (typeof(i.onComplete) == 'function') {
            i.onComplete(d.body.innerHTML);
        }
    }
}
//------------------------------------------------------------------------------------------------------------------
//these functions are for the ajax.TemplateAdmin.aspx page
function imageFadeOut(imgName) 
{
  if ( document.all) 
  {
  	var _img = document.getElementById(imgName);
    _img.opacity = 100;
    setOpacity(_img.name, -10, 100);
  }
}

function imageFadeIn(imgName) 
{
  if ( document.all) 
  {
   	var _img = document.getElementById(imgName);
    _img.opacity = 0;
    setOpacity(_img.name, 10, 100);
  }
}

function setOpacity (imgName, step, delay) 
{
  var img = document.images[imgName];
  if(img.opacity < 40 && step <= 0) return;
  img.opacity += step;
  if (document.all) img.style.filter = 'alpha(opacity = ' + img.opacity + ')'; 
  if (step > 0 && img.opacity < 100 || step < 0 && img.opacity > 0)
    setTimeout('setOpacity("' + img.name + '",' + step + ', ' + delay + ')', delay);
} 

//------------------------------------------------------------------------------------------------------------------
function CloseNewsPanel(isClosed)
{
	if(isClosed)
		document.getElementById('NewsDetailsSection').style.display = 'none';
	else
		document.getElementById('NewsDetailsSection').style.display = 'block';
}
//------------------------------------------------------------------------------------------------------------------
function ShowBoolDetailsLayer(isShow,id)
{
	if(_isBoolDetailsLayerFirstLoad)
	{
		document.body.className='bodyInvis';
		document.getElementById('MainDiv').style.visibility='hidden';
		document.getElementById('BoolDetailsLayer').style.visibility='visible';
		centerView('BoolDetailsLayer');			
		var serverPage = '/';
		var _xmlHttp = null;
		if((_xmlHttp = createAjaxObject()) != null){
			_xmlHttp.onreadystatechange=function()
			{
				if(_xmlHttp.readyState==4)
				{
					document.getElementById('BoolDetailsLayer').innerHTML=_xmlHttp.responseText;
					_isBoolDetailsLayerFirstLoad = false;
					centerView('BoolDetailsLayer');
//					ShowBoolDetailsLayer(true,id);
				}
			}
			
			_xmlHttp.open("POST",serverPage,true);
			_xmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded; charset=UTF-8");
			_xmlHttp.send('mode=LoadBookDetails&id=' + id);
		}
	}
	else
	{
//		document.getElementById('post').disabled=false;
		if(isShow)
		{
			document.body.className='bodyInvis';
			document.getElementById('MainDiv').style.visibility='hidden';
			document.getElementById('BoolDetailsLayer').style.visibility='visible';
		}
		else
		{
			document.body.className='body';
			document.getElementById('MainDiv').style.visibility='visible';
			document.getElementById('BoolDetailsLayer').style.visibility='hidden';
		}
	}
}
function centerView(layer/*no display:none*/, doNotAddOffsets){
	if(typeof layer=="string"){layer=document.getElementById(layer);};
	if(layer){
	var parent=layer.parentNode;/*unless body tag, must have position to relative or absolute*/
	parent.style.overflow="auto";
	layer.style.position="absolute";/*much better if top and left are specified in style, with 'px'*/
	layer.style.top=layer.style.top||layer.offsetTop+'px';
	layer.style.left=layer.style.left||layer.offsetLeft+'px';
	var clientH=0, clientW=0, offsetT=0, offsetL=0, top=0, left=0;
		if(parent && parent.nodeType==1/*a tag*/){
			if(parent.nodeName=="BODY"){
				if(typeof window.innerHeight!="undefined"){clientH=window.innerHeight; clientW=window.innerWidth;}
				else if(document.documentElement && document.documentElement.clientHeight){clientH=document.documentElement.clientHeight; clientW=document.documentElement.clientWidth;}
				else if(document.body.clientHeight){clientH=document.body.clientHeight; clientW=document.body.clientWidth;}
				else{clientH=parent.clientHeight; clientW=parent.clientWidth;};
				//
				if(typeof pageYOffset!="undefined"){offsetT=pageYOffset; offsetL=pageXOffset;}
				else if(document.documentElement && document.documentElement.scrollTop){offsetT=document.documentElement.scrollTop; offsetL=document.documentElement.scrollLeft;}
				else if(document.body && typeof document.body.scrollTop!="undefined"){offsetT=document.body.scrollTop; offsetL=document.body.scrollLeft;}
				else{offsetT=0; offsetL=0;};
			top=Math.abs(parent.offsetTop + ((clientH/2) - (layer.offsetHeight/2)));
			left=Math.abs(parent.offsetLeft + ((clientW/2) - (layer.offsetWidth/2)));
			}
			else{
			clientH=parent.offsetHeight; clientW=parent.offsetWidth;
			offsetT=parent.scrollTop; offsetL=parent.scrollLeft;
			top=Math.abs(((clientH/2) - (layer.offsetHeight/2))); left=Math.abs(((clientW/2) - (layer.offsetWidth/2)));
			};
		if(!doNotAddOffsets){top+=offsetT; left+=offsetL;};
		layer.style.top=top+'px';//comment out to avoid positioning and allow returning only
		layer.style.left=left+'px';//comment out to avoid positioning and allow returning only
		return [top, left, top+'px', left+'px'];
		};
	};
}
//------------------------------------------------------------------------------------------------------------------
var slideimages=new Array()
var slidelinks=new Array()
function slideshowimages(_images){
for (i=0;i<_images.length;i++){
slideimages[i]=new Image()
slideimages[i].src=_images[i];
}
}

function slideshowlinks(_ids){
for (i=0;i<_ids.length;i++)
slidelinks[i]=_ids[i]
}

function gotoshow(){
//	alert(slidelinks[whichlink]);

	_isBoolDetailsLayerFirstLoad=true;ShowBoolDetailsLayer(true,slidelinks[whichlink]);
}

//configure the speed of the slideshow, in miliseconds
var slideshowspeed=3000;

var whichlink=0;
var whichimage=0;
function slideit(){
//	alert(document.images.slide.src);
//	return;
	if (!document.images)
	return;
	else
	
	{
		document.getElementById('loaderImg_RandomImages').style.display = 'none';
		document.getElementById('resultText_RandomImages').style.display = 'block';
	}
	document.images.slide.src=slideimages[whichimage].src;
	document.images.slide.style.width='150px';
	document.images.slide.style.height='170px';
	document.images.slide.style.borderWidth='1px';
	whichlink=whichimage;
	if (whichimage<slideimages.length-1)
	whichimage++;
	else
	whichimage=0;
	setTimeout("slideit()",slideshowspeed);
}

function BookSlides(__images,__ids)
{
	_images = eval(__images);
	_ids = eval(__ids);
	document.getElementById('loaderImg_RandomImages').style.display = 'block';
	document.getElementById('resultText_RandomImages').style.display = 'none';
	slideshowimages(_images);
	slideshowlinks(_ids);
	slideit();
}
//------------------------------------------------------------------------------------------------------------------