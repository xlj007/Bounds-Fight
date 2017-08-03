$(document).ready(function () {
    GetNodeList();

    $("#btnSaveNew").click(function () {
        SaveNew();
    })

    $("#btnSaveEdit").click(function () {
        SaveEdit();
    })

    $("#btnOrgCheck").click(function () {
        SaveCheck();
    });
});

function GetNodeList() {
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
                    return false;
                } else {
                    //alert(treeNode.b_Name)
                    return true;
                }
            },
            onRightClick: function (event, treeId, treeNode) {
                if (!treeNode && event.target.tagName.toLowerCase() != "button" && $(event.target).parents("a").length == 0) {
                    showRMenu("root", event.clientX, event.clientY, treeNode);
                } else if (treeNode && !treeNode.noR) {
                    showRMenu("node", event.clientX, event.clientY, treeNode);
                } else {
                    return false;
                }
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
            var t = $("#tree");
            t = $.fn.zTree.init(t, setting, data);
            var zTree = $.fn.zTree.getZTreeObj("tree");
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) { //发送失败事件
            alert(XMLHttpRequest);
            alert(textStatus);
        }
    });
}

//显示右键菜单  
function showRMenu(type, x, y, node) {
    $("#rMenu ul").show();
    $("#node").val(node.ID);
    $("#txtName_Edit").val(node.b_Name);
    $("#rMenu").css({ "top": y + "px", "left": x + "px", "visibility": "visible" }); //设置右键菜单的位置、可见  
    $("body").bind("mousedown", onBodyMouseDown);
}
//隐藏右键菜单  
function hideRMenu() {
    if ($("#rMenu")) $("#rMenu").css({ "visibility": "hidden" }); //设置右键菜单不可见  
    $("body").unbind("mousedown", onBodyMouseDown);
}
//鼠标按下事件  
function onBodyMouseDown(event) {
    if (!(event.target.id == "rMenu" || $(event.target).parents("#rMenu").length > 0)) {
        $("#rMenu").css({ "visibility": "hidden" });
    }
}

function SaveNew() {
    var nodeid = $("#node").val();
    var nodevalue = $("#txtName_New").val();
    $.ajax({
        url: "/b_Organize/SaveNew",
        type: "post",
        dataType: "json",
        async: false,
        data: { NodeId: nodeid, NodeValue: nodevalue },
        success: function (data) {
            if (data == "true") {
                alert("添加成功！");
                GetNodeList();
                $("#b_OrgNew").modal('hide');
                $("#txtName_New").val("");
            } else {
                alert(data);
            }
        }
    });
}

function SaveEdit() {
    var node_id = $("#node").val();
    var node_value = $("#txtName_Edit").val();
    $.ajax({
        url: "/b_Organize/SaveEdit",
        type: "post",
        dataType: "json",
        data: { NodeId: node_id, NodeValue: node_value },
        success: function (data) {
            if (data == "true") {
                alert("修改成功！");
                GetNodeList();
                $("#b_OrgEdit").modal('hide');
                $("#txtName_Edit").val("");
            } else {
                alert(data);
            }
        }
    });
}

function DeleteOrg() {
    if (confirm("删除后无法恢复，确定删除此节点？")) {
        var node_id = $("#node").val();
        $.ajax({
            url: "b_Organize/DeleteOrg",
            type: "post",
            dataType: "json",
            data: { NodeId: node_id },
            success: function (data) {
                if (data == "true") {
                    alert("删除成功！");
                    GetNodeList();
                } else {
                    alert(data);
                }
            }
        });
    }
}

function GetCheck() {
    var node_id = $("#node").val();
    $.ajax({
        url: "b_Organize/GetCheck",
        type: "post",
        dataType: "json",
        data: { NodeId: node_id },
        success: function (data) {
            if (data.length > 0) {
                data = eval('(' + data + ')');
                $("#txtFirstCheck").val(data.b_First_User);
                $("#txtFinalCheck").val(data.b_Final_User);
            } else {
                alert(data);
            }
        }
    });
}

function ClearCheckUI() {
    $("#hid_first_check_value").val('');
    $("#hid_final_check_value").val('');
    $("#txtFirstCheck").val('');
    $("#txtFinalCheck").val('');
}

function SaveCheck() {
    var node_id = $("#node").val();
    var first_check = $("#hid_first_check_value").val();
    var final_check = $("#hid_final_check_value").val();
    $.ajax({
        url: "b_Organize/SaveCheck",
        type: "post",
        dataType: "json",
        data: { NodeId: node_id, FirstCheck: first_check, FinalCheck: final_check },
        success: function (data) {
            if (data == "true") {
                alert("保存成功！");
                $("#b_OrgCheck").modal('hide');
                ClearCheckUI();
            } else {
                alert(data);
            }
        }
    });
}