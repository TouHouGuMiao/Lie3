﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpeakData
{
    public delegate void NPCSpeakHander();
    /// <summary>
    /// 玩家确定输入后触发的委托
    /// </summary>
    public Dictionary<int, NPCSpeakHander> OnEnterDownDic = new Dictionary<int, NPCSpeakHander>();

    public int Id
    {
        get;
        set;
    }


    public int SpeakCount
    {
        get;
        set;
    }


    public List<string> MainList = new List<string>();

    public StoryData storyData = new StoryData();


}
