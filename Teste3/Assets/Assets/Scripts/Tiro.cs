using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiro : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy (gameObject, 1);
	}

	// Update is called once per frame
	void Update () {

	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Finish") {
			Destroy (other.gameObject);
			Destroy (gameObject);
		}
	}
}