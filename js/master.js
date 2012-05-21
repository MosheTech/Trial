function ClearIf(control, value) {
    if (control.value == value || control.value == 'Required') {
        control.value = "";
        control.style.color = "Black";
    }
}

function SetLabel(control, value, target) {
    if (control.value == value || control.value == 'Required') {
        document.getElementById(target).innerHTML = value + ":";

        ClearIf(control, value);
    }
}

function validateRB(list)
{ return getRBvalue(list) != ""; }
function getRBvalue(list) {
    var cnt = -1;
    for (var i = list.length - 1; i > -1; i--) {
        if (list[i].checked) {
            cnt = i;
            i = -1;
        }
    }

    if (cnt > -1)
        return list[cnt].value;
    else
        return "";
}

String.prototype.trim = function() { return this.replace(/^\s+|\s+$/g, ""); }
String.prototype.ltrim = function() { return this.replace(/^\s+/, ""); }
String.prototype.rtrim = function() { return this.replace(/\s+$/, ""); }
String.prototype.startsWith = function(str) { return (this.match("^" + str) == str) }
String.prototype.endsWith = function(str) { return (this.match(str + "$") == str) }

function NumbersOnly(e, decimals) {
    var charCode = (e.which) ? e.which : e.keyCode

    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        if (decimals == null)
            return charCode == 46;
        else
            return false;
    }
    else
        return true;
    
//    var keynum;
//    var keychar;
//    var numcheck;

//    if (window.event) // IE
//    {
//        keynum = e.keyCode;
//    }
//    else if (e.which) // Netscape/Firefox/Opera
//    {
//        keynum = e.which;
//    }

//    if (keynum == 8 //backspace
//    || keynum == 9  //tab
//    || keynum == 13
//    || keynum == 16 //shift
//    || keynum == 35 //end
//    || keynum == 36 || keynum == 37 || keynum == 39
//    || keynum == 46 //decimal
//    || keynum == 188 || keynum == 190)
//        return true;

//    keychar = String.fromCharCode(keynum);

//    numcheck = /\d/;

//    return numcheck.test(keychar);
}

function moveCursorToEnd(element) {
    element.value = element.value;
}

function ToggleView(element_id)
{
    var e = document.getElementById(element_id);
    if (e != null)
    {
        if (e.style.display == '')
            e.style.display = 'none';
        else
            e.style.display = '';
    }
}

function SwapView(element_id, id2)
{
    var x = document.getElementById(element_id);
    var y = document.getElementById(id2);
    
    if (x.style.display == '')
    {
        x.style.display = 'none';
        y.style.display = '';
    }
    else
    {
        x.style.display = '';
        y.style.display = 'none';
    }
}

function ToggleButton(button, element_id)
{
    var e = document.getElementById(element_id);
    if (e.style.display == '')
    {
        button.src = '/images/expand-button.gif';
        e.style.display = 'none';
    }
    else
    {
        button.src = '/images/contract-button.gif';
        e.style.display = '';
    }
}

function ToggleButton2(button, element_id)
{
    var e = document.getElementById(element_id);
    if (e.style.display == '')
    {
        button.src = '/images/arrow-hidden.gif';
        e.style.display = 'none';
    }
    else
    {
        button.src = '/images/arrow-view.gif';
        e.style.display = '';
    }
}

function ImageHover(e, hover)
{
    var src = e.src;
    if (hover)
        src = src.replace(".png", "-hover.png");
    else
        src = src.replace("-hover.png", ".png");
        
    e.src = src;
}

function AddItem(control_id, values_id, item_value, item_text)
{
    var control_values = document.getElementById(values_id).value;
    var item_list = document.getElementById(control_id).innerHTML;
    item_list = item_list.replace(/&amp;/g,'&');
    
    if (item_list.match(item_text))
    {
        item_list = item_list.replace('<A href="javascript:RemoveItem(\'' + control_id + '\',\'' + values_id + '\',' + item_value + ',\'' + item_text + '\')">' + item_text + '</A>, ','');
        control_values = control_values.replace(','+item_value+',',',');
    }
    else
    {
        item_list += '<a href="javascript:RemoveItem(\'' + control_id + '\',\'' + values_id + '\',' + item_value + ',\'' + item_text + '\')">' + item_text + '</a>, ';
        control_values += item_value+',';
    }

    document.getElementById(control_id).innerHTML = item_list;
    document.getElementById(values_id).value = control_values;
}

