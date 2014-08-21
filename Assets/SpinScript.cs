using UnityEngine;
using System.Collections;

public class SpinScript : MonoBehaviour {
	public float dx = 1.0f, dy = 1.0f, dz = 1.0f;
	// Use this for initialization
	void Start() {
	}

	// Update is called once per frame
	void Update() {
		transform.Rotate(new Vector3(dx,dy,dz));
	}
}
