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
        GetCheckNodeList();
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
        GetCheckNodeList();
        $("#hid_User_Type").val(source_type);
        GetExistUser(group_id);
        $("#div_Sel_User").modal('show');
    }
}

var Fix_Point = {
    BindMember: function (fix_type_id, source_type) {
        GetCheckNodeList();
        $("#hid_User_Type").val(source_type);
        GetExistUser(fix_type_id);
        $("#div_Sel_User").modal('show');
    }
}

var SetCheck = {
    BindUser: function (check_type, source_type) {
        GetCheckNodeList();
        $("#hid_User_Type").val(source_type);
        $("#hid_check_type").val(check_type);
        GetExistUser(check_type);
        $("#div_Sel_User").modal('show');
        $("#b_OrgCheck").modal("hide");
    },
    BindUserInfo: function (objSel, check_type) {
        var selText = '';
        var selValue = '';
        objSel.each(function () {
            selText += $(this).text().trim() + ",";
            selValue += this.id.substring(this.id.lastIndexOf('_') + 1) + ",";
        });
        if (check_type == 1) {
            $("#txtFirstCheck").empty();
            $("#txtFirstCheck").val(selText);
            $("#hid_first_check_value").val(selValue);
        } else if (check_type == 2) {
            $("#txtFinalCheck").empty();
            $("#txtFinalCheck").val(selText);
            $("#hid_final_check_value").val(selValue);
        }
        $("#div_Sel_User").modal("hide");
        $("#b_OrgCheck").modal("show");
    }
}

