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
        url: configUrl + '/redmomeryserver.asmx/UploadLB',
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







function createtemp()
{
    var form1 = document.createElement("from");
    form1.id = "form1";
    form1.name = "form1";
    var files = $("input[name='lbphoto']")[0];
    form1.appendChild(files);

}