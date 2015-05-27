using UnityEngine;
using System.Collections;

public class SpriteFadeOut : MonoBehaviour {
	public float fadeTime = 1f;
	private float currentRemainTime;
	private SpriteRenderer spRenderer;
	private bool fadeOutFlg;
	public bool FadeOutFlg
	{
		set { this.fadeOutFlg = value; }
		get { return this.fadeOutFlg; }
	}
	// Use this for initialization
	void Start () {
		// 初期化
		currentRemainTime = fadeTime;
		spRenderer = GetComponent<SpriteRenderer>();
		fadeOutFlg = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.fadeOutFlg && fadeTime > 0) {
			fadeOut();
		}
	}
	
	private void fadeOut(){
		
		// 残り時間を更新
		currentRemainTime -= (Time.deltaTime / fadeTime);
 
		if ( currentRemainTime <= 0f ) {
			// 残り時間が無くなったら自分自身を消滅
			GameObject.Destroy(gameObject);
			return;
		}
 
		// フェードアウト
		float alpha = currentRemainTime;
		var color = spRenderer.color;
		color.a = alpha;
		spRenderer.color = color;
	}
	
	public void init() {
		init(this.fadeTime);
	}
	
	public void init(float fadeTime) {
		this.fadeTime = fadeTime;
		this.currentRemainTime = fadeTime;
	}
}
