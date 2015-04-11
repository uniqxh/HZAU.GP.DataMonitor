/*窗口最大化*/
function maximizeme() {
    window.moveTo(0, 0);
    window.resizeTo(screen.width, screen.height);
}

/*获取URL参数值*/
function queryUrlString(pItem) {
    var _sValue = location.search.match(new RegExp("[?&]" + pItem + "=([^&]*)(&?)", "i"));
    var _res = _sValue ? _sValue[1] : _sValue;
    return decodeURI(_res);
}

 /*Cookies操作*/
function setCookie(pName, pValue) { //增、改
    var _exp = new Date();
    //_exp.setTime(_exp.getTime() + Days * 24 * 60 * 60 * 1000);
    //document.cookie = pName + "=" + escape(pValue) + ";expires=" + _exp.toUTCString() + ";path=/;domain=" + g_roothost; +";secure"
    _exp.setTime(_exp.getTime() + 24 * 60 * 60 * 1000);
    document.cookie = pName + "=" + escape(pValue) + ";expires=" + _exp.toUTCString() + ";path=/";
}

function getCookie(pName) { //读
    var _arr, _reg = new RegExp("(^| )" + pName + "=([^;]*)(;|$)");
    if (document.cookie.match(_reg)) {
        _arr = document.cookie.match(_reg);
        return unescape(_arr[2]);
    }
    else return null;
}

function delCookie(pName) { //删
    var _exp = new Date();
    _exp.setTime(_exp.getTime() - 1);
    var _cval = GetCookie(pName);
    if (_cval != null) document.cookie = pName + "=" + _cval + ";expires=" + _exp.toUTCString() + ";path=/";
}

function delCookies() { //删本站点所有
    var _cookarr = document.cookie.toString().split(";");
    var _exp = new Date()
    _exp.setTime(_exp.getTime() - 1);
    for (var i = 0; i < _cookarr.length; i++) {
        var _cookarrsub = _cookarr[i].split("=");
        document.cookie = _cookarrsub[0] + "=" + _cookarrsub[1] + ";expires=" + _exp.toUTCString() + ";path=/";
    }
}

var global_Options = {
    showAjaxError: function (errorCode, errorMsg) {
        $("#ajax_error_code_span").text(errorCode);
        $("#ajax_error_message_span").text(errorMsg);
        $("#gloab_ajax_error_div").show();
    },
    closeAjaxError: function () {
        $("#ajax_error_code_span").text("");
        $("#ajax_error_message_span").text("");
        $("#gloab_ajax_error_div").hide();
    },
    datePickerOpt:  {
        dateFormat: 'yy-mm-dd'
    },

    jQGridOpts: function () {
        //var offset = $.browser.msie ? 30 : 40;
        var gridWidth = 1000; //$(document.body).width() - offset;
        var opts = {
            url: "",
            datatype: 'json',
            mtype: 'GET',
            colNames: [],
            colModel: [],
            width: gridWidth,
            height: 250,
            rowNum: 50,
            autoresize: true,
            autowidth:true,
            rowList: [10, 20, 50, 100, 200, 300, 400],
            pager: '#pager',
            shrinkToFit: false,
            viewrecords: true,
            altRows: true,
            altclass: 'ui-row-alt',
            toppager:true,
            jsonReader: {
                repeatitems: false,
                id: "0",
                root: "rows",
                page: "page",
                total: "total",
                records: "records"
            },
            loadError: function (jAjax) {
                try {
                    var errObj = $.parseJSON(jAjax.responseText);
                    var errCode = errObj.ErrorCode;
                    var errMsg = errObj.ErrorMessage;
                    global_Options.showAjaxError(errCode, errMsg);
                }
                catch (e) {

                }
            },
            loadBeforeSend: global_Options.closeAjaxError
        };
        return opts;
    },
    formatOptions: {
        dateFormat: { newformat: 'Y-m-d' },
        dateTimeFormat: { newformat: 'Y-m-d H:i:s' },
        dateMonthFormat: {newformat: 'Y-m' }
    },

    multiselectOpts: function () {
        return {
            //            minWidth: function (element) { return element.parent().parent().width() * 0.49; },
            /*minWidth: '350px',*/
            minWidth: '255px',
            checkAllText: '全选',
            uncheckAllText: '取消选择',
            noneSelectedText: '未选择',
            displayItemCount: 2, //最多显示项个数
            targetValueId: "",
            selectedText: function (numchecked, inputLength, seletecElements) {
                var thatOpts = this.options, //this被绑定到jQuery对象，即当前Options的父对象
                 selectedValues = [], diaplayValues = [], diaplayText, values;

                //遍历所有被选中的项
                $.each(seletecElements, function (index, element) {
                    selectedValues.push($(element).val());
                    diaplayValues.push($(element).attr("title"))
                });
                values = selectedValues.join(",");
                //设置显示的项
                var length = seletecElements.length;
                var displayCount = thatOpts.displayItemCount;
                if (seletecElements.length > displayCount) {
                    diaplayValues = [];
                    var index = 0;
                    for (index = 0; index < displayCount; index++) {
                        diaplayValues.push(seletecElements[index].title);
                    }
                    diaplayText = diaplayValues.join(",") + "..."
                }
                else {
                    diaplayText = diaplayValues.join(",");
                }

                //将选择的值赋值给目标对象
                if (thatOpts.targetValueId !== '') {
                    $("#" + thatOpts.targetValueId).val(values);
                }
                //返回值是用于显示的
                return diaplayText;
            }
        };
    },

    multiselectFilterOpts: {
        label: "",
        placeholder: "输入关键字"
    },

    multiselectFilterWithNameOpts: {
        label: "<span style=padding-left:150px;height:25px;line-height:25px'><a id='getMoreDataId' onClick='getMoreData()' style='color:red'>查看全部渠道</a></span></br>",
        placeholder: "输入关键字"
    },

    startOnlineCheck: false
}

