using System;
using System.Collections;
using System.Collections.Generic;
using IEnumerableExtensions;
using UnityEngine;

// ずっと、カメラが追いかける（水平に）
public class Forever_ChaseCameraH : MonoBehaviour
{
    Vector3 base_pos;

    Vector3 pos;

    GameObject playerObjct;

    NekketsuManager Nmng;

    float TopPlayerX;

    void Start()
    {
        // 最初に行う
        // カメラの元の位置を覚えておく
        base_pos = Camera.main.gameObject.transform.position;

        playerObjct = GameObject.Find("NekketsuManager");
        Nmng = playerObjct.GetComponent<NekketsuManager>();
    }

    void LateUpdate()
    {
        // ずっと行う（いろいろな処理の最後に）
        //何プレイヤーが先頭を走っているか取得
        int topPlayerNum = getTopPlayerNumber(Nmng);

        //カメラは先頭を走るプレイヤーを追いかける
        TopPlayerX = Nmng.Players[topPlayerNum].NVariable.X;
        pos.x =
            Nmng.Players[topPlayerNum].NVariable.X +
            Settings.Instance.Game.TopPlayerCameraX;

        pos.z = -10; // カメラ位置なのでプレイヤーよりやや手前に移動させる
        pos.y = base_pos.y; // カメラの元の高さを使う
        Camera.main.gameObject.transform.position = pos;
    }

    /// <summary>
    /// 先頭を走るプレイヤーのプレイヤー番号(1～4)を返す
    /// </summary>
    /// <param name="Nmng"></param>
    /// <returns>TopPlayerNum</returns>
    int getTopPlayerNumber(NekketsuManager Nmng)
    {
        float[] playersX;
        int TopPlayerNum = 0;

        playersX = new float[Settings.Instance.Game.playerCount];

        for (int i = 0; i < Settings.Instance.Game.playerCount; ++i)
        {
            playersX[i] = Nmng.Players[i].NVariable.X;
        }

        int top = maxIndex(playersX);
        if (Nmng.Players[top].NVariable.DeathFlag != DeathPattern.death)
        {
            TopPlayerNum = top;
        }

        return TopPlayerNum;
    }

    int maxIndex(float[] player)
    {
        int topIndex = 0;
        float topX = player[0];
        for (
            int
                i = 0,
                l = player.Length;
            i < l;
            i++
        )
        {
            if (topX < player[i])
            {
                topX = player[i];
                topIndex = i;
            }
        }
        return topIndex;
    }
}
