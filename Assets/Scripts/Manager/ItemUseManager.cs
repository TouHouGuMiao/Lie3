﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUseManager {


    private List<ItemData> list = new List<ItemData>();
    private static ItemUseManager _Instance = null;
	public static ItemUseManager Instance {
        get {
            if (_Instance == null) {
                _Instance = new ItemUseManager();                
            }
            return _Instance;
        }
    }
     void ClickUse() {
        switch (BagPanel.ButtonType) {
            case 0:
                list = ItemDataManager.Instance.GetHasEquipList();
                break;
            case 1:
                list = ItemDataManager.Instance.GetHasMaterialList();
                break;
            case 2:
                list = ItemDataManager.Instance.GetHasItemsList();
                break;
        } 
       
        Debug.Log("当前使用的物体名称是"+list[BagPanel.index].name );
    }
    //后续的用其他物体的方法就是
    //在ClickUse方法里面写
    //得到物体的id，看是否匹配，在外部写物体单独的使用方法，添加进ClickUse里
    
    //void click_UseMed() {
    //    if () {
    //        Debug.Log();
    //    }
    //}
    
    public void addDelegate_Use(ItemUseData data) {
        //EquipList = ItemDataManager.Instance.GetHasEquipList();
        data.u_Hander += ClickUse;
    }
    
}