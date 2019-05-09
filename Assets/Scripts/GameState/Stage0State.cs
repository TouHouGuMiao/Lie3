﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage0State : GameState
{
    protected override void OnLoadComplete(params object[] args)
    {
        GUIManager.ShowView("BattleUIPanel");
        GameZaXiangManager.Instance.ShowCover();
        CGManager.instance.ShowBlackCover();


        //GUIManager.HideView("GameHelpPanel");

    }

    private void StartEvent()
    {
        StoryEventManager.Instance.ShowEventPanel_ChapterOne(2,0);
        GUIManager.HideView("CoverPanel");   
    }


    protected override void OnStart()
    {
        AudioManager.Instance.PlayEffect_Source("openDoor",StartEvent);
        
        //StoryManager.Instacne.ShowStoryList(StoryManager.Instacne.GetStage0State0List());
    }

    protected override void OnStop()
    {
      
    }

}
