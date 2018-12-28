using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class MenuBlock : MonoBehaviour{

	private Renderer _renderer;
	private TextMeshPro _text;
	
    // Start is called before the first frame update
    private void Start(){
	    _renderer = gameObject.GetComponent<Renderer>();
	    _renderer.material.color = Color.green;
	    
	    Debug.Log(this);
	    
	    GameObject ga = new GameObject();
	    _text = ga.AddComponent<TextMeshPro>();
	    _text.transform.parent = this.transform;
	    _text.transform.transform.localEulerAngles = new Vector3(0,0,0);
	    _text.text = "CLICK TO START";
	    _text.transform.localScale = Vector3.one;
	    _text.fontSize = 2;
	    _text.transform.localPosition  = new Vector3(0,0,-0.3f);
	    _text.alignment = TextAlignmentOptions.Center;

	    var s = DOTween.Sequence()
		    .OnComplete(()=> Debug.Log("This will work"));
	    s.Complete();
	    
    }

    // Update is called once per frame
    void Update(){
        
    }

	private void OnMouseDown(){
		gameObject.transform.parent.gameObject.SendMessage("menuButtonClicked",null);
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
