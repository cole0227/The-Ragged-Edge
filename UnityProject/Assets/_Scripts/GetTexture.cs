using UnityEngine;
using System.Collections;

public class GetTexture : MonoBehaviour
{

	public int m_Index = 0;
	
	private Terrain m_Terrain;
	private TerrainData m_Data;
	private Vector3 m_Pos;

	void Start () 
	{
		
		m_Terrain = Terrain.activeTerrain;
		m_Data = m_Terrain.terrainData;
		m_Pos = m_Terrain.transform.position;
		
	}

	void Update () 
	{
		m_Index = GetMainTexture(transform.position);
	}

	/*
	void OnGUI () 
	{
		GUI.Box(new Rect( 100, 100, 200, 25 ), "index: " + m_Index.ToString() + ", name: " + m_Data.splatPrototypes[m_Index].texture.name);
	}
	*/

	private float[] GetTextureMix(Vector3 WorldPos)
	{
		// Calculate which map cell the worldPos falls within 
		int mapX = (int)(((WorldPos.x - m_Pos.x) / m_Data.size.x) * m_Data.alphamapWidth);
		int mapZ = (int)(((WorldPos.z - m_Pos.z) / m_Data.size.z) * m_Data.alphamapHeight);
		
		// Get the splat data for this cell
		float[,,] mapData = m_Data.GetAlphamaps( mapX, mapZ, 1, 1 );
		
		// Turn the 3 dimentional array into a 1-D 
		float[] mix = new float[mapData.GetUpperBound(2) + 1];
		
		for(int i = 0; i < mix.Length; i++)
		{
			mix[i] = mapData[ 0, 0, i ];
		}

		return mix;
	}
	
	private int GetMainTexture(Vector3 WorldPos)
	{
		// returns index of the most dominant texture
		float[] mix = GetTextureMix(WorldPos);
		
		float maxMix = 0;
		int maxIndex = 0;
		
		// loop through and find the maximum
		for(int i = 0; i < mix.Length; i++)
		{
			if(mix[i] > maxMix)
			{
				maxIndex = i;
				maxMix = mix[i];
			}
		}

		return maxIndex;
	}
}
