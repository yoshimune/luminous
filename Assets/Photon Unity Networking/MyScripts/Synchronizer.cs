using UnityEngine;
using System.Collections;

public class Synchronizer : Photon.MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting) {
            //データの送信
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            //stream.SendNext(GetComponent<Rigidbody2D>().velocity);
        } else {
            //データの受信
            transform.position = (Vector3)stream.ReceiveNext();
            transform.rotation = (Quaternion)stream.ReceiveNext();
            //GetComponent<Rigidbody2D>().velocity = (Vector2)stream.ReceiveNext();
        }
    }
}
