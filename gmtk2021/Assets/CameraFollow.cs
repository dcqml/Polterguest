using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public float smoothSpeed = 0.125f;
	public Vector2 offset;

	void FixedUpdate()
	{
		Player target = FindObjectOfType<Player>();
		if(target != null)
		{
			Vector2 desiredPosition = new Vector2(target.transform.position.x, target.transform.position.y) + offset;
			Vector2 smoothedPosition = Vector2.Lerp(transform.position, desiredPosition, smoothSpeed);
			transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);

			//transform.LookAt(target.transform);
		}
		
	}
}
