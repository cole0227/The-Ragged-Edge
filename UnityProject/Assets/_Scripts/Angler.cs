using UnityEngine;
using System.Collections;

public class Angler : MonoBehaviour
{
	public Vector3 euler
	{
		get {return transform.eulerAngles;}
		set { euler = value;}
	}
}