$.ajaxSetup({
    error: function (request, textstatus, repsonse) {
        var errObj = $.parseJSON(repsonse);
        var errCode = errObj.ErrorCode;
        var errMsg = errObj.ErrorMessage;
        global_Options.showAjaxError(errCode, errMsg);
    }
});

(function ($) { 
    var handleError=function(error){
        try{
               var errObj =$.parseJSON(error.responseText);
               var errCode = errObj.ErrorCode;
                var errMsg = errObj.ErrorMessage;
                global_Options.showAjaxError(errCode, errMsg);
            }
            catch(ex)
            {
                throw ex;
            }
    };
    $.postJson=function(opts){
       opts.dataType="json";
       opts.contentType="application/json,charset=utf-8";
       opts.type="POST";
       opts.beforeSend=function(){
        showNorepeat();
       };
       opts.error= handleError;
       opts.complete =function(response){
           hideNorepeat();
       }
       $.ajax(opts);
    };

    $.postJsonForIE=function(opts){
       opts.dataType="json";
       opts.contentType="application/json,charset=utf-8";
       opts.type="POST";
       opts.beforeSend=function(){
           //showNorepeat();
       };
       opts.error= handleError;
       opts.complete =function(response){
           //hideNorepeat();
       }
       $.ajax(opts);
    };

    $.postJsonNoMask=function(opts){
       opts.dataType="json";
       opts.contentType="application/json,charset=utf-8";
       opts.type="POST";
       opts.error= handleError;
       $.ajax(opts);
    };

    $.getJson=function(opts){
       opts.dataType="json";
       opts.contentType="application/json,charset=utf-8";
       opts.type="GET";
       opts.error=handleError;
       if(opts.url){
         var stamp=(new Date()).valueOf().toString();
         opts.url=opts.url+"&_st_="+stamp;
       }
       $.ajax(opts);
    }
})(jQuery);

function openFile(s, callback) {
    if (s != "") {
        location.href = s;
    }
    else
    {
        alert("数据正常");
    }
    if ($.isFunction(callback)) {
        callback(s.FileName, s.Path);
    }

}
function exportExcel(url, type, before, complete,error) {
    if ($.isFunction(before)){
        before();
    }
    $.ajax({
        type: type,
        url: url,
        success: function (s) {
            openFile(s);
            if ($.isFunction(complete)) {
                complete();
            }
        },
        error: function (s) {
            if ($.isFunction(complete)) {
                complete();
            }
            if ($.isFunction(error)) {
                error(s);
            }
        }
    });
}

function exportExcelWithPostData(url, postData, before, complete, error) {
    if ($.isFunction(before)) {
        before();
    }
    $.ajax({
        type: "POST",
        url: url,
        contentType: "application/json,charset=utf-8",
        data: postData,
        success: function (s) {
            openFile(s);
            if ($.isFunction(complete)) {
                complete();
            }
        },
        error: function (s) {
            if ($.isFunction(complete)) {
                complete();
            }
            if ($.isFunction(error)) {
                error(s);
            }
        }
    });
}

function openSearchListFullWin(url) {
    if ($.browser.msie) {//Fuck the IE;
        var featuresInIE = ['resize:1; unadorned:yes; dialogHeight:' + window.screen.height + "px;", 'dialogWidth:' + window.screen.width + "px"].join(' ');
        window.showModalDialog(url, window, featuresInIE);
    } else {
        var features = ['height=' + window.screen.height, 'width=' + window.screen.width].join(',');
        window.open(url, "", features, false);
    }
}


