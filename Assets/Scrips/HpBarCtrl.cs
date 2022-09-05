﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarCtrl : MonoBehaviour
{
    GameObject PlayerObjct;

    NekketsuManager Nmng;

    Slider Slider;

    void Start()
    {
        PlayerObjct = GameObject.Find("NekketsuManager");
        Nmng = PlayerObjct.GetComponent<NekketsuManager>();

        // スライダーを取得する
        Slider = GameObject.Find("Slider").GetComponent<Slider>();

        Slider.maxValue = Nmng.Players[0].NVariable.st_life;
    }

    void Update()
    {
        // HPゲージに値を設定
        Slider.value = Nmng.Players[0].NVariable.st_life;
    }
}
