using UnityEngine;
using System.Collections;

public class TriggerBehavior : MonoBehaviour {

	public delegate void TriggerAction();
	public static event TriggerAction OnTrigger;
	public static event TriggerAction OnRelease;
	public Transform Tracker;
	public Transform MarkerA;
	public Transform MarkerB;
	public Transform FingerJoint;
	public Transform Actor;
	public Transform Goal;
	public Transform Ghost;
	public Transform RayRoot;
	public float Tension;
	public float Separation; // distance between two marker
	public float WalkScalar = 400f;
	public bool Ready = false;
	public GameObject ReadyLED;

	// Use this for initialization
	void Start () {
	}

	// Use this for initialization
	void OnEnable () {
		TriggerBehavior.OnTrigger += Trigger;
		TriggerBehavior.OnRelease += Release;
	}
	
	void OnDisable () {
		TriggerBehavior.OnTrigger -= Trigger;
		TriggerBehavior.OnRelease -= Release;
	}

	// Update is called once per frame
	void Update () {
		Tracker.position = MarkerA.position;
		Quaternion aRot = MarkerA.rotation; 
		Quaternion bRot = MarkerB.rotation; 
		float PrevTension = Tension;
		Tension = Quaternion.Angle(aRot, bRot);
		Separation = (MarkerA.position - MarkerB.position).magnitude;

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

		Tracker.rotation = aRot;

		FingerJoint.localEulerAngles = new Vector3(0, 0, Tension);

		if(Tension < 30)
		{
			Ghost.position = Goal.position;
			Ghost.rotation = Goal.rotation;
		}
		else if (Tension > 30 && Separation < 0.5)
		{
			Vector3 vel = (Goal.position - Ghost.position) * WalkScalar;
			vel.y = 0;
			Actor.rigidbody.AddForce(vel);
		}

	}

	void Trigger () {
		/*
		Renderer[] BlastRendererComponents = Blast.GetComponentsInChildren<Renderer>(true);

		// Disable rendering:
		foreach (Renderer component in BlastRendererComponents)
		{
			component.enabled = true;
		}
		*/

		/*
		Renderer ledRenderer = ReadyLED.GetComponent<Renderer>();
		ledRenderer.material.color = new Color(1,0,0,1);
		*/
		
		// Raycasting (this should go to TrackerBehavior eventually)
		RaycastHit hit;
		if(Physics.Raycast (RayRoot.position, RayRoot.forward, out hit, Mathf.Infinity)) {
			Debug.Log ("hit "+hit.transform.name);
			if (hit.transform.name == "Reset")
				Application.LoadLevel("ARTest");
			if (hit.rigidbody != null) 
				hit.rigidbody.AddForceAtPosition(RayRoot.forward*300, hit.point);

		}

		// Move ghost to current location
		Ghost.position = Goal.position;
		Ghost.rotation = Goal.rotation;
		
	}
	
	void Release () {
		/*
		Renderer[] BlastRendererComponents = Blast.GetComponentsInChildren<Renderer>(true);

		// Disable rendering:
		foreach (Renderer component in BlastRendererComponents)
		{
			component.enabled = false;
		}
		*/

		/*
		Renderer ledRenderer = ReadyLED.GetComponent<Renderer>();
		ledRenderer.material.color = new Color(0,1,0, 1);
		*/
	}
}
