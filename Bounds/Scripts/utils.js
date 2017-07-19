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

            } else {
                alert(data);
            }
        }
    });
}