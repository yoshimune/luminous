using UnityEngine;
using System.Collections;
using MyLive2D;

public class AmiAnimation : MonoBehaviour {
	
	public SimpleModel simpleModel;
	public SimpleAnimation simpleAnimation;

	// Use this for initialization
	void Start () {
		if(simpleModel == null) {
			Debug.LogError("SimpleModel is null.");
			return;
		}
		if(simpleAnimation == null) {
			Debug.LogError("SimpleAnimation is null.");
			return;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
    		var tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    		var collition2d = Physics2D.OverlapPoint(tapPoint);
    		if (collition2d) {
        		var hitObject = Physics2D.Raycast(tapPoint,-Vector2.up);
        		if (hitObject) {
            		Debug.Log("hit object is " + hitObject.collider.gameObject.name);
					this.changeAction();
        		}
    		}
		}
	}
	
	private void changeAction() {
		simpleAnimation.ActionChangeRandom();
	}
}
