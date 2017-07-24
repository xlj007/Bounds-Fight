function CheckAll() {
}

function SaveNewUser() {
    var User_Model = {
        b_UserName: $("#b_UserName").val(),
        b_RealName: $("#b_RealName").val(),
        b_Sex: $("input[name='b_Sex']:checked").val(),
        b_Password: $("#b_Password").val(),
        b_WorkNum: $("#b_WorkNum").val(),
        b_Email: $("#b_Email").val(),
        b_PhoneNum: $("#b_PhoneNum").val(),
        b_Depart_ID: $("#hid_b_Depart_ID").val(),
        b_EntryDate: $("#b_EntryDate").val(),
        b_Role_ID: $("#b_Role_ID").val(),
        b_Reward_Auth_ID: $("#b_Reward_Auth_ID").val(),
        b_Ranking: $("input[name='b_Ranking']:checked").val()
    };
    $.ajax({
        url: 'b_User/Create',
        type: 'post',
        contentType: 'application/json;charset=utf-8',
        data: JSON.stringify(User_Model),
        success: function (data) {
            if (data == "OK") {
                alert("添加成功");
                $("#NewUser").modal('hide');
                //页面刷新
                location.href = "/b_User"
            } else {
                alert(data);
            }
        }
    });
}

var Auth = {
    Add: function () {
        var b_auth_list = '';
        $('input:checkbox:checked').each(function () {
            b_auth_list += $(this).attr('id').replace(/chk_/, ',')
        });
        if (b_auth_list.length > 1) {
            b_auth_list = b_auth_list.substring(1);
        }
        var auth_model = {
            //__RequestVerificationToken: $("#__RequestVerificationToken").val(),
            Name: $("#b_Role_Name").val(),
            Description: $("#b_Role_Description").val(),
            Auth_List: b_auth_list
        };

        $.ajax({
            url: "/b_Role/Create",
            type: "post",
            contentType: "application/json;charset=utf-8",
            data: JSON.stringify(auth_model),
            success: function (data) {
                if (data == "OK") {
                    alert("添加成功。")
                    location.href = "/b_Role";
                } else {
                    alert(data);
                }
            }
        });
    },
    Edit: function (role_id) {
        var b_auth_list = '';
        $('input:checkbox:checked').each(function () {
            b_auth_list += $(this).attr('id').replace(/chk_/, ',')
        });
        if (b_auth_list.length > 1) {
            b_auth_list = b_auth_list.substring(1);
        }
        var auth_model = {
            //__RequestVerificationToken: $("#__RequestVerificationToken").val(),
            Name: $("#b_Role_Name").val(),
            Description: $("#b_Role_Description").val(),
            Auth_List: b_auth_list
        };

        $.ajax({
            url: "/b_Role/Edit/" + role_id,
            type: "post",
            contentType: "application/json;charset=utf-8",
            data: JSON.stringify(auth_model),
            success: function (data) {
                if (data == "OK") {
                    alert("修改成功。");
                    location.href = "/b_Role";
                } else {
                    alert(data);
                }
            }
        });
    },
    BindMember: function (role_id) {
        GetExistUser(role_id);
        $("#div_Sel_User").modal('show');
    }
}