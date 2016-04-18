using UnityEngine;
using System.Collections;

public class MoveCube : MonoBehaviour
{
	public DrawCheckerboard drawCheckerboard;
	public CubeColor cubeColor;

	private float squareWidth = 1f;
	private bool[,] checkerboard;
	private int positionX = 0;
	private int positionY = 0;
	private int squareNumX;
	private int squareNumY;
	private float originX = 0;
	private float originY = 0;

	public float rotSpeed = 300f;

	private bool isMoving = false;
	private int movingDir;	// 1...gor, 2...dol, 3...levo, 4...desno

	private Vector3 rotation;
	private Vector3 pivot;

	private float[] angles = {0f, 90f, 180f, 270f, 360f};

	private bool start = true;

	private Faces faces;

	private struct Faces {
		public bool bottom;
		public bool left;
		public bool right;
		public bool front;
		public bool back;
		public bool top;
	}

	void Start () {
		squareNumX = drawCheckerboard.getSquareNumX();
		squareNumY = drawCheckerboard.getSquareNumY();

		squareWidth = 5f / squareNumX;
		originX = (-1f + squareWidth) / 2f;
		originY = (squareWidth - 1f) / 2f;

		transform.Translate(originX, 0, originY);

		gameObject.transform.localScale = new Vector3(squareWidth, squareWidth, squareWidth);

		faces = new Faces();
		faces.bottom = cubeColor.getFaces()[0];
		faces.left = cubeColor.getFaces()[1];
		faces.right = cubeColor.getFaces()[2];
		faces.front = cubeColor.getFaces()[3];
		faces.back = cubeColor.getFaces()[4];
		faces.top = cubeColor.getFaces()[5];
	}

	void Update () {
		if (start) {
			checkerboard = drawCheckerboard.getCheckerboard();
			start = false;
		}

		if (isMoving) {
			switch (movingDir) {
			case 1:
				transform.RotateAround(pivot, Vector3.right, rotSpeed * Time.deltaTime);
				if (transform.position.y < 1f) {
					Vector3 tempAngle = findAngle();
					transform.eulerAngles = tempAngle;
					isMoving = false;
				}
				break;
			case 2:
				transform.RotateAround(pivot, Vector3.left, rotSpeed * Time.deltaTime);
				if (transform.position.y < 1f) {
					Vector3 tempAngle = findAngle();
					transform.eulerAngles = tempAngle;
					isMoving = false;
				}
				break;
			case 3:
				transform.RotateAround(pivot, Vector3.forward, rotSpeed * Time.deltaTime);
				if (transform.position.y < 1f) {
					Vector3 tempAngle = findAngle();
					transform.eulerAngles = tempAngle;
					isMoving = false;
				}
				break;
			case 4:
				transform.RotateAround(pivot, Vector3.back, rotSpeed * Time.deltaTime);
				if (transform.position.y < 1f) {
					Vector3 tempAngle = findAngle();
					transform.eulerAngles = tempAngle;
					isMoving = false;
				}
				break;
			}
		}
	}

	public void moveUp () {
		if (!isMoving && positionY != squareNumY - 1 && checkerboard[positionX, positionY + 1] == faces.front) {
			isMoving = true;
			movingDir = 1;
			changeSquaresUp();
			transform.position = new Vector3(originX + (positionX * squareWidth), 1f, originY + (positionY * squareWidth));
			positionY++;
			pivot = transform.position + new Vector3(0, -squareWidth / 2f, squareWidth / 2f);
		}
	}

	public void moveDown () {
		if (!isMoving && positionY != 0 && checkerboard[positionX, positionY - 1] == faces.back) {
			isMoving = true;
			movingDir = 2;
			changeSquaresDown();
			transform.position = new Vector3(originX + (positionX * squareWidth), 1f, originY + (positionY * squareWidth));
			positionY--;
			pivot = transform.position + new Vector3(0, -squareWidth / 2f, -squareWidth / 2f);
		}
	}

	public void moveRight () {
		if (!isMoving && positionX != squareNumX - 1 && checkerboard[positionX + 1, positionY] == faces.right) {
			isMoving = true;
			movingDir = 4;
			changeSquaresRight();
			transform.position = new Vector3(originX + (positionX * squareWidth), 1f, originY + (positionY * squareWidth));
			positionX++;
			pivot = transform.position + new Vector3(squareWidth / 2f, -squareWidth / 2f, 0);
		}
	}

	public void moveLeft () {
		if (!isMoving && positionX != 0 && checkerboard[positionX - 1, positionY] == faces.left) {
			isMoving = true;
			movingDir = 3;
			changeSquaresLeft();
			transform.position = new Vector3(originX + (positionX * squareWidth), 1f, originY + (positionY * squareWidth));
			positionX--;
			pivot = transform.position + new Vector3(-squareWidth / 2, -squareWidth / 2f, 0);
		}
	}

	//poklicana, ko se cube premakne levo in poskrbi za menjavo stranic kocke
	private void changeSquaresLeft () {
		//zabeleži postavitev stranic v začasen seznam
		Faces tempFaces = faces;
		//spremeni barve stranic
		faces.bottom = tempFaces.left;
		faces.left = tempFaces.top;
		faces.right = tempFaces.bottom;
		faces.top = tempFaces.right;
	}

	//poklicana, ko se cube premakne desno in poskrbi za menjavo stranic kocke
	private void changeSquaresRight () {
		//zabeleži postavitev stranic v začasen seznam
		Faces tempFaces = faces;
		//spremeni barve stranic
		faces.bottom = tempFaces.right;
		faces.left = tempFaces.bottom;
		faces.right = tempFaces.top;
		faces.top = tempFaces.left;
	}

	//poklicana, ko se cube premakne gor in poskrbi za menjavo stranic kocke
	private void changeSquaresUp () {
		//zabeleži postavitev stranic v začasen seznam
		Faces tempFaces = faces;
		//spremeni barve stranic
		faces.bottom = tempFaces.front;
		faces.front = tempFaces.top;
		faces.back = tempFaces.bottom;
		faces.top = tempFaces.back;
	}

	//poklicana, ko se cube premakne dol in poskrbi za menjavo stranic kocke
	private void changeSquaresDown () {
		//zabeleži postavitev stranic v začasen seznam
		Faces tempFaces = faces;
		//spremeni barve stranic
		faces.bottom = tempFaces.back;
		faces.front = tempFaces.bottom;
		faces.back = tempFaces.top;
		faces.top = tempFaces.front;
	}

	private Vector3 findAngle() {
		Vector3 tempRot = transform.eulerAngles;

		float[] min = {100, 100, 100};
		int[] minIndex = new int[3];

		for (int i = 0; i < angles.Length; i++) {
			if (Mathf.Abs(angles[i] - tempRot.x) < min[0]) {
				min[0] = Mathf.Abs(angles[i] - tempRot.x);
				minIndex[0] = i;
			}
			if (Mathf.Abs(angles[i] - tempRot.y) < min[1]) {
				min[1] = Mathf.Abs(angles[i] - tempRot.y);
				minIndex[1] = i;
			}
			if (Mathf.Abs(angles[i] - tempRot.z) < min[2]) {
				min[2] = Mathf.Abs(angles[i] - tempRot.z);
				minIndex[2] = i;
			}
		}
		return new Vector3(angles[minIndex[0]], angles[minIndex[1]], angles[minIndex[2]]);
	}
}
