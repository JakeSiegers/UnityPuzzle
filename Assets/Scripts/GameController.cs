using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour{
	public Tower tower1;
	//public Tower tower2;
	public Dictionary<string, PuzzleKey> keys;
	private float holdDelay = 0.1f;
	public Text debugText;

	public static Vector2 cursorPos = new Vector2(0,0);
	
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

	public static void penis(){
		
	}	
	
	private void processKey(string keyName){
		if (!keys[keyName].Pressed || Time.time - keys[keyName].LastPressed > holdDelay){
			keys[keyName].LastPressed = Time.time;
			keys[keyName].Active = true;	
		}else{
			keys[keyName].Active = false;
		}
		keys[keyName].Pressed = true;
	}
}