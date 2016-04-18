using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateCheckerboard : MonoBehaviour
{
	public CubeColor cubeColor;

	private bool[,] checkerboard;
	private int squareNumX;
	private int squareNumY;
	private int blackSquareNum;
	private List<Layout> layoutCombinations = new List<Layout>();
	private List<int> path;

	private struct Layout {
		public int positionX;
		public int positionY;
		public int moveCount;
		public bool bottom;
		public bool left;
		public bool right;
		public bool front;
		public bool back;
		public bool top;
	}

	public bool[,] getCheckerboard(int numX, int numY, int numBlack, int minSteps) {
		squareNumX = numX;
		squareNumY = numY;
		blackSquareNum = numBlack;

		Layout defaultLayout = new Layout();
		defaultLayout.bottom = cubeColor.getFaces()[0];
		defaultLayout.left = cubeColor.getFaces()[1];
		defaultLayout.right = cubeColor.getFaces()[2];
		defaultLayout.front = cubeColor.getFaces()[3];
		defaultLayout.back = cubeColor.getFaces()[4];
		defaultLayout.top = cubeColor.getFaces()[5];
		defaultLayout.positionX = 0;
		defaultLayout.positionY = 0;
		defaultLayout = moveDown(defaultLayout);
		defaultLayout.moveCount = -1;

		for (int i = 0; i < 10000; i++) {
			generateCheckerboard();

			path = moveCube(defaultLayout, 0);

			if (path != null && path[0] >= minSteps) {
				 for (int j = path.Count - 1; j > 0; j--) {
					switch (path[j]) {
					case 0:
						print("Gor");
						break;
					case 1:
						print("Desno");
						break;
					case 2:
						print("Dol");
						break;
					case 3:
						print("Levo");
						break;
					}
				} 
				break;
			}
			layoutCombinations = new List<Layout>();
		}

		return checkerboard;
	}

	public List<int> getPath(int posX, int posY, bool[] faces, bool[,] checkerboard) {
		Layout layout = new Layout();
		layout.bottom = faces[0];
		layout.left = faces[1];
		layout.right = faces[2];
		layout.front = faces[3];
		layout.back = faces[4];
		layout.top = faces[5];
		layout.positionX = posX;
		layout.positionY = posY;
		layout = moveDown(layout);
		layout.moveCount = -1;

		List<int> tempPath = moveCube(layout, 0);
		List<int> restultPath = tempPath;

		for (int i = 1; i < tempPath.Count; i++) {
			restultPath[i] = tempPath[tempPath.Count - i];
		}
		return restultPath;
	}

	private void generateCheckerboard() {
		int squareNum = squareNumX * squareNumY;
		bool[] colorList = new bool[squareNum];
		checkerboard = new bool[squareNumX, squareNumY];

		for (int i = 0; i < squareNum; i++)
			colorList[i] = (i < blackSquareNum - 1) ? true : false;

		checkerboard[0, 0] = true;
		int squaresLeft = squareNum;
		for (int y = 0; y < squareNumY; y++) {
			for (int x = 0; x < squareNumX; x++) {
				if (x == 0 && y == 0) {
					squaresLeft--;
					continue;
				}
				int randomNum = Random.Range(0, squaresLeft);
				checkerboard[x, y] = colorList[randomNum];
				for (int nextNum = randomNum; nextNum < squaresLeft - 1; nextNum++)
					colorList[nextNum] = colorList[nextNum + 1];
				squaresLeft--;
			}
		}
	}

	// lastSquarePos -> 0...up, 1...right, 2...down, 3...left
	private List<int> moveCube(Layout layout, int lastMove) {
		switch (lastMove) {
		case 0:
			layout = moveUp(layout);
			break;
		case 1:
			layout = moveRight(layout);
			break;
		case 2:
			layout = moveDown(layout);
			break;
		case 3:
			layout = moveLeft(layout);
			break;
		}

		layout.moveCount++;

		if (layout.positionX == squareNumX - 1 && layout.positionY == squareNumY - 1) {
			List<int> path = new List<int>();
			path.Add(layout.moveCount);
			return path;
		}

		if (checkLayoutList(layout))
			return null;

		List<List<int>> pathList = new List<List<int>>();

		if (lastMove != 2 && layout.positionY != squareNumY - 1 && layout.front == checkerboard[layout.positionX, layout.positionY + 1]) {
			List<int> path = new List<int>();
			path = moveCube(layout, 0);
			if (path != null) {
				path.Add(0);
				pathList.Add(path);
			}
		}
		if (lastMove != 3 && layout.positionX != squareNumX - 1 && layout.right == checkerboard[layout.positionX + 1, layout.positionY]) {
			List<int> path = new List<int>();
			path = moveCube(layout, 1);
			if (path != null) {
				path.Add(1);
				pathList.Add(path);
			}
		}
		if (lastMove != 0 && layout.positionY != 0 && layout.back == checkerboard[layout.positionX, layout.positionY - 1]) {
			List<int> path = new List<int>();
			path = moveCube(layout, 2);
			if (path != null) {
				path.Add(2);
				pathList.Add(path);
			}
		}
		if (lastMove != 1 && layout.positionX != 0 && layout.left == checkerboard[layout.positionX - 1, layout.positionY]) {
			List<int> path = new List<int>();
			path = moveCube(layout, 3);
			if (path != null) {
				path.Add(3);
				pathList.Add(path);
			}
		}

		if (pathList.Count == 0)
			return null;

		float[] pathArray = new float[pathList.Count];
		for (int i = 0; i < pathList.Count; i++) {
			pathArray[i] = (float)pathList[i][0];
		}

		int min = (int)Mathf.Min(pathArray);
		for (int i = 0; i < pathList.Count; i++) {
			if (pathList[i][0] == min)
				return pathList[i];
		}

		return null;
	}
	
	private Layout moveLeft(Layout layout) {
		Layout tempLayout = layout;

		layout.bottom = tempLayout.left;
		layout.left = tempLayout.top;
		layout.right = tempLayout.bottom;
		layout.top = tempLayout.right;

		layout.positionX--;

		return layout;
	}
	
	private Layout moveRight(Layout layout) {
		Layout tempLayout = layout;

		layout.bottom = tempLayout.right;
		layout.left = tempLayout.bottom;
		layout.right = tempLayout.top;
		layout.top = tempLayout.left;

		layout.positionX++;

		return layout;
	}
	
	private Layout moveUp(Layout layout) {
		Layout tempLayout = layout;

		layout.bottom = tempLayout.front;
		layout.front = tempLayout.top;
		layout.back = tempLayout.bottom;
		layout.top = tempLayout.back;

		layout.positionY++;

		return layout;
	}
	
	private Layout moveDown(Layout layout) {
		Layout tempLayout = layout;

		layout.bottom = tempLayout.back;
		layout.front = tempLayout.bottom;
		layout.back = tempLayout.top;
		layout.top = tempLayout.front;

		layout.positionY--;

		return layout;
	}

	private bool checkLayoutList(Layout layout) {
		for (int i = 0; i < layoutCombinations.Count; i++) {
			if (layout.top == layoutCombinations[i].top &&
			    layout.bottom == layoutCombinations[i].bottom &&
			    layout.left == layoutCombinations[i].left &&
			    layout.right == layoutCombinations[i].right &&
			    layout.back == layoutCombinations[i].back &&
			    layout.front == layoutCombinations[i].front &&
			    layout.positionX == layoutCombinations[i].positionX &&
			    layout.positionY == layoutCombinations[i].positionY) {

				if (layout.moveCount < layoutCombinations[i].moveCount) {
					layoutCombinations[i] = layout;
					return false;
				} else {
					return true;
				}
			}
		}
		layoutCombinations.Add(layout);

		return false;
	}
}