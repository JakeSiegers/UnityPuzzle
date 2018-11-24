using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Menu : MonoBehaviour {
	private double _time;
	
	public Camera camera;
	public Text debugText;
	private Material _baseCubeMaterial;
	private Texture2D _baseCubeTexture;
	private TextMeshPro _text;
	

	// Use this for initialization
	private void Start () {
		_baseCubeTexture = Resources.Load<Texture2D>("Textures/block_circle");
		_baseCubeMaterial = Resources.Load<Material>("Materials/base");
		
		//gameObject.GetComponent<Renderer>().material.mainTexture = new Texture2D(128, 128);

		GameObject ga = new GameObject();
		_text = ga.AddComponent<TextMeshPro>();
		_text.transform.parent = gameObject.transform;
		_text.text = "My Menu";
		_text.fontSize = 10;
		_text.transform.localPosition  = new Vector3(0,0,-1);
		_text.alignment = TextAlignmentOptions.Center;
		Debug.Log(_text);


		//gameObject.GetComponent<Renderer>().material = new Material(_baseCubeMaterial){
		//	color = Color.white,
		//	mainTexture = new Texture2D(500,500),
		//};


		gameObject.GetComponent<Renderer>().material.DOColor(Color.green, 10);
		
		InvokeRepeating ("flip", 0, 2); 
		
	}

	void flip(){
		gameObject.transform.DORotate(new Vector3(Random.Range(0,360), Random.Range(0,360),Random.Range(0,360)),1).SetEase(Ease.OutExpo);
	}
	
	// Update is called once per frame
	private void Update () {
		//_time = (Time.time/2)%PuzzleVars.TwoPi;
		//gameObject.transform.position = new Vector3((float)Math.Sin(_time)*3,7,-9);
		//gameObject.transform.eulerAngles = new Vector3(0, 0, (float) (Math.Sin(_time) * 90));
		MenuRayCasts();
	}
	
	private static void MenuRayCasts(){
		/*
		RaycastHit hit;
		Ray ray = camera.ScreenPointToRay(Input.mousePosition);
		Debug.DrawRay(ray.origin,ray.direction,Color.green);
		
		debugText.text = "";
		if (Physics.Raycast(ray, out hit)) {
			Transform objectHit = hit.transform;
		
			Vector3 incomingVec = hit.normal - Vector3.up;
			if (incomingVec == new Vector3(0, -1, -1)){

				
				Renderer rend = hit.transform.GetComponent<Renderer>();
				MeshCollider meshCollider = hit.collider as MeshCollider;

				if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
					return;

				Texture2D tex = rend.material.mainTexture as Texture2D;
				Vector2 pixelUV = hit.textureCoord;
				debugText.text = pixelUV.ToString();
				pixelUV.x *= tex.width;
				pixelUV.y *= tex.height;

				tex.SetPixel((int)pixelUV.x, (int)pixelUV.y, Color.black);
				tex.Apply();
				
			
			}

		}
		*/
	}
}
