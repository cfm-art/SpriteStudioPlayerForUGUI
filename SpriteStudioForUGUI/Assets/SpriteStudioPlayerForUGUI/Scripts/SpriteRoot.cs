using UnityEngine;
using System.Collections.Generic;

namespace a.spritestudio
{
    /// <summary>
    /// ルート
    /// </summary>
    [DisallowMultipleComponent]
    [ExecuteInEditMode]
    public class SpriteRoot
        : MonoBehaviour
    {
        /// <summary>
        /// FPS
        /// </summary>
        [SerializeField]
        private int fps_ = 30;

        /// <summary>
        /// フレーム数
        /// </summary>
        [SerializeField]
        private int totalFrames_ = 1;

        /// <summary>
        /// 現在のフレーム
        /// </summary>
        [SerializeField]
        private float frame_ = 0;

        /// <summary>
        /// 現在のフレーム(intで)
        /// </summary>
        private int currentFrame_;

        /// <summary>
        /// ループの指定
        /// </summary>
        [SerializeField]
        private bool isLoop_ = true;

        /// <summary>
        /// 逆再生
        /// </summary>
        [SerializeField]
        private bool isReverse_ = false;

        /// <summary>
        /// 速度
        /// </summary>
        [SerializeField]
        private float speed_;

        /// <summary>
        /// セルマップ
        /// </summary>
        [SerializeField]
        private List<CellMap> cellMaps_;

        /// <summary>
        /// ⊿タイムを利用するか。
        /// 利用しない場合はFPSによらずゲーム1フレームに付き1フレーム進む。
        /// </summary>
        [SerializeField]
        private bool isUseDeltaTime_ = true;

        /// <summary>
        /// Spriteを保持する場所
        /// </summary>
        [SerializeField]
        private GameObject spriteHolder_;

        /// <summary>
        /// パーツ
        /// </summary>
        private Dictionary<string, SpritePart> parts_;

        /// <summary>
        /// 優先度の更新
        /// </summary>
        private bool requireUpdatePriority_;

        /// <summary>
        /// レンダラー一覧
        /// </summary>
        [SerializeField]
        private List<SpritePartRenderer> renderers_;

        /// <summary>
        /// 一時停止
        /// </summary>
        [SerializeField]
        private bool isPause_;

        /// <summary>
        /// 完了
        /// </summary>
        public event System.Action<SpriteRoot> OnComplete;

        /// <summary>
        /// ユーザー定義の通知
        /// </summary>
        public event System.Action<SpriteRoot, SpritePart, string> OnUserData;

        /// <summary>
        /// 開始
        /// </summary>
        void Start()
        {
            if ( Application.targetFrameRate > 0 && Application.targetFrameRate <= 120 ) {
                speed_ = (fps_ / (float) Application.targetFrameRate);
            } else {
                speed_ = 1f;
            }
            parts_ = new Dictionary<string, SpritePart>();
            UpdatePriority();

            // イベント用意
            if ( OnComplete == null ) {
                OnComplete = delegate { };
            }
            if ( OnUserData == null ) {
                OnUserData = delegate { };
            }
        }

        /// <summary>
        /// 基本設定
        /// </summary>
        /// <param name="fps"></param>
        /// <param name="frames"></param>
        /// <param name="pivotX"></param>
        /// <param name="pivotY"></param>
        public void Setup( int fps, int frames, float pivotX, float pivotY )
        {
            fps_ = fps;
            totalFrames_ = frames;
            //pivotX_ = pivotX;
            //pivotY_ = pivotY;
        }

        /// <summary>
        /// Spriteを保持する場所を設定
        /// </summary>
        public void SetupSpriteHolder()
        {
            renderers_ = new List<SpritePartRenderer>();
            spriteHolder_ = new GameObject( "sprites", typeof( RectTransform ) );
            spriteHolder_.transform.SetParent( transform );
        }

        /// <summary>
        /// Spriteを保持する場所
        /// </summary>
        public Transform SpriteHolder
        {
            get { return spriteHolder_.transform; }
        }

        /// <summary>
        /// レンダラ追加
        /// </summary>
        /// <param name="renderer"></param>
        public void AddSprite( SpritePartRenderer renderer )
        {
            renderer.transform.SetParent( SpriteHolder );
            renderers_.Add( renderer );
        }

        /// <summary>
        /// セルマップの設定
        /// </summary>
        /// <param name="cellMaps"></param>
        public void SetupCellMaps( List<CellMap> cellMaps )
        {
            if ( cellMaps_ == null ) {
                cellMaps_ = new List<CellMap>( cellMaps );
            } else {
                cellMaps_.Clear();
                cellMaps_.AddRange( cellMaps );
            }
        }

        /// <summary>
        /// 優先度の更新
        /// </summary>
        public void UpdatePriority()
        {
            requireUpdatePriority_ = true;
        }

        /// <summary>
        /// 破棄
        /// </summary>
        void OnDestroy()
        {
            if ( parts_ != null ) {
                parts_.Clear();
            }
            if ( renderers_ != null ) {
                renderers_.Clear();
            }
            OnComplete = null;
            OnUserData = null;
        }

        /// <summary>
        /// 更新
        /// </summary>
        void Update()
        {
#if UNITY_EDITOR
            if ( !Application.isPlaying ) { return; }
#endif
            // 一時停止判定
            if ( isPause_ ) { return; }

            // フレーム制御
            if ( totalFrames_ <= 0 ) { totalFrames_ = 1; }
            if ( isReverse_ ) {
                // 逆再生
                frame_ -= isUseDeltaTime_ ? fps_ * speed_ * Time.deltaTime : 1;
                if ( frame_ < 0 ) {
                    if ( isLoop_ ) {
                        while ( frame_ < 0 ) {
                            frame_ += totalFrames_;
                        }
                    } else {
                        frame_ = 0;
                    }
                    OnComplete( this );
                }
                if ( frame_ >= totalFrames_ ) { frame_ = totalFrames_ - 1; }
            } else {
                // 順再生
                frame_ += isUseDeltaTime_ ? fps_ * speed_ * Time.deltaTime : 1;
                if ( frame_ >= totalFrames_ ) {
                    if ( isLoop_ ) {
                        while ( frame_ >= totalFrames_ ) {
                            frame_ -= totalFrames_;
                        }
                    } else {
                        frame_ = totalFrames_ - 1;
                    }
                    OnComplete( this );
                }
                if ( frame_ < 0 ) { frame_ = 0; }
            }
            // intへの変換コストの為に事前にintへ
            currentFrame_ = (int) Mathf.FloorToInt( frame_ );
        }

        /// <summary>
        /// 遅延更新
        /// </summary>
        void LateUpdate()
        {
            if ( isPause_ ) { return; }
            // 優先度更新
            if ( requireUpdatePriority_ ) {
                renderers_.Sort( ( l, r ) => l.Priority - r.Priority );

                int count = renderers_.Count;
                for ( int i = 0; i < count; ++i ) {
                    renderers_[i].transform.SetSiblingIndex( i );
                }
            }
            requireUpdatePriority_ = false;
        }

        /// <summary>
        /// 追加
        /// </summary>
        /// <param name="name"></param>
        /// <param name="part"></param>
        public void AddPart( string name, SpritePart part )
        {
            if ( parts_ == null ) { parts_ = new Dictionary<string, SpritePart>(); }
            parts_.Add( name, part );
        }

        /// <summary>
        /// パーツ検索
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public SpritePart FindPart( string name )
        {
            SpritePart result;
            parts_.TryGetValue( name, out result );
            return result;
        }

        /// <summary>
        /// ユーザーデータキーの通知
        /// </summary>
        /// <param name="part"></param>
        /// <param name="message"></param>
        public void NotifyUserData( SpritePart part, string message )
        {
            OnUserData( this, part, message );
        }

        /// <summary>
        /// フレーム数
        /// </summary>
        public int TotalFrames
        {
            get { return totalFrames_; }
        }

        /// <summary>
        /// 現在のフレーム
        /// </summary>
        public int CurrentFrame
        {
            get { return currentFrame_; }
        }

        /// <summary>
        /// セルマップの取得
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public CellMap CellMap( int index )
        {
            return cellMaps_[index];
        }

        /// <summary>
        /// 速度の指定
        /// </summary>
        public float Speed
        {
            get { return speed_; }
            set
            {
                if ( value > 0 ) {
                    speed_ = value;
                }
            }
        }

        /// <summary>
        /// 一時停止する
        /// </summary>
        public void Pause()
        {
            isPause_ = true;
        }

        /// <summary>
        /// 再生
        /// </summary>
        public void Play()
        {
            isPause_ = false;
        }

        /// <summary>
        /// 一時停止中かどうか
        /// </summary>
        public bool IsPause
        {
            get { return isPause_; }
        }

        /// <summary>
        /// 再生中かどうか
        /// </summary>
        public bool IsPlaying
        {
            get { return !IsPause && gameObject.activeInHierarchy; }
        }

        /// <summary>
        /// 逆再生
        /// </summary>
        public bool IsReverse
        {
            get { return isReverse_; }
            set { isReverse_ = value; }
        }

    }
}
