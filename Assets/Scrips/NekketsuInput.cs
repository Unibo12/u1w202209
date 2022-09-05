using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// コントローラ(キーボード)の入力を管理するクラス
/// </summary>
public class NekketsuInput
{
    // GameObject GObj; //ゲームオブジェクトそのものが入る変数
    NekketsuAction NAct; //NekketsuActionが入る変数

    public NekketsuInput(NekketsuAction nekketsuAction)
    {
        NAct = nekketsuAction;
    }

    /// <summary>
    /// キー入力の状態を常に監視し、入力状態を切り替える。
    /// </summary>
    public void InputMain()
    {
        // ゲームパッドの状態の取得
        UnityEngine.InputSystem.Gamepad gamepad = Gamepad.current;

        // キーボードの状態の取得
        UnityEngine.InputSystem.Keyboard keyboard = Keyboard.current;

        if (gamepad != null)
        {
            //ゲームパッドの入力受付
            GamePadInput (gamepad);
        }

        if (keyboard != null)
        {
            //キーボードの入力受付
            KeyboardInput (keyboard);
        }
    }

    /// <summary>
    /// ゲームパッドの入力受付
    /// </summary>
    private void GamePadInput(UnityEngine.InputSystem.Gamepad gamepad)
    {
        if (
            gamepad.dpad.right.IsPressed() ||
            gamepad.leftStick.right.IsPressed()
        )
        {
            if (
                NAct.NMoveV.XInputState == XInputState.XNone ||
                NAct.NMoveV.XInputState == XInputState.XLeftPushMoment ||
                NAct.NMoveV.XInputState == XInputState.XLeftPushButton ||
                NAct.NMoveV.XInputState == XInputState.XLeftReleaseButton
            )
            {
                NAct.NMoveV.XInputState = XInputState.XRightPushMoment;
            }
            else if (NAct.NMoveV.XInputState == XInputState.XRightPushMoment)
            {
                NAct.NMoveV.XInputState = XInputState.XRightPushButton;
            }
        }
        else if (
            gamepad.dpad.left.IsPressed() || gamepad.leftStick.left.IsPressed()
        )
        {
            if (
                NAct.NMoveV.XInputState == XInputState.XNone ||
                NAct.NMoveV.XInputState == XInputState.XRightPushMoment ||
                NAct.NMoveV.XInputState == XInputState.XRightPushButton ||
                NAct.NMoveV.XInputState == XInputState.XRightReleaseButton
            )
            {
                NAct.NMoveV.XInputState = XInputState.XLeftPushMoment;
            }
            else if (NAct.NMoveV.XInputState == XInputState.XLeftPushMoment)
            {
                NAct.NMoveV.XInputState = XInputState.XLeftPushButton;
            }
        }
        else
        {
            if (NAct.NMoveV.XInputState == XInputState.XRightPushButton)
            {
                NAct.NMoveV.XInputState = XInputState.XRightReleaseButton;
            }
            else if (NAct.NMoveV.XInputState == XInputState.XLeftPushButton)
            {
                NAct.NMoveV.XInputState = XInputState.XLeftReleaseButton;
            }
            else if (
                NAct.NMoveV.XInputState == XInputState.XRightReleaseButton ||
                NAct.NMoveV.XInputState == XInputState.XLeftReleaseButton
            )
            {
                NAct.NMoveV.XInputState = XInputState.XNone;
            }
        }

        if (gamepad.dpad.up.IsPressed() || gamepad.leftStick.up.IsPressed())
        {
            if (
                NAct.NMoveV.ZInputState == ZInputState.ZNone ||
                NAct.NMoveV.ZInputState == ZInputState.ZFrontPushMoment ||
                NAct.NMoveV.ZInputState == ZInputState.ZFrontPushButton ||
                NAct.NMoveV.ZInputState == ZInputState.ZFrontReleaseButton
            )
            {
                NAct.NMoveV.ZInputState = ZInputState.ZBackPushMoment;
            }
            else if (NAct.NMoveV.ZInputState == ZInputState.ZBackPushMoment)
            {
                NAct.NMoveV.ZInputState = ZInputState.ZBackPushButton;
            }
        }
        else if (
            gamepad.dpad.down.IsPressed() || gamepad.leftStick.down.IsPressed()
        )
        {
            if (
                NAct.NMoveV.ZInputState == ZInputState.ZNone ||
                NAct.NMoveV.ZInputState == ZInputState.ZBackPushMoment ||
                NAct.NMoveV.ZInputState == ZInputState.ZBackPushButton ||
                NAct.NMoveV.ZInputState == ZInputState.ZBackReleaseButton
            )
            {
                NAct.NMoveV.ZInputState = ZInputState.ZFrontPushMoment;
            }
            else if (NAct.NMoveV.ZInputState == ZInputState.ZFrontPushMoment)
            {
                NAct.NMoveV.ZInputState = ZInputState.ZFrontPushButton;
            }
        }
        else
        {
            if (NAct.NMoveV.ZInputState == ZInputState.ZBackPushButton)
            {
                NAct.NMoveV.ZInputState = ZInputState.ZBackReleaseButton;
            }
            else if (NAct.NMoveV.ZInputState == ZInputState.ZFrontPushButton)
            {
                NAct.NMoveV.ZInputState = ZInputState.ZFrontReleaseButton;
            }
            else if (
                NAct.NMoveV.ZInputState == ZInputState.ZBackReleaseButton ||
                NAct.NMoveV.ZInputState == ZInputState.ZFrontReleaseButton
            )
            {
                NAct.NMoveV.ZInputState = ZInputState.ZNone;
            }
        }

        // ジャンプステータス判定
        //★TODO:AB同時押しジャンプは別で処理いれること
        //★例：既にA押した状態でB→ジャンプなど...
        if (
            gamepad.xButton.wasPressedThisFrame ||
            gamepad.aButton.wasPressedThisFrame &&
            gamepad.bButton.wasPressedThisFrame
        )
        {
            NAct.NJumpV.JumpButtonState = JumpButtonPushState.PushMoment;
        }
        else if (
            gamepad.xButton.isPressed ||
            gamepad.aButton.isPressed && gamepad.bButton.isPressed
        )
        {
            NAct.NJumpV.JumpButtonState = JumpButtonPushState.PushButton;
        }
        else if (
            gamepad.xButton.wasReleasedThisFrame ||
            gamepad.aButton.wasReleasedThisFrame &&
            gamepad.bButton.wasReleasedThisFrame
        )
        {
            NAct.NJumpV.JumpButtonState = JumpButtonPushState.ReleaseButton;
        }

        // 攻撃処理
        NAct.NAttackV.AttackMomentFlag = false;

        if ((gamepad.aButton.wasPressedThisFrame))
        {
            if (NAct.NMoveV.leftFlag)
            {
                DosukoiVector();
            }
            else
            {
                if (NAct.NJumpV.jumpFlag && NAct.NVariable.Y >= 0)
                {
                    if (!NAct.NMoveV.leftFlag)
                    {
                        NAct.NAttackV.NowAttack = AttackPattern.JumpKick;
                        NAct.NAttackV.AttackMomentFlag = true;
                    }
                }
                else
                {
                    if (
                        NAct.NMoveV.XInputState == XInputState.XNone &&
                        NAct.NMoveV.ZInputState == ZInputState.ZNone
                    )
                    {
                        NAct.NAttackV.NowAttack = AttackPattern.Hiji;
                        NAct.NAttackV.AttackMomentFlag = true;
                    }
                    else
                    {
                        NAct.NAttackV.NowAttack = AttackPattern.HijiWalk;
                        NAct.NAttackV.AttackMomentFlag = true;
                    }
                }
            }
        }
        else if (((gamepad.bButton.wasPressedThisFrame)))
        {
            if (NAct.NMoveV.leftFlag)
            {
                if (NAct.NJumpV.jumpFlag && NAct.NVariable.Y >= 0)
                {
                    if (NAct.NMoveV.leftFlag)
                    {
                        NAct.NAttackV.NowAttack = AttackPattern.JumpKick;
                        NAct.NAttackV.AttackMomentFlag = true;
                    }
                }
                else
                {
                    if (
                        NAct.NMoveV.XInputState == XInputState.XNone &&
                        NAct.NMoveV.ZInputState == ZInputState.ZNone
                    )
                    {
                        NAct.NAttackV.NowAttack = AttackPattern.Hiji;
                        NAct.NAttackV.AttackMomentFlag = true;
                    }
                    else
                    {
                        NAct.NAttackV.NowAttack = AttackPattern.HijiWalk;
                        NAct.NAttackV.AttackMomentFlag = true;
                    }
                }
            }
            else
            {
                DosukoiVector();
                NAct.NAttackV.AttackMomentFlag = true;
            }
        }
        else
        {
            //NAct.NowAttack = AttackPattern.None;
        }
    }

