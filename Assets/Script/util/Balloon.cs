using UnityEngine;
using System.Collections;

public class Balloon : MonoBehaviour {

	public Vector3 localPosition;
	public float fadeInTime = 1f;
	public SpriteFadeIn spriteFadeIn;
	public Vector3 fadeInTargetPos;
	public float fadeTime = 1f;
	public Vector3 targetPos;
	public SpriteFadeOut spriteFadeOut;
	public float fadeOutTime = 1f;
	public Vector3 fadeOutTargetPos;
	private float currentFadeOutRemainTime;
	public TweenPosition tweenPosition;

	void Start () {
		// 初期化
		
		fadeIn();
		currentFadeOutRemainTime = 0;
	}
	
	private void init() {
		transform.localPosition = localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		updateTime();
		
		// 親のx座標に合わせる
		transform.position = new Vector3 (
			transform.parent.position.x,
			transform.position.y,
			transform.position.z
		);
	}
	
	// FadeIn実行
	private void fadeIn() {
		if (spriteFadeIn == null) return;
		spriteFadeIn.init(fadeInTime);
		tweenPosition.SetTween(fadeInTime, transform.localPosition, fadeInTargetPos);
		spriteFadeIn.FadeInFlg = true;
	}
	
	// fadeOut実行
	private void fadeOut() {
		if (spriteFadeOut == null) return;
		spriteFadeOut.init(fadeOutTime);
		tweenPosition.SetTween(fadeOutTime, transform.localPosition, fadeOutTargetPos);
		spriteFadeOut.FadeOutFlg = true;
	}
	
	private void updateTime() {		
		// 残り時間を更新
		currentFadeOutRemainTime += Time.deltaTime;
		if (currentFadeOutRemainTime > (fadeInTime + fadeTime) && !(spriteFadeOut.FadeOutFlg)){
			fadeOut();
		}
	}
}
