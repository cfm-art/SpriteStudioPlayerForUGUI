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

        /// <summary>
        /// 以前の色
        /// </summary>
        private Color oldColor_ = Color.white;

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
            var v = UIVertex.simpleVert;
            v.color = color;

            vertices_ = new List<UIVertex>( new UIVertex[] { v, v, v, v } );
            oldColor_ = color;
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
            // テクスチャのひずみを減らすために4ポリゴンで
            Vector3 v0 = Utility.AppendXY( position_, leftBottom );
            Vector3 v1 = Utility.AppendXY( Utility.AppendY( position_, size_ ), leftTop );
            Vector3 v2 = Utility.AppendXY( Utility.AppendXY( position_, size_ ), rightTop );
            Vector3 v3 = Utility.AppendXY( Utility.AppendX( position_, size_ ), rightBottom );
            Vector3 v4 = new Vector3( (v1.x + v2.x) * 0.5f, (v1.y + v2.y) * 0.5f, v1.z );
            Vector3 v5 = new Vector3( (v1.x + v0.x) * 0.5f, (v1.y + v0.y) * 0.5f, v1.z );
            Vector3 v7 = new Vector3( (v2.x + v3.x) * 0.5f, (v2.y + v3.y) * 0.5f, v1.z );
            Vector3 v8 = new Vector3( (v0.x + v3.x) * 0.5f, (v0.y + v3.y) * 0.5f, v1.z );
            Vector3 v6 = new Vector3( (v4.x + v8.x) * 0.5f, (v5.y + v7.y) * 0.5f, v1.z );
            // v1 v4 v2
            // v5 v6 v7
            // v0 v8 v3

            if ( vertices_.Count == 4 ) {
                for ( int i = 0; i < 3 * 4; ++i ) {
                    vertices_.Add( UIVertex.simpleVert );
                }
            }
            // 左下
            UpdatePosition( 0, v0 );
            UpdatePosition( 1, v5 );
            UpdatePosition( 2, v6 );
            UpdatePosition( 3, v8 );
            // 左上
            UpdatePosition( 4, v5 );
            UpdatePosition( 5, v1 );
            UpdatePosition( 6, v4 );
            UpdatePosition( 7, v6 );
            // 右上
            UpdatePosition( 8, v6 );
            UpdatePosition( 9, v4 );
            UpdatePosition( 10, v2 );
            UpdatePosition( 11, v7 );
            // 右下
            UpdatePosition( 12, v8 );
            UpdatePosition( 13, v6 );
            UpdatePosition( 14, v7 );
            UpdatePosition( 15, v3 );

            UpdateTextureCoords();

            //UpdatePosition( 0, Utility.AppendXY( position_, leftBottom ) );
            //UpdatePosition( 1, Utility.AppendXY( Utility.AppendY( position_, size_ ), leftTop ) );
            //UpdatePosition( 2, Utility.AppendXY( Utility.AppendXY( position_, size_ ), rightTop ) );
            //UpdatePosition( 3, Utility.AppendXY( Utility.AppendX( position_, size_ ), rightBottom ) );
            //SetVerticesDirty();
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
            if ( vertices_.Count == 4 ) {
                UpdateTextureCoord( 0, new Vector2( uv[kS], uv[kT] ) );
                UpdateTextureCoord( 1, new Vector2( uv[kS], uv[kV] ) );
                UpdateTextureCoord( 2, new Vector2( uv[kU], uv[kV] ) );
                UpdateTextureCoord( 3, new Vector2( uv[kU], uv[kT] ) );
            } else {
                // 4ポリの拡張版
                // v1 v4 v2
                // v5 v6 v7
                // v0 v8 v3
                Vector2 v0 = new Vector2( uv[kS], uv[kT] );
                Vector2 v1 = new Vector2( uv[kS], uv[kV] );
                Vector2 v2 = new Vector2( uv[kU], uv[kV] );
                Vector2 v3 = new Vector2( uv[kU], uv[kT] );
                Vector2 v4 = new Vector2( (v1.x + v2.x) * 0.5f, (v1.y + v2.y) * 0.5f );
                Vector2 v5 = new Vector2( (v1.x + v0.x) * 0.5f, (v1.y + v0.y) * 0.5f );
                Vector2 v7 = new Vector2( (v2.x + v3.x) * 0.5f, (v2.y + v3.y) * 0.5f );
                Vector2 v8 = new Vector2( (v0.x + v3.x) * 0.5f, (v0.y + v3.y) * 0.5f );
                Vector2 v6 = new Vector2( (v4.x + v8.x) * 0.5f, (v5.y + v7.y) * 0.5f );

                // 左下
                UpdateTextureCoord( 0, v0 );
                UpdateTextureCoord( 1, v5 );
                UpdateTextureCoord( 2, v6 );
                UpdateTextureCoord( 3, v8 );
                // 左上
                UpdateTextureCoord( 4, v5 );
                UpdateTextureCoord( 5, v1 );
                UpdateTextureCoord( 6, v4 );
                UpdateTextureCoord( 7, v6 );
                // 右上
                UpdateTextureCoord( 8, v6 );
                UpdateTextureCoord( 9, v4 );
                UpdateTextureCoord( 10, v2 );
                UpdateTextureCoord( 11, v7 );
                // 右下
                UpdateTextureCoord( 12, v8 );
                UpdateTextureCoord( 13, v6 );
                UpdateTextureCoord( 14, v7 );
                UpdateTextureCoord( 15, v3 );
            }
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

        #region メッシュ生成 (Unityのバージョン差分あり)