    /// <summary>
    /// キーボードの方向キー入力受付
    /// </summary>
    private void KeyboardInput(UnityEngine.InputSystem.Keyboard keyboard)
    {
        if (keyboard.rightArrowKey.wasPressedThisFrame)
        {
            NAct.NMoveV.XInputState = XInputState.XRightPushMoment;
        }
        else if (keyboard.rightArrowKey.isPressed)
        {
            NAct.NMoveV.XInputState = XInputState.XRightPushButton;
        }
        else if (keyboard.rightArrowKey.wasReleasedThisFrame)
        {
            NAct.NMoveV.XInputState = XInputState.XRightReleaseButton;
        }

        if (keyboard.leftArrowKey.wasPressedThisFrame)
        {
            NAct.NMoveV.XInputState = XInputState.XLeftPushMoment;
        }
        else if (keyboard.leftArrowKey.isPressed)
        {
            NAct.NMoveV.XInputState = XInputState.XLeftPushButton;
        }
        else if (keyboard.leftArrowKey.wasReleasedThisFrame)
        {
            NAct.NMoveV.XInputState = XInputState.XLeftReleaseButton;
        }

        if (keyboard.upArrowKey.wasPressedThisFrame)
        {
            NAct.NMoveV.ZInputState = ZInputState.ZBackPushMoment;
        }
        else if (keyboard.upArrowKey.isPressed)
        {
            NAct.NMoveV.ZInputState = ZInputState.ZBackPushButton;
        }
        else if (keyboard.upArrowKey.wasReleasedThisFrame)
        {
            NAct.NMoveV.ZInputState = ZInputState.ZBackReleaseButton;
        }

        if (keyboard.downArrowKey.wasPressedThisFrame)
        {
            NAct.NMoveV.ZInputState = ZInputState.ZFrontPushMoment;
        }
        else if (keyboard.downArrowKey.isPressed)
        {
            NAct.NMoveV.ZInputState = ZInputState.ZFrontPushButton;
        }
        else if (keyboard.downArrowKey.wasReleasedThisFrame)
        {
            NAct.NMoveV.ZInputState = ZInputState.ZFrontReleaseButton;
        }

        // ジャンプステータス判定
        //★TODO:AB同時押しジャンプは別で処理いれること
        //★例：既にA押した状態でB→ジャンプなど...
        if (
            keyboard.aKey.wasPressedThisFrame ||
            keyboard.zKey.wasPressedThisFrame &&
            keyboard.xKey.wasPressedThisFrame
        )
        {
            NAct.NJumpV.JumpButtonState = JumpButtonPushState.PushMoment;
        }
        else if (
            keyboard.aKey.isPressed ||
            keyboard.zKey.isPressed && keyboard.xKey.isPressed
        )
        {
            NAct.NJumpV.JumpButtonState = JumpButtonPushState.PushButton;
        }
        else if (
            keyboard.aKey.wasReleasedThisFrame ||
            keyboard.zKey.wasReleasedThisFrame &&
            keyboard.xKey.wasReleasedThisFrame
        )
        {
            NAct.NJumpV.JumpButtonState = JumpButtonPushState.ReleaseButton;
        }

        // 攻撃処理
        NAct.NAttackV.AttackMomentFlag = false;

        if ((keyboard.zKey.wasPressedThisFrame))
        {
            if (NAct.NMoveV.leftFlag)
            {
                DosukoiVector();
            }
            else
            {
                if (NAct.NJumpV.jumpFlag && NAct.NVariable.Y >= 0)
                {
                    if (!NAct.NMoveV.leftFlag)
                    {
                        NAct.NAttackV.NowAttack = AttackPattern.JumpKick;
                        NAct.NAttackV.AttackMomentFlag = true;
                    }
                }
                else
                {
                    if (
                        NAct.NMoveV.XInputState == XInputState.XNone &&
                        NAct.NMoveV.ZInputState == ZInputState.ZNone
                    )
                    {
                        NAct.NAttackV.NowAttack = AttackPattern.Hiji;
                        NAct.NAttackV.AttackMomentFlag = true;
                    }
                    else
                    {
                        NAct.NAttackV.NowAttack = AttackPattern.HijiWalk;
                        NAct.NAttackV.AttackMomentFlag = true;
                    }
                }
            }
        }
        else if (((keyboard.xKey.wasPressedThisFrame)))
        {
            if (NAct.NMoveV.leftFlag)
            {
                if (NAct.NJumpV.jumpFlag && NAct.NVariable.Y >= 0)
                {
                    if (NAct.NMoveV.leftFlag)
                    {
                        NAct.NAttackV.NowAttack = AttackPattern.JumpKick;
                        NAct.NAttackV.AttackMomentFlag = true;
                    }
                }
                else
                {
                    if (
                        NAct.NMoveV.XInputState == XInputState.XNone &&
                        NAct.NMoveV.ZInputState == ZInputState.ZNone
                    )
                    {
                        NAct.NAttackV.NowAttack = AttackPattern.Hiji;
                        NAct.NAttackV.AttackMomentFlag = true;
                    }
                    else
                    {
                        NAct.NAttackV.NowAttack = AttackPattern.HijiWalk;
                        NAct.NAttackV.AttackMomentFlag = true;
                    }
                }
            }
            else
            {
                DosukoiVector();
                NAct.NAttackV.AttackMomentFlag = true;
            }
        }
        else
        {
            //NAct.NowAttack = AttackPattern.None;
        }
    }

