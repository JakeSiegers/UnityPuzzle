using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour{
	public Tower tower1;
	//public Tower tower2;
	public Dictionary<string, PuzzleKey> keys;
	private float holdDelay = 0.1f;
	
	private void Awake(){
		keys = new Dictionary<string, PuzzleKey>();
		keys["left"] = new PuzzleKey();
		keys["right"] = new PuzzleKey();
		keys["up"] = new PuzzleKey();
		keys["down"] = new PuzzleKey();
	}

	void Start(){
		tower1.GenerateMap();
		//tower2.GenerateMap();
	}

	void Update(){

		var xInput = Input.GetAxisRaw("Horizontal");
		var yInput = Input.GetAxisRaw("Vertical");
		
		if (xInput < 0){
			processKey("left");
		}else{
			keys["left"].Reset();
		}
		
		if (xInput > 0){
			processKey("right");
		}else{
			keys["right"].Reset();
		}
		
		if (yInput > 0){
			processKey("up");
		}else{
			keys["up"].Reset();
		}
		
		if (yInput < 0){
			processKey("down");
		}else{
			keys["down"].Reset();
		}
		
		
		tower1.ProcessInputs(keys);

	}

	private void processKey(string keyName){
		if (!keys[keyName].pressed || Time.time - keys[keyName].lastPressed > holdDelay){
			keys[keyName].lastPressed = Time.time;
			keys[keyName].active = true;	
		}else{
			keys[keyName].active = false;
		}
		keys[keyName].pressed = true;
	}
}