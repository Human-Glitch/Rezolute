using UnityEngine;
using System.Collections;

public class BackgroundFollow : MonoBehaviour {
	public Transform Target;

	// Update is called once per frame
	void LateUpdate () {
		transform.position = new Vector3 (Target.position.x, 0, Target.position.z);
		//transform.localRotation = new Quaternion (Target.rotation.x, Target.rotation.y, Target.rotation.z , Target.rotation.w);
	}
}
