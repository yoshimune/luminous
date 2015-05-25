using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MyLive2D
{
	public class MultiDictionary<TKind, TName, TValue> {
		private readonly Dictionary<TKind, Dictionary<TName, TValue>> mDictionary = new Dictionary<TKind, Dictionary<TName, TValue>>();
		
		public TValue this[ TKind kind, TName name ]
		{
			get { return mDictionary[kind][name]; }
			set { mDictionary[kind][name] = value; }
		}
		
		public Dictionary<TName, TValue> this[ TKind kind ]
		{
			get { return mDictionary[kind]; }
		}
		
		public List<TValue> getFromName(TName name) {
			var list = new List<TValue>();
			foreach(Dictionary<TName, TValue> dic in mDictionary.Values) {
				list.Add(dic[name]);
			}
			return list;
		}
		
		public void Add( TKind kind, TName name, TValue value )
		{
			if (mDictionary.ContainsKey(kind)){
				mDictionary[kind].Add(name, value);
			}
			else{
				var dic = new Dictionary<TName, TValue>();
				dic.Add(name, value);
				mDictionary.Add(kind, dic);
			}
		}
        
        public bool ContainsKey( TKind kind )
        {
            return mDictionary.ContainsKey(kind);
        }
        
        public bool ContainsKey( TKind kind, TName name )
        {
            if (mDictionary.ContainsKey(kind)) 
            {
                return mDictionary[kind].ContainsKey(name);
            }
            else return false;
        }
	}
	
	public class MotionParam {
        private TextAsset mtn;
        private AudioClip sound;
        private int fadeIn;
        private int fadeOut;
        
        public MotionParam(TextAsset mtn, AudioClip sound, int fadeIn, int fadeOut)
        {
            this.mtn = mtn;
            this.sound = sound;
            this.fadeIn = fadeIn;
            this.fadeOut = fadeOut;
        }
        
        public TextAsset Mtn
        {
            set { this.mtn = value; }
            get { return this.mtn; }
        }
        
        public AudioClip Sound
        {
            set { this.sound = value; }
            get { return this.sound; }
        }
        
        public int FadeIn
        {
            set { this.fadeIn = value; }
            get { return this.fadeIn; }
        }
        
        public int FadeOut
        {
            set { this.fadeOut = value; }
            get { return this.fadeOut; }
        }
    }
    
    public static class DictionaryUtil<TKey, TValue> {
        // Dictionaryからランダムで値を取り出す
        public static TValue getRandom( Dictionary<TKey, TValue> dic )
        {
            TValue[] values = new TValue[dic.Values.Count];
            dic.Values.CopyTo(values, 0);
            
             int seed = Environment.TickCount;
             System.Random rnd = new System.Random(seed++);
             int num = rnd.Next(0, dic.Count);
             
             return values[num];
        }
    }
}
