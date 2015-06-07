using UnityEngine;
using System.Collections;
using live2d;

namespace MyLive2D
{
	public class SimpleAnimation : MonoBehaviour {
		
		public SimpleModel simpleModel;
        public string idleMotionKind;             // アイドリングモーション
        public string actionMotionKind;           // アクションモーション
        private Matrix4x4 live2DCanvasPos;
        private Live2DMotion motion;                // モーションクラス
        private MotionQueueManager motionManager;     // モーション管理クラス

		// Use this for initialization
		void Start () {
			if (simpleModel == null) {
				Debug.LogError("simpleModel is null.");
				return;
			}
			else if (idleMotionKind == null || idleMotionKind.Length <= 0) {
				Debug.LogError("idleMotionKind is Empty.");
				return;
			}
			
			// モデル初期化
			simpleModel.init();
            
			// モーション管理クラスのインスタンス作成
            motionManager = new MotionQueueManager();
            // Live2Dモデルの表示位置計算
            float modelWidth = simpleModel.Live2DModel.getCanvasWidth();
            live2DCanvasPos = Matrix4x4.Ortho(0, modelWidth, modelWidth, 0, -50.0f, 50.0f);
		}
		
		// Update is called once per frame
		void Update () {
            // 口パクフラグチェック
            
		}
		
		/// <summary>
        /// カメラシーンにレンダリング時呼ばれる
        /// </summary>
        //void OnRenderObject()
        public void updateMotion()
        {
            if (simpleModel.Live2DModel == null) return;
            simpleModel.Live2DModel.setMatrix(transform.localToWorldMatrix * live2DCanvasPos);
            // アプリが終了していた場合
            if (!Application.isPlaying)
            {
                simpleModel.Live2DModel.update();
                simpleModel.Live2DModel.draw();
                return;
            }
    
            // 再生中のモーションからモデルパラメータを更新
            motionManager.updateParam(simpleModel.Live2DModel);
            // 頂点の更新
            simpleModel.Live2DModel.update();
            // モデルの描画
            //simpleModel.Live2DModel.draw();
        }
        
        public void ActionChangeRandom(){
            var motionParam = DictionaryUtil<string, MotionParam>.getRandom(simpleModel.MtnFiles[actionMotionKind]);
            ActionMotionChange(motionParam.Mtn.name);
            Debug.Log("actionMotionKind:" + actionMotionKind + "  name:" + motionParam.Mtn.name);
        }
        
        public string GetRandomIdleMotionName(){
            var motionParam = DictionaryUtil<string, MotionParam>.getRandom(simpleModel.MtnFiles[idleMotionKind]);
            Debug.Log("actionMotionKind:" + actionMotionKind + "  name:" + motionParam.Mtn.name);
            return motionParam.Mtn.name;
        }
		
        public void ActionMotionChange(string filenm) 
        {
            this.MotionChange(actionMotionKind, filenm);
        }
        
        public void IdleMotionChange(string filenm)
        {
            this.MotionChange(idleMotionKind, filenm);
        }
        
		/// <summary>
        /// モーションチェンジ
        /// </summary>
        /// <param name="filenm"></param>
        public void MotionChange(string kind, string filenm) {
            // 指定ファイルがない場合はreturn
            if (!simpleModel.MtnFiles.ContainsKey(kind, filenm)) return;
            // モーションのロードをする
            motion = Live2DMotion.loadMotion(simpleModel.MtnFiles[kind][filenm].Mtn.bytes);
            // フェードインの設定
            motion.setFadeIn(simpleModel.MtnFiles[kind][filenm].FadeIn);
            // フェードアウトの設定
            motion.setFadeOut(simpleModel.MtnFiles[kind][filenm].FadeOut);
            // モーション再生
            motionManager.startMotion(motion, false);
            // 音声再生
            if (simpleModel.MtnFiles[kind][filenm].Sound != null) {
                GetComponent<AudioSource>().clip = simpleModel.MtnFiles[kind][filenm].Sound;
                GetComponent<AudioSource>().Play();
            }
        }
        
        public bool isMotionFinished(){
            return (motionManager != null && motionManager.isFinished());
        }
	}
}