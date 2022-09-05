﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 熱血風アクション
/// </summary>
public class NekketsuAction : MonoBehaviour
{
#region 変数定義
    Vector3 pos; //最終的な描画で使用

    public Animator animator; //アニメ変更用

    GameObject ThisGameObjct; //自身にアタッチされたゲームオブジェクト取得用

    GameObject NmngGameObjct; //熱血マネージャ取得用

    //変数クラス
    public NekketsuVariable NVariable; //座標・ステータス変数

    public NekketsuMoveVariable NMoveV; //移動関連変数

    public NekketsuJumpVariable NJumpV; //ジャンプ関連変数

    public NekketsuAttackVariable NAttackV; //攻撃関連変数

    //各処理を呼び出す為の定義
    public NekketsuManager Nmng; //熱血マネージャ

    private NekketsuSound NSound; //効果音

    // private NekketsuAttack NAttack; //攻撃処理　※現状アニメーション内で呼び出しているので不要
    private NekketsuMove NMove; //移動処理

    private NekketsuJump NJump; //ジャンプ処理

    private NekketsuInput NInput; //キー入力受付

    private NekketsuHurtBox NHurtBox; //喰らい判定処理

    private NekketsuStateChange NStateChange; //状態変化処理

    private NekketsuHaveItem NHaveItem; //所持アイテム処理

    private NekketsuScoreManger NScore; //スコア

    // private NekketsuMerikomiCheck NMerikomiCheck;   //壁・地面めり込みチェック
    //影の位置
    Transform shadeTransform;


#endregion


    void Start()
    {
        // 最初に行う
        Application.targetFrameRate = 60;
        pos = transform.position;
        animator = this.GetComponent<Animator>();

        //自分にアタッチされている効果音、変数クラスを取得
        ThisGameObjct = this.gameObject;
        NSound = ThisGameObjct.GetComponent<NekketsuSound>();
        NVariable = ThisGameObjct.GetComponent<NekketsuVariable>();
        NMoveV = ThisGameObjct.GetComponent<NekketsuMoveVariable>();
        NJumpV = ThisGameObjct.GetComponent<NekketsuJumpVariable>();
        NAttackV = ThisGameObjct.GetComponent<NekketsuAttackVariable>();

        //熱血マネージャ取得
        NmngGameObjct = GameObject.Find("NekketsuManager");
        Nmng = NmngGameObjct.GetComponent<NekketsuManager>();

        // 生成（コンストラクタ）の引数にNekketsuActionを渡してやる
        NMove = new NekketsuMove(this);
        NJump = new NekketsuJump(this);
        NInput = new NekketsuInput(this);
        NHurtBox = new NekketsuHurtBox(this);
        NStateChange = new NekketsuStateChange(this);
        NHaveItem = new NekketsuHaveItem(this);

        // NMerikomiCheck = new NekketsuMerikomiCheck(this);
        shadeTransform =
            GameObject.Find(this.gameObject.name + "_Shade").transform;

        NVariable.map = new float[0, 0, 0];
    }

