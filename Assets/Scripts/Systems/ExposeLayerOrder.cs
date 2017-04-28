//https://github.com/bradur/ld48-30/blob/master/Assets/TextMeshLayerOrderer.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExposeLayerOrder : MonoBehaviour
{
	public int OrderInLayer = 0;

	void Start () 
	{
		MeshRenderer renderer = GetComponent<MeshRenderer>();
		renderer.sortingOrder = OrderInLayer;
	}
}
