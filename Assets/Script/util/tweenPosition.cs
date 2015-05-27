using UnityEngine;
using System.Collections;

public class TweenPosition : MonoBehaviour {
	
	public float fadeTime = 1f;
	public Vector3 startPosition;
	public Vector3 finishPosition;
	public bool fadeFlg = false;
	private Vector3 slopeVec;
	private float currentTime = 0;

	// Use this for initialization
	void Start () {
	    if (startPosition == null) startPosition = new Vector3();
		if (finishPosition == null) finishPosition = new Vector3();
		
		
	}
	
	// Update is called once per frame
	void Update () {
			if (fadeFlg) {
				if(currentTime < fadeTime) UpdatePosition();
				else {
					transform.localPosition = finishPosition;
					fadeFlg = false;
				}
			}
	}
	
	private void UpdatePosition() {
		transform.localPosition = new Vector3(
			transform.localPosition.x + (slopeVec.x * Time.deltaTime),
			transform.localPosition.y + (slopeVec.y * Time.deltaTime),
			transform.localPosition.z + (slopeVec.z * Time.deltaTime)
		);
		currentTime += Time.deltaTime;
	}
	
	public void SetTween(float fadeTime, Vector3 startPosition, Vector3 finishPosition){
		currentTime = 0;
		
		this.fadeTime = fadeTime;
		this.startPosition = startPosition;
		this.finishPosition = finishPosition;
		
		transform.localPosition = startPosition;
		
		slopeVec = new Vector3(
			(finishPosition.x - startPosition.x) / fadeTime,
			(finishPosition.y - startPosition.y) / fadeTime,
			(finishPosition.z - startPosition.z) / fadeTime
		);
	}
}
