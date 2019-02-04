using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	PlayerController playerScript;

	float untilReloadTime = 0.4f;

	void Start(){
		playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		playerScript.canInput = true;
		Time.timeScale = 1f; //ゲーム内の時間の流れを通常にする
		FadeManager.FadeIn(1f, 0); //フェードインでシーン開始
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
