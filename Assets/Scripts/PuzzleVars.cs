using System;
using UnityEngine;

public class PuzzleVars{
	public const double Pi = Math.PI;
	public const double TwoPi = Pi * 2;
	public const double HalfPi = Pi/2;
}

public class PuzzleKey{
	public float LastPressed = 0f;
	public bool Pressed = false;
	public bool Active = false;
	
	public void Reset(){
		Pressed = false;
		Active = false;
	}
}