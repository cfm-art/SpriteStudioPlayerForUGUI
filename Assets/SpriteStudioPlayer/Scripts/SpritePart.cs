using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

using a.spritestudio.attribute;

namespace a.spritestudio
{
    /// <summary>
    /// パーツ
    /// </summary>
    [RequireComponent( typeof(RectTransform) )]
    [RequireComponent( typeof(CanvasRenderer) )]
    public class SpritePart
        : Graphic
    {
        private const int kS = 0;
        private const int kT = 1;
        private const int kU = 2;
        private const int kV = 3;

        /// <summary>
        /// 親
        /// </summary>
        [SerializeField]
        private SpriteRoot root_;

        /// <summary>
        /// 頂点情報
        /// </summary>
        private List<UIVertex> vertices_;

        /// <summary>
        /// セルマップ
        /// </summary>
        [SerializeField]
        private CellMap cellMap_;

        /// <summary>
        /// 位置
        /// </summary>
        private Vector3 position_;

        /// <summary>
        /// 大きさ
        /// </summary>
        private Vector2 size_;

        /// <summary>
        /// UV
        /// </summary>
        private Vector4 uv_;

        /// <summary>
        /// キーフレーム
        /// </summary>
        [SerializeField]
        private List<List<AttributeBase>> keyFrames_;

        /// <summary>
        /// 親の取得
        /// </summary>
        public SpriteRoot Root
        {
            get { return root_; }
        }

        /// <summary>
        /// テクスチャの指定
        /// </summary>
        public override Texture mainTexture
        {
            get
            {
                return cellMap_ != null ? cellMap_.Texture : base.mainTexture;
            }
        }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="root"></param>
        public void Setup( SpriteRoot root )
        {
            root_ = root;

            // 4つ頂点生成
            vertices_ = new List<UIVertex>( new UIVertex[] {
                UIVertex.simpleVert, UIVertex.simpleVert, UIVertex.simpleVert, UIVertex.simpleVert
            } );
            position_ = Vector3.zero;

            keyFrames_ = new List<List<AttributeBase>>( root_.TotalFrames );
            for ( int i = 0; i < root_.TotalFrames; ++i ) {
                keyFrames_.Add( new List<AttributeBase>() );
            }
        }

        /// <summary>
        /// キーフレームの追加
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="attribute"></param>
        public void AddKey( int frame, AttributeBase attribute )
        {
            if ( frame > keyFrames_.Count ) {
                // SSの不具合で範囲外のキーフレームが存在するので無視する
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
            List<AttributeBase> attributes = keyFrames_[frame];

            foreach ( var attribute in attributes ) {
                attribute.Update( this );
            }
        }

        /// <summary>
        /// セルマップの取得・更新
        /// </summary>
        public CellMap CellMap
        {
            get { return cellMap_; }
        }

        /// <summary>
        /// セルマップの指定
        /// </summary>
        /// <param name="index"></param>
        /// <param name="mapIndex"></param>
        public void SetCellMap( int index, int mapIndex )
        {
            cellMap_ = root_.CellMap( index );
            size_ = new Vector2( cellMap_.Width( mapIndex ), cellMap_.Height( mapIndex ) );
            uv_ = cellMap_.UV( mapIndex );
            position_ = size_ * -0.5f;
            UpdateVertices();
            SetMaterialDirty();
        }

        /// <summary>
        /// 頂点情報の更新
        /// </summary>
        private void UpdateVertices()
        {
            // 座標
            UpdatePositions();
            // UV
            UpdateTextureCoords();
        }

        /// <summary>
        /// 頂点座標の一括更新
        /// </summary>
        private void UpdatePositions()
        {
            UpdatePosition( 0, position_ );
            UpdatePosition( 1, Utility.AppendY( position_, size_ ) );
            UpdatePosition( 2, Utility.AppendXY( position_, size_ ) );
            UpdatePosition( 3, Utility.AppendX( position_, size_ ) );
            SetVerticesDirty();
        }

        /// <summary>
        /// UVの一括更新
        /// </summary>
        private void UpdateTextureCoords()
        {
            UpdateTextureCoord( 0, new Vector2( uv_[kS], uv_[kT] ) );
            UpdateTextureCoord( 1, new Vector2( uv_[kS], uv_[kV] ) );
            UpdateTextureCoord( 2, new Vector2( uv_[kU], uv_[kV] ) );
            UpdateTextureCoord( 3, new Vector2( uv_[kU], uv_[kT] ) );
            SetVerticesDirty();
        }

        /// <summary>
        /// 頂点座標の更新
        /// </summary>
        /// <param name="index"></param>
        /// <param name="position"></param>
        private void UpdatePosition( int index, Vector3 position )
        {
            var vertex = vertices_[index];
            vertex.position = position_;
            vertices_[index] = vertex;
        }

        /// <summary>
        /// UVの更新
        /// </summary>
        /// <param name="index"></param>
        /// <param name="uv"></param>
        private void UpdateTextureCoord( int index, Vector2 uv )
        {
            var vertex = vertices_[index];
            vertex.uv0 = uv;
            vertices_[index] = vertex;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vbo"></param>
        protected override void OnFillVBO( List<UIVertex> vbo )
        {
            //base.OnFillVBO( vbo );
            vbo.AddRange( vertices_ );
        }
    }
}