/* -------------------------------------------- */
/* 金额格式化方法                               */
/* -------------------------------------------- */
function formatAmount(eValue) {
    eValue = new String(eValue);
    eValue = new Number(eValue.replace(/,/g, '')).toFixed(2);
    var intPart = "";
    var decPart = "";
    if (eValue.indexOf(",") >= 0) {
        eValue = eValue.replace(/,/g, "");
    }
    if (eValue.indexOf(".") >= 0) {
        intPart = eValue.split(".")[0];
        decPart = eValue.split(".")[1];
    }
    else {
        intPart = eValue;
    }
    var num = intPart + "";
    var re = /(-?\d+)(\d{3})/
    while (re.test(num)) {
        num = num.replace(re, "$1,$2")
    }
    if (eValue.indexOf(".") >= 0) {
        eValue = num + "." + decPart;
    }
    else {
        eValue = num;
        if (eValue.length > 0 && eValue != 'NaN') eValue += ".00";
    }

    return eValue;
}


/* -------------------------------------------- */
/* 2010-07-08									*/
/* 得到日期格式                                 */
/* -------------------------------------------- */
function getDateFromString(str) {
    if (str == "") {
        var _now = new Date();
        return "/Date(" + Date.parse(_now.format("yyyy/MM/dd")) + "+0800)/";
    }
    try {
        return "/Date(" + Date.parse(parseDate(str).format("yyyy/MM/dd")) + "+0800)/";
    } catch (e) {
        var _now = new Date();
        return "/Date(" + Date.parse(_now.format("yyyy/MM/dd")) + "+0800)/";
    }
}