    /// <summary>
    ///　キー入力より、どすこい張り手の奥・手前・横の状態を判断する。
    /// </summary>
    void DosukoiVector()
    {
        if (NAct.NJumpV.jumpFlag)
        {
            NAct.NAttackV.NowAttack = AttackPattern.UmaHariteJump;
            NAct.NAttackV.AttackMomentFlag = true;
        }
        else if (
            (
            NAct.NMoveV.ZInputState == ZInputState.ZBackPushMoment ||
            NAct.NMoveV.ZInputState == ZInputState.ZBackPushButton
            ) &&
            !NAct.NJumpV.jumpFlag
        )
        {
            NAct.NAttackV.NowAttack = AttackPattern.DosukoiBack;
        }
        else if (
            (
            NAct.NMoveV.ZInputState == ZInputState.ZFrontPushMoment ||
            NAct.NMoveV.ZInputState == ZInputState.ZFrontPushButton
            ) &&
            !NAct.NJumpV.jumpFlag
        )
        {
            NAct.NAttackV.NowAttack = AttackPattern.DosukoiFront;
        }
        else
        {
            if (
                NAct.NMoveV.XInputState == XInputState.XLeftPushButton ||
                NAct.NMoveV.XInputState == XInputState.XLeftPushMoment ||
                NAct.NMoveV.XInputState == XInputState.XRightPushButton ||
                NAct.NMoveV.XInputState == XInputState.XRightPushMoment
            )
            {
                NAct.NAttackV.NowAttack = AttackPattern.DosukoiWalk;
            }
            else
            {
                NAct.NAttackV.NowAttack = AttackPattern.Dosukoi;
            }
        }
    }
}
