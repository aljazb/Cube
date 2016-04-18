using UnityEngine;
using System.Collections;

public class TouchGame : MonoBehaviour {

	public MoveCube moveCube;
	public GameObject cube;
	
	public float minDistance = 5f;
	private Vector2 startPos;
	private Vector2 distance;

	private float camPosY;
	private int touchMode;

	void Start() {
		camPosY = Camera.main.transform.position.y;
		touchMode = PlayerPrefs.GetInt("touchMode", 0);
		/***Odstrani***/
		touchMode = 0;
		/**************/
	}
	
	void Update() {

		/************** Za testiranje ***************/
		if (Input.GetKey("up"))
			moveCube.moveUp();
		else if (Input.GetKey("down"))
			moveCube.moveDown();
		else if (Input.GetKey("left"))
			moveCube.moveLeft();
		else if (Input.GetKey("right"))
			moveCube.moveRight();
		/********************************************/

		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch(0);

			if (touchMode == 0) {
				if (touch.phase == TouchPhase.Began) {
					startPos = touch.position;
				} else if (touch.phase == TouchPhase.Ended) {
					distance = new Vector2(touch.position.x - startPos.x, touch.position.y - startPos.y);

					if (Mathf.Abs(distance.x) > Mathf.Abs(distance.y) && Mathf.Abs(distance.x) > minDistance) {
						if (distance.x > 0){
							moveCube.moveRight();
						} else {
							moveCube.moveLeft();
						}
					} else if (Mathf.Abs(distance.y) > Mathf.Abs(distance.x) && Mathf.Abs(distance.y) > minDistance) {
						if (distance.y > 0) {
							moveCube.moveUp();
						} else {
							moveCube.moveDown();
						}			
					}
				}
			} else if (touchMode == 1) {
				Ray ray = Camera.main.ScreenPointToRay(touch.position);

				if (Physics.Raycast(ray)) {
					Vector3 boardTouchPos = ray.GetPoint(camPosY);
					Vector3 cubePos = cube.transform.position;

					distance = new Vector2(boardTouchPos.x - cubePos.x, boardTouchPos.z - cubePos.z);

					if (Mathf.Abs(distance.x) > Mathf.Abs(distance.y)) {
						if (distance.x > 0){
							moveCube.moveRight();
						} else {
							moveCube.moveLeft();
						}
					} else if (Mathf.Abs(distance.y) > Mathf.Abs(distance.x)) {
						if (distance.y > 0) {
							moveCube.moveUp();
						} else {
							moveCube.moveDown();
						}			
					}
				}
			}
		}
	}
}