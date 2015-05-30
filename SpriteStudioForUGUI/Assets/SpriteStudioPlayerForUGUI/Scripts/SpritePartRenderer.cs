using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

using a.spritestudio.attribute;

namespace a.spritestudio
{
    /// <summary>
    /// パーツ
    /// </summary>
    public class SpritePartRenderer
        : Graphic
    {
        private const int kS = 0;
        private const int kT = 1;
        private const int kU = 2;
        private const int kV = 3;

        /// <summary>
        /// パーツ
        /// </summary>
        [SerializeField]
        private SpritePart part_;

        /// <summary>
        /// 頂点情報
        /// </summary>
        private List<UIVertex> vertices_;

        /// <summary>
        /// 位置
        /// </summary>
        [SerializeField]
        private Vector3 position_;

        /// <summary>
        /// 大きさ
        /// </summary>
        [SerializeField]
        private Vector2 size_;

        /// <summary>
        /// UV
        /// </summary>
        [SerializeField]
        private Vector4 uv_;

        /// <summary>
        /// 追加分
        /// </summary>
        [SerializeField]
        private Vector4 appendUv_;

        /// <summary>
        /// セルマップ
        /// </summary>
        [SerializeField]
        private CellMap cellMap_;

        /// <summary>
        /// セル内の矩形の指定
        /// </summary>
        [SerializeField]
        private int cellIndex_;

        /// <summary>
        /// 優先度
        /// </summary>
        [SerializeField]
        private int priority_;

        public int Priority
        {
            get { return priority_; }
            set
            {
                priority_ = value;
                part_.Root.UpdatePriority();
            }
        }

        /// <summary>
        /// パーツの設定
        /// </summary>
        /// <param name="part"></param>
        public void SetPart( SpritePart part )
        {
            part_ = part;
        }

        /// <summary>
        /// テクスチャの指定
        /// </summary>
        public override Texture mainTexture
        {
            get
            {
                return cellMap_ != null ? cellMap_.Texture : null;
            }
        }

        /// <summary>
        /// GOの初期化時
        /// </summary>
        protected override void Start()
        {
            base.Start();
            appendUv_ = Vector2.zero;
            SetupVertices();
            UpdateVertices();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="part"></param>
        public void Setup( SpritePart part )
        {
            part_ = part;

            // 4つ頂点生成
            SetupVertices();
            position_ = Vector3.zero;
        }

        /// <summary>
        /// 頂点バッファ生成
        /// </summary>
        public void SetupVertices()
        {
            vertices_ = new List<UIVertex>( new UIVertex[] {
                UIVertex.simpleVert, UIVertex.simpleVert, UIVertex.simpleVert, UIVertex.simpleVert
            } );
        }

        /// <summary>
        /// 頂点変換
        /// </summary>
        /// <param name="leftTop"></param>
        /// <param name="rightTop"></param>
        /// <param name="leftBottom"></param>
        /// <param name="rightBottom"></param>
        public void TransformVertices( Vector2 leftTop, Vector2 rightTop, Vector2 leftBottom, Vector2 rightBottom )
        {
            UpdatePosition( 0, Utility.AppendXY( position_, leftBottom ) );
            UpdatePosition( 1, Utility.AppendXY( Utility.AppendY( position_, size_ ), leftTop ) );
            UpdatePosition( 2, Utility.AppendXY( Utility.AppendXY( position_, size_ ), rightTop ) );
            UpdatePosition( 3, Utility.AppendXY( Utility.AppendX( position_, size_ ), rightBottom ) );

            SetVerticesDirty();
        }

        /// <summary>
        /// UV更新
        /// </summary>
        /// <param name="value"></param>
        public void UpdateTexCoordS( float value )
        {
            Debug.Log( "UpdateTexCoordS" + value );
            //appendUv_[kS] = value;
            //UpdateTextureCoord( 0, new Vector2( uv_[kS] + value, uv_[kT] ) );
            //UpdateTextureCoord( 1, new Vector2( uv_[kS] + value, uv_[kV] ) );
        }

        /// <summary>
        /// UV更新
        /// </summary>
        /// <param name="value"></param>
        public void UpdateTexCoordT( float value )
        {
            Debug.Log( "UpdateTexCoordT" + value );
            //appendUv_[kT] = value;
            //UpdateTextureCoord( 0, new Vector2( uv_[kS], uv_[kT] + value ) );
            //UpdateTextureCoord( 3, new Vector2( uv_[kU], uv_[kT] + value ) );
        }

        /// <summary>
        /// UV更新
        /// </summary>
        /// <param name="value"></param>
        public void UpdateTexCoordU( float value )
        {
            appendUv_[kS] = value;
            appendUv_[kU] = value;
            UpdateTextureCoords();
        }

        /// <summary>
        /// UV更新
        /// </summary>
        /// <param name="value"></param>
        public void UpdateTexCoordV( float value )
        {
            appendUv_[kT] = -value;
            appendUv_[kV] = -value;
            UpdateTextureCoords();
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
            cellMap_ = part_.Root.CellMap( index );
            cellIndex_ = mapIndex;

            // UV
            size_ = new Vector2( cellMap_.Width( mapIndex ), cellMap_.Height( mapIndex ) );
            uv_ = cellMap_.UV( mapIndex );

            // PIVOT
            var pivot = cellMap_.Pivot( mapIndex );
            position_ = new Vector3( size_.x * -pivot.x, size_.y * -pivot.y, 0 );
            rectTransform.pivot = pivot;
            part_.GetComponent<RectTransform>().pivot = rectTransform.pivot;
            
            UpdateVertices();
            SetMaterialDirty();
        }

        /// <summary>
        /// 頂点情報の更新
        /// </summary>
        /// <param name="isDirty"></param>
        private void UpdateVertices( bool isDirty = true )
        {
            // 座標
            UpdatePositions( isDirty );
            // UV
            UpdateTextureCoords( isDirty );
        }

        /// <summary>
        /// 頂点座標の一括更新
        /// </summary>
        /// <param name="isDirty"></param>
        private void UpdatePositions( bool isDirty = true )
        {
            UpdatePosition( 0, position_ );
            UpdatePosition( 1, Utility.AppendY( position_, size_ ) );
            UpdatePosition( 2, Utility.AppendXY( position_, size_ ) );
            UpdatePosition( 3, Utility.AppendX( position_, size_ ) );
            if ( isDirty ) {
                SetVerticesDirty();
            }
        }

        /// <summary>
        /// UVの一括更新
        /// </summary>
        /// <param name="isDirty"></param>
        private void UpdateTextureCoords( bool isDirty = true )
        {
            Vector4 uv = uv_ + appendUv_;
            UpdateTextureCoord( 0, new Vector2( uv[kS], uv[kT] ) );
            UpdateTextureCoord( 1, new Vector2( uv[kS], uv[kV] ) );
            UpdateTextureCoord( 2, new Vector2( uv[kU], uv[kV] ) );
            UpdateTextureCoord( 3, new Vector2( uv[kU], uv[kT] ) );
            if ( isDirty ) {
                SetVerticesDirty();
            }
        }

        /// <summary>
        /// 頂点座標の更新
        /// </summary>
        /// <param name="index"></param>
        /// <param name="position"></param>
        private void UpdatePosition( int index, Vector3 position )
        {
            var vertex = vertices_[index];
            vertex.position = position;
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
            if ( vertices_ != null ) {
                //UpdateVertices( false );
                vbo.AddRange( vertices_ );
            } else {
                SetupVertices();
                UpdateVertices( false );
                vbo.AddRange( vertices_ );
            }
        }
    }
}
