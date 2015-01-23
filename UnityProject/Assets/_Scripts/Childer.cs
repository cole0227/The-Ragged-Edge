using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//THIS IS PLACEHOLDER, NOT TO BE USED YET.
public class Childer : MonoBehaviour
{
	// Update is called once per frame
	void OnTriggerEnter(Collider other)
	{
		// If the script is disabled, don't do stuff.
		if (!enabled) return;

		other.transform.parent = transform;
	}
	
	// Update is called once per frame
	void OnTriggerExit(Collider other)
	{
		// If the script is disabled, don't do stuff.
		if (!enabled) return;

		other.transform.parent = null;
	}
}
