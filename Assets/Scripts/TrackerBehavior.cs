using UnityEngine;
using System.Collections;

public class TrackerBehavior : MonoBehaviour {

	public delegate void TrackerAction();
	public static event TrackerAction OnTrigger;
	public static event TrackerAction OnRelease;
	public GameObject Crosshair;
	public GameObject Revolver;
	public GameObject FingerJoint;
	public GameObject Ghost;
	public float Tension;
	public float Separation; // distance between two marker
	public bool Ready = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Revolver.transform.position;
		Quaternion revolverRot = Revolver.transform.rotation; 
		Quaternion crosshairRot = Crosshair.transform.rotation; 
		float PrevTension = Tension;
		Tension = Quaternion.Angle(revolverRot, crosshairRot);
		Separation = (Crosshair.transform.position - Revolver.transform.position).magnitude;

		// Trigger broadcasting
		if (Tension < 30 && Ready != true) {
			Debug.Log ("Released");
			OnRelease();
			Ready = true;
		}

		if (Tension > 30 /*&& Tension > PrevTension*/ && Ready == true) {
			OnTrigger();
			Debug.Log ("Triggered");
			Ready = false;
		}

		transform.rotation = revolverRot;

		FingerJoint.transform.localEulerAngles = new Vector3(0, 0, Tension);

	}
}
