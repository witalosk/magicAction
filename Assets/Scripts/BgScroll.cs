using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgManager : MonoBehaviour {

	Transform bgTrans;
	Transform cameraTrans;
	Transform playerTrans;

	[SerializeField] float speed;
	Vector3 prePos;

	void Awake(){
		bgTrans = transform;
		prePos = Vector3.zero;
	}

	void Start(){
		if(speed == 0)
			cameraTrans = Camera.main.transform;
		else
			playerTrans = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	void LateUpdate(){
		if (speed == 0) {
			bgTrans.position = cameraTrans.position + Vector3.forward * 20;
		}else{
			bgTrans.position += (playerTrans.position - prePos) * speed;
			prePos = playerTrans.position;
		}
	}
}
