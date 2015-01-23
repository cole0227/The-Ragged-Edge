using UnityEngine;
using System.Collections;

public class ObjectHurler : MonoBehaviour
{
	public int m_ObjectsToDistribute;
	public string m_ObjectTag;
	public Mesh[] m_Meshes;
	public Mesh[] m_ColliderMeshes;
	public Material[] m_MaterialsToUse;
	public AudioClip[] m_CollisionClips;
	public string[] m_NamesOfComponentsToAdd;

	public float m_ObjectMass = 5.0f;
	
	public float m_ExplosionShaperOffsetDown = 3.0f;
	public float m_ExplosionShaperRadius = 1.5f;

	private GameObject m_SphereForShapingExplosion;

	// Use this for initialization
	void Start ()
	{
#if UNITY_EDITOR
		if(m_Meshes.Length > m_ColliderMeshes.Length)
		{
			Debug.LogWarning("There are more meshes than ColliderMeshes. Adding box colliders instead. This may not be desired.");
		}
#endif

		//Create all the gameobjects that will be required
		for(int i = 0; i < m_ObjectsToDistribute; i++)
		{
			GameObject newObject = new GameObject(name);
			newObject.transform.position = transform.position;
			newObject.transform.eulerAngles = Random.insideUnitSphere * 360.0f;
			newObject.transform.localScale *= m_ObjectMass/2.0f;
			newObject.tag = m_ObjectTag;

			newObject.AddComponent<Rigidbody>().mass = m_ObjectMass;

			int MeshElement = Mathf.FloorToInt(Random.Range(0.0f,0.999999f) * m_Meshes.Length);

			newObject.AddComponent<MeshFilter>().mesh = m_Meshes[MeshElement];
			newObject.AddComponent<MeshRenderer>().materials = new Material[]{m_MaterialsToUse[Mathf.FloorToInt(Random.Range(0.0f,0.999999f) * m_MaterialsToUse.Length)]};

			//Add mesh colliders, or box colliders if mesh colliders are not provided
			if(MeshElement < m_ColliderMeshes.Length)
			{
				MeshCollider meshCollider = newObject.AddComponent<MeshCollider>();
				meshCollider.sharedMesh = m_ColliderMeshes[MeshElement];
				meshCollider.convex = true;
			}
			else
			{
				newObject.AddComponent<BoxCollider>();
			}

			//Add any components that the user of this tool requested
			for (int c = 0; c < m_NamesOfComponentsToAdd.Length; c++)
			{
				newObject.AddComponent(m_NamesOfComponentsToAdd[c]);
			}

			//Set the parent of the exploded objects to this game object so that they have a convienient folder.
			newObject.transform.parent = gameObject.transform;
		}

		//Create a sphere collider under the explosion to shape it outwards a bit more
		m_SphereForShapingExplosion = new GameObject ("Boom Stick");
		m_SphereForShapingExplosion.transform.position = transform.position + Vector3.down * m_ExplosionShaperOffsetDown;
		m_SphereForShapingExplosion.AddComponent<SphereCollider> ().radius = m_ExplosionShaperRadius;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Time.timeSinceLevelLoad > 1.0f)
		{
			Destroy(m_SphereForShapingExplosion);
			Destroy (this);
		}
	}
}
