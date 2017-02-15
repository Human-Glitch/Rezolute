using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class destroyCoin : MonoBehaviour {
	public takeCoin counter; //needs object in order access the script so this is workaround

	void OnTriggerEnter2D (Collider2D other){

		if (other.gameObject.tag == "Player") {
			counter.IncrementCounter ();//needs script, but cant call with only public functions

			Destroy (gameObject);
		}
	}
}
