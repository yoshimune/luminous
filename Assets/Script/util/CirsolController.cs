using UnityEngine;
using System.Collections;

public class CirsolController : Photon.MonoBehaviour {
	
	private bool mouseDown = false;
	// Use this for initialization
	void Start () {
		if (!photonView.isMine) gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) mouseDown = isCircleClick();
		if (Input.GetMouseButtonUp(0)) mouseDown = false;
		if (mouseDown) updatePosition();
	}
	
	private bool isCircleClick() {
		var target = InputController.GetGameObjectByMousePoint();
		return target == gameObject;
	}
	
	private void updatePosition() {
		var x = Input.mousePosition.x;
		Vector3 newPosition = new Vector3(
			InputController.ConvertMousePositionToScreen(Input.mousePosition).x,
			transform.parent.position.y,
			transform.parent.position.z
		);
		transform.parent.position = newPosition;
	}
}
