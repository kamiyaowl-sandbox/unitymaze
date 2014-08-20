using UnityEngine;
using System.Collections;

public class ActionPointDetect : MonoBehaviour {

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}
	void OnTriggerEnter(Collider col) {
	}
	void OnTriggerStay(Collider col) {
		switch (col.gameObject.tag) {
			case "Start":
				Debug.Log("Start");
				break;
			case "Goal":
				Debug.Log("Goal");
				break;
		}
	}
	void OnTriggernExit(Collider col) {
	}

}
