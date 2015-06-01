using UnityEngine;
using System.Collections;

public class BalloonController : Photon.MonoBehaviour {
	
	public GameObject balloon;
	public string BalloonPath;
	private float zposition = 100f;

	// Use this for initialization
	void Start () {
		// 自分をinputFieldに登録
		//GameObject field = GameObject.Find("Canvas/InputField");
		//Debug.Log(field.name);
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void createBalloon(string value) {
		Vector3 pos = transform.position;
		pos.z = zposition;
		var bal = Instantiate(balloon, pos, transform.rotation) as GameObject;
		//var bal = PhotonNetwork.Instantiate(BalloonPath, Vector3.zero, Quaternion.identity, 0);
		bal.transform.parent = transform;
		var textMesh = bal.transform.FindChild("BalloonText").gameObject.GetComponent<TextMesh>();
		textMesh.text = value;
		this.zposition -= 1.0f;
	}
}
