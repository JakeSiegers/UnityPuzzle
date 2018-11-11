using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : GameController{

	public string BlockType;
	public int x;
	public int y;

	public void angleBlockUsingPos(){
		var parent = gameObject.transform.parent;
		var parentTower = parent.GetComponent<Tower>();

		var newX = (Math.Cos(parentTower.CirclePieceSize * x) * parentTower.BoardRadius) + parent.transform.position.x;
		var newY = (y * parentTower.BlockHeight) + (parentTower.BlockHeight / 2);
		var newZ = Math.Sin(parentTower.CirclePieceSize * x) * parentTower.BoardRadius;
		var newR = -parentTower.CirclePieceSize * x + PuzzleVars.HalfPi;
		gameObject.transform.localPosition = new Vector3((float)newX,(float)newY,(float)newZ);
		var degrees = newR * (180 / PuzzleVars.Pi);
		gameObject.transform.rotation = Quaternion.Euler(0, (float)degrees, 0);
		
	}

	void Start () {
		
	}

	void Update () {
		
	}
}
