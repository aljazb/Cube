using UnityEngine;
using System.Collections;

public class MenuCubeMove : MonoBehaviour {

	private float rotSpeed = 50f;
	private Vector3 rot;
	private float fallSpeed = 3f;
	private float destroyTime;

	void Start () {
		destroyTime = Time.time + 5f;
		rot.x = Random.Range(-rotSpeed, rotSpeed) * Time.deltaTime;
		rot.y = Random.Range(-rotSpeed, rotSpeed) * Time.deltaTime;
		rot.z = Random.Range(-rotSpeed, rotSpeed) * Time.deltaTime;
	}
	
	void Update () {
		transform.position += new Vector3(0, -fallSpeed * Time.deltaTime, 0);
		transform.Rotate(rot);
		if (Time.time > destroyTime)
			Destroy(gameObject);
	}
}
