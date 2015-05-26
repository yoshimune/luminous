using UnityEngine;
using System.Collections;

public class BalloonTest : MonoBehaviour {
	
	private TextMesh textMesh;
	public string text;

	// Use this for initialization
	void Start () {
		textMesh = transform.FindChild ("BalloonText").gameObject.GetComponent<TextMesh>();
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void UpdateText(string value) {
		if (textMesh == null) {
			Debug.Log("textMesh is null.");
			return;
		}
		textMesh.text = value;
	}
}
