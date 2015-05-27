using UnityEngine;
using System.Collections;

public class SpriteFadeIn : MonoBehaviour {

	public float fadeTime = 1f;
	private float currentRemainTime;
	private SpriteRenderer spRenderer;
	private bool fadeInFlg = false;
	public bool FadeInFlg
	{
		set { this.fadeInFlg = value; }
		get { return this.fadeInFlg; }
	}
	// Use this for initialization
	void Start () {
		// 初期化
		currentRemainTime = 0;
		spRenderer = GetComponent<SpriteRenderer>();
			//SetTween();
	}
	
	// Update is called once per frame
	void Update () {
		if (this.fadeInFlg && fadeTime > 0) {
			fadeIn();
		}
	}
	
	private void fadeIn(){
		// 残り時間を更新
		currentRemainTime += (Time.deltaTime/fadeTime);
 
		if ( currentRemainTime > 1f ) {
			// 残り時間が無くなったらフラグを折る
			updateAlpha(1f);
			fadeInFlg = false;
			return;
		}
 
		// フェードイン
		updateAlpha(currentRemainTime);
	}
	
	private void updateAlpha(float alpha) {
		var color = spRenderer.color;
		color.a = alpha;
		spRenderer.color = color;
	}
	
	public void init() {
		init(this.fadeTime);
	}
	
	public void init(float fadeTime) {
		this.fadeTime = fadeTime;
		this.currentRemainTime = 0;
	}
}