function getStringFromDate(str) {
    try {
        var myDate = eval("new " + str.replace(/\//g, ""));
        return myDate.format("yyyy-MM-dd");
    }
    catch (e) {
        return '';
    }
}
function getStringFromTime(str) {
    try {
        var myDate = eval("new " + str.replace(/\//g, ""));
        return myDate.format("yyyy-MM-dd HH:mm:ss");
    }
    catch (e) {
        return '';
    }
}
function getMonthFromTime(str) {
    try {
        return parseDate(str).format("yyyy-MM")
    }
    catch (e) {
        return '';
    }
}

Date.prototype.format = function (format) {
    var o = {
        "M+": this.getMonth() + 1, //month     
        "d+": this.getDate(),    //day     
        "H+": this.getHours(),   //hour     
        "m+": this.getMinutes(), //minute     
        "s+": this.getSeconds(), //second     
        "q+": Math.floor((this.getMonth() + 3) / 3), //quarter     
        "S": this.getMilliseconds() //millisecond     
    }
    if (/(y+)/.test(format))
        format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(format))
            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
    return format;
}

Date.prototype.addDay = function (day) {
    if (isNaN(day)) {
        return (new Date());
    }
    var _day = day * 24 * 60 * 60 * 1000;
    if (isNaN(_day)) {
        return (new Date());
    }
    var dateTime = this.getTime();
    var d = new Date();
    d.setTime(dateTime + _day);
    return d;
}

Date.prototype.addMonth = function (month) {
    var dtTemp = this;
    if (isNaN(month)) {
        return new Date();
    }
    return new Date(dtTemp.getFullYear(), (dtTemp.getMonth()) + month, dtTemp.getDate());
}

Date.prototype.getCurMonthFirstDay = function() {
    var firstDate = new Date();
    firstDate.setDate(1);
    return firstDate;
}

Date.prototype.getCurMonthLastDay = function () {
    var endDate = this.getCurMonthFirstDay();
    endDate.setMonth(endDate.getMonth() + 1);
    endDate.setDate(0);
    return endDate;
}

function parseDate(str) {
    if (typeof str == 'string') {
        var results = str.match(/^ *(\d{4})-(\d{1,2}) *$/);
        if (results && results.length > 2) {
            return new Date(results[1], results[2] - 1);
        }
        results = str.match(/^ *(\d{4})-(\d{1,2})-(\d{1,2}) *$/);
        if (results && results.length > 3) {
            return new Date(results[1], results[2] - 1, results[3]);
        }
    }
    else if (typeof str == 'object') return str;
    return null;
}

//得到当前日期
function getNowDate() {
    var now = new Date();
    var year = now.getFullYear();       //年
    var month = now.getMonth() + 1;     //月
    var day = now.getDate();            //日
    return year + "-" + month + '-' + day;
}

//两个日期的差值(d1 - d2).
function dateDiff(d1, d2) {
    var day = 24 * 60 * 60 * 1000;
    try {
        var dateArr = d1.split("-");
        var checkDate = new Date();
        checkDate.setFullYear(dateArr[0], dateArr[1] - 1, dateArr[2]);
        var checkTime = checkDate.getTime();

        var dateArr2 = d2.split("-");
        var checkDate2 = new Date();
        checkDate2.setFullYear(dateArr2[0], dateArr2[1] - 1, dateArr2[2]);
        var checkTime2 = checkDate2.getTime();

        var cha = (checkTime - checkTime2) / day;
        return cha;
    } catch (e) {
        return false;
    }
}

/* -------------------------------------------- */
/* 2010-08-30									*/
/*  小写金额转换成大写                          */
/* -------------------------------------------- */
function lowerToUpperForAmount(pNumberValue) {
    var _numberValue = new String(Math.round(pNumberValue * 100)); // 数字金额  
    var _chineseValue = ""; // 转换后的汉字金额  
    var _string1 = "零壹贰叁肆伍陆柒捌玖"; // 汉字数字  
    var _string2 = "万仟佰拾亿仟佰拾万仟佰拾元角分"; // 对应单位  
    var _len = _numberValue.length; // _numberValue 的字符串长度  
    var _ch1; // 数字的汉语读法  
    var _ch2; // 数字位的汉字读法  
    var _nZero = 0; // 用来计算连续的零值的个数  
    var _string3; // 指定位置的数值
    if (_len > 15) {
        alert("超出计算范围");
        return "";
    }
    if (_numberValue == 0) {
        _chineseValue = "零元整";
        return _chineseValue;
    }
    _string2 = _string2.substr(_string2.length - _len, _len); // 取出对应位数的_string2的值
    for (var i = 0; i < _len; i++) {
        _string3 = parseInt(_numberValue.substr(i, 1), 10); // 取出需转换的某一位的值
        if (i != (_len - 3) && i != (_len - 7) && i != (_len - 11) && i != (_len - 15)) {
            if (_string3 == 0) {
                _ch1 = "";
                _ch2 = "";
                _nZero = _nZero + 1;
            }
            else if (_string3 != 0 && _nZero != 0) {
                _ch1 = "零" + _string1.substr(_string3, 1);
                _ch2 = _string2.substr(i, 1);
                _nZero = 0;
            }
            else {
                _ch1 = _string1.substr(_string3, 1);
                _ch2 = _string2.substr(i, 1);
                _nZero = 0;
            }
        }
        else { // 该位是万亿，亿，万，元位等关键位  
            if (_string3 != 0 && _nZero != 0) {
                _ch1 = "零" + _string1.substr(_string3, 1);
                _ch2 = _string2.substr(i, 1);
                _nZero = 0;
            }
            else if (_string3 != 0 && _nZero == 0) {
                _ch1 = _string1.substr(_string3, 1);
                _ch2 = _string2.substr(i, 1);
                _nZero = 0;
            }
            else if (_string3 == 0 && _nZero >= 3) {
                _ch1 = "";
                _ch2 = "";
                _nZero = _nZero + 1;
            }
            else {
                _ch1 = "";
                _ch2 = _string2.substr(i, 1);
                _nZero = _nZero + 1;
            }
            if (i == (_len - 11) || i == (_len - 3)) { // 如果该位是亿位或元位，则必须写上  
                _ch2 = _string2.substr(i, 1);
            }
        }
        _chineseValue = _chineseValue + _ch1 + _ch2;
    }
    if (_string3 == 0) { // 最后一位（分）为0时，加上“整”  
        _chineseValue = _chineseValue + "整";
    }
    return _chineseValue;
}

/*******************************************print begin*******************************************/

/*
你们可以使用jQuery或者其他方式进行打包公用，用它来代替window.print就好了。
剩下的将和原来的方式一样，我按照之前的格式写。
页面逻辑处理也是。(这个方法只负责调用插件将数据传送打印机槽中。)
*/
function doPrint() {
    if ($.browser.msie && factory && typeof (factory.printing) != 'undefined') {
        //如果是ie，使用activex控件
        //用户安装了插件
        // set empty page header
        factory.printing.header = "";
        // set empty page footer
        factory.printing.footer = "";
        //是否横向打印
        factory.printing.portrait = true;
        //边框全部由用户和css去控制。
        //如有需要请参考官网:http://scriptx.meadroid.com (全功能收费的。)
        //factory.printing.leftMargin = 1.0;
        //factory.printing.topMargin = 1.0;
        //factory.printing.rightMargin = 1.0;
        //factory.printing.bottomMargin = 1.0;
        //参数false为直接打印
        factory.printing.Print(false);
    } else if ($.browser.mozilla && typeof (jsPrintSetup) == 'object') {
        //如果是firefox并且安装相关的插件。
        //这里也不控制边框等属性了，如有需要，请参考官网:http://jsprintsetup.mozdev.org/reference.html (开源的。)
        //先把默认的设置保存下来。
        var headerLeft = jsPrintSetup.getOption('headerStrLeft');
        var headerCenter = jsPrintSetup.getOption('headerStrCenter');
        var headerRight = jsPrintSetup.getOption('headerStrRight');

        var footerLeft = jsPrintSetup.getOption('footerStrLeft');
        var footerCenter = jsPrintSetup.getOption('footerStrCenter');
        var footerRight = jsPrintSetup.getOption('footerStrRight');

        var silentPrint = false; //jsPrintSetup.getSilentPrint();
        var showPrintProgress = true; //jsPrintSetup.getShowPrintProgress();
        // set empty page header
        jsPrintSetup.setOption('headerStrLeft', '');
        jsPrintSetup.setOption('headerStrCenter', '');
        jsPrintSetup.setOption('headerStrRight', '');
        // set empty page footer
        jsPrintSetup.setOption('footerStrLeft', '');
        jsPrintSetup.setOption('footerStrCenter', '');
        jsPrintSetup.setOption('footerStrRight', '');
        //清空确认框设置
        //jsPrintSetup.clearSilentPrint();
        //设置为不显示
        //jsPrintSetup.setOption('printSilent', 1);
        //这句等效
        jsPrintSetup.setSilentPrint(true);
        //不显示进度框
        jsPrintSetup.setShowPrintProgress(false);

        //打印当前窗口
        jsPrintSetup.printWindow(window);

        //打印完成后把参数设置回默认的。
        jsPrintSetup.setOption('headerStrLeft', headerLeft);
        jsPrintSetup.setOption('headerStrCenter', headerCenter);
        jsPrintSetup.setOption('headerStrRight', headerRight);

        jsPrintSetup.setOption('footerStrLeft', footerLeft);
        jsPrintSetup.setOption('footerStrCenter', footerCenter);
        jsPrintSetup.setOption('footerStrRight', footerRight);

        jsPrintSetup.setSilentPrint(silentPrint);
        jsPrintSetup.setShowPrintProgress(showPrintProgress);
    } else {
        //否则就没什么办法了，准备按确认吧
        window.print();
    }
}


/* -------------------------------------------- */
/* 2010-03-31									*/
/* spectorye									*/
/* 实现弹出模态窗口								*/
/* 需要jquery支持								*/
/* -------------------------------------------- */
function GetCover() {
    var divCover = $("#norepeat_cover");
    if (divCover == null || divCover.length <= 0) {
        divCover = $('<div id="norepeat_cover" style="z-index:300;"><iframe frameborder="0">&nbsp;</iframe></div>');
        divCover.appendTo("body");
    }
    return divCover;
}
function GetModel() {
    var divModel = $("#norepeat_model");
    if (divModel == null || divModel.length <= 0) {
        divModel = $('<div id="norepeat_model" style="z-index:30001;"><div class="modelheader">'
			+ '<div class="title"></div>'
			+ '<div class="link">'
			+ '<a href="javascript:showModel();">关闭</a>'
			+ '</div>'
			+ '</div><iframe frameborder="0" scrolling="no">&nbsp;</iframe></div>');
        divModel.appendTo("body");
    }
    return divModel;
}
function GetModelWidthRefresh() {
    var divModel = $("#norepeat_model");
    if (divModel == null || divModel.length <= 0) {
        divModel = $('<div id="norepeat_model" style="z-index:30001;"><div class="modelheader">'
			+ '<div class="title"></div>'
			+ '<div class="link">'
			+ '<a href="javascript:showModelWithRefresh();">关闭</a>'
			+ '</div>'
			+ '</div><iframe frameborder="0" scrolling="no">&nbsp;</iframe></div>');
        divModel.appendTo("body");
    }
    return divModel;
}
function window_resize() {
    var divCover = GetCover();
    if (divCover == null || divCover.length <= 0) {
        return;
    }
    var h = Math.max($(window).height(), $(document).height());
    var w = Math.max($(window).width(), $(document).width());
    divCover.width(w - 2);
    divCover.height(h - 2);
    //
    window_scroll();
}
function window_scroll() {
    var divCover = GetCover();
    var divModel = GetModel("");
    if (divCover != null && divModel != null) {
        var scrollTop = $(window).scrollTop();
        var scrollLeft = $(window).scrollLeft();
        //设置浮动层
        divModel.css("top", ($(window).height() - divModel.height()) / 2 + scrollTop);
        divModel.css("left", ($(window).width() - divModel.width()) / 2 + scrollLeft);
    }
}
function showModel(url, title, w, h) {
    if (url && url.length > 0) {

        var nr_cover = GetCover();
        var nr_model = GetModel();
        
        if (h && h > 0) {
            nr_model.height(h);
        }
        if (w && w > 0) {
            nr_model.width(w);
        }
        if (title && title.length > 0) {
            nr_model.find(".title").text(title);
        }
        var iframe = nr_model.children("iframe");
        if (iframe.length > 0) {
            iframe.attr("src", url);
            iframe.height(h - 28);
            iframe.width(w - 8);
        }
        window_resize();
        nr_cover.show();
        nr_model.show();
        
        $(window).resize(window_resize);
        $(window).scroll(window_scroll);
       
    }
    else {
        var nr_cover = GetCover();
        var nr_model = GetModel();
        nr_cover.hide();
        nr_model.hide();
        $(window).die("resize", window_resize);
        $(window).die("scroll", window_scroll);
    }
}

function showModelWithRefresh(url, title, w, h) {
    if (url && url.length > 0) {

        var nr_cover = GetCover();
        var nr_model = GetModelWidthRefresh();

        if (h && h > 0) {
            nr_model.height(h);
        }
        if (w && w > 0) {
            nr_model.width(w);
        }
        if (title && title.length > 0) {
            nr_model.find(".title").text(title);
        }
        var iframe = nr_model.children("iframe");
        if (iframe.length > 0) {
            iframe.attr("src", url);
            iframe.height(h - 28);
            iframe.width(w - 8);
        }
        window_resize();
        nr_cover.show();
        nr_model.show();

        $(window).resize(window_resize);
        $(window).scroll(window_scroll);

    }
    else {
        var nr_cover = GetCover();
        var nr_model = GetModelWidthRefresh();
        nr_cover.hide();
        nr_model.hide();
        $(window).die("resize", window_resize);
        $(window).die("scroll", window_scroll);

        parent.window.location.href = parent.window.location.href;
    }
}
/*******************************************print end*******************************************/

/* ---------------------- */
/* 防止重复提交的遮盖层   */
/* ---------------------- */
function showNorepeat(tipsText) {
    var nr_cover = GetNrCover();
    var nr_tips = GetNrTips();
    window_resizeNr();
    $(nr_cover).show();
    $(nr_tips).show();
    //
    $(window).resize(window_resizeNr);
    $(window).scroll(window_scrollNr);
    //
    if (tipsText == null || tipsText.length < 1) {
        tipsText = "请稍候";
    }
    else {
        tipsText += "<br>请稍候";
    }
    $(nr_tips).children("#text").html(tipsText);
}
function hideNorepeat() {
    var nr_cover = GetNrCover();
    var nr_tips = GetNrTips("");
    $(nr_cover).hide();
    $(nr_tips).hide();
    //
    $(window).die("resize", window_resizeNr);
    $(window).die("scroll", window_scrollNr);
}
function GetNrCover() {
    var divCover = null;
    try {
        divCover = parent.document.getElementById("top_norepeat_cover");
    }
    catch (e) {
        divCover = window.document.getElementById("top_norepeat_cover");
    }

    if (divCover == null) {
        divCover = $('<div id="top_norepeat_cover" style="border:solid 1px #888888;background-color:#ffffff;display:none;width:0;height:0;text-align:center;vertical-align:middle;top:0;left:0;position:absolute;z-index:999999;overflow:hidden;"><iframe frameborder="0" style="width:100%;height:100%;background-color:#666666;">&nbsp;</iframe></div>').get()[0];
        $(divCover).css("opacity", "0.8");
        $(divCover).css("filter", "alpha(opacity=80)");
        $(divCover).css("zoom", "1");
        $(divCover).appendTo("body");
    }
    return divCover;
}
function GetNrTips(tipsText) {
    var divTip = null;
    try {
        divTip = parent.document.getElementById("top_norepeat_tips");
    }
    catch (e) {
        divTip = window.document.getElementById("top_norepeat_tips");
    }
    if (divTip == null) {
        divTip = $('<div id="top_norepeat_tips" style="display:none;border:solid 1px #266392;background-color:#eeeeee;padding:10px 5px;width:202px;position:absolute;z-index:1000000;"><div style="float:left;width:32px;"><img src="../Content/themes/DaVinci/img/loading.gif"/></div><div id="text" style=" padding-left: 20px; line-height: 17px; font-family: Verdana; font-size: 13px; overflow: hidden;">请稍候 ……</div></div>').get()[0];
        $(divTip).appendTo("body");
    }
    return divTip;
}
function window_resizeNr() {
    var win = parent;
    try { var tmp = win.document.body; } catch (e) { win = window; }
    var divCover = GetNrCover();
    if (divCover == null || divCover.className == null) {
        return;
    }
    //遮罩层size设为窗口size
    if (typeof (win.innerWidth) == "number") {
        width = win.innerWidth;
        height = win.innerHeight;
    }
    else if (win.document.documentElement && (win.document.documentElement.clientWidth || win.document.documentElement.clientHeight)) {
        width = win.document.documentElement.clientWidth;
        height = win.document.documentElement.clientHeight;
    }
    else if (win.document.body && (win.document.body.clientWidth || win.document.body.clientHeight)) {
        width = win.document.body.clientWidth;
        height = win.document.body.clientHeight;
    }
    divCover.style.width = (width - 2) + 'px';
    divCover.style.height = (height - 2) + 'px';
    //
    window_scrollNr();
}
function window_scrollNr() {
    //浏览器窗口滚动的时候设置遮罩层和浮动层的位置
    var win = parent;
    try { var tmp = win.document.body; } catch (e) { win = window; }
    var divCover = GetNrCover();
    var divTips = GetNrTips("");
    if (divCover != null && divTips != null) {
        //滚动上边距
        var scrollTop;
        if (typeof win.pageYOffset != 'undefined') {
            scrollTop = win.pageYOffset;
        }
        else if (typeof win.document.compatMode != 'undefined' && win.document.compatMode != 'BackCompat') {
            scrollTop = win.document.documentElement.scrollTop;
        }
        else if (typeof win.document.body != 'undefined') {
            scrollTop = win.document.body.scrollTop;
        }
        //滚动左边距
        var scrollLeft;
        if (typeof win.pageXOffset != 'undefined') {
            scrollLeft = win.pageXOffset;
        }
        else if (typeof win.document.compatMode != 'undefined' && win.document.compatMode != 'BackCompat') {
            scrollLeft = win.document.documentElement.scrollLeft;
        }
        else if (typeof win.document.body != 'undefined') {
            scrollLeft = win.document.body.scrollLeft;
        }
        //设置遮罩层
        divCover.style.top = scrollTop + 'px';
        divCover.style.left = scrollLeft + 'px';
        //设置浮动层
        divTips.style.top = ((Number(divCover.style.height.replace("px", "")) - Number(divTips.style.height.replace("px", ""))) / 2 + scrollTop) + 'px';
        divTips.style.left = ((Number(divCover.style.width.replace("px", "")) - Number(divTips.style.width.replace("px", ""))) / 2 + scrollLeft) + 'px';
    }
}
function showAlert(msg) {
    window.alert(msg);
}

/*Trim*/
//此处为string类添加三个成员 
String.prototype.Trim = function () { return Trim(this); }
String.prototype.LTrim = function () { return LTrim(this); }
String.prototype.RTrim = function () { return RTrim(this); }
String.prototype.ExchangeEmpty = function () { return ExchangeEmpty(this); }


//此处为独立函数 
function LTrim(pStr) {
    var i;
    for (i = 0; i < pStr.length; i++) {
        if (pStr.charAt(i) != " " && pStr.charAt(i) != " ") break;
    }
    pStr = pStr.substring(i, pStr.length);
    return pStr;
}
function RTrim(pStr) {
    var i;
    for (i = pStr.length - 1; i >= 0; i--) {
        if (pStr.charAt(i) != " " && pStr.charAt(i) != " ") break;
    }
    pStr = pStr.substring(0, i + 1);
    return pStr;
}
function Trim(pStr) {
    return LTrim(RTrim(pStr));
}
function ExchangeEmpty(pStr) {
    return pStr.replace(/null|undefined|NaN/g, "");
}

/* -------------------------------------------- */
/* 2010-08-30									*/
/* 换算金额为目标汇率的金额                     */
/* -------------------------------------------- */
function ExchangeAmountForRate(pSrcAmount, pSrcRate, pDestRate) {
    var _srcAmount = new Number(new String(pSrcAmount).replace(/,/g, '').Trim()); //源金额
    _srcAmount = (isNaN(_srcAmount) ? 0 : _srcAmount);
    var _srcRate = new Number(new String(pSrcRate).replace(/,/g, '').Trim()); //源汇率
    _srcRate = (isNaN(_srcRate) ? 1 : _srcRate);
    var _destRate = new Number(new String(pDestRate).replace(/,/g, '').Trim()); //目标汇率
    _destRate = (isNaN(_destRate) ? 1 : _destRate);

    var _destAmount = new Number(_srcAmount * _srcRate / _destRate).toFixed(2);

    return new Number(_destAmount);
}

/*	MoneyInput 
*	spectorye
*	2010-5-7
*	参考MonthInput
*/
MoneyInput = (function ($) {
    function MoneyInput(el) {
        this.input = $(el);
        var oThis = this;
        this.input.focus(function () {
            //oThis.input.val(oThis.stringToMoney(oThis.input.val()));
            if (oThis.input.val() == '') {
                oThis.input.val('0.00');
            }
            oThis.input.get()[0].select();
        });
        this.input.blur(function () {
            //alert(oThis.input.val());
            oThis.input.val(oThis.moneyToString(oThis.stringToMoney(oThis.input.val())));
        });
    };

    MoneyInput.prototype = {
        stringToMoney: function (string) {
            string = string.replace(/,/g, '');
            if (String(parseFloat(string)) != 'NaN') {
                return Math.round(parseFloat(string) * 100) / 100;
            }
            else {
                return 0.00;
            }
        },
        moneyToString: function (eValue) {
            try {
                eValue = new Number(eValue).toFixed(2);
                var intPart = "";
                var decPart = "";
                if (eValue.indexOf(",") >= 0) {
                    eValue = eValue.replace(/,/g, "");
                }
                if (eValue.indexOf(".") >= 0) {
                    intPart = eValue.split(".")[0];
                    decPart = eValue.split(".")[1];
                }
                else {
                    intPart = eValue;
                }
                var num = intPart + "";
                var re = /(-?\d+)(\d{3})/
                while (re.test(num)) {
                    num = num.replace(re, "$1,$2")
                }
                if (eValue.indexOf(".") >= 0) {
                    eValue = num + "." + decPart;
                }
                else {
                    eValue = num;
                }
                return eValue;
            }
            catch (e) {
                return '';
            }
        }
    };

    $.fn.money_input = function () {
        return this.each(function () { new MoneyInput(this); });
    };
})(jQuery);

function atoc(pNumberValue) {// 转换为中文大写数字
    var _numberValue = new String(Math.round(pNumberValue * 100)); // 数字金额  
    var _chineseValue = ""; // 转换后的汉字金额  
    var _string1 = "零壹贰叁肆伍陆柒捌玖"; // 汉字数字  
    var _string2 = "万仟佰拾亿仟佰拾万仟佰拾元角分"; // 对应单位  
    var _len = _numberValue.length; // _numberValue 的字符串长度  
    var _ch1; // 数字的汉语读法  
    var _ch2; // 数字位的汉字读法  
    var _nZero = 0; // 用来计算连续的零值的个数  
    var _string3; // 指定位置的数值
    if (_len > 15) {
        alert("超出计算范围");
        return "";
    }
    if (_numberValue == 0) {
        _chineseValue = "零元整";
        return _chineseValue;
    }
    _string2 = _string2.substr(_string2.length - _len, _len); // 取出对应位数的_string2的值
    for (var i = 0; i < _len; i++) {
        _string3 = parseInt(_numberValue.substr(i, 1), 10); // 取出需转换的某一位的值
        if (i != (_len - 3) && i != (_len - 7) && i != (_len - 11) && i != (_len - 15)) {
            if (_string3 == 0) {
                _ch1 = "";
                _ch2 = "";
                _nZero = _nZero + 1;
            }
            else if (_string3 != 0 && _nZero != 0) {
                _ch1 = "零" + _string1.substr(_string3, 1);
                _ch2 = _string2.substr(i, 1);
                _nZero = 0;
            }
            else {
                _ch1 = _string1.substr(_string3, 1);
                _ch2 = _string2.substr(i, 1);
                _nZero = 0;
            }
        }
        else { // 该位是万亿，亿，万，元位等关键位  
            if (_string3 != 0 && _nZero != 0) {
                _ch1 = "零" + _string1.substr(_string3, 1);
                _ch2 = _string2.substr(i, 1);
                _nZero = 0;
            }
            else if (_string3 != 0 && _nZero == 0) {
                _ch1 = _string1.substr(_string3, 1);
                _ch2 = _string2.substr(i, 1);
                _nZero = 0;
            }
            else if (_string3 == 0 && _nZero >= 3) {
                _ch1 = "";
                _ch2 = "";
                _nZero = _nZero + 1;
            }
            else {
                _ch1 = "";
                _ch2 = _string2.substr(i, 1);
                _nZero = _nZero + 1;
            }
            if (i == (_len - 11) || i == (_len - 3)) { // 如果该位是亿位或元位，则必须写上  
                _ch2 = _string2.substr(i, 1);
            }
        }
        _chineseValue = _chineseValue + _ch1 + _ch2;
    }
    if (_string3 == 0) { // 最后一位（分）为0时，加上“整”  
        _chineseValue = _chineseValue + "整";
    }
    return _chineseValue;
}

(function ($) {
    $.fn.scrollFollow = function (options) {
        if (this.attr('scrollFollowInited') == 'true') return;
        this.attr('scrollFollowInited', 'true');
        if (!options || !options.offset)
            options = { offset: 0 };
        this.attr('orgPos', this.css('position'));
        this.attr('orgTop', this.css('top'));
        this.attr('orgOffset', this.offset().top);
        this.attr('isfixed', 'false');
        var obj = this;
        $(window).scroll(function () {
            var sh = parseInt(Math.max(document.body.scrollTop, document.documentElement.scrollTop), 10);
            if (obj.attr('isfixed') == 'false' && sh > obj.attr('orgOffset')) {
                //固顶在屏幕上,不随滚动条变化
                obj.parent().css("min-height", obj.parent().height());
                obj.width(obj.width());
                if ($.browser.msie && $.browser.version <= '6.0') {
                    obj.css('position', 'absolute');
                }
                else {
                    obj.css('position', 'fixed');
                    obj.css('top', options.offset + 'px');
                }
                obj.attr('isfixed', 'true');
            }
            else if (obj.attr('isfixed') && sh < obj.attr('orgOffset')) {
                obj.css('position', obj.attr('orgPos'));
                obj.css('top', obj.attr('orgTop'));
                obj.attr('isfixed', 'false');
            }
            if (obj.attr('isfixed') && $.browser.msie && $.browser.version <= '6.0' && sh > obj.attr('orgOffset')) {
                obj.css('top', (sh + options.offset) + 'px');
            }
        });
    };
})(jQuery);

(function($){  
        $.fn.serializeJson=function(){  
            var serializeObj={};  
            var array=this.serializeArray();  
            var str=this.serialize();  
            $(array).each(function(){  
                if(serializeObj[this.name]){  
                    if($.isArray(serializeObj[this.name])){  
                        serializeObj[this.name].push(this.value);  
                    }else{  
                        serializeObj[this.name]=[serializeObj[this.name],this.value];  
                    }  
                }else{  
                    serializeObj[this.name]=this.value;   
                }  
            });  
            return serializeObj;  
        };  
    })(jQuery);  