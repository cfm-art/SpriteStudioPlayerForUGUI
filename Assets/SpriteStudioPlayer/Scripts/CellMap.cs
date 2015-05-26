using UnityEngine;

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
        private Vector4 uv_;

        /// <summary>
        /// 横幅
        /// </summary>
        [SerializeField]
        private int width_;

        /// <summary>
        /// 縦幅
        /// </summary>
        [SerializeField]
        private int height_;

        /// <summary>
        /// 生成
        /// </summary>
        /// <returns></returns>
        public static CellMap Create()
        {
            return ScriptableObject.CreateInstance<CellMap>();
        }

        /// <summary>
        /// テクスチャの設定
        /// </summary>
        public Texture Texture
        {
            get { return texture_; }
        }

        /// <summary>
        /// UV
        /// </summary>
        public Vector4 UV
        {
            get { return uv_; }
        }

        /// <summary>
        /// 横幅
        /// </summary>
        public int Width
        {
            get { return width_; }
        }

        /// <summary>
        /// 縦幅
        /// </summary>
        public int Height
        {
            get { return height_; }
        }
    }
}
