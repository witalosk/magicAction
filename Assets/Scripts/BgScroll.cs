using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgScroll : MonoBehaviour {

	Transform bgParentTrans;
	Transform cameraTrans;
	Transform playerTrans;

	[SerializeField] float speed;
	Vector3 prePos;


	Transform[] bgTranses;
	float spriteWidth;
	int centerSpriteIdx;

	void Awake(){
		bgParentTrans = GetComponentInChildren<Transform>();
		bgTranses = new Transform[3];
		for(int i = 0; i < 3; i++)
			bgTranses[i] = bgParentTrans.GetChild(i);
		spriteWidth = bgTranses[0].gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
		centerSpriteIdx = 1;
	}

	void Start(){
		playerTrans = GameObject.FindGameObjectWithTag ("Player").transform;
		prePos = playerTrans.position;
	}

	void LateUpdate(){
		bgParentTrans.position += Vector3.right * (playerTrans.position.x - prePos.x) * speed;
		// bgParentTrans.position += (playerTrans.position - prePos) * speed;
		// bgParentTrans.position -= Vector3.up * (playerTrans.position.y - prePos.y);
		prePos = playerTrans.position;

		CheckSeamless();
	}

	void CheckSeamless(){

		float diff = playerTrans.position.x - bgTranses[centerSpriteIdx].position.x;
		if(diff > spriteWidth / 2){
			bgTranses[(centerSpriteIdx + 2) % 3].position += Vector3.right * spriteWidth * 3;
			centerSpriteIdx = (centerSpriteIdx + 1) % 3;
		}else if(diff < -spriteWidth / 2){
			bgTranses[(centerSpriteIdx + 1) % 3].position -= Vector3.right * spriteWidth * 3;
			centerSpriteIdx = (centerSpriteIdx + 2) % 3;
		}
	}
}
