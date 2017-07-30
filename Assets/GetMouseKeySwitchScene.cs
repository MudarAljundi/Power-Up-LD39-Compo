using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMouseKeySwitchScene : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0) == true) {
			GetComponent<SceneSwitcher>().SwitchScene("test1");
		}
	}
}
