using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowWind : MonoBehaviour {

	[SerializeField] Vector2 windVec;

	[SerializeField] bool playOnAwake;

	bool active;

	void Awake(){
		active = playOnAwake;
	}

	public void ActivateWind(){
		active = !active;
	}

	void Blow(Rigidbody2D rb){
		rb.AddForce(windVec);
	}

	void OnTriggerStay2D(Collider2D other) {
		if(active) Blow(other.transform.GetComponent<Rigidbody2D>());
	}
}
