using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadTrigger : MonoBehaviour {



	//Triggerに当たったら呼び出される関数
	//オブジェクトのLayerをCollisionOnlyPlayerにすることで、Playerのみ衝突検知される
	//各レイヤーの衝突対象は Edit > Project Settings > Physics2D > Layer Collision Matrix で設定されている
	 void OnTriggerEnter2D() {
		GameObject.Find("GameManager").GetComponent<GameManager>().PlayerDead();
	}
}
