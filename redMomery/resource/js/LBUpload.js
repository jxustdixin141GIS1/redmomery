function uploadimage()
{
    var imginfo = $("input[name='lbphoto']")[0];
    var imgdiv = $("#imagepreview")[0];
    var imgpre = imgdiv.getElementsByTagName("img")[0];
    imgpre.src = window.URL.createObjectURL(imginfo.files.item(0));
    imgdiv.style.display = "block";
    $('form').on('submit', function (e) {
        e.preventDefault();
    });
}
function uploadimg()
{
    var imginfo = $("input[name='lbphoto']")[0];
    var imgdiv = $("#imagepreview")[0];
    var imgpre = imgdiv.getElementsByTagName("img")[0];
    var formData = new FormData();
    formData.append("filename", imginfo.files[0].name);
    formData.append("data", imginfo);
    $.ajax({
        url: configUrl + '/redmomeryserver.asmx/uploadImage',
        type: 'POST',
        data: formData,
        async: false,
        cache: false,
        contentType: false,
        processData: false,
        //                    dataType: "jsonp",//问题就在这里，如果用了jsonp，那么后台就接收不到文件流，无法获得文件流，就没办法把文件写入服务器。如果不指定，就是注释掉，虽然ajax提交之后，还是跑到error那里去，但是文件已经是成功写入服务器的了。
        success: function (returndata) {
            alert("成功");
        },
        error: function (returndata) {
            alert("失败");
        }
    });
}
function uploadlB()
{
    var lbform = $("#lbziliao")[0];
    var formData = new FormData(lbform);
    $.ajax({
        url: configUrl + '/LBbx/UploadLB',
        type: 'POST',
        data: formData,
        async: false,
        cache: false,
        contentType: false,
        processData: false,
        success: function (returndata) {
            alert("成功");
        },
        error: function (returndata) {
            alert("失败");
        },
        complete: function (data) {
            alert(data.responseText);
        }
    });
}

function geocoding()
{
    var addressname = $("input[name='lbaddress']")[0];
    var addres = addressname.value;
    GetXYByAddressName(addres, function (request)
    {
        var lng = "";
        var lat = "";
        //这里就是用来处理返回地理坐标座不爱的部分
        var resultText = request.responseText;
        var resultd = (JSON.parse(resultText)).d;
        //下面开始进行处理
        for(var i=0;i<resultd.length;i++)
        {
            var temp = resultd[i].split(":");
            if (temp[0] == "lng")
            {
                lng = temp[1];
            }
            if (temp[0] == "lat")
            {
                lat = temp[1];
            }
        }
        alert("lng:" + lng.toString() + "\n\r" + "lat:" + lat.toString()+"\n\r"+"这行代码在LBUpload.js 行：85，功能扩展在以后应能自动实现地图的定位和移动");
    });
}





function createtemp()
{
    var form1 = document.createElement("from");
    form1.id = "form1";
    form1.name = "form1";
    var files = $("input[name='lbphoto']")[0];
    form1.appendChild(files);

}

