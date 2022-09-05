﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// キーを押すと、アニメーションを切り換える
public class Anime : MonoBehaviour
{
    public OnKeyPress_MoveGravity onKeyPress_MoveGravity;

    public string upAnime = "";         // 上向き：Inspectorで指定
    public string downAnime = "";       // 下向き：Inspectorで指定
    public string rightAnime = "";      // 右向き：Inspectorで指定
    public string leftAnime = "";       // 左向き：Inspectorで指定
    public string StopAnime = "";       // 静止状態：Inspectorで指定
    public string HijiAnime = "";       // 肘打ち：Inspectorで指定
    public string DosukoiAnime = "";    // どすこい張り手：Inspectorで指定
    public string ThrowAnime = "";      // まいぼうるもどき投げ：Inspectorで指定
    public string JumpShagamiAnime = "";// ジャンプ時しゃがみ：Inspectorで指定
    public string JumpAnime = "";       // ジャンプ時：Inspectorで指定

    bool HijiFlg = true;                // 肘ボタン押しっぱなしで連打不可にしようとしたが未実装
    bool DosukoiFlg = true;             // どすこいも同様

    string nowMode = "";
    string oldMode = "";

    public AudioClip JumpSE;
    AudioSource audioSource;

    void Start()
    { // 最初に行う

        //Componentを取得
        audioSource = GetComponent<AudioSource>();

        nowMode = StopAnime;
        oldMode = "";
    }

    void Update()
    { // ずっと行う
        if (Input.GetKey("up") || Input.GetAxis("Vertical") < 0)
        { // 上キーなら
            nowMode = upAnime;
        }
        else if (Input.GetKey("down") || Input.GetAxis("Vertical") >  0)
        { // 下キーなら
            nowMode = downAnime;
        }
        else if (Input.GetKey("right") || Input.GetAxis("Horizontal") < 0)
        { // 右キーなら
            nowMode = rightAnime;
        }
        else if (Input.GetKey("left") || Input.GetAxis("Horizontal") > 0)
        { // 左キーなら
            nowMode = leftAnime;
        }
        else
        {
            nowMode = StopAnime;
        }

        // Xbox360コン B　どすこい張り手
        if ((Input.GetKey("z") || Input.GetKey("joystick button 0")) && DosukoiFlg)
        { 
            nowMode = HijiAnime;
            HijiFlg = false;
        }
        else
        {
            // どすこい押しっぱなしで連打不可にしようとしたが未完成
            if (DosukoiFlg == false)
            {
                DosukoiFlg = true;
            }
        }

        // Xbox360コン A　肘打ち
        if ((Input.GetKey("x") || Input.GetKey("joystick button 1")&& DosukoiFlg) && DosukoiFlg)
        {
            nowMode = DosukoiAnime;
            DosukoiFlg = false;
        }
        else
        {
            // 肘打ち押しっぱなしで連打不可にしようとしたが未完成
            if (HijiFlg == false)
            {
                HijiFlg = true;
            }
        }

        // Xbox360コン Y　まいぼうるもどき投げ
        if ((Input.GetKey("s") || Input.GetKey("joystick button 3")))
        {
            nowMode = ThrowAnime;
        }

        // Xbox360コン X ジャンプ　
        if ((Input.GetKey("a") || Input.GetKey("joystick button 2")))
        {
            nowMode = JumpAnime;
            //audioSource.PlayOneShot(JumpSE);
            //効果音おためし。 うるさいので保留
        }

        //if (!onKeyPress_MoveGravity.groundFlag)
        //{
        //    nowMode = JumpAnime;
        //}
        

    }
    void FixedUpdate()
    { // ずっと行う（一定時間ごとに）

        //if (oldMode == JumpShagamiAnime)
        //{
        //    oldMode = nowMode;
        //    // アニメを切り換える
        //    Animator animator = this.GetComponent<Animator>();
        //    animator.Play(JumpAnime);
        //}

      // もし違うキーが押されたら
        if (nowMode != oldMode)
        {
            oldMode = nowMode;
            // アニメを切り換える
            Animator animator = this.GetComponent<Animator>();
            animator.Play(nowMode);
        }
    }
}
