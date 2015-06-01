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
			
			// 改行文字を削除
			System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"[\r\n]");
			string message = r.Replace(inputString, "");
			GetComponent<BalloonManager>().SetBalloonCreater(message);
			MyCharactorManager.MouthAnimationStart();
			GetComponent<InputField>().text = "";
		}
	}
}
