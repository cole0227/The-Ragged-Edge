using UnityEngine;
using System.Collections;

public class GameObjectBehaviourTimer : MonoBehaviour {

	private float m_Thirty = 0f;
	private float m_Tenth = 0f;
	private float m_Fifth = 0f;
	private float m_Half = 0f;
	private float m_Second = 0f;

	public bool[,] m_UpdateStatus = new bool[5,7];

	//let's make and manage our instance
	private static GameObjectBehaviourTimer m_Instance = null;

	public static GameObjectBehaviourTimer GetInstance()
	{
		//If _instance hasn't been set yet, we grab it from the scene!
		//This will only happen the first time this reference is used.
		if(m_Instance == null)
			m_Instance = GameObject.FindObjectOfType<GameObjectBehaviourTimer>();
		return m_Instance;
	}

	//early intialization
	void Awake() 
	{
		//initialization to get around the editor's bull
		m_UpdateStatus = new bool[5,7];

		for(int i = 0; i < 5; i++)
		{
			for(int j = 0; j < 7; j++)
			{
				m_UpdateStatus [i,j] = false;
			}
		}
	}

	void FixedUpdate()
	{
		float t = Time.deltaTime;

		m_Thirty += t;
		m_Tenth += t;
		m_Fifth += t;
		m_Half += t;
		m_Second += t;

		for(int i = 0; i < 5; i++)
		{
			for(int j = 7-1; j > 0; j--)
			{
				m_UpdateStatus [i,j] = m_UpdateStatus [i,j-1];
			}
		}

		m_UpdateStatus [0,0] = m_Thirty > 7;
		m_UpdateStatus [1,0] = m_Tenth > 7;
		m_UpdateStatus [2,0] = m_Fifth > 7;
		m_UpdateStatus [3,0] = m_Half > 7;
		m_UpdateStatus [4,0] = m_Second > 7;

		if(m_UpdateStatus[0,0])
		{
			m_Thirty=0f;
		}
		if(m_UpdateStatus[1,0])
		{
			m_Tenth=0f;
		}
		if(m_UpdateStatus[2,0])
		{
			m_Fifth=0f;
		}
		if(m_UpdateStatus[3,0])
		{
			m_Half=0f;
		}
		if(m_UpdateStatus[4,0])
		{
			m_Second=0f;
		}
	}
}
