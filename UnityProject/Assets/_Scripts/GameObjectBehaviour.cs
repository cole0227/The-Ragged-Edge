using UnityEngine;
using System.Collections;

public class GameObjectBehaviour : MonoBehaviour {

	private static GameObjectBehaviourTimer m_Timer;
	private static ulong m_TotalFixedUpdateOffset = 0;
	private byte m_FixedUpdateOffset = (byte)(m_TotalFixedUpdateOffset++ % 7);

	public void SetUpdateOffset(byte b)
	{
		m_FixedUpdateOffset = (byte) Mathf.Clamp (b,0,7 - 1);
	}

	// Use this for initialization. Start Won't get called because we are not the base class...
	void Awake () {
		m_Timer = GameObjectBehaviourTimer.GetInstance();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		//update for our objects is now called
		FixedUpdate60 ();

		if(m_Timer.m_UpdateStatus[0,m_FixedUpdateOffset])
		{
			FixedUpdate30();
		}
		if(m_Timer.m_UpdateStatus[1,m_FixedUpdateOffset])
		{
			FixedUpdate10();
		}
		if(m_Timer.m_UpdateStatus[2,m_FixedUpdateOffset])
		{
			FixedUpdate5();
		}
		if(m_Timer.m_UpdateStatus[3,m_FixedUpdateOffset])
		{
			FixedUpdate2();
		}
		if(m_Timer.m_UpdateStatus[4,m_FixedUpdateOffset])
		{
			FixedUpdate1();
		}
	}

	// fast update
	protected virtual void FixedUpdate60()
	{}

	// slow updates
	protected virtual void FixedUpdate30()
	{}
	
	protected virtual void FixedUpdate10()
	{}
	
	protected virtual void FixedUpdate5()
	{}

	protected virtual void FixedUpdate2()
	{}

	protected virtual void FixedUpdate1()
	{}
}
