using UnityEngine;
using System.Collections;

public class BalloonManager : MonoBehaviour {
	
	public GameObject balloon;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void createBalloon(string value) {
		var bal = Instantiate(balloon);
		var textMesh = bal.transform.FindChild("BalloonText").gameObject.GetComponent<TextMesh>();
		textMesh.text = value;
	}
}
