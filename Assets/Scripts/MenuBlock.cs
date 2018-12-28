using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class MenuBlock : MonoBehaviour{

	private Renderer _renderer;
	private TextMeshPro _text;
	public ButtonConfig config;

	public struct ButtonConfig{
		public string text;
		public float x;
		public float y;

		public ButtonConfig(string text, float x, float y){
			this.text = text;
			this.x = x;
			this.y = y;
		}
	}
	
    private void Awake(){
	    _renderer = gameObject.GetComponent<Renderer>();
	    _renderer.material.color = Color.green;
	    transform.localPosition = new Vector3(0,0,-0.4f);
	    transform.localEulerAngles = Vector3.zero;
	    transform.localScale = Vector3.one;
    }

    public static MenuBlock createMenuBlock(ButtonConfig config,Transform parent){
	    var menuBlock = Instantiate(Resources.Load<GameObject>("Prefabs/MenuBlock"), parent, false).GetComponent<MenuBlock>();
	    menuBlock.initMenuBlock(config);
	    return menuBlock;
    }

    private void initMenuBlock(ButtonConfig inConfig){
	    config = inConfig;
	    
	    transform.localPosition = new Vector3(config.x,config.y,-0.4f);
	  
	    GameObject ga = new GameObject();
	    _text = ga.AddComponent<TextMeshPro>();
	    _text.transform.parent = transform;
	    _text.transform.transform.localEulerAngles = new Vector3(0, 0, 0);
	    _text.text = config.text;
	    _text.transform.localScale = Vector3.one;
	    _text.fontSize = 2;
	    _text.transform.localPosition  = new Vector3(0,0,-0.3f);
	    _text.alignment = TextAlignmentOptions.Center; 
    }

    private void Update(){
        
    }

	private void OnMouseDown(){
		gameObject.transform.parent.gameObject.SendMessage("menuButtonClicked",config);
		_renderer.material.color = Color.red;
	}
	
	private void OnMouseUp(){
		_renderer.material.color = Color.blue;
	}
	
    private void OnMouseEnter(){
	    _renderer.material.color = Color.blue;
	    ResetAngle();
	    gameObject.transform.DOShakeRotation(0.3f, new Vector3(20, 0, 0)).OnComplete(ResetAngle);
    }
	
	void ResetAngle(){
		gameObject.transform.rotation = Quaternion.Euler(0,0,0);
	}
	
	private void OnMouseExit(){
		_renderer.material.color = Color.green;
	}
}
