using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class StageManager : MonoBehaviour {

	[SerializeField] Transform cameraChild;

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	static void sceneCheck(){
		string stageName = SceneManager.GetActiveScene().name;
		if(stageName != "StageBase" && stageName.Contains("Stage") && SceneManager.sceneCount == 1){
			int stageNo = int.Parse(Regex.Replace (stageName, @"[^0-9]", ""));
			PlayerPrefs.SetInt("STAGE_NO", stageNo);
			SceneManager.LoadScene("StageBase");
		}
	}

	void Start(){
		cameraChild.parent = Camera.main.transform;
		GameObject.Find("GameManager").GetComponent<GameManager>().InitStage();
        FadeManager.FadeIn(1f, 0); //フェードインでシーン開始
	}
}
