using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputFieldController : MonoBehaviour {
	
	public CharactorManager MyCharactorManager;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void EnterField(){
		string inputString = GetComponent<InputField>().text;
		
		if(System.Text.RegularExpressions.Regex.IsMatch(inputString, @"[\r\n]")){			
			GetComponent<BalloonManager>().SetBalloonCreater(formatText(inputString));
			MyCharactorManager.MouthAnimationStart();
			GetComponent<InputField>().text = "";
		}
	}
	
	private string formatText(string text) {
		System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"[\r\n]");
		string formatText = r.Replace(text, "");
		if (formatText.Length > 10){
			string headString = formatText.Substring(0,10);
			string tailString;
			if(formatText.Length > 20) tailString = formatText.Substring(10,10);
			else tailString = tailString = formatText.Substring(10);
			formatText = headString + "\n" + tailString;
		}
		return formatText;
	}
}
