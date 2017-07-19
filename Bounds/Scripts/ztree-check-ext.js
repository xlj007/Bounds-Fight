$(document).ready(function () {
    var setting = {
        check: {
            enable: true,
            //chkboxType: { "Y": "ps", "N": "ps" }
            chkboxType: { "Y": "", "N": "" }
        },
        data: {
            simpleData: {
                enable: true,
                idKey: "id",
                pIdKey: "b_PID"
            }
        },
        callback: {
            onCheck: onCheck
        }
    };

    //进行异步传输
    $.ajax({
        url: "/b_Organize/ShowTree",
        type: "post",
        dataType: "json",
        data: { nPid: 0 }, //发送服务器数据
        success: function (data) {  //成功事件
            $.fn.zTree.init($("#treecheck"), setting, data);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) { //发送失败事件
            alert(XMLHttpRequest);
            alert(textStatus);
        }
    });
});

function HideOrg() {
    $("#CheckTreeModel").modal("hide");
}

function onCheck(e, treeId, treeNode) {
    if (treeNode.checked) {
        //添加选中的项
        AddDiv(treeNode.ID, treeNode.b_Name)
    } else {
        //去除选中的项
        RemoveDiv(treeNode.ID)
    }
}

function AddDiv(nodeId, nodeValue) {
    var objDiv = $("#div_" + nodeId);
    if (objDiv.length == 0) {
        var div = $('<div id="div_' + nodeId + '" class ="childDiv">' + nodeValue + '</div>');
        $("#selOrg").append(div);
        $("#selOrg").scrollTop(200);
    }
}

function RemoveDiv(nodeId) {
    var objDiv = $("#div_" + nodeId);
    if (objDiv.length != 0) {
        objDiv.remove();
    }
}

function OrgConfirm() {
    var sel_ids = "";
    var sel_names = "";
    $("#selOrg div").each(function () {
        sel_names += $(this).text() + ",";
        sel_ids += $(this).attr("id").replace(/div_/, '') + ",";
    });
    $("#b_Depart_ID").val(sel_names);
    $("#hid_b_Depart_ID").val(sel_ids);
    $("#CheckTreeModel").modal('hide');
}