using System;
using UnityEngine;

[
    CreateAssetMenu(
        menuName = "Settings/AttackSettings",
        fileName = "AttackSettings")
]
public sealed class AttackSettings : ScriptableObject
{
    public float defaultAttackRangeZ = 0.1f; // デフォのZ軸攻撃判定(原作はすべて4)

    public Rect AttackNone = new Rect(0, 0, 0, 0);

    public Rect DosukoiSide = new Rect(0.1f, -0.05f, 0.1f, 0.1f);

    public Rect DosukoiBack = new Rect(0.1f, 0.2f, 0.2f, 0.5f);

    public Rect DosukoiFront = new Rect(0.05f, 0f, 0.2f, 0.2f);

    public Rect JumpDosukoi = new Rect(0.1f, -0.05f, 0.1f, 0.1f);

    public Rect Hiji = new Rect(0.1f, -0.05f, 0.1f, 0.1f);

    public Rect JumpKick = new Rect(0f, 0.2f, 0.2f, 0.1f);

    public float Damage1Time = 0.2f; // 非凹み状態の硬直時間

    public float Damage2Time = 0.8f; // 凹み状態の硬直時間

    public float DosukoiZ = 0.02f; // どすこい奥手前で飛ばされたときのZ軸距離
}
