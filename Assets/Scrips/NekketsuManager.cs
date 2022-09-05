using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 各オブジェクト(プレイヤー・アイテム)等が
/// それぞれを参照できるよう一元管理するクラス
/// </summary>
public class NekketsuManager : MonoBehaviour
{
    private GameObject playerObjct;

    public int playerCount;

    /// @@@PlayerXのように連番になるようなものはリストか配列で管理するほうが良いです
    /// リストは数が可変するもの、配列は変わらない予定のものという判断で大丈夫です
    /// 配列で管理するとインデックスでキャラを管理、判断できるので
    /// 後々楽になります
    public NekketsuAction[] Players;

    // public NekketsuHitBox[] HitBoxs;
    public List<NekketsuHitBox> hitBoxes;

    //アイテム
    private GameObject ItemObjct;

    public item Item1;

    //マップ上の障害物
    private GameObject MapObjct;

    public Object MapObjct1;

    //　★デバッグ用★
    public UmaiboSandbag sandbag;

    public DamageTest uni;

    public DamageTest uni2;

    //　★デバッグ用★
    public NekketsuAction NAct; //NekketsuActionが入る変数

    public UmaiboSandbag UmaSnd; //NekketsuActionが入る変数

    void Start()
    {
        Players = new NekketsuAction[Settings.Instance.Game.playerCount];
        for (int i = 0; i < Settings.Instance.Game.playerCount; ++i)
        {
            // 各オブジェクトの変数を参照できるようにする。
            playerObjct = GameObject.Find("Player" + i.ToString());
            Players[i] = playerObjct.GetComponent<NekketsuAction>();
        }
    }

    void Update()
    {
    }

    void GetMapObject()
    {
        MapObjct = GameObject.Find("table");
        MapObjct1 = MapObjct.GetComponent<Object>();
    }
}
