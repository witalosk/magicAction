using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    PlayerController playerScript;

    float untilReloadTime = 0.4f;


	// [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    void Awake(){
        int stageNo = PlayerPrefs.GetInt("STAGE_NO", 1);
		SceneManager.LoadScene("Stage" + stageNo, LoadSceneMode.Additive);
    }

    void Start(){
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        InitStage();
    }

    public void InitStage(){
        playerScript.canInput = true;
        Time.timeScale = 1f; //ゲーム内の時間の流れを通常にする
    }

    //Player死亡時に呼び出す関数
    public void PlayerDead(){
        Debug.Log("dead!");
        Time.timeScale = 0f; //ゲーム内の時間の流れを停止（これ以降はInvokeとか使えなくなるので注意、使いたい場合はコルーチンを利用する）
        FadeManager.FadeOut(untilReloadTime, 0, ReloadScene, true);
    }

    void ReloadScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //現在のシーンを再読み込み
    }
}

