using UnityEngine;
using System.Collections.Generic;

public enum ObjectTag
{
	Player=0,
	Rock,
	Tree,
	Stone_Spike,
	Hemisphere,
	ObjectTagLength
}

public class TaggedObject : MonoBehaviour
{
	public ObjectTag[] m_Tags;
	private static List<TaggedObject>[] m_ObjectTagLists = new List<TaggedObject>[(int)ObjectTag.ObjectTagLength];

	public void Start()
	{
		if(m_ObjectTagLists[0] == null)
		{
			for(int i = 0; i < m_ObjectTagLists.Length; i++)
			{
				m_ObjectTagLists[i] = new List<TaggedObject>();
			}
		}

		for(int i = 0; i < m_Tags.Length; i++)
		{
			m_ObjectTagLists[(int)m_Tags[i]].Add(this);
		}

	}

	private static void RemoveNullObjectsForTag(ObjectTag tag)
	{
		for(int i = m_ObjectTagLists[(int)tag].Count - 1; i > 0; i--)
		{
			if(m_ObjectTagLists[(int)tag][i] == null)
			{
				m_ObjectTagLists[(int)tag].RemoveAt(i);
			}
		}
	}

	public static GameObject[] FindObjectsWithTag(ObjectTag tag)
	{
		RemoveNullObjectsForTag (tag);

		GameObject[] tagged = new GameObject[m_ObjectTagLists[(int)tag].Count];

		for(int i = 0; i < tagged.Length; i++)
		{
			tagged[i] = m_ObjectTagLists[(int)tag][i].gameObject;
		}
		return tagged;
	}

	public bool Contains(ObjectTag tag)
	{
		for(int i = 0; i < m_Tags.Length; i++)
		{
			if(m_Tags[i] == tag)
			{
				return true;
			}
		}
		return false;
	}
}
