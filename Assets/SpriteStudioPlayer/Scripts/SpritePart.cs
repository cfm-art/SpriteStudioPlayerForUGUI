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
        private SpriteRoot root_;

        /// <summary>
        /// 頂点情報
        /// </summary>
        private List<UIVertex> vertices_;

        /// <summary>
        /// セルマップ
        /// </summary>
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
        /// キーフレーム
        /// </summary>
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
        /// <param name="name"></param>
        /// <param name="root"></param>
        /// <param name="partData"></param>
        public void Setup( SpriteRoot root, string name, object partData )
        {
            // TODO: partDataをちゃんとする
            root_ = root;

            // 4つ頂点生成
            vertices_ = new List<UIVertex>( new UIVertex[] {
                UIVertex.simpleVert, UIVertex.simpleVert, UIVertex.simpleVert, UIVertex.simpleVert
            } );
            position_ = Vector3.zero;

            keyFrames_ = new List<List<AttributeBase>>();

            // 0フレーム目で初期化
            SetFrame( 0 );
        }

        /// <summary>
        /// キーフレームの設定
        /// </summary>
        /// <param name="frame"></param>
        private void SetFrame( int frame )
        {
            // ここで設定
            List<AttributeBase> attributes = keyFrames_[0];

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
            set
            {
                cellMap_ = value;
                size_ = new Vector2( cellMap_.Width, cellMap_.Height );
                position_ = size_ * -0.5f;
                UpdateVertices();
                SetMaterialDirty();
            }
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
            UpdateTextureCoord( 0, new Vector2( cellMap_.UV[kS], cellMap_.UV[kT] ) );
            UpdateTextureCoord( 1, new Vector2( cellMap_.UV[kS], cellMap_.UV[kV] ) );
            UpdateTextureCoord( 2, new Vector2( cellMap_.UV[kU], cellMap_.UV[kV] ) );
            UpdateTextureCoord( 3, new Vector2( cellMap_.UV[kU], cellMap_.UV[kT] ) );
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
