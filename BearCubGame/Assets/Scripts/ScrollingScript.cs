using UnityEngine;
using System.Collections;

/// <summary>
/// Parallax scrolling script that should be assigned to a layer
/// </summary>
public class ScrollingScript : MonoBehaviour
{
	/// <summary>
	/// Scrolling speed
	/// </summary>
	public float layerSpeed = 0.3f;

	private void Awake()
	{
		
	}


	public void MoveLayer(Transform camTrans) {

		this.gameObject.transform.position = camTrans.transform.position * layerSpeed;

	}
}
