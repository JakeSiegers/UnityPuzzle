using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
	private double _time;
	
	public Camera camera;
	public Text debugText;
	private Material _baseCubeMaterial;
	private Texture2D _baseCubeTexture;
	private TextMeshPro _text;
	

	// Use this for initialization
	void Start () {
		_baseCubeTexture = Resources.Load<Texture2D>("Textures/block_circle");
		_baseCubeMaterial = Resources.Load<Material>("Materials/base");
		
		gameObject.GetComponent<Renderer>().material.mainTexture = new Texture2D(128, 128);

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
	}
	
	// Update is called once per frame
	void Update () {
		_time = (Time.time/2)%PuzzleVars.TwoPi;
		gameObject.transform.position = new Vector3((float)Math.Sin(_time)*3,7,-9);
		gameObject.transform.eulerAngles = new Vector3(0, 0, (float) (Math.Sin(_time) * 90));
		this.menuRaycasts();
	}
	
	private void menuRaycasts(){
		
		RaycastHit hit;
		Ray ray = camera.ScreenPointToRay(Input.mousePosition);
		Debug.DrawRay(ray.origin,ray.direction,Color.green);
		
		debugText.text = "";
		if (Physics.Raycast(ray, out hit)) {
			Transform objectHit = hit.transform;
			//Debug.Log(objectHit);
		
			Vector3 incomingVec = hit.normal - Vector3.up;
			if (incomingVec == new Vector3(0, -1, -1)){
				//South side of cube (the side facing the camera)
				//var localHit = transform.InverseTransformPoint(hit.point);
				//debugText.text = localHit.ToString();
				
				
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
				
				/*
				var xScale = objectHit.transform.localScale.x;
				var yScale = objectHit.transform.localScale.y;
				var menuXPos = Math.Abs((objectHit.transform.position.x - (xScale / 2) - hit.point.x) / xScale);
				var menuYPos = Math.Abs((objectHit.transform.position.y - (yScale / 2) - hit.point.y) / yScale);
				
				debugText.text = Math.Round(menuXPos*100) + "%, " + Math.Round(menuYPos*100)+"%";
				*/
			}

			/*
			MeshCollider meshCollider = hit.collider as MeshCollider;
			if (meshCollider == null || meshCollider.sharedMesh == null)
				return;

			Mesh mesh = meshCollider.sharedMesh;
			Debug.Log(mesh);
			Vector3[] vertices = mesh.vertices;
			int[] triangles = mesh.triangles;
			Vector3 p0 = vertices[triangles[hit.triangleIndex * 3 + 0]];
			Vector3 p1 = vertices[triangles[hit.triangleIndex * 3 + 1]];
			Vector3 p2 = vertices[triangles[hit.triangleIndex * 3 + 2]];
			Transform hitTransform = hit.collider.transform;
			p0 = hitTransform.TransformPoint(p0);
			p1 = hitTransform.TransformPoint(p1);
			p2 = hitTransform.TransformPoint(p2);
			Debug.DrawLine(p0, p1,Color.green);
			Debug.DrawLine(p1, p2,Color.green);
			Debug.DrawLine(p2, p0,Color.green);
			*/
			
		}
	}
}
