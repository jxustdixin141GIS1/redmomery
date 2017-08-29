/// <reference path="D:\题库系统\redMomery\redmomery.server\resource/Commandjs.js" />
//--------------------老兵操作代码---------------------------
function LoadLBbyname(LBname, isappend) {

    var name = LBname;
    var url = "/LBbx/getLBINfo";
    $.ajax({
        type: "post",
        dataType: "json",
        url: url,
        data: { LBname: name },
        success: function (data) {
            var data1 = data.response;
            var g = JSON.parse(data1)
            var data = g.d;//取出对象，json
            temp = data1;
            var divcontex = $("#LB_story")[0];
            divcontex.innerHTML = data1[0].toString();
        },

        error: function (XMLHttpRequest, textStatus, errorThrown) {

        },
        complete: function (data) {
            //注意这里是需要判别下是不是json格式的数据，若是不是的话，就只能硬钢Xml了
            var data1 = data.responseText;
            var xmls = xmlfromtext(data1);
            var temp = JsonFromXml(xmls);
            for (var i = 0; i < temp.length; i++) {
                lb = temp[i];
                LB_INFO = lb;
                showLB(LB_INFO, isappend);
            }
            LBList = temp;
            //了数据转换完成
        }
    });
}
function LoadLBsByTID(TID, isappend)
{
    var name = TID;
    var url = "/LBbx/GetLBByTID";
    $.ajax({
        type: "post",
        dataType: "json",
        url: url,
        data: { T_ID: TID },
        success: function (data) {
            var data1 = data.response;
            var g = JSON.parse(data1)
            var data = g.d;//取出对象，json
            temp = data1;
            var divcontex = $("#LB_story")[0];
            divcontex.innerHTML = data1[0].toString();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
        },
        complete: function (data) {
            //注意这里是需要判别下是不是json格式的数据，若是不是的话，就只能硬钢Xml了
            var data1 = data.responseText;
            var xmls = xmlfromtext(data1);
            var temp = JsonFromXml(xmls);
            for (var i = 0; i < temp.length; i++) {
                lb = temp[i];
                LB_INFO = lb;
                showLB(LB_INFO, isappend);
            }

            LBList = temp;
            //了数据转换完成
        }
    });
}


//----------------------论坛帖子加载代码---------------------
function LoadTitleByID(T_ID,isappend)
{
    var name = T_ID;
    var url = "/LBbx/GetLBByTID";
    $.ajax({
        type: "post",
        dataType: "json",
        url: url,
        data: { T_ID: name },


        success: function (data) {
            var data1 = data.response;
            var g = JSON.parse(data1)
            var data = g.d;//取出对象，json
            temp = data1;
            var divcontex = $("#lB_TILTE")[0];
            divcontex.innerHTML = data1[0].toString();
        },

        error: function (XMLHttpRequest, textStatus, errorThrown) {

        },
        complete: function (data) {
            //注意这里是需要判别下是不是json格式的数据，若是不是的话，就只能硬钢Xml了
            var data1 = data.responseText;
            var xmls = xmlfromtext(data1);
            var temp = JsonFromXml(xmls);
            TitleList = temp;
            for (var i = 0; i < TitleList.length; i++) {
                CurrentTitle = TitleList[i];//默认为第一条数据为当前帖子
                showTitle(CurrentTitle, isappend);
            }
           
        }
    });
}
function LoadCommentByTID(T_ID, isappend)
{
    var name = T_ID;
    var url = "/LBbx/getComentbyTitleId";
    $.ajax({
        type: "post",
        dataType: "json",
        url: url,
        data: { T_ID: name },


        success: function (data) {
            var data1 = data.response;
            var g = JSON.parse(data1)
            var data = g.d;//取出对象，json
            temp = data1;
            var divcontex = $("#lB_TILTE")[0];
            divcontex.innerHTML = data1[0].toString();
        },

        error: function (XMLHttpRequest, textStatus, errorThrown) {

        },
        complete: function (data) {
            //注意这里是需要判别下是不是json格式的数据，若是不是的话，就只能硬钢Xml了
            var data1 = data.responseText;
            var xmls = xmlfromtext(data1);
            var temp = JsonFromXml(xmls);
            commentList = temp;
            showCommentList(commentList);
        }
    });
}

