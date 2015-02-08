using UnityEngine;
using System.Collections;

public class MainBehavior : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Update GUI
	void OnGUI () {
		GUI.Label(new Rect(10, 10, 100, 100), "FPS:"+1/Time.deltaTime);
	}
}
