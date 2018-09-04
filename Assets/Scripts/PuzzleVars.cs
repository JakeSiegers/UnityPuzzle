using System;

public class PuzzleVars{
	public const double Pi = Math.PI;
	public const double TwoPi = Pi * 2;
	public const double HalfPi = Pi/2;
}

public class PuzzleKey{
	public float lastPressed = 0f;
	public bool pressed = false;
	public bool active = false;

	public void Reset(){
		pressed = false;
		active = false;
	}
}