//---------------------页面布局部分js代码--------------------
function showLB(lb,isappend) {
    var divs = $("#LB_story")[0];
    var lbtext="<br />" + "<p >" + "<span style='font-size:17px;font-weight:bold;font-family:华文新魏;'>" + "姓&nbsp&nbsp名：" + "</span>" + "<span style='font-size:16px; font-family:楷体;font-weight:bold'>" + lb.LBname + "</span>" + "</p>" +
                  "<p >" + "<span style='font-size:17px;font-weight:bold;font-family:华文新魏;'>" + "性&nbsp&nbsp别：" + "</span>" + "<span style='font-size:16px; font-family:楷体;font-weight:bold'>" + lb.LBsex + "</span>" + "</p>" +
                  "<p >" + "<span style='font-size:17px;font-weight:bold;font-family:华文新魏;'>" + "出生日期：" + "</span>" + "</span>" + "<span style='font-size:16px; font-family:楷体;font-weight:bold'>" + lb.LBbirthday + "</span>" + "</p>" +
                  "<p >" + "<span style='font-size:17px;font-weight:bold;font-family:华文新魏;'>" + "部队职务：" + "</span>" + "<span style='font-size:16px; font-family:楷体;font-weight:bold'>" + lb.LBjob + "</span>" + "</p>" +
                  "<p >" + "<span style='font-size:17px;font-weight:bold;font-family:华文新魏;'>" + "现居住地：" + "</span>" + "<span style='font-size:16px; font-family:楷体;font-weight:bold'>" + lb.LBdomicile + "</span>" + "</p>" +
                  "<p >" + "<span style='font-size:17px;font-weight:bold;font-family:华文新魏;'>" + "部队番号：" + "</span>" + "<span style='font-size:16px; font-family:楷体;font-weight:bold'>" + lb.designation + "</span>" + "</p>" +
                  "<p >" + "<span style='font-size:17px;font-weight:bold;font-family:华文新魏;'>" + "生活现状：" + "</span>" + "<span style='font-size:16px; font-family:楷体;font-weight:bold'>" + lb.LBlife + "</span>" + "</p>"
                   + "<p >" + "<span style='font-size:16px; font-family:楷体;font-weight:bold'>" + lb.LBexperience + "</span>" + "</p>" +
                    "<p >" + "<img src='" + lb.LBPhoto.toString().replace('~/', "") + "' width=30%   height=30% />" + "</p>";

    if (isappend == false) {
        divs.innerHTML = lbtext;
    }
    else {
        divs.innerHTML =divs.innerHTML+ lbtext;
    }
}
function showTitle(title, isList) {
    var jubu = title.Context;
    var showHtml = "<div id=\"C_comment\">" +
                   "<p>" + title.TITLE + "</p>" +
                   "<hr>" +
                   "<br/>" +
                   "<span id=\"c_info\">" + "<p>时间：" + title.F_TIME + " &nbsp &nbsp 发帖人：" + title.USER_NETNAME + "</p></span><br/>" +
                   "<span id=\"c_context\">" + jubu + "</span><br/>" +
                   "<hr>" +
                  "<span id=\"appraise\"><p>" + "&nbsp &nbsp回复数：" + title.N_RESPONSE + "&nbsp &nbsp点赞数：" + title.N_YES + "</span><br/>";

    var divs = $("#lB_TILTE")[0];
    if (isList == false) {
        divs.innerHTML = showHtml;
    }
    else {
        divs.innerHTML += showHtml;
    }

}
function showCommentList(CList)
{
    for(var i=0;i<CList.length;i++)
    {
        CurrentComment = CList[i];
        showCommment(CList[i]);
    }
}
function showCommment(C)
{
    var oSize =C.Context;
    //动态创建评论模块
    oHtml = '<div class="comment-show-con clearfix">' +
               '<div id="CListTID_' + C.T_ID + '">' +
               '<div id="CList_' + C.C_ID+'">'+
               '<div class="comment-show-con-img pull-left">' +
                     '<img src="' + C.USER_IMG.replace("~", "") + '" style="height:90px width:90px" alt="">' +
               '</div>' +
                '<div class="comment-show-con-list pull-left clearfix">' +
                     '<div class="pl-text clearfix">' +
                           '<a href="#" class="comment-size-name">' + C.USER_NETNAME + ' : </a>' +
                            '<span class="my-pl-con">&nbsp;' + oSize + '</span>' +
                     '</div>' +
                     ' <div class="date-dz">' +
                         '<span class="date-dz-left pull-left comment-time">' + C.F_TIME.replace("T"," ").split('.')[0] + '</span>' +
                         '<div class="date-dz-right pull-right comment-pl-block">' +
                             '<a href="javascript:;" class="removeBlock">删除</a> ' +
                             '<a href="javascript:;" class="date-dz-pl pl-hf hf-con-block pull-left">回复(' + C.n_c + ')</a>' +
                             '<span class="pull-left date-dz-line">|</span>' +
                             '<a href="javascript:;" class="date-dz-z pull-left">' +
                             '<i class="date-dz-z-click-red"></i>赞'+
                             '(<i class="z-num">' + C.n_y + '</i>)' +
                             '</a> ' +
                          '</div>' +
                      '</div>' +
                      '<div class="hf-list-con">' +
                      '</div>' +
                    '</div>' +
                    '</div>' +
                    '</div>'+
                 ' </div>';
    if (oSize.replace(/(^\s*)|(\s*$)/g, "") != '') {
        $(".reviewArea.clearfix").children("a.plBtn").parents('.reviewArea ').siblings('.comment-show').prepend(oHtml);
        $(".reviewArea.clearfix").children("a.plBtn").siblings('.flex-text-wrap').find('.comment-input').prop('value', '').siblings('pre').find('span').text('');
    }
}
//--------------------论坛操作代码---------------------------
function postComment(TID,Commentcontext)
{
    var name = TID;
    var url = "/LBbx//addCommentByTID"
    $.ajax({
        type: "post",
        dataType: "json",
        url: url,
        data: { TID: name, context: Commentcontext},
        success: function (data) {
            var data1 = data.response;
            var g = JSON.parse(data1)
            var data = g.d;//取出对象，json
            temp = data1;
            var divcontex = $("#lB_TILTE")[0];
            divcontex.innerHTML = data1[0].toString();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {},
        complete: function (data) {
            //注意这里是需要判别下是不是json格式的数据，若是不是的话，就只能硬钢Xml了
            var data1 = data.responseText;
            var xmls = xmlfromtext(data1);
            var temp = JsonFromXml(xmls);
            postmessage = temp;
            showCommentList(postmessage);
        }
    });
}
function deletecommet(CID, objtemp)
{
    var name = CID;
    var url =  "/LBbx//deleteCommentByCID"
    $.ajax({
        type: "post",
        dataType: "json",
        url: url,
        data: { CID: name },


        success: function (data) {
            var data1 = data.response;
            var g = JSON.parse(data1)
            var data = g.d;//取出对象，json
            temp = data1;
            var divcontex = $("#lB_TILTE")[0];
            divcontex.innerHTML = data1[0].toString();
        },

        error: function (XMLHttpRequest, textStatus, errorThrown) {

        },
        complete: function (data) {
            //注意这里是需要判别下是不是json格式的数据，若是不是的话，就只能硬钢Xml了
            var data1 = data.responseText;
            var xmls = xmlfromtext(data1);
            if (xmls.childNodes[0].childNodes[1].innerHTML.toLowerCase() == "true") {
                alert("删除成功");

                var oT = $(objtemp).parents('.date-dz-right').parents('.date-dz').parents('.all-pl-con');
                if (oT.siblings('.all-pl-con').length >= 1) {
                    oT.remove();
                } else {
                    $(objtemp).parents('.date-dz-right').parents('.date-dz').parents('.all-pl-con').parents('.hf-list-con').css('display', 'none')
                    oT.remove();
                }
                $(objtemp).parents('.date-dz-right').parents('.date-dz').parents('.comment-show-con-list').parents('.comment-show-con').remove();
            }
            else {
                alert(temp[0]);
            }

        }
    });
}