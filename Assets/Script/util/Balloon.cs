using UnityEngine;
using System.Collections;

public class Balloon : MonoBehaviour {

	public float fadeTime = 1f;
	public SpriteFadeIn spriteFadeIn;
	public SpriteFadeOut spriteFadeOut;
	private float currentFadeOutRemainTime;
	// Use this for initialization
	void Start () {
		// 初期化
		fadeIn();
		currentFadeOutRemainTime = fadeTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (fadeTime > 0) fadeOut();
	}
	
	private void fadeIn() {
		if (spriteFadeIn == null) return;
		spriteFadeIn.FadeInFlg = true;
	}
	
	private void fadeOut() {
		if (spriteFadeOut == null) return;
		// 残り時間を更新
		currentFadeOutRemainTime -= Time.deltaTime;
 
		if ( currentFadeOutRemainTime <= 0f ) {
			// 残り時間が無くなったらFadeOut実行
			if(spriteFadeOut != null) spriteFadeOut.FadeOutFlg = true;
		}
	}
}
