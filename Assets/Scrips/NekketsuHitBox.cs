using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 攻撃判定を管理するクラス
/// </summary>
public class NekketsuHitBox : MonoBehaviour
{
    GameObject ThisGameObjct; //自身にアタッチされたゲームオブジェクト取得用

    NekketsuAction NAct; //NekketsuActionが入る変数

    NekketsuManager Nmng;

    NekketsuHitboxVariable NHitboxV;

    float power = 0; //威力

    int attackPlayerNumber = -1; //攻撃したひと

    float attackFrame = 0; //攻撃判定の長さ

    bool attackLeftFlag = false; //攻撃の向き

    Rect HitBox = new Rect(0, 0, 0, 0); //攻撃当たり判定

    AttackPattern attackPattern = AttackPattern.None; //相手の攻撃パターン

    bool isAttackHit = false; // 攻撃がヒット済みか

    void Start()
    {
        ThisGameObjct = this.gameObject;
        NHitboxV = ThisGameObjct.GetComponent<NekketsuHitboxVariable>();
    }

    public NekketsuHitBox(
        float power,
        int attackPlayerNumber,
        float attackFrame,
        bool attackLeftFlag,
        Rect HitBox,
        AttackPattern otherPlayerAttack,
        bool otherPlayeAttackHit
    )
    {
        // NAct = nekketsuAction;
    }

    public void add()
    {
        Nmng
            .hitBoxes
            .Add(new NekketsuHitBox(power,
                attackPlayerNumber,
                attackFrame,
                attackLeftFlag,
                HitBox,
                attackPattern,
                isAttackHit));
    }
}
