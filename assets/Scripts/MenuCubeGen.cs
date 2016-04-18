using UnityEngine;
using System.Collections;

public class MenuCubeGen : MonoBehaviour {

	public GameObject cube;
	private float time = 0;

	void Start () {

	}
	
	void Update () {
		if (Time.time > time) {
			Instantiate(cube, transform.position + new Vector3(Random.Range(0f, 4f), 0, 0), transform.rotation);
			time = Time.time + Random.Range(1f, 3f);
		}
	}
}
