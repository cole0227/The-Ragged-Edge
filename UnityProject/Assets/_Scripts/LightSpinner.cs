using UnityEngine;
using System.Collections;

public class LightSpinner : MonoBehaviour
{

	public float m_Speed = 0.5f;
	public Vector3 m_Axis = Vector3.up;
	
	// Update is called once per frame
	void Update ()
	{
		//Rotate the game object around an axis
		transform.RotateAround (transform.position, m_Axis.normalized, m_Speed * Time.deltaTime);
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.magenta;
		Gizmos.DrawLine (transform.position, transform.position + m_Axis * 10.0f);
	}
}
