using UnityEngine;
using System.Collections;
using System.IO;

public class LevelTransition : MonoBehaviour
{
	public string m_Level = "5 Menu Screen"; // Level to transition to
	private bool m_StartedLoad = false;

	void OnTriggerStay(Collider other)
	{
		// If the script is disabled, don't do stuff.
		if (!enabled) return;

		//If the player enters the trigger, transition the player to the next level
		if(other.gameObject.tag.CompareTo("Player") == 0)
		{
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
			StreamWriter TimeLog = new StreamWriter((int)(System.DateTime.UtcNow.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds + ".Log");
#endif
			TimeLog.WriteLine(Time.timeSinceLevelLoad + "\n" + Application.loadedLevelName + "\n" + System.Security.Principal.WindowsIdentity.GetCurrent().Name);
			TimeLog.Close();

			Application.LoadLevel(m_Level);
		}
	}
}
