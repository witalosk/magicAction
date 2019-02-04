using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloor : MonoBehaviour {

	[SerializeField] Vector2[] positions; //移動する地点の座標
	[SerializeField] float[] times; //移動する秒数
	[SerializeField] int offset; //開始位置
	[SerializeField] bool playOnAwake; //最初から実行するか
	[SerializeField] bool loop; //ループするか

	Transform myTrans;
	int state;

	float t;

	bool playerOnMe;

	[System.NonSerialized] public bool active;

	void Awake(){
		myTrans = transform;
		state = offset;
		myTrans.position = (Vector3)positions[state];
		active = playOnAwake;
		playerOnMe = false;
	}

	void Update () {
		if(active){
			t += Time.deltaTime / times[state];
			myTrans.position = (Vector3)Vector2.Lerp(positions[state], positions[(state + 1) % positions.Length], t); //移動処理
			if(t >= 1f) {
				t = 0;
				if(!loop & state == positions.Length - 1) //ループしない場合は一周したら停止
					active = false;
				state = (state + 1) % positions.Length; //次の地点へ
			}
		}
	}

	//衝突を検知した時に呼び出される関数
	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.tag == "Player"){ //衝突したのがPlayerだった場合
			Transform playerTrans = col.transform;
			RaycastHit2D hit = playerTrans.GetComponent<PlayerController>().GetCollisionUnderPlayer(); //Playerの足元にあるオブジェクトを取得
			if(hit && hit.transform == myTrans){ //Playerの足元に自分(動く床)があった場合
				playerTrans.parent = myTrans; //Playerを動く床の子オブジェクトにすることで、一緒に移動する
				playerOnMe = true;
			}
		}
	}

	//離れたのを検知した時に呼び出される関数
	void OnCollisionExit2D(Collision2D col) {
		if(col.gameObject.tag == "Player" && playerOnMe){ //離れたのがPlayerで、かつ今までPlayerが乗っていた場合
			Transform playerTrans = col.transform;
			playerTrans.parent = null;
			playerOnMe = false;
		}
	}
}
