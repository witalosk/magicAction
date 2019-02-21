using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBox : MonoBehaviour {

    GameObject gObjPlayer;

    [SerializeField] Vector2 forceVec;

	[SerializeField] bool playOnAwake;

	bool active;

	void Awake(){
		active = playOnAwake;
	}

    void Start(){
        gObjPlayer = GameObject.FindGameObjectWithTag("Player");
    }

	public void Take(){
        // プレイヤーの向きを取得
        float dir = gObjPlayer.transform.localScale.x;
        if (dir < 0.0f) forceVec.x = Mathf.Abs(forceVec.x) * -1.0f;
        else forceVec.x = Mathf.Abs(forceVec.x);

        // 力を加算
        gameObject.GetComponent<Rigidbody2D>().AddForce(forceVec);
    }

	public void OnTriggerStay2D(Collider2D other) {

	}
}
