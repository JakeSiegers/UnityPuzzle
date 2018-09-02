using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Tower : MonoBehaviour {
	
	private const int CubeCount = 42;
	private List<GameObject> _cubes;
	private GameObject _cube;
	private double _time = 0;
	private const double Pi = Math.PI;
	private const double TwoPi = Pi * 2;
	private const double HalfPi = Pi/2;
	
	public int BoardWidth = 30;
	public int BoardHeight = 13;
	
	private double _circlePieceSize;
	private double _boardRadius;

	public int StartingHeight = 13;

	private double _blockWidth = 1;
	private double _blockHeight = 1;
	private double _blockDepth = 1;

	private Dictionary<string, Color32> _blockColors;
	private Dictionary<string, Texture2D> _blockTextures;
	private List<string> _blockColorKeys;

	private List<List<GameObject>> _map;

	private Material _baseCubeMaterial;


	// Use this for initialization
	void Start (){

		_circlePieceSize = TwoPi / BoardWidth;
		_boardRadius = (_blockWidth*BoardWidth) / TwoPi;
		
		_baseCubeMaterial = Resources.Load<Material>("Materials/base");
		
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
		
		_blockColorKeys = new List<string>(_blockColors.Keys);
		
		/*
		_cubes = new List<GameObject>(CubeCount);
		for (var i = 0; i < _cubes.Capacity; i++){
			var color = i % _blockColorKeys.Count;
			_cubes.Add(Instantiate(Resources.Load("Cube", typeof(GameObject))) as GameObject);
			_cubes[i].GetComponent<Renderer>().material = new Material(_baseCubeMaterial){
				color = _blockColors[_blockColorKeys[color]],
				mainTexture = _blockTextures[_blockColorKeys[color]]	
			};
		}
		*/

		//var b = _generateBlock("heart");
		//Debug.Log(b);
	}
	
	public void GenerateMap(){
		Debug.Log("Generating Map!");
		
		_map = new List<List<GameObject>>(BoardWidth);
		
		for (var gx = 0; gx < BoardWidth; gx++) {
			var column = new List<GameObject>(BoardHeight);
			for (var gy = 0; gy < BoardHeight; gy++) {
				column.Add(null);
			}
			_map.Add(column);
		}

		Debug.Log(BoardWidth);
		Debug.Log(BoardHeight);
		for (var x = 0; x < BoardWidth; x++) {
			for (var y = 0; y < BoardHeight; y++) {

				if (y >= StartingHeight) {
					//_map[x][y] = null;
					continue;
				}

				var colorPool = _blockColorKeys.GetRange(0,_blockColorKeys.Count);
				var lastXType = "";
				var lastYType = "";

				for (var i = -2; i <= 2; i++) {
					if (i == 0) {
						continue;
					}
					var nextXBlock = _map[(x - i + this.BoardWidth) % this.BoardWidth][y];
					if (nextXBlock != null) {
						var xType = nextXBlock.GetComponent<Block>().BlockType;
						var xPos = colorPool.IndexOf(xType);
						if (xType == lastXType && xPos != -1 && colorPool.Count > 1) {
							colorPool.RemoveAt(xPos);
						}
						lastXType = xType;
					}
					var nextYBlock = _map[x][(y - i + this.BoardHeight) % this.BoardHeight];
					if (nextYBlock != null) {
						var yType = nextYBlock.GetComponent<Block>().BlockType;
						var yPos = colorPool.IndexOf(yType);
						if (yType == lastYType && yPos != -1 && colorPool.Count > 1) {
							colorPool.RemoveAt(yPos);
						}
						lastYType = yType;
					}
				}
				//grid[x][y] = colorPool[Math.floor(Math.random() * colorPool.length)];
				var type = colorPool[(int)Math.Floor(Random.value*colorPool.Count)];
				//Debug.Log(type);
				_map[x][y] = _generateBlock(type);
				_setBlock3dPos(_map[x][y], x, y);
			}
		}
		//return grid;
	}

	private void _setBlock3dPos(GameObject block, int x, int y){
		var newX = (Math.Cos(_circlePieceSize * x) * _boardRadius) + gameObject.transform.position.x;
		var newY = (y * _blockHeight) + (_blockHeight / 2);
		var newZ = Math.Sin(_circlePieceSize * x) * _boardRadius;
		var newR = -_circlePieceSize * x + HalfPi;
		block.transform.position = new Vector3((float)newX,(float)newY,(float)newZ);
		var degrees = newR * (180 / Pi);
		block.transform.rotation = Quaternion.Euler(0, (float)degrees, 0);
	}

	private GameObject _generateBlock(string type){
		var cube = Instantiate(Resources.Load<GameObject>("Cube"));
		cube.GetComponent<Renderer>().material = new Material(_baseCubeMaterial){
			color = _blockColors[type],
			mainTexture = _blockTextures[type]	
		};
		cube.GetComponent<Block>().BlockType = type;
		cube.transform.parent = gameObject.transform;
		return cube;
	}
	
	
	public void up(){
		
	}
	
	// Update is called once per frame
	void Update () {
		
		_time = (Time.time/2)%TwoPi;

		/*
		for (var i = 0; i < _cubes.Capacity; i++)
		{
			var radians = (Pi2 / _cubes.Capacity) * i;
			var x = (float) (Math.Cos(radians + _time) * 8);
			var y = (float) (Math.Sin(radians + _time) * 8);
			var z = (float) (Math.Tan(_time) * (i+1));
			_cubes[i].transform.position = new Vector3(x, y, z);
			_cubes[i].transform.rotation = new Quaternion(x, y, z,0);
			
			//this.transform.position = new Vector3((float)Math.Sin(_time),(float)Math.Cos(_time), -10);
		}
		*/

		for (var x = 0; x < BoardWidth; x++){
			for (var y = 0; y < BoardHeight; y++){
				_map[x][y].transform.rotation *= Quaternion.Euler(Random.value,Random.value,Random.value);
			}
		}
		
		gameObject.transform.rotation = Quaternion.Euler(0,(float)Math.Sin(_time)*360,0);
		
	}
}
