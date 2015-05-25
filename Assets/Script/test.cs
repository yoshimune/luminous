using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

    public GameObject Model;

	// Use this for initialization
	void Start () {
		if (Model == null) {
			Debug.Log("Model is null");
			return;
		}
		Debug.Log("Model is not null");
		StartCoroutine ("createModel");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private IEnumerator createModel() {
		Debug.Log("1");
		GameObject model1 = Instantiate (Model);
		yield return new WaitForSeconds (1.0f);
		
		Debug.Log("2");
		GameObject model2 = Instantiate (Model);
		yield return new WaitForSeconds (1.0f);
		
		Debug.Log("3");
		GameObject model3 = Instantiate (Model);
		yield return new WaitForSeconds (1.0f);
	}
}
