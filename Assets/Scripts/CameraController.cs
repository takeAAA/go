using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	Transform m_transform;
	//readonly Quaternion _BASE_ROTATION = Quaternion.Euler(90, 0, 0);
	readonly Quaternion _BASE_ROTATION = Quaternion.Euler(90, 0, 0);

	void Start()
	{
		m_transform = transform;
	}


	void Update () {
		Input.gyro.enabled = true;
		if (Input.gyro.enabled)
		{
			Quaternion gyro = Input.gyro.attitude;
			m_transform.localRotation = _BASE_ROTATION * (new Quaternion(-gyro.x, -gyro.y, gyro.z, gyro.w));
		}
	}
}