function b_Point_Event_Memver() {
    this.ID;
    this.b_A_Point;
    this.b_B_Point;
    this.b_Value_Type;
    this.b_Value_Point;
    this.b_User_ID;
}
function b_Point_Event() {
    this.ID;
    this.b_Event;
    this.b_Event_Note;
    this.b_Point_Event_Memver;
}
function b_Point() {
    this.ID;
    this.b_Event_Date;
    this.b_Subject;
    this.b_Note;
    this.b_First_Check_ID;
    this.b_Final_Check_ID;
    this.b_Point_Event;
}
var Point = {
    ReOrder: function () {
        var nIndex = 0;
        $("div[name='div_event']").each(function () {
            if (this.id != "div_event_" + nIndex) {
                var virtal_index = Number(this.id.substring(this.id.lastIndexOf("_") + 1));
                var divHtml = $(this).prop('outerHTML').replace(new RegExp(this.id, 'g'), "div_event_" + nIndex);
                divHtml = divHtml.replace(new RegExp(virtal_index + ",\"event", 'g'), nIndex + ",\"event");
                divHtml = divHtml.replace(new RegExp("div_attend_" + virtal_index, 'g'), "div_attend_" + nIndex);
                divHtml = divHtml.replace(new RegExp("事件" + (Number(virtal_index) + 1), 'g'), "事件" + (Number(nIndex) + 1));
                $(this).prop('outerHTML', divHtml);
            }
            nIndex++;
        });
    },
    Close: function (obj_id) {
        $("#" + obj_id).remove();
        Point.ReOrder();
    },
    BindMember: function (event_id, source_type) {
        GetCheckNodeList();
        $("#hid_User_Type").val(source_type);
        $("#hid_event_id").val(event_id);
        //GetExistUser(event_id);
        $("#div_Sel_User").modal('show');
    },
    BindMemberInfo: function (objSel, event_id) {
        var obj_attend = $("#div_attend_" + event_id);
        obj_attend.empty();
        objSel.each(function () {
            var user_id = this.id.substring(this.id.lastIndexOf('_') + 1);
            var obj_user = $("<div id='event_user_" + user_id + "' class = 'text-center' name = 'div_event_user'><span name='event_user_name'>" + $(this).text().trim() + "</span>： <span class='padding-left'>B分：</span><input type='text' class='mini-text form-control inline' name='B_Point' /><span class='padding-left'>A分：</span><input type='text' class='mini-text form-control inline' name='A_Point' /><span class='padding-left'>产值：</span><select class='mini-select form-control inline' name='Value_Type'><option value=0>创</option><option value=1>实</option><option value=2 selected>虚</option></select><input type='text' class='mini-text form-control inline' name='Value_Point' /><a href='#' class = 'padding-left' onclick = 'Point.RemoveUser(\"event_user_" + user_id + "\")'>删除</a></div>");
            obj_attend.append(obj_user);
        });
    },
    RemoveUser: function (obj_id) {
        $("#" + obj_id).remove();
    },
    AddEvent: function (event_lib) {
        var nIndex = $("div[name='div_event']").length;
        //生成事件选择下拉列表
        var select_html = "<select id='event_list_" + nIndex + "' class = 'form-control width_large' name='event_library'><option value='0'>---请选择---</option>";
        for (var i = 0; i < event_lib.length; i++) {
            select_html += "<option value = '" + event_lib[i].ID + "'>" + event_lib[i].b_Event_Name + "</option>";
        }
        select_html += "</select>";

        var div_Events = $("#div_Events");
        var div_Event = $("<div id='div_event_" + nIndex + "' class='form-group mini-layout fluid' name='div_event'><button type='button' class='close' onclick='Point.Close(\"div_event_" + nIndex + "\")'>&times;</button><div class='text-center font-16 bold'>事件" + (nIndex + 1) + "</div><hr /><div class='form-group'><span class='col-md-2 control-label'>事件</span><div class='col-md-10'>" + select_html + "</div></div><div class='form-group'><span class='col-md-2 control-label'>描述</span><div class='col-md-10'><input type = 'text' class='form-control width_large' name='event_note'></input></div></div><div class='form-group'><span class='col-md-2 control-label'>参与人</span><div class='col-md-10'><input class='btn btn-success' type='button' onclick='Point.BindMember(" + nIndex + ",\"event\")' value='添加参与人' />&nbsp;&nbsp;&nbsp;&nbsp;<input class='btn btn-info' type='button' onclick='' value='添加本人' /></div><div class = 'form-group padding-top' id='div_attend_" + nIndex + "' name='div_event_user_container'></div></div>");
        div_Events.append(div_Event);
    },
    Submit: function () {
        var list_event_model = new Array();
        var list_user_model = new Array();
        $("div[name='div_event']").each(function () {
            $(this).find("div[name='div_event_user']").each(function () {
                //var event_user = new b_User();
                //event_user.ID = this.id.substring(this.id.lastIndexOf('_') + 1);
                //event_user.b_RealName = $($(this).find("span[name='event_user_name']")[0]).text();
                var event_member = new b_Point_Event_Memver();
                event_member.b_A_Point = $($(this).find("input[name='A_Point']")[0]).val();
                event_member.b_B_Point = $($(this).find("input[name='B_Point']")[0]).val();
                event_member.b_Value_Type = $($(this).find("select[name='Value_Type']")[0]).val();
                event_member.b_Value_Point = $($(this).find("input[name='Value_Point']")[0]).val();
                event_member.b_User_ID = this.id.substring(this.id.lastIndexOf('_') + 1);;

                list_user_model.push(event_member);
            });

            var event = new b_Point_Event();
            //var event_lib = new b_Event_Library();
            //event_lib.ID = $($(this).find("select[name='event_library']")[0]).val();
            //event_lib.b_Event_Name = $($($(this).find("select[name='event_library']")[0]).find("option:selected")).text();
            event.b_Event_ID = $($(this).find("select[name='event_library']")[0]).val();
            event.b_Event_Note = $($(this).find("input[name='event_note']")[0]).val();
            event.b_Point_Event_Member = list_user_model;

            list_event_model.push(event);
        });

        var point = new b_Point();
        point.b_Subject = $("#b_Subject").val();
        point.b_Note = $("#b_Note").val();
        point.b_Event_Date = $("#b_Event_Date").val();
        point.b_First_Check_ID = $("#b_First_Check_ID").val().join(',');
        point.b_Final_Check_ID = $("#b_Final_Check_ID").val().join(',');
        point.b_Point_Event = list_event_model;

        $.ajax({
            url: "/b_Point/Save/",
            type: "post",
            contentType: "application/json;charset=utf-8",
            data: JSON.stringify(point),
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