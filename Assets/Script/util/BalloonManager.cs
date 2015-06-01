using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BalloonManager : MonoBehaviour {
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public  void SetBalloonCreater(string message){
		// CharactorManager以下からBalloonCreaterを探す
		foreach(GameObject target in GameObject.FindGameObjectsWithTag("CharactorSet")){
			var balloonCreater = target.transform.GetComponent<BalloonCreater>();
			if (balloonCreater == null) continue;
			balloonCreater.CreateBalloon(message);
		}
	}
}
