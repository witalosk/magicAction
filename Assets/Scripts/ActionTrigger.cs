using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionTrigger : MonoBehaviour {

	[SerializeField] UnityEvent[] actions;

	public void ExecuteAction(){
		foreach (UnityEvent action in actions){
			Debug.Log("action");
			action.Invoke();
		}
	}
}