function RemoveItem(control_id, values_id, item_value, item_text)
{
    var control_values = document.getElementById(values_id).value;
    var item_list = document.getElementById(control_id).innerHTML;
    item_list = item_list.replace(/&amp;/g,'&');
    item_list = item_list.replace('<A href="javascript:RemoveItem(\'' + control_id + '\',\'' + values_id + '\',' + item_value + ',\'' + item_text + '\')">' + item_text + '</A>, ','');
    document.getElementById(control_id).innerHTML = item_list;
    control_values = control_values.replace(','+item_value+',',',');
    document.getElementById(values_id).value = control_values;
}

function findPos(obj) {
	var curleft = curtop = 0;

	if (obj.offsetParent) {
	    do {
	        curleft += obj.offsetLeft;
	        curtop += obj.offsetTop;
	    } while (obj = obj.offsetParent);
	}

    return [curleft,curtop];
}

function GetLeftPos(obj) {
	var curleft = 0;

    if (obj.offsetParent) {
        do {
			    curleft += obj.offsetLeft;
        } while (obj = obj.offsetParent);
    }

    return curleft;
}

function validateEmailAddress(e)
{
    if (e.value == '') return true;
    
    var email = e.value;
    
	var http="@";
	var dot=".";
	var lat=email.indexOf(http);
	var lstr=email.length;
	var ldot=email.indexOf(dot);
	
	if (email.indexOf(http)==-1) return false;
	
	if (email.indexOf(http)==0 || email.indexOf(http)==lstr) return false;
	
	if (email.indexOf(dot)==-1 || email.indexOf(dot)==0 || email.indexOf(dot)==lstr) return false;
	
	if (email.indexOf(http,(lat+1))!=-1) return false;

	if (email.substring(lat-1,lat)==dot || email.substring(lat+1,lat+2)==dot) return false;

	if (email.indexOf(dot,(lat+2))==-1) return false;
	
	if (email.indexOf(" ")!=-1) return false;

    return true;
}

function validateURL(e)
{
    if (e.value == '') return true;
    
    var url = e.value;
    
	var http="http";
	var dot=".";
	var httpIndex=url.indexOf(http);
	var dotIndex=url.indexOf(dot);
	
	if (httpIndex < 1 || httpIndex == url.length) return false;
	
	if (dotIndex < 8 || dotIndex == url.length) return false;
	
	return true;
}

function validateUserInput(value) {
    if (value == '') return false;

    if (value.replace("</li>", "").replace("</ul>", "").replace("</b>", "").replace("</h", "").indexOf("</") >= 0) {
        alert('HTML tags are no longer permitted.');
        return false;
    } else {
        return true;
    }
}

function AddDays(date, days)
{
    var day = 86400000;
    var diff = days * day;
    var newDate = new Date(date.valueOf() + diff);
    return (newDate.getMonth() + 1) + '/' + newDate.getDate() + '/' + newDate.getFullYear();
}

//tab effects

var TabbedContent = {
    init: function () {
        $(".tab_item").click(function () {

            var background = $(this).parent().find(".moving_bg");

            $(background).stop().animate({
                left: $(this).position()['left']
            }, {
                duration: 300
            });

            TabbedContent.slideContent($(this));
        });
    },

    slideContent: function (obj) {
        var margin = $("#menu").find(".slide_content").width();
        //var margin = $(obj).parent().parent().find(".slide_content").width();
        margin = margin * ($(obj).prevAll().size() - 1);
        margin = margin * -1;

        //$(obj).parent().parent().find(".tabslider").stop().animate({
        $("#menu").find(".tabslider").stop().animate({
            marginLeft: margin + "px"
        }, {
            duration: 300
        });
    }
}

$(document).ready(function () {
    TabbedContent.init();
});

$(function () {
    $(".draggable").draggable();
});

function redirectMe(destination) {
    if (window.opener && window.opener.open && !window.opener.closed)
        window.opener.location.href = destination;
    else if (window.top == window.self)
        location.href = destination;
    else
        window.parent.location.href = destination;
}