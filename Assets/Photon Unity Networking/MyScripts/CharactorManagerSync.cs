using UnityEngine;
using System.Collections;

public class CharactorManagerSync : Photon.MonoBehaviour {
	
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting) {
            //データの送信
            //stream.SendNext(transform.position);
            //stream.SendNext(transform.rotation);
            //stream.SendNext(GetComponent<Rigidbody2D>().velocity);
			//stream.SendNext(GetComponent<CharactorManager>);
        } else {
            //データの受信
            //transform.position = (Vector3)stream.ReceiveNext();
            //transform.rotation = (Quaternion)stream.ReceiveNext();
            //GetComponent<Rigidbody2D>().velocity = (Vector2)stream.ReceiveNext();
        }
    }
}
