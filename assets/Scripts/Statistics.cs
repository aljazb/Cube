using UnityEngine;
using System.Collections;

public class Statistics : MonoBehaviour {

	private int minMoves;
	private int moves = 0;
	
	void Start () {

	}

	void Update () {
	
	}

	public void addMove() {
		moves++;
	}

	public int getMinMoves() {
		return minMoves;
	}

	public void setMinMoves(int minMoves) {
		this.minMoves = minMoves;
	}
}
