using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

using a.spritestudio.attribute;

namespace a.spritestudio
{
    /// <summary>
    /// パーツ
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent( typeof(RectTransform) )]
    public class SpritePart
        : MonoBehaviour
    {
        /// <summary>
        /// 親
        /// </summary>
        [SerializeField]
        private SpriteRoot root_;

        /// <summary>
        /// キーフレーム
        /// </summary>
        [SerializeField]
        private List<KeyFrame> keyFrames_;

        /// <summary>
        /// NULLかどうか
        /// </summary>
        [SerializeField]
        [HideInInspector]
        private bool isNull_;

        /// <summary>
        /// 描画
        /// </summary>
        [SerializeField]
        [HideInInspector]
        private SpritePartRenderer renderer_;

        /// <summary>
        /// 前のフレーム
        /// </summary>
        private int oldFrame_;

        /// <summary>
        /// GOの初期化時
        /// </summary>
        void Start()
        {
            SetupVertices();
            SetFrame( 0 );
        }

        /// <summary>
        /// 親の取得
        /// </summary>
        public SpriteRoot Root
        {
            get { return root_; }
        }

        /// <summary>
        /// レンダラ
        /// </summary>
        public SpritePartRenderer Renderer
        {
            get { return renderer_; }
        }

        /// <summary>
        /// 優先度
        /// </summary>
        public int Priority
        {
            get { return renderer_.Priority; }
            set { renderer_.Priority = value; }
        }

        /// <summary>
        /// 更新
        /// </summary>
        void Update()
        {
            int frame = root_.CurrentFrame;
            if ( oldFrame_ != frame ) {
                SetFrame( frame );

            }
        }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="root"></param>
        public void Setup( SpriteRoot root, types.NodeType nodeType )
        {
            root_ = root;

            isNull_ = nodeType != types.NodeType.kNormal;
            if ( !isNull_ ) {
                // NULLノードでなければレンダラ生成
                var r = new GameObject( name, typeof( SpritePartRenderer ) );
                renderer_ = r.GetComponent<SpritePartRenderer>();
                root.AddSprite( renderer_ );
                renderer_.Setup( this );
                SetupVertices();
            }

            keyFrames_ = new List<KeyFrame>( root_.TotalFrames );
            for ( int i = 0; i < root_.TotalFrames; ++i ) {
                keyFrames_.Add( new KeyFrame() );
            }
        }

        /// <summary>
        /// 頂点バッファ生成
        /// </summary>
        private void SetupVertices()
        {
            if ( renderer_ != null ) {
                renderer_.SetupVertices();
            }
        }

        /// <summary>
        /// キーフレームの追加
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="attribute"></param>
        public void AddKey( int frame, AttributeBase attribute )
        {
            if ( frame >= keyFrames_.Count ) {
                // SSの不具合で範囲外のキーフレームが存在するので無視する
                Debug.LogWarning( "Key frame '" + attribute + "(" + frame + ")' is out of range in " + keyFrames_.Count );
                return;
            }
            keyFrames_[frame].Add( attribute );
        }

        /// <summary>
        /// キーフレームの設定
        /// </summary>
        /// <param name="frame"></param>
        public void SetFrame( int frame )
        {
            // TODO: 非キーフレームの取り扱い -> 最初と最後のフレームは全てあるのでそこから何とか？
            //       もしくは非キーフレームも全て生成しちゃう？
            KeyFrame attributes = keyFrames_[frame];

            foreach ( var attribute in attributes ) {
                attribute.Update( this );
            }

            oldFrame_ = frame;

            UpdateTransform();
        }

        /// <summary>
        /// セルマップの指定
        /// </summary>
        /// <param name="index"></param>
        /// <param name="mapIndex"></param>
        public void SetCellMap( int index, int mapIndex )
        {
            if ( renderer_ != null ) {
                renderer_.SetCellMap( index, mapIndex );
            }
        }

        /// <summary>
        /// Transformの更新
        /// </summary>
        private void UpdateTransform()
        {
            if ( renderer_ == null ) {return;}
            CopyTransform( GetComponent<RectTransform>(), renderer_.rectTransform );
        }

        /// <summary>
        /// Transformをコピー
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        private static void CopyTransform( RectTransform from, RectTransform to )
        {
            to.position = from.position;
        }
    }
}
