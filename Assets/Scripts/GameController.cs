﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
	
	public Tower tower1;
	public Tower tower2;
	

	// Use this for initialization
	void Start(){
		tower1.GenerateMap();
		tower2.GenerateMap();
	}

	// Update is called once per frame
	void Update()
	{
		/*
		_time += 0.01;
		if (_time > Pi2)
		{
			_time = 0;
		}
		*/


		
	}
}