#if UNITY_4 || UNITY_5_1 || UNITY_5_0
        /// <summary>
        /// メッシュ生成(Unity5.1以前)
        /// </summary>
        /// <param name="vbo"></param>
        protected override void OnFillVBO( List<UIVertex> vbo )
        {
            if ( vertices_ != null ) {
                if ( oldColor_ != color ) {
                    int count = vertices_.Count;
                    for ( int i = 0; i < count; ++i ) {
                        var v = vertices_[i];
                        v.color = color;
                        vertices_[i] = v;
                    }
                }
                //UpdateVertices( false );
                vbo.AddRange( vertices_ );
            } else {
                SetupVertices();
                UpdateVertices( false );
                vbo.AddRange( vertices_ );
            }
        }


#elif UNITY_5_2
        /// <summary>
        /// メッシュ生成 (5.2版)
        /// </summary>
        /// <param name="vh"></param>
        protected override void OnPopulateMesh( Mesh mesh )
        {
            using ( var vh = new VertexHelper( mesh ) ) {
                if ( vertices_ != null ) {
                    if ( oldColor_ != color ) {
                        int count = vertices_.Count;
                        for ( int i = 0; i < count; ++i ) {
                            var v = vertices_[i];
                            v.color = color;
                            vertices_[i] = v;
                        }
                    }
                    //UpdateVertices( false );
                    vh.AddUIVertexQuad( vertices_.ToArray() );
                } else {
                    SetupVertices();
                    UpdateVertices( false );
                    vh.AddUIVertexQuad( vertices_.ToArray() );
                }
            }
        }

#else
        /// <summary>
        /// メッシュ生成(5.3以降版)
        /// </summary>
        /// <param name="vh"></param>
        protected override void OnPopulateMesh( VertexHelper vh )
        {
            vh.Clear();
            if ( vertices_ != null ) {
                if ( oldColor_ != color ) {
                    int count = vertices_.Count;
                    for ( int i = 0; i < count; ++i ) {
                        var v = vertices_[i];
                        v.color = color;
                        vertices_[i] = v;
                    }
                }
                //UpdateVertices( false );
                vh.AddUIVertexQuad( vertices_.ToArray() );
            } else {
                SetupVertices();
                UpdateVertices( false );
                vh.AddUIVertexQuad( vertices_.ToArray() );
            }
        }
#endif
        #endregion
    }
}
