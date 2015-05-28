using UnityEngine;
using System.Collections;

public class BalloonController : MonoBehaviour {
	
	public GameObject balloon;
	private float zposition = 100f;

	// Use this for initialization
	void Start () {
		// 自分をinputFieldに登録
		GameObject field = GameObject.Find("Canvas/InputField");
		Debug.Log(field.name);
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void createBalloon(string value) {
		Vector3 pos = transform.position;
		pos.z = zposition;
		var bal = Instantiate(balloon, pos, transform.rotation) as GameObject;
		bal.transform.parent = transform;
		var textMesh = bal.transform.FindChild("BalloonText").gameObject.GetComponent<TextMesh>();
		textMesh.text = value;
		this.zposition -= 1.0f;
	}
}
