﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestory : MonoBehaviour
{


    private void Awake()
    {
        Destroy(gameObject, 1.2f);
    }


}
