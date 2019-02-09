using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowWind : MonoBehaviour {

	[SerializeField] Vector2 windVec;

	[SerializeField] Transform[] blownTranses;
	bool[] isBlown;
	[SerializeField] bool playOnAwake;

	bool active;

	void Awake(){
		isBlown = new bool[blownTranses.Length];
		for(int i = 0; i < isBlown.Length; i++){
			isBlown[i] = false;
		}
		active = playOnAwake;
	}

	public void ActivateWind(){
		active = !active;
	}

	void FixedUpdate () {
		if(active){
			for(int i = 0; i < blownTranses.Length; i++){
				if(isBlown[i]){
					Blow(blownTranses[i].GetComponent<Rigidbody2D>());
				}
			}
		}
	}

	void Blow(Rigidbody2D rb){
		rb.AddForce(windVec);
	}

	void OnTriggerEnter2D(Collider2D col) {
		for(int i = 0; i < blownTranses.Length; i++){
			if(col.transform == blownTranses[i]){
				isBlown[i] = true;
			}
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		for(int i = 0; i < blownTranses.Length; i++){
			if(col.transform == blownTranses[i]){
				isBlown[i] = false;
			}
		}
	}
}
