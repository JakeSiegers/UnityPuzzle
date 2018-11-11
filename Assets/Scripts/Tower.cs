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
	
	public int BoardWidth = 30;
	public int BoardHeight = 13;

	public int StartingHeight = 13;

	private Dictionary<string, Color32> _blockColors;
	private Dictionary<string, Texture2D> _blockTextures;
	private List<string> _blockColorKeys;

	private List<List<GameObject>> _map;

	private Material _baseCubeMaterial;

	public int SelectX { get; private set; } = 0;
	public int SelectY { get; private set; } = 0;

	public double CirclePieceSize { get; private set; }
	public double BoardRadius { get; private set; }
	public double BlockWidth { get; } = 1;
	public double BlockHeight { get; } = 1;
	public double BlockDepth { get; } = 1.0/3.0;
	
	void Awake(){
		CirclePieceSize = PuzzleVars.TwoPi / BoardWidth;
		BoardRadius = (BlockWidth*BoardWidth) / PuzzleVars.TwoPi;
		
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
	}

	void Start (){
		
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
		//Debug.Log("Generating Map!");
		
		_map = new List<List<GameObject>>(BoardWidth);
		
		for (var gx = 0; gx < BoardWidth; gx++) {
			var column = new List<GameObject>(BoardHeight);
			for (var gy = 0; gy < BoardHeight; gy++) {
				column.Add(null);
			}
			_map.Add(column);
			
		}

		//Debug.Log(BoardWidth);
		//Debug.Log(BoardHeight);
		for (var x = 0; x < BoardWidth; x++) {
			for (var y = 0; y < BoardHeight; y++) {

				if (y >= StartingHeight) {
					//_map[x][y] = null;
					continue;
				}

				//Debug.Log(_blockColorKeys);
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
				_map[x][y] = _generateBlock(type,x,y);
				//_setBlock3dPos(_map[x][y], x, y);
			}
		}
		//return grid;
	}
	
	/*
	private void _setBlock3dPos(GameObject block, int x, int y){
		var newX = (Math.Cos(CirclePieceSize * x) * BoardRadius) + gameObject.transform.position.x;
		var newY = (y * BlockHeight) + (BlockHeight / 2);
		var newZ = Math.Sin(CirclePieceSize * x) * BoardRadius;
		var newR = -CirclePieceSize * x + HalfPi;
		block.transform.position = new Vector3((float)newX,(float)newY,(float)newZ);
		var degrees = newR * (180 / Pi);
		block.transform.rotation = Quaternion.Euler(0, (float)degrees, 0);
	}
	*/
	

	private GameObject _generateBlock(string type,int x, int y){
		var cube = Instantiate(Resources.Load<GameObject>("Cube"));
		cube.GetComponent<Renderer>().material = new Material(_baseCubeMaterial){
			color = _blockColors[type],
			mainTexture = _blockTextures[type],
		};
		cube.transform.parent = gameObject.transform;
		var cubeScript = cube.GetComponent<Block>();
		cubeScript.BlockType = type;
		cubeScript.x = x;
		cubeScript.y = y;
		cubeScript.angleBlockUsingPos();
		return cube;
	}
	
	public void up(){
		
	}

	public void ProcessInputs(Dictionary<string,PuzzleKey> inputs){
		if (inputs["left"].Active){
			SelectX--;	
		}
		if (inputs["right"].Active){
			SelectX++;	
		}
		if (inputs["up"].Active){
			SelectY++;	
		}
		if (inputs["down"].Active){
			SelectY--;	
		}
		
		
		if (SelectY >= BoardHeight){
			SelectY = BoardHeight - 1;
		}
		if (SelectY < 0){
			SelectY = 0;
		}
		if (SelectX >= BoardWidth){
			SelectX = 0;
		}
		if (SelectX < 0){
			SelectX = BoardWidth-1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		_time = (Time.time/2)%PuzzleVars.TwoPi;

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
				if (x == SelectX && y == SelectY){
					_map[x][y].transform.rotation *= Quaternion.Euler(0,5f,0);	
				}
				else{
					//_setBlock3dPos(_map[x][y],x,y);
					_map[x][y].GetComponent<Block>().angleBlockUsingPos();
				}
				
			}
		}


		//gameObject.transform.rotation = Quaternion.Euler(0,(float)Math.Sin(_time)*360,0);

	}
}
