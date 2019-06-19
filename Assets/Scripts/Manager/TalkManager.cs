﻿using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class TalkManager
{
    private static TalkManager _Instance = null;
    public static TalkManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new TalkManager();
                _Instance.InitLoad();
            }
            return _Instance;
        }
    }

    private Dictionary<int, StoryData> StoryDataDic=new Dictionary<int, StoryData> ();

    public void ShowTalkPanel(int id,int index=0)
    {
        StoryData data = GetStoryDataById(id);
        data.index = index;
        TalkPanel.data = data;
        GUIManager.ShowView("TalkPanel");
    }

    public void ShowTalkPanel(StoryData data,int index=0)
    {
        data.index = index;
        TalkPanel.data = data;
        GUIManager.ShowView("TalkPanel");
    }

    void InitLoad()
    {
       LoadStoryXML("StoryConfig",StoryDataDic);
        InitHander();
    }
  
    public StoryData GetStoryDataById(int id)
    {
        StoryData data;
        if(!StoryDataDic.TryGetValue(id, out data))
        {
            Debug.LogError("id has error" + data.id + "      " + "TalkManager");
        }
        return data;
    }


    void LoadStoryXML(string pathName, Dictionary<int, StoryData> DataDic)
    {
        string path = "Config";

        string text = ResourcesManager.Instance.LoadConfig(path, pathName).text;
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(text);

        XmlNode node = xmlDoc.SelectSingleNode("Story");
        XmlNodeList nodeList = node.ChildNodes;

        foreach (XmlNode item in nodeList)
        {
            XmlNode id = item.SelectSingleNode("id");
            XmlNode index = item.SelectSingleNode("index");
            XmlNode state = item.SelectSingleNode("State");
            XmlNode name = item.SelectSingleNode("name");
            XmlNode cout = item.SelectSingleNode("cout");
            XmlNode speak = item.SelectSingleNode("Speak");
            XmlNode spriteName = item.SelectSingleNode("spriteName");

            StoryData data = new StoryData();
            data.id = CommonHelper.Str2Int(id.InnerText);
            data.state = CommonHelper.Str2Int(state.InnerText);
            data.name = name.InnerText;
            data.index = CommonHelper.Str2Int(index.InnerText);
            data.cout = CommonHelper.Str2Int(cout.InnerText);


            foreach (XmlNode pair in speak)
            {
                data.SpeakList.Add(pair.InnerText);
            }
            DataDic.Add(data.id, data);
        }
    }


    #region 第一章事件绑定
    void InitHander()
    {
        StoryData data = GetStoryDataById(0);
        data.StoryHanderDic.Add(0, KongWuGuaiTan0);
        data.StoryHanderDic.Add(1, KongWuGuaiTan1);
        data.StoryHanderDic.Add(2, KongWuGuaiTan_MengNanGuMiao0);
        data.StoryHanderDic.Add(4, KongWuGuaiTan_InToCangKu);
        data.StoryHanderDic.Add(5, KongWuGuaiTan_InToCangKu1);
        data.StoryHanderDic.Add(6, KongWuGuaiTan_InToCangKu2);
        data.StoryHanderDic.Add(7, KongWuGuaiTan_InToCangKu3);
        data.StoryHanderDic.Add(8, KongWuGuaiTan_InToCangKu4);

        StoryData data1 = GetStoryDataById(1);
        data1.StoryHanderDic.Add(0, CunMingLaiFang0);
        data1.StoryHanderDic.Add(1, CunMingLaiFang1);
        data1.StoryHanderDic.Add(2, CunMingLaiFang2);
        data1.StoryHanderDic.Add(4, CunMingLaiFang4);
    }


    #endregion


    #region 村民来访
    void CunMingLaiFang0()
    {
        ShowTalkPanel(1, 1);
    }

    void CunMingLaiFang1()
    {
        ShowTalkPanel(1, 2);
    }

    void CunMingLaiFang2()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(3, 26);
    }

    void CunMingLaiFang4()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(3, 28);
    }

    #endregion
    #region 空鹜怪谈
    void KongWuGuaiTan0()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 9);
    }

    void KongWuGuaiTan1()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 11);
    }
   

    void KongWuGuaiTan_MengNanGuMiao0()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 33);
    }
    void KongWuGuaiTan_InToCangKu() {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 37);
    }
    void KongWuGuaiTan_InToCangKu1() {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 49);
       
    }
    void KongWuGuaiTan_InToCangKu2() {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 52);
       // ChoseManager.Instance.ShowChosePanel(3);
    }
    void KongWuGuaiTan_InToCangKu3() {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 54);
        //ChoseManager.Instance.ShowChosePanel(4);
    }
    void KongWuGuaiTan_InToCangKu4() {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(0, 56);
    }
        #endregion
}
