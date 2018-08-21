using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnBlocks : MonoBehaviour
{

	public int CubeCount = 10;
	private List<GameObject> _cubes;
	private GameObject _cube;
	private double _time = 0;
	const double Pi2 = Math.PI * 2;

	// Use this for initialization
	void Start()
	{
		_cubes = new List<GameObject>(CubeCount);
		for (var i = 0; i < _cubes.Capacity; i++)
		{				
			_cubes.Add(Instantiate(Resources.Load("Cube", typeof(GameObject))) as GameObject);
			var material = new Material(Shader.Find("Diffuse")) {color = new Color(Random.value, Random.value, Random.value)};
			_cubes[i].GetComponent<Renderer>().material = material;
		}

	}

	// Update is called once per frame
	void Update()
	{
		_time += 0.01;
		if (_time > Pi2)
		{
			_time = 0;
		}

		for (var i = 0; i < _cubes.Capacity; i++)
		{
			var radians = (Pi2 / _cubes.Capacity) * i;
			var x = (float) (Math.Cos(radians + _time) * 5);
			var y = (float) (Math.Sin(radians + _time) * 5);
			var z = (float) (Math.Tan(_time) * 5);
			_cubes[i].transform.position = new Vector3(x, y, z);
			_cubes[i].transform.rotation = new Quaternion(x, y, z,0);
		}
	}
}