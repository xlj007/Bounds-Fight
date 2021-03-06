﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bounds
{
    public enum b_Status
    {
        待初审 = 0,
        初审驳回,
        待终审,
        终审驳回,
        审核通过,
        撤回
    }

    public enum b_Value_Type
    {
        创富产值,
        实产值,
        虚产值
    }

    public enum b_Fix_Point_Type
    {
        技能,
        职务,
        学历,
        职称,
        特长,
        荣誉
    }

    public enum b_Task_Cycle
    {
        日任务,
        周任务,
        月任务
    }

    public enum b_Task_Type
    {
        奖分任务,
        罚分任务,
        人次任务,
        罚分比例任务
    }
}