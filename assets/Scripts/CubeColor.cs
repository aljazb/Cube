using UnityEngine;
using System.Collections;

public class CubeColor : MonoBehaviour
{
	public GameObject right;
	public GameObject left;
	public GameObject bottom;
	public GameObject top;
	public GameObject back;
	public GameObject front;
	public Material black;
	public Material white;

	private bool[] faces = {true, true, true, false, false, false};			// ureditev črnih in belih plasti kocke (0...pod, 1...levo, 2...desno, 3...gor, 4...dol, 5...nad)

	void Start () {
		setColor(bottom, faces[0]);
		setColor(left, faces[1]);
		setColor(right, faces[2]);
		setColor(front, faces[3]);
		setColor(back, faces[4]);
		setColor(top, faces[5]);
	}

	private void setColor(GameObject face, bool isBlack) {
		if (isBlack)
			face.GetComponent<Renderer>().material = black;
		else
			face.GetComponent<Renderer>().material = white;
	}

	public bool[] getFaces() {
		return faces;
	}
}
