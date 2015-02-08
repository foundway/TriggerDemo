using UnityEngine;
using System.Collections;

public class GunBehavior : MonoBehaviour {

	public GameObject ReadyLED;
	public GameObject Blast;
	public GameObject Actor;
	public Transform Goal;
	public Transform Ghost;
	public TriggerBehavior TriggerNode;
	public float WalkSpeed = 0.4f;
	public float MaxSpeed = 0.1f;

	// Use this for initialization
	void OnEnable () {
		TriggerBehavior.OnTrigger += Trigger;
		TriggerBehavior.OnRelease += Release;
	}

	void OnDisable () {
		TriggerBehavior.OnTrigger -= Trigger;
		TriggerBehavior.OnRelease -= Release;
	}

	void Start () { }
	
	// Update is called once per frame
	void Update () { }

	void Trigger () {
		/*
		Renderer[] BlastRendererComponents = Blast.GetComponentsInChildren<Renderer>(true);

		// Disable rendering:
		foreach (Renderer component in BlastRendererComponents)
		{
			component.enabled = true;
		}
		*/

		Renderer ledRenderer = ReadyLED.GetComponent<Renderer>();
		ledRenderer.material.color = new Color(1,0,0,1);

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

		Renderer ledRenderer = ReadyLED.GetComponent<Renderer>();
		ledRenderer.material.color = new Color(0,1,0, 1);
	}
}
