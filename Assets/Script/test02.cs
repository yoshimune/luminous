using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class test02 : MonoBehaviour {
	
	private List<string> returnChars = new List<string>();

    public Text text;
	public InputField inputText;
	public GameObject baloon;
	public BalloonManager balloonManager;
	// Use this for initialization
	void Start () {
		if(text == null) text = GetComponent<Text>();
		returnChars.Add("\r");
		returnChars.Add("\n");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void UpdateText(string value) {
		bool isReturn = false;
		foreach( string returnChar in returnChars ) {
			if (value.EndsWith(returnChar)){
				isReturn = true;
				break;
			}
		}
		
		if (isReturn) {
			text.text = value;
			inputText.text = "";
			
			/*
			var balloonIns = Instantiate(baloon);
			balloonIns.GetComponent<BalloonTest>().UpdateText(value);
			*/
			balloonManager.createBalloon(value);
		}
	}
}
