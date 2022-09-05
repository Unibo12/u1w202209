using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // シーンの切り替えに必要

public class ChangeScene : MonoBehaviour
{
    public string sceneName; // 読み込むシーン名

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    // シーンを読み込む
    public void SceneLoad()
    {
        SceneManager.LoadScene (sceneName);
    }

    // シーンを読み込む
    public void PlayerLoad()
    {
        // イベントに登録
        SceneManager.sceneLoaded += GameSceneLoaded;
        
        SceneManager.LoadScene (sceneName);
    }

    private void GameSceneLoaded(Scene next, LoadSceneMode mode)
    {
        // シーン切り替え後のスクリプトを取得
        var nekketsuManager =
            GameObject
                .FindWithTag("NekketsuManager")
                .GetComponent<NekketsuManager>();

        // データを渡す処理
        nekketsuManager.playerCount = Settings.Instance.Game.playerCount;
        // nekketsuManager.playerCount = 2; // debug用 シーン切り替えしなくて良い用

        // イベントから削除
        SceneManager.sceneLoaded -= GameSceneLoaded;
    }
}
