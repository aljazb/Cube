using UnityEngine;
using System.Collections;

public class DrawCheckerboard : MonoBehaviour
{
	public GameObject whiteSquare;
	public GameObject blackSquare;
	public GenerateCheckerboard generateCheckerboard;
	public GameObject frame;

	private float squareWidth;
	private Vector3 squarePosition;
	private float positionZ = 0.5f;
	private float originX = 0;
	private float originY = 0;
	public int squareNumX = 5;
	public int squareNumY = 7;
	public int blackSquareNum = 18;
	private bool[,] checkerboard;
	public int minSteps = 30;

	void Start () {
		squareWidth = 5f / squareNumX;
		originX = (-1f + squareWidth) / 2f;
		originY = (squareWidth - 1f) / 2f;

		Instantiate(frame, new Vector3(2f, 0.48f, -2.88f), Quaternion.Euler(0, 0, 0));
		Instantiate(frame, new Vector3(2f, 0.48f, (squareWidth * squareNumY) + 2f), Quaternion.Euler(0, 0, 0));

		checkerboard = generateCheckerboard.getCheckerboard(squareNumX, squareNumY, blackSquareNum, minSteps);

		squarePosition = new Vector3(originX, positionZ, originY);

		for (int y = 0; y < squareNumY; y++) {
			for (int x = 0; x < squareNumX; x++) {
				GameObject temp;
				if (checkerboard[x, y])
					temp = (GameObject)Instantiate(blackSquare, squarePosition, Quaternion.Euler(0, 0, 0));
				else
					temp = (GameObject)Instantiate(whiteSquare, squarePosition, Quaternion.Euler(0, 0, 0));
				temp.transform.localScale = new Vector3(squareWidth, squareWidth, squareWidth);
				squarePosition += Vector3.right * squareWidth;
				temp.transform.parent = gameObject.transform;
			}
			squarePosition = new Vector3(originX, positionZ, squarePosition.z);
			squarePosition += Vector3.forward * squareWidth;
		}
	}

	public int getSquareNumX() {
		return squareNumX;
	}

	public int getSquareNumY() {
		return squareNumY;
	}

	public int getBlackSquareNum() {
		return squareNumX;
	}

	public bool[,] getCheckerboard() {
		return checkerboard;
	}
}
