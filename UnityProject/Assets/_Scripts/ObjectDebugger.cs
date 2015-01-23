using UnityEngine;
using System.Collections;

//ObjectDebugger class used for tracking information about objects
public class ObjectDebugger : MonoBehaviour
{
	// Public variables accesible from the inspector.
	public Vector3 m_TextPosition = new Vector3(0.05f, 0.95f, 0.0f);
	public int m_FontSize = 12;
	public Color m_FontColour = Color.black;

	public bool m_DisplayPosition = false;
	public bool m_DisplayRotation = false;
	public bool m_DisplayScale = false;
	public bool m_DisplayVelocity = false;
	public bool m_DisplayTimer = false;

	// Variables pertaining to the variables accessible from the inspector,
	// but which are not publicly accessible, themselves.
	private Vector3 m_PreviousLocation;
	private float m_Velocity = 0.0f;
	private float m_AverageVelocity = 0.0f;
	private float m_Time = 0.0f;
	private bool m_TimerResetKeyDown = false;

	private GameObject m_TextDisplay;
	private GUIText m_GUIText;

	// Use this for initialization
	void Start ()
	{
		// Create the GUIText and its GameObject.
		m_TextDisplay = new GameObject();
		m_GUIText = m_TextDisplay.AddComponent<GUIText> ();
		m_GUIText.transform.position = m_TextPosition;
		m_GUIText.fontSize = m_FontSize;
		m_GUIText.color = m_FontColour;

		// Set the previous location to the current location so that the first velocity calculation is accurate.
		m_PreviousLocation = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Check to see if Transform, Font Size, and Colour, need to be updated. If they need to be, do so.
		if(m_GUIText.transform.position != m_TextPosition) m_TextDisplay.transform.position = m_TextPosition;
		if(m_GUIText.fontSize != m_FontSize) m_GUIText.fontSize = m_FontSize;
		if(m_GUIText.color != m_FontColour) m_GUIText.color = m_FontColour;

		// Calculate values only if necessary.
		if(m_DisplayVelocity)
		{
			m_Velocity = Vector3.Distance(transform.position, m_PreviousLocation) / Time.deltaTime;
			m_PreviousLocation = transform.position;
			m_AverageVelocity += (m_Velocity - m_AverageVelocity) / 20.0f;
		}
		if(m_DisplayTimer) m_Time += Time.deltaTime;

		// Reset timer if the 'R' key is pressed.
		if (Input.GetKeyDown (KeyCode.R)) m_TimerResetKeyDown = true;
		if (m_TimerResetKeyDown && Input.GetKeyUp (KeyCode.R))
		{
			m_Time = 0.0f;
			m_TimerResetKeyDown = false;
		}

		// Display the requested information
		m_GUIText.text = "Debug information for " + name +
			(m_DisplayPosition?"\nPosition: " + transform.position:null) +
			(m_DisplayRotation?"\nRotation: " + transform.eulerAngles:null) +
			(m_DisplayScale?"\nScale: " + transform.localScale:null) +
			(m_DisplayVelocity?"\nVelocity: " + m_Velocity + "\nAverage Velocity: " + m_AverageVelocity:null) +
			(m_DisplayTimer?"\nTime: " + m_Time:null) ;
	
	}

	// When this script is disabled
	void OnDisable()
	{
		// Clear the text when the debugger is disabled.
		if(m_GUIText != null)
		{
			m_GUIText.text = string.Empty;
		}
	}

	//Draws the real position of a GameObject to the editor
	void OnDrawGizmos()
	{
		
		// Draw a line to display the X orientation of the GameObject
		Gizmos.color = Color.red;
		Gizmos.DrawRay (transform.position, transform.right * 0.5f);
		
		// Draw a line to display the Y orientation of the GameObject
		Gizmos.color = Color.green;
		Gizmos.DrawRay (transform.position, transform.up * 0.5f);

		// Draw a line to display the Z orientation of the GameObject
		Gizmos.color = Color.blue;
		Gizmos.DrawRay (transform.position, transform.forward * 0.5f);
	}
}
