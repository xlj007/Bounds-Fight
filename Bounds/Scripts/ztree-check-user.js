$(document).ready(function () {
    //GetCheckNodeList();
});

function GetCheckNodeList() {
    var zTree;

    var setting = {
        view: {
            dblClickExpand: false,
            showLine: true,
            selectedMulti: false
        },
        data: {
            simpleData: {
                enable: true,
                idKey: "id",
                pIdKey: "b_PID",
                rootPId: ""
            }
        },
        callback: {
            beforeClick: function (treeId, treeNode) {
                var zTree = $.fn.zTree.getZTreeObj("tree");
                if (treeNode.isParent) {
                    //zTree.expandNode(treeNode);
                    return true;
                } else {
                    //alert(treeNode.b_Name)
                    return true;
                }
            },
            onClick: function (event, treeId, treeNode, clickFlag) {
                GetDepartUser(treeNode.ID)
            }
        }
    };

    //进行异步传输
    $.ajax({
        url: "/b_Organize/ShowTree",
        type: "post",
        dataType: "json",
        data: { nPid: 0 }, //发送服务器数据
        success: function (data) {  //成功事件
            var t = $("#usertree");
            t = $.fn.zTree.init(t, setting, data);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) { //发送失败事件
            alert(XMLHttpRequest);
            alert(textStatus);
        }
    });
}

function GetExistUser(role_id) {
    $("#hid_Sel_Role_Id").val(role_id);
    var user_type = $("#hid_User_Type").val();
    var target_url = '';
    var post_data = '';
    if (user_type == "group") {
        target_url = "/b_Cus_Group/GetGroupUser";
        post_data = '{ "group_id": ' + role_id + '}';
    } else if (user_type == "check") {
        target_url = "/b_Organize/GetCheckUser";
        post_data = '{ "check_type": ' + role_id + '}';
    } else {
        target_url = "/b_User/GetRoleUser";
        post_data = '{ "role_id":' + role_id + '}';
    }

    $.ajax({
        url: target_url,
        type: "post",
        dataType: "json",
        data: JSON.parse(post_data),
        success: function (data) {
            $("#selOrg").empty();
            $("#hid_Sel_User_Id").val('');
            if (data.length != 0) {
                for (var i = 0; i < data.length; i++) {
                    AddDiv(data[i].ID, data[i].b_UserName);
                }
            }
        }
    });
}

function AddDiv(nodeId, nodeValue) {
    var objDiv = $("#div_" + nodeId);
    if (objDiv.length == 0) {
        var div = $('<div id="div_' + nodeId + '" class ="childDiv" name="div_sel_user">' + nodeValue + '  <span style="cursor:pointer" onclick="RemoveDiv(' + nodeId + ')"><img src="../../Content/img/cancel.png" /></span></div>');
        $("#selOrg").append(div);
        $("#selOrg").scrollTop(2000);

        //添加id
        $("#hid_Sel_User_Id").val($("#hid_Sel_User_Id").val() + nodeId + ",");
    }
}

function RemoveByValue(Arr, value) {
    for (var i = 0; i < Arr.length; i++) {
        if (Arr[i] == value) {
            Arr.splice(i, 1);
            break;
        }
    }

    return Arr;
}

function RemoveDiv(nodeId) {
    var objDiv = $("#div_" + nodeId);
    if (objDiv.length != 0) {
        objDiv.remove();
        $("#" + nodeId).prop("checked", false);

        //删除ID
        var arrIds = $("#hid_Sel_User_Id").val().split(',');
        RemoveByValue(arrIds, nodeId);
        
        $("#hid_Sel_User_Id").val(arrIds.join(","));
    }
}

function AddUser(obj) {
    if (obj.checked == true) {
        AddDiv(obj.id, obj.value);
    } else {
        RemoveDiv(obj.id);
    }
}

function GetDepartUser(depart_id) {
    $.ajax({
        url: "/b_User/GetDepartUser",
        type: "post",
        dataType: "json",
        data: { depart_id: depart_id },
        success: function (data) {
            if (data.length != 0) {
                $("#div_SelUser").empty();
                for (var i = 0; i < data.length; i++) {
                    var strChecked = "";
                    var array = $("#hid_Sel_User_Id").val().split(',');
                    if ($.inArray(data[i].ID.toString(), array) > -1) {
                        strChecked = "checked";
                    }
                    var Div = $('<div class="UserItem"><table border=0 width="100%"><tr><td class="left_td">' + data[i].b_UserName + '</td><td class="right_td"><input type="checkbox" id="' + data[i].ID + '" value="' + data[i].b_UserName + '" onclick="AddUser(this)" ' + strChecked + ' /></td></tr></table>');
                    $("#div_SelUser").append(Div);
                }
            }
        }
    });
}


function SaveRoleUser() {
    var role_id = $("#hid_Sel_Role_Id").val();
    var user_ids = $("#hid_Sel_User_Id").val();
    
    if ($("#hid_User_Type").val() == "event") {
        Point.BindMemberInfo($("div[name='div_sel_user']"), $("#hid_event_id").val());
        $("#div_Sel_User").modal("hide");
    } else if ($("#hid_User_Type").val() == "check") {
        SetCheck.BindUserInfo($("div[name='div_sel_user']"), $("#hid_check_type").val());
    } else if ($("#hid_User_Type").val() == "group") {
        $.ajax({
            url: "/b_Cus_Group/SaveGroupUser",
            type: "post",
            dataType: "json",
            data: { group_id: role_id, user_id: user_ids },
            success: function (data) {
                if (data == "OK") {
                    alert("保存成功。")
                    $("#div_Sel_User").modal("hide");
                    location.href = "/b_Cus_Group";
                } else {
                    alert(data);
                }
            }, error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(XMLHttpRequest.status);
                alert(XMLHttpRequest.readyState);
                alert(textStatus);
                alert(errorThrown);
            }
        });
    } else {
        $.ajax({
            url: "/b_User/SaveRoleUser",
            type: "post",
            dataType: "json",
            data: { role_id: role_id, user_id: user_ids },
            success: function (data) {
                if (data == "OK") {
                    alert("保存成功。")
                    $("#div_Sel_User").modal("hide");
                    location.href = "/b_Role";
                } else {
                    alert(data);
                }
            }, error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(XMLHttpRequest.status);
                alert(XMLHttpRequest.readyState);
                alert(textStatus);
                alert(errorThrown);
            }
        });
    }
}