    void Update()
    {
        // ずっと行う
        //NVariable.mapY= NVariable.map[(int)NVariable.X, (int)NVariable.Y, (int)NVariable.Z];
        // //★テスト　民家のテーブルの高さを固定で設定してみる★
        // //X座標はオブジェクトの中心部を指す為、中止部から全体幅÷2したものを加減算
        // if (Nmng.MapObjct1.TopBox.x - (Nmng.MapObjct1.myObjectWidth / 2) <= NVariable.X
        //     && NVariable.X <= Nmng.MapObjct1.TopBox.x + (Nmng.MapObjct1.myObjectWidth / 2)
        //     && Nmng.MapObjct1.TopBox.yMin <= NVariable.Z && NVariable.Z <= Nmng.MapObjct1.TopBox.yMax)
        // {
        //     NVariable.mapY = Nmng.MapObjct1.topBoxY;
        //     //if (NJumpV.squatFlag)
        //     //{
        //     //}
        // }
        // else
        // {
        //     NVariable.mapY = 0;
        // }
        NVariable.mapY = 0;

        NVariable.vx = 0;
        NVariable.vz = 0;

        // インプット処理呼び出し
        NInput.InputMain();

        // 攻撃の処理
        //NAttack.AttackMain();
        // 移動処理呼び出し
        NMove.MoveMain (NSound);

        // ジャンプ処理呼び出し
        NJump.JumpMain (NSound);

        // 攻撃喰らい判定
        NHurtBox.HurtBoxMain (NSound);

        // 所持アイテムの管理
        NHaveItem.NekketsuHaveItemMain();

        // 入力されたインプット内容でステータスを変更
        NStateChange.StateChangeMain();

        // // 壁・地面のめり込み状態をチェックし、必要であれば補正
        // NMerikomiCheck.MerikomiMain();

#region 画面への描画
        // 入力された内部XYZをtransformに設定する。
        //座標への速度反映
        NVariable.X += NVariable.vx;

        //Y += vy;
        NVariable.Z += NVariable.vz;

        pos.x = NVariable.X;
        pos.y = NVariable.Z + NVariable.Y;

        transform.position = pos;

        // 喰らい判定の移動
        if (
            NAttackV.NowDamage == DamagePattern.UmaTaore ||
            NAttackV.NowDamage == DamagePattern.UmaTaoreUp
        )
        {
            //倒れ状態の当たり判定(アイテム化)
            NAttackV.hurtBox =
                new Rect(NVariable.X,
                    NVariable.Y + NVariable.Z,
                    NAttackV.hurtBox.height,
                    NAttackV.hurtBox.width);
        }
        else
        {
            //通常当たり判定
            NAttackV.hurtBox =
                new Rect(NVariable.X,
                    NVariable.Y + NVariable.Z,
                    NAttackV.hurtBox.width,
                    NAttackV.hurtBox.height);
        }


#endregion



#region スプライト反転処理
        Vector3 scale = transform.localScale;
        if (NMoveV.leftFlag)
        {
            scale.x = -1; // 反転する（左向き）
        }
        else
        {
            scale.x = 1; // そのまま（右向き）
        }

        transform.localScale = scale;
#endregion



#region キャラクターの影の位置描画処理

        pos.y = NVariable.mapY + NVariable.Z - 0.25f;

        if (!NJumpV.squatFlag)
        {
            pos.y = NVariable.mapY + NVariable.Z - 0.25f;
        }
        else if (NJumpV.jumpFlag)
        {
            pos.y = NVariable.mapY + NVariable.Z;
        }

        shadeTransform.position = pos;


#endregion



#region アニメ処理
        if (NAttackV.NowDamage != DamagePattern.None)
        {
            // ダメージアニメ処理
            animator.Play(NAttackV.NowDamage.ToString());
        }
        else
        {
            if (!NJumpV.squatFlag && !NMoveV.brakeFlag)
            {
                if (NAttackV.NowAttack != AttackPattern.None)
                {
                    if (NAttackV.haveItem == ItemPattern.None)
                    {
                        // 現在の攻撃状態をアニメーションさせる。
                        animator.Play(NAttackV.NowAttack.ToString());
                    }
                    else
                    {
                    }
                }
                else
                {
                    //  攻撃以外のアニメーション
                    if (NVariable.vx == 0 && NVariable.vz == 0)
                    {
                        animator.SetBool("walk", false);
                    }
                    else
                    {
                        animator.SetBool("walk", true);
                    }

                    if (
                        NJumpV.jumpFlag &&
                        NAttackV.NowDamage == DamagePattern.None
                    )
                    {
                        animator.Play("Jump");
                    }
                }
            }
            else
            {
                if (NMoveV.brakeFlag)
                {
                    animator.Play("Brake");
                }

                if (NJumpV.squatFlag)
                {
                    animator.Play("Squat");
                }
            }
        }


#endregion



#region 失格判定(ゲームシーンから削除)

        if (NVariable.DeathFlag == DeathPattern.death)
        {
            Destroy(this.gameObject);
        }


#endregion
    }

    void OnDrawGizmos()
    {
        // 喰らい判定のギズモを表示
        Gizmos.color = Color.yellow;
        Gizmos
            .DrawWireCube(transform.position,
            new Vector3(NAttackV.hurtBox.width, NAttackV.hurtBox.height, 0));

        // 攻撃判定のギズモを表示
        Gizmos.color = Color.red;
        Gizmos
            .DrawWireCube(new Vector3(NAttackV.hitBox.x,
                NVariable.Z + NAttackV.hitBox.y),
            new Vector3(NAttackV.hitBox.width, NAttackV.hitBox.height, 0.1f));
    }
}
