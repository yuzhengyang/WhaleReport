
//<script src="~/Plugin/Scripts/bootstrap.utils.js"></script>
//<script src="~/Plugin/Scripts/ajax.tools.js"></script>
//<a  ajax ajax-url="/Home/Test" ajax-aim="#yooo">测试中的测试</a>
//<div id="yooo"></div>
//带加载提示框

$(document).ready(function () {
    //ajax 操作
    $("[ajax]").each(function (index, element) {
        //初始化（有且只有一次）
        if (typeof ($(element).attr("ajax-init")) == "undefined") {
            //lert($(element).attr("ajax-aim") + " - " + $(element).attr("ajax-init") + " - " + "初始化");
            $(element).on("click", function () {
                //加载提示
                $('#ajax-waiting').modal('show');
                //参数
                var url = $(element).attr("ajax-url");
                var type = typeof ($(element).attr("ajax-type")) == "undefined" ? "get" : $(element).attr("ajax-type");
                var htmlobj = "";

                //简单 ajax 操作
                //ajax ajax-url="/WakaTime/Dashboard" ajax-aim="#container" ajax-eval="alert(%ajax-eval);"
                if ($(element).attr("ajax") == "") {
                    htmlobj = $.ajax({ url: url, type: type, async: false });
                }
                //正常 ajax 操作
                //ajax="normal" ajax-url="/Home/Test" ajax-type="post" ajax-data="$name=#name&age=#age" ajax-aim="#container" ajax-eval="alert(%ajax-eval);"
                if ($(element).attr("ajax") == "normal") {
                    var data = typeof ($(element).attr("ajax-data")) == "undefined" ? "" : $(element).attr("ajax-data");
                    //分割字符串 并组装拾取内容
                    if (data.length > 0 && data.substr(0, 1) == "$") {
                        data = data.substring(1, data.length);
                        var strs = data.split("&");
                        data = "";
                        for (i = 0 ; i < strs.length; i++) {
                            var items = strs[i].split("=");
                            data += items[0] + "=" + $(items[1]).val() + (i < strs.length - 1 ? "&" : "");
                        }
                    }
                    htmlobj = $.ajax({ url: url, type: type, data: data, async: false });
                }

                //填充内容
                if (typeof ($(element).attr("ajax-aim")) != "undefined")
                    $($(element).attr("ajax-aim")).html(htmlobj.responseText);

                //可执行代码
                if (typeof ($(element).attr("ajax-eval")) != "undefined") {
                    var value = "\"" + htmlobj.responseText.replace(/\n/g, "").replace(/\r\n/g, "").replace(/ /g, "").replace(/"/g, "") + "\"";
                    var evalCmd = $(element).attr("ajax-eval").replace("%ajax-eval", value);
                    eval(evalCmd);
                }

                //结束加载
                setTimeout("$('#ajax-waiting').modal('hide')", 1000);
                //alert($(element).attr("ajax-aim") + " - " + $(element).attr("ajax-init") + " - " + "执行完毕");
            });
            //已初始化标记
            $(element).attr("ajax-init", "ok");
        }
    });
});