using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	Transform cameraTrans;
	[SerializeField] Transform playerTrans;

	[SerializeField] Vector3 cameraVec; //Playerとの一定距離

	[SerializeField] float speed; //ぬるっと動く度合い

	void Awake(){
		if(playerTrans == null) //StageBase以外のカメラは削除
			Destroy(gameObject);
		cameraTrans = transform;
	}

	//毎Update()の直後に呼び出される関数
	void LateUpdate(){
		//Playerの座標に合わせてカメラを移動
		cameraTrans.position = Vector3.Lerp(cameraTrans.position, playerTrans.position + cameraVec, speed * Time.deltaTime);
	}

	public void ResetCamera(){
		cameraTrans.position = playerTrans.position + cameraVec;
	}

}
