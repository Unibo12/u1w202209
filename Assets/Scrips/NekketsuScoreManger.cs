using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NekketsuScoreManger : MonoBehaviour
{
    public NekketsuManager NMng; //NekketsuManagerが入る変数

    public GameObject score_object = null; // Textオブジェクト

    public int score_num = 0; // スコア変数

    private TextMeshProUGUI score_text;

    public NekketsuScoreManger(NekketsuManager nekketsuManager)
    {
        NMng = nekketsuManager;
    }

    // 初期化
    void Start()
    {
    }

    // 更新
    void Update()
    {
        // オブジェクトからTextコンポーネントを取得
        score_text = score_object.GetComponent<TextMeshProUGUI>();

        // // テキストの表示を入れ替える
        // score_text.text =
        //     NMng.Players[0].NVariable.st_life.ToString() +
        //     '|' +
        //     NMng.Players[1].NVariable.st_life.ToString();
    }
}
