using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col) {
		col.gameObject.GetComponent<PlayerController>().canInput = false;
		FadeManager.FadeOut(1f, 1, GameObject.Find("GameManager").GetComponent<GameManager>().LoadNextStage);
	}

}
