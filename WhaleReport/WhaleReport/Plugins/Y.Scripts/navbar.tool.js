$(document).ready(function () {
    //导航状态
    $("#navbar>li").each(function (index, element) {
        $(this).on('click', function () {
            $("#navbar>li").each(function (num) { $(this).attr("class", null); });
            $(this).addClass("active");
        })
    });
});