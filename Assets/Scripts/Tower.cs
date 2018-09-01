using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Tower : MonoBehaviour {
	
	private const int CubeCount = 21;
	private List<GameObject> _cubes;
	private GameObject _cube;
	private double _time = 0;
	const double Pi2 = Math.PI * 2;
	
	public int BoardWidth = 30;
	public int BoardHeight = 13;

	private Dictionary<string, Color32> _blockColors;
	private Dictionary<string, Texture2D> _blockTextures;


	// Use this for initialization
	void Start () {
		
		var baseCubeMaterial = Resources.Load<Material>("Materials/base");

		Debug.Log(baseCubeMaterial);
		
		_blockColors = new Dictionary<string, Color32>{
			{"circle", new Color32(76,175,80,1)},
			{"diamond", new Color32(156,39,176,1)},
			{"heart", new Color32(244,67,54,1)},
			{"star",new Color32(255,235,59,1)},
			{"triangle",new Color32(0,188,212,1)},
			{"triangle2", new Color32(63,81,181,1)},
			{"penta", new Color32(96,125,139,1)}
		};

		_blockTextures = new Dictionary<string, Texture2D>{
			{"circle", Resources.Load<Texture2D>("Textures/block_circle")},
			{"diamond", Resources.Load<Texture2D>("Textures/block_diamond")},
			{"heart", Resources.Load<Texture2D>("Textures/block_heart")},
			{"star",Resources.Load<Texture2D>("Textures/block_star")},
			{"triangle",Resources.Load<Texture2D>("Textures/block_triangle")},
			{"triangle2", Resources.Load<Texture2D>("Textures/block_triangle2")},
			{"penta", Resources.Load<Texture2D>("Textures/block_penta")}
		};
		
		List<string> _blockColorKeys = new List<string>(this._blockColors.Keys);
		
		
		_cubes = new List<GameObject>(CubeCount);
		for (var i = 0; i < _cubes.Capacity; i++){
			var color = i % _blockColorKeys.Count;
			_cubes.Add(Instantiate(Resources.Load("Cube", typeof(GameObject))) as GameObject);
			_cubes[i].GetComponent<Renderer>().material = new Material(baseCubeMaterial){
				color = _blockColors[_blockColorKeys[color]],
				mainTexture = _blockTextures[_blockColorKeys[color]]	
			};
		}
		

	}
	
	/*
	public void GenerateMap(){
		Debug.Log("Generating Tower!");
		
		let grid = [];
		for (let gx = 0; gx < this.BoardWidth; gx++) {
			let column = [];
			for (let gy = 0; gy < this.BoardHeight; gy++) {
				column.push(null);
			}
			grid.push(column);
		}

		for (let x = 0; x < this.BoardWidth; x++) {
			for (let y = 0; y < this.BoardHeight; y++) {

				if (y >= this.startingHeight) {
					grid[x][y] = null;
					continue;
				}

				let colorPool = colorPoolIn.slice(0);
				let lastXType = '';
				let lastYType = '';

				for (let i = -2; i <= 2; i++) {
					if (i === 0) {
						continue;
					}

					let nextXBlock = grid[(x - i + this.BoardWidth) % this.BoardWidth][y];

					if (nextXBlock !== null) {
						let xType = nextXBlock;
						let xPos = colorPool.indexOf(xType);
						if (xType === lastXType && xPos !== -1 && colorPool.length > 1) {
							colorPool.splice(xPos, 1);
						}
						lastXType = xType;
					}
					let nextYBlock = grid[x][(y - i + this.BoardHeight) % this.BoardHeight];

					if (nextYBlock !== null) {
						let yType = nextYBlock;
						let yPos = colorPool.indexOf(yType);
						if (yType === lastYType && yPos !== -1 && colorPool.length > 1) {
							colorPool.splice(yPos, 1);
						}
						lastYType = yType;
					}
				}
				grid[x][y] = colorPool[Math.floor(Math.random() * colorPool.length)];
			}
		}
		return grid;
		
	}
	*/
	
	public void up(){
		
	}
	
	// Update is called once per frame
	void Update () {
		
		_time = (Time.time*2)%Pi2;

		for (var i = 0; i < _cubes.Capacity; i++)
		{
			var radians = (Pi2 / _cubes.Capacity) * i;
			var x = (float) (Math.Cos(radians + _time) * 5);
			var y = (float) (Math.Sin(radians + _time) * 5);
			var z = (float) (Math.Cos(_time) * i);
			_cubes[i].transform.position = new Vector3(x, y, z);
			_cubes[i].transform.rotation = new Quaternion(x, y, z,0);
			
			//this.transform.position = new Vector3((float)Math.Sin(_time),(float)Math.Cos(_time), -10);
		}
		
	}
}
