using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	Transform cameraTrans;
	[SerializeField] Transform playerTrans;

	[SerializeField] Vector3 cameraVec;
	// [SerializeField] Vector3 cameraRot;  //Vector3(45, 0, 0)

	[SerializeField] float speed;

	void Awake(){
		if(playerTrans == null) //StageBase以外のカメラは削除
			Destroy(gameObject);
		cameraTrans = transform;
		// cameraTrans.rotation = Quaternion.Euler(cameraRot);
	}

	void LateUpdate(){
		cameraTrans.position = Vector3.Lerp(cameraTrans.position, playerTrans.position + cameraVec, speed * Time.deltaTime);
	}

	public void ResetCamera(){
		cameraTrans.position = playerTrans.position + cameraVec;
	}

}
