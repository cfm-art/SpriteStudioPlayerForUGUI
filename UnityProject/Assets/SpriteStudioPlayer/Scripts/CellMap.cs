using UnityEngine;
using System.Collections.Generic;

namespace a.spritestudio
{
    /// <summary>
    /// 画像の指定
    /// </summary>
    public class CellMap
        : ScriptableObject
    {
        /// <summary>
        /// 対象テクスチャ
        /// </summary>
        [SerializeField]
        private Texture texture_;

        /// <summary>
        /// UV
        /// </summary>
        [SerializeField]
        private List<Vector4> uv_;

        /// <summary>
        /// 中心
        /// </summary>
       [SerializeField]
        private List<Vector2> pivot_;

        /// <summary>
        /// 横幅
        /// </summary>
        [SerializeField]
        private List<int> width_;

        /// <summary>
        /// 縦幅
        /// </summary>
        [SerializeField]
        private List<int> height_;

        /// <summary>
        /// セルマップの名前の対応
        /// </summary>
        [System.NonSerialized]
        private Dictionary<string, int> fragmentMap_;

        /// <summary>
        /// 生成
        /// </summary>
        /// <returns></returns>
        public static CellMap Create()
        {
            var self = ScriptableObject.CreateInstance<CellMap>();
            self.uv_ = new List<Vector4>();
            self.width_ = new List<int>();
            self.height_ = new List<int>();
            self.pivot_ = new List<Vector2>();
            self.fragmentMap_ = new Dictionary<string, int>();
            return self;
        }

        /// <summary>
        /// コピー
        /// </summary>
        /// <param name="o"></param>
        public void CopyTo( CellMap o )
        {
            o.uv_ = uv_;
            o.width_ = width_;
            o.height_ = height_;
            o.pivot_ = pivot_;
            o.fragmentMap_ = fragmentMap_;
            o.texture_ = texture_;
        }

        /// <summary>
        /// テクスチャの設定
        /// </summary>
        public Texture Texture
        {
            get { return texture_; }
            set { texture_ = value; }
        }

        /// <summary>
        /// セルの追加
        /// </summary>
        /// <param name="name"></param>
        /// <param name="uv"></param>
        /// <param name="size"></param>
        public void AddCell( string name, float[] uv, int[] size, float[] pivot )
        {
            Vector4 coord = new Vector4( uv[0], uv[1], uv[2], uv[3] );
            int width = size[0];
            int height = size[1];
            int index = uv_.Count;

            uv_.Add( coord );
            width_.Add( width );
            height_.Add( height );
            pivot_.Add( new Vector2( pivot[0] + 0.5f, pivot[1] + 0.5f ) );

            fragmentMap_.Add( name, index );
        }

        /// <summary>
        /// セル参照
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int FindCell( string name )
        {
            return fragmentMap_[name];
        }

        /// <summary>
        /// UV
        /// </summary>
        /// <param name="index"></param>
        public Vector4 UV( int index )
        {
            return uv_[index];
        }

        /// <summary>
        /// 横幅
        /// </summary>
        /// <param name="index"></param>
        public int Width( int index )
        {
            return width_[index];
            // TODO: 容量減るけど誤差が出る。 -> UV側を0-1でなくテクセル座標系で持つ？
            //return Mathf.FloorToInt( (uv_[index].z - uv_[index].x) * texture_.width );
        }

        /// <summary>
        /// 縦幅
        /// </summary>
        /// <param name="index"></param>
        public int Height( int index )
        {
            return height_[index];
            // TODO: 容量減るけど誤差が出る。 -> UV側を0-1でなくテクセル座標系で持つ？
            //return Mathf.FloorToInt( (uv_[index].w - uv_[index].y) * texture_.height );
        }

        /// <summary>
        /// PIVOTの取得
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Vector2 Pivot( int index )
        {
            return pivot_[index];
        }
    }
}
