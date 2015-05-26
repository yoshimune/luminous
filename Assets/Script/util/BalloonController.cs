using UnityEngine;
using System.Collections;

public class BalloonController : MonoBehaviour {
	
	public GameObject balloon;

	// Use this for initialization
	void Start () {
		if (this.balloon == null) {
			Debug.Log("Balloon is null.");
			return;
		}
		StartCoroutine("addBalloon", this.balloon);
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	private IEnumerator addBalloon (GameObject balloon) {
		Debug.Log("Coroutine Start.");
		
		while (true){
			GameObject balloon01 = (GameObject)Instantiate (balloon, transform.position, Quaternion.identity);
			//Destroy(balloon01, 1f);
			yield return new WaitForSeconds (3f);
		}
	}
}
