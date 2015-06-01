using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {
	
	public static GameObject GetGameObjectByMousePoint() {
		var tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		var collition2d = Physics2D.OverlapPoint(tapPoint);
		if (collition2d) {
    		var hitObject = Physics2D.Raycast(tapPoint,-Vector2.up);
    		if (hitObject) {
        		Debug.Log("hit object is " + hitObject.collider.gameObject.name);
				return hitObject.collider.gameObject;
    		}
		}
		return null;
	}
	
	public static Vector2 ConvertMousePositionToScreen(Vector2 mousePosition){
		return Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}
}
