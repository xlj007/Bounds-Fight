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

function Global_Config_Item() {
    this.ID;
    this.b_Global_Config_ID;
    this.b_Item_Name;
    this.b_Item_Value;
    this.b_Item_Type;
    this.Create_Time;
    this.Update_Time;
}
var nItem = 10;
var Global_Config = {
    Add: function (obj_id) {
        var div_Items = '';
        if (obj_id == "div_Recorder") {
            if ($("div[id^='div_Recorder_Item']").length >= 5) {
                alert("设置项不能多于5个");
                return;
            }
            div_Items = '<div id="div_Recorder_Item_' + nItem + '"><label class="control-label col-md-2"></label><div class="col-md-10 padding-bottom">奖扣人次达 <input class="form-control inline short_txt text-box single-line" id="b_Global_Config_Item_' + nItem + '__b_Item_Name" name="b_Global_Config_Item[' + nItem + '].b_Item_Name" type="text" value="" /> 奖 <input class="form-control inline short_txt text-box single-line" id="b_Global_Config_Item_' + nItem + '__b_Item_Value" name="b_Global_Config_Item[' + nItem + '].b_Item_Value" type="text" value="" /> <span onclick="Global_Config.Remove(\'div_Recorder_Item_' + nItem + '\')" style="cursor:pointer"><img src="/Content/img/dec.png" /></span></div>';
        } else {
            if ($("div[id^='div_Attence_Item']").length >= 5) {
                alert("设置项不能多于5个");
                return;
            }
            div_Items = '<div id="div_Attence_Item_' + nItem + '"><label class="control-label col-md-2"></label><div class="col-md-10 padding-bottom">> <input class="form-control inline short_txt text-box single-line" id="b_Global_Config_Item_' + nItem + '__b_Item_Name" name="b_Global_Config_Item[' + nItem + '].b_Item_Name" type="text" value="" /> 小时，超出部分奖 <input class="form-control inline short_txt text-box single-line" id="b_Global_Config_Item_' + nItem + '__b_Item_Value" name="b_Global_Config_Item[' + nItem + '].b_Item_Value" type="text" value="" /> 分/小时 <span onclick="Global_Config.Remove(\'div_Attence_Item_' + nItem + '\')" style="cursor:pointer"><img src="/Content/img/dec.png" /></span></div>';
        }
        $("#" + obj_id).append($(div_Items));
        nItem++;
    },
    Remove: function (obj_id) {
        $("#" + obj_id).remove();
    },
    Save: function () {
        var list_item = new Array();
        //获取记录人加分数组
        var nIndex = 0;
        var item;

        $("#div_Recorder").find("input:text").each(function () {
            if (nIndex % 2 == 0) {
                item = new Global_Config_Item();
                item.b_Item_Name = this.value;
            } else {
                item.b_Item_Value = this.value;
                item.b_Item_Type = 1;
                list_item.push(item);
            }
            nIndex++;
        });

        nIndex = 0;

        //获取加班积分数组
        $("#div_Attence").find("input:text").each(function () {
            if (nIndex % 2 == 0) {
                item = new Global_Config_Item();
                item.b_Item_Name = this.value;
            } else {
                item.b_Item_Value = this.value;
                item.b_Item_Type = 2;
                list_item.push(item);
            }
            nIndex++;
        });

        var config_model = {
            ID: $("#ID").val(),
            //b_Recorder_Add: "",
            //b_Attence_To_Bounds: "",
            b_Recorder_Price: $("#b_Recorder_Price").val(),
            b_ChuangFu_To_Bounds: $("#b_ChuangFu_To_Bounds").val(),
            b_ActualValue_To_Bounds: $("#b_ActualValue_To_Bounds").val(),
            b_VirtualValue_To_Bounds: $("#b_VirtualValue_To_Bounds").val(),
            b_Sale_To_Bounds: $("#b_Sale_To_Bounds").val(),            
            b_A_To_B: $("#b_A_To_B").val(),
            b_Price_Paper_Set: $("#b_Price_Paper_Set").val(),
            b_SignIn_Bounds: $("#b_SignIn_Bounds").val(),
            b_SignIn_Time: $("#b_SignIn_Time").val(),
            b_FixedBounds_ToAttence: $("#b_FixedBounds_ToAttence").val(),
            b_Check_Date: $("#b_Check_Date").val(),
            b_Global_Config_Item: list_item
        }

        $.ajax({
            url: "/b_Global_Config/Save/",
            type: "post",
            contentType: "application/json;charset=utf-8",
            data: JSON.stringify(config_model),
            success: function (data) {
                if (data == "OK") {
                    alert("保存成功。");
                } else {
                    alert(data);
                }
            }
        });
    }
}

var Cus_Group = {
    BindMember: function (group_id, source_type) {
        $("#hid_User_Type").val(source_type);
        GetExistUser(group_id);
        $("#div_Sel_User").modal('show');
    }
}