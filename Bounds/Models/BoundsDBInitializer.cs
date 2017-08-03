using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bounds.Models
{
    public class BoundsDBInitializer : System.Data.Entity.DropCreateDatabaseAlways<BoundsContext>
    {
        protected override void Seed(BoundsContext context)
        {
            //context.b_Global_Config.Add(new b_Global_Config() { b_Enterprise_ID = 11000 });
            //var list_config = new List<b_Global_Config_Item> { new b_Global_Config_Item() { b_Global_Config_ID = 1, b_Item_Name = "5", b_Item_Value = "10", b_Item_Type = 1 },
            //    new b_Global_Config_Item() { b_Global_Config_ID = 1, b_Item_Name = "10", b_Item_Value = "20", b_Item_Type = 2 } };
            //list_config.ForEach(item => context.b_Global_Config_Item.Add(item));

            context.b_User.Add(new b_User() { b_UserName = "Admin", b_Password = "202CB962AC59075B964B07152D234B70", b_RealName = "管理员", b_Create_Time = DateTime.Now, b_Update_Time = DateTime.Now, b_Enterprise_ID = 11000 });
            context.b_Enterprise.Add(new b_Enterprise() { b_Enterprise_Code = "11000", b_Name = "测试企业" });
            context.b_Organize.Add(new b_Organize() { b_PID = 0, b_Name = "组织机构", b_Enterprise_Id = 11000 });

            var list_auth = new List<b_Auth>
            {
                new b_Auth() { b_Auth_Name="积分配置", b_Auth_Group_ID = 0 },//1
                new b_Auth() { b_Auth_Name="工龄分配置", b_Auth_Group_ID = 1 },//2
                new b_Auth() { b_Auth_Name="启动分配置", b_Auth_Group_ID = 1 },//3
                new b_Auth() { b_Auth_Name="组织机构", b_Auth_Group_ID = 0 },//4
                new b_Auth() { b_Auth_Name="人员启用", b_Auth_Group_ID = 4 },//5
                new b_Auth() { b_Auth_Name="人员批量新增", b_Auth_Group_ID = 4 },//6
                new b_Auth() { b_Auth_Name="人员导出", b_Auth_Group_ID = 4 },//7
                new b_Auth() { b_Auth_Name="人员移动", b_Auth_Group_ID = 4 },//8
                new b_Auth() { b_Auth_Name="人员禁用", b_Auth_Group_ID = 4 },//9
                new b_Auth() { b_Auth_Name="组织机构删除", b_Auth_Group_ID = 4 },//10
                new b_Auth() { b_Auth_Name="组织机构新增编辑", b_Auth_Group_ID = 4 },//11
                new b_Auth() { b_Auth_Name="设置审核人员", b_Auth_Group_ID = 4 },//12
                new b_Auth() { b_Auth_Name="添加编辑人员", b_Auth_Group_ID = 4 },//13

                new b_Auth() { b_Auth_Name="系统设置", b_Auth_Group_ID = 0 },//14
                new b_Auth() { b_Auth_Name="角色权限设置", b_Auth_Group_ID = 14 },//15
                new b_Auth() { b_Auth_Name="编辑人员", b_Auth_Group_ID = 15 },//16
                new b_Auth() { b_Auth_Name="添加人员", b_Auth_Group_ID = 15 },//17
                new b_Auth() { b_Auth_Name="新建角色", b_Auth_Group_ID = 15 },//18
                new b_Auth() { b_Auth_Name="删除角色", b_Auth_Group_ID = 15 },//19
                new b_Auth() { b_Auth_Name="编辑角色", b_Auth_Group_ID = 15 },//20

                new b_Auth() { b_Auth_Name="奖扣权限设置", b_Auth_Group_ID = 14 },//21
                new b_Auth() { b_Auth_Name="添加", b_Auth_Group_ID = 21 },//22
                new b_Auth() { b_Auth_Name="修改", b_Auth_Group_ID = 21 },//23
                new b_Auth() { b_Auth_Name="删除", b_Auth_Group_ID = 21 },//24
                new b_Auth() { b_Auth_Name="添加人员", b_Auth_Group_ID = 21 },//25
                new b_Auth() { b_Auth_Name="删除人员", b_Auth_Group_ID = 21 },//26

                new b_Auth() { b_Auth_Name="全局参数设置", b_Auth_Group_ID = 14 },//27

                new b_Auth() { b_Auth_Name="员工考勤配置", b_Auth_Group_ID = 14 },//28
                new b_Auth() { b_Auth_Name="添加", b_Auth_Group_ID = 28 },//29
                new b_Auth() { b_Auth_Name="编辑", b_Auth_Group_ID = 28 },//30
                new b_Auth() { b_Auth_Name="删除", b_Auth_Group_ID = 28 },//31
                new b_Auth() { b_Auth_Name="人员配置", b_Auth_Group_ID = 28 },//32

                new b_Auth() { b_Auth_Name="报表设置", b_Auth_Group_ID = 14 },//33
                new b_Auth() { b_Auth_Name="自定义分组", b_Auth_Group_ID = 33 },//34
                new b_Auth() { b_Auth_Name="新增", b_Auth_Group_ID = 34 },//35
                new b_Auth() { b_Auth_Name="编辑", b_Auth_Group_ID = 34 },//36
                new b_Auth() { b_Auth_Name="删除", b_Auth_Group_ID = 34 },//37
                new b_Auth() { b_Auth_Name="查看", b_Auth_Group_ID = 34 },//38
                new b_Auth() { b_Auth_Name="更新分组人员", b_Auth_Group_ID = 34 },//39
                new b_Auth() { b_Auth_Name="删除分组人员", b_Auth_Group_ID = 34 },//40

                new b_Auth() { b_Auth_Name="自定义报表", b_Auth_Group_ID = 33 },//41
                new b_Auth() { b_Auth_Name="新增", b_Auth_Group_ID = 41 },//42
                new b_Auth() { b_Auth_Name="编辑", b_Auth_Group_ID = 41 },//43
                new b_Auth() { b_Auth_Name="删除", b_Auth_Group_ID = 41 },//44
                new b_Auth() { b_Auth_Name="查看", b_Auth_Group_ID = 41 },//45
                new b_Auth() { b_Auth_Name="查询人员排名列表", b_Auth_Group_ID = 41 },//46

                new b_Auth() { b_Auth_Name="积分管理", b_Auth_Group_ID = 0 },//47
                new b_Auth() { b_Auth_Name="积分奖扣", b_Auth_Group_ID = 47 },//48
                new b_Auth() { b_Auth_Name="编辑", b_Auth_Group_ID = 48 },//49
                new b_Auth() { b_Auth_Name="新增", b_Auth_Group_ID = 48 },//50
                new b_Auth() { b_Auth_Name="删除", b_Auth_Group_ID = 48 },//51
                new b_Auth() { b_Auth_Name="发送", b_Auth_Group_ID = 48 },//52
                new b_Auth() { b_Auth_Name="撤回", b_Auth_Group_ID = 48 },//53
                new b_Auth() { b_Auth_Name="提交", b_Auth_Group_ID = 48 },//54

                new b_Auth() { b_Auth_Name="我的产值", b_Auth_Group_ID = 47 },//55
                new b_Auth() { b_Auth_Name="我的积分", b_Auth_Group_ID = 47 },//56
                new b_Auth() { b_Auth_Name="我的审核", b_Auth_Group_ID = 47 },//57
                new b_Auth() { b_Auth_Name="编辑", b_Auth_Group_ID = 57 },//58
                new b_Auth() { b_Auth_Name="撤回", b_Auth_Group_ID = 57 },//59
                new b_Auth() { b_Auth_Name="审核", b_Auth_Group_ID = 57 },//60
                new b_Auth() { b_Auth_Name="驳回", b_Auth_Group_ID = 57 },//61

                new b_Auth() { b_Auth_Name="个人明细查询", b_Auth_Group_ID = 47 },//62
                new b_Auth() { b_Auth_Name="我的排名", b_Auth_Group_ID = 62 },//63
                new b_Auth() { b_Auth_Name="奖扣任务类", b_Auth_Group_ID = 62 },//64
                new b_Auth() { b_Auth_Name="奖扣申请/下达", b_Auth_Group_ID = 62 },//65
                new b_Auth() { b_Auth_Name="固定积分类", b_Auth_Group_ID = 62 },//66

                new b_Auth() { b_Auth_Name="日常奖扣查询", b_Auth_Group_ID = 47 },//67
                new b_Auth() { b_Auth_Name="打印", b_Auth_Group_ID = 67 },//68

                new b_Auth() { b_Auth_Name="事件库管理", b_Auth_Group_ID = 47 },//69
                new b_Auth() { b_Auth_Name="事件类型重命名", b_Auth_Group_ID = 69 },//70
                new b_Auth() { b_Auth_Name="库转换", b_Auth_Group_ID = 69 },//71
                new b_Auth() { b_Auth_Name="新增", b_Auth_Group_ID = 69 },//72
                new b_Auth() { b_Auth_Name="导入事件", b_Auth_Group_ID = 69 },//73
                new b_Auth() { b_Auth_Name="删除事件类型", b_Auth_Group_ID = 69 },//74
                new b_Auth() { b_Auth_Name="移动事件类型", b_Auth_Group_ID = 69 },//75
                new b_Auth() { b_Auth_Name="新增事件类型", b_Auth_Group_ID = 69 },//76
                new b_Auth() { b_Auth_Name="事件类型排序", b_Auth_Group_ID = 69 },//77
                new b_Auth() { b_Auth_Name="移动事件", b_Auth_Group_ID = 69 },//78
                new b_Auth() { b_Auth_Name="编辑", b_Auth_Group_ID = 69 },//79
                new b_Auth() { b_Auth_Name="删除", b_Auth_Group_ID = 69 },//80

                new b_Auth() { b_Auth_Name="固定积分配置", b_Auth_Group_ID = 47 },//81
                new b_Auth() { b_Auth_Name="新增类型", b_Auth_Group_ID = 81 },//82
                new b_Auth() { b_Auth_Name="修改类型", b_Auth_Group_ID = 81 },//83
                new b_Auth() { b_Auth_Name="删除类型", b_Auth_Group_ID = 81 },//84
                new b_Auth() { b_Auth_Name="新增积分", b_Auth_Group_ID = 81 },//85
                new b_Auth() { b_Auth_Name="删除积分", b_Auth_Group_ID = 81 },//86
                new b_Auth() { b_Auth_Name="编辑积分", b_Auth_Group_ID = 81 },//87
                new b_Auth() { b_Auth_Name="更新人员", b_Auth_Group_ID = 81 },//88
                new b_Auth() { b_Auth_Name="批量添加", b_Auth_Group_ID = 81 },//89

                new b_Auth() { b_Auth_Name="奖票管理", b_Auth_Group_ID = 0 },//90
                new b_Auth() { b_Auth_Name="我的奖票", b_Auth_Group_ID = 90 },//91
                new b_Auth() { b_Auth_Name="奖票打印", b_Auth_Group_ID = 90 },//92
                new b_Auth() { b_Auth_Name="奖票设置", b_Auth_Group_ID = 92 },//93
                new b_Auth() { b_Auth_Name="打印", b_Auth_Group_ID = 92 },//94
                new b_Auth() { b_Auth_Name="删除奖票", b_Auth_Group_ID = 92 },//95
                new b_Auth() { b_Auth_Name="恢复奖票", b_Auth_Group_ID = 92 },//96
                new b_Auth() { b_Auth_Name="清理回收站", b_Auth_Group_ID = 92 },//97

                new b_Auth() { b_Auth_Name="奖扣任务", b_Auth_Group_ID = 0 },//98
                new b_Auth() { b_Auth_Name="奖扣任务配置", b_Auth_Group_ID = 98 },//99
                new b_Auth() { b_Auth_Name="新增", b_Auth_Group_ID = 99 },//100
                new b_Auth() { b_Auth_Name="编辑", b_Auth_Group_ID = 99 },//101
                new b_Auth() { b_Auth_Name="删除", b_Auth_Group_ID = 99 },//102
                new b_Auth() { b_Auth_Name="更新人员", b_Auth_Group_ID = 99 },//103
                new b_Auth() { b_Auth_Name="删除人员", b_Auth_Group_ID = 99 },//104

                new b_Auth() { b_Auth_Name="奖扣任务查看", b_Auth_Group_ID = 98 },//105
                new b_Auth() { b_Auth_Name="计算得分", b_Auth_Group_ID = 105 },//106
                new b_Auth() { b_Auth_Name="删除得分", b_Auth_Group_ID = 105 },//107

                new b_Auth() { b_Auth_Name="核查结算", b_Auth_Group_ID = 0 },//108
                new b_Auth() { b_Auth_Name="积分核查", b_Auth_Group_ID = 108 },//109
                new b_Auth() { b_Auth_Name="编辑", b_Auth_Group_ID = 109 },//110
                new b_Auth() { b_Auth_Name="作废", b_Auth_Group_ID = 109 },//111
                new b_Auth() { b_Auth_Name="恢复", b_Auth_Group_ID = 109 },//112
                new b_Auth() { b_Auth_Name="奖票转换", b_Auth_Group_ID = 109 },//113
                new b_Auth() { b_Auth_Name="批量导入", b_Auth_Group_ID = 109 },//114
                new b_Auth() { b_Auth_Name="驳回主题", b_Auth_Group_ID = 109 },//115

                new b_Auth() { b_Auth_Name="产值核查", b_Auth_Group_ID = 108 },//116
                new b_Auth() { b_Auth_Name="编辑", b_Auth_Group_ID = 116 },//117
                new b_Auth() { b_Auth_Name="通过或不通过", b_Auth_Group_ID = 116 },//118

                new b_Auth() { b_Auth_Name="考勤/固定积分计算", b_Auth_Group_ID = 108 },//119
                new b_Auth() { b_Auth_Name="保存/批量设置", b_Auth_Group_ID = 119 },//120
                new b_Auth() { b_Auth_Name="删除", b_Auth_Group_ID = 119 },//121

                new b_Auth() { b_Auth_Name="统计排名", b_Auth_Group_ID = 0 },//122
                new b_Auth() { b_Auth_Name="分组平均分报表排名", b_Auth_Group_ID = 122 },//123
                new b_Auth() { b_Auth_Name="平均分结算", b_Auth_Group_ID = 123 },//124

                new b_Auth() { b_Auth_Name="产值排名报表", b_Auth_Group_ID = 122 },//125
                new b_Auth() { b_Auth_Name="管理人员奖扣分报表", b_Auth_Group_ID = 122 },//126
                new b_Auth() { b_Auth_Name="结算", b_Auth_Group_ID = 126 }//127
            };
            list_auth.ForEach(auth => context.b_Auth.Add(auth));

            var list_Point_Type = new List<b_Point_Type_Dic>()
            {
                new b_Point_Type_Dic() { b_Point_Type_ID =1, b_Point_Type_Name="日常奖扣A分", Create_Time=DateTime.Now, Update_Time=DateTime.Now },
                new b_Point_Type_Dic() { b_Point_Type_ID =2, b_Point_Type_Name="日常奖扣B分", Create_Time=DateTime.Now, Update_Time=DateTime.Now },
                new b_Point_Type_Dic() { b_Point_Type_ID =3, b_Point_Type_Name="日常奖扣创富产值", Create_Time=DateTime.Now, Update_Time=DateTime.Now },
                new b_Point_Type_Dic() { b_Point_Type_ID =4, b_Point_Type_Name="日常奖扣实产值", Create_Time=DateTime.Now, Update_Time=DateTime.Now },
                new b_Point_Type_Dic() { b_Point_Type_ID =5, b_Point_Type_Name="日常奖扣虚产值", Create_Time=DateTime.Now, Update_Time=DateTime.Now },
                new b_Point_Type_Dic() { b_Point_Type_ID =6, b_Point_Type_Name="考勤转积分", Create_Time=DateTime.Now, Update_Time=DateTime.Now },
                new b_Point_Type_Dic() { b_Point_Type_ID =7, b_Point_Type_Name="营销转积分", Create_Time=DateTime.Now, Update_Time=DateTime.Now },
                new b_Point_Type_Dic() { b_Point_Type_ID =8, b_Point_Type_Name="A分转B分", Create_Time=DateTime.Now, Update_Time=DateTime.Now },
            };
            list_Point_Type.ForEach(type => context.b_Point_Type_Dic.Add(type));

            context.SaveChanges();
        }
    }
}