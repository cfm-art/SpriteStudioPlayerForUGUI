using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

using a.spritestudio.attribute;

namespace a.spritestudio
{
    /// <summary>
    /// NULL
    /// TODO: NULLノード作る？
    /// </summary>
    [RequireComponent( typeof(RectTransform) )]
    public class NullPart
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
        private List<List<AttributeBase>> keyFrames_;

        /// <summary>
        /// 優先度
        /// </summary>
        private int priority_;

        /// <summary>
        /// 親の取得
        /// </summary>
        public SpriteRoot Root
        {
            get { return root_; }
        }

        /// <summary>
        /// 優先度
        /// </summary>
        public int Priority
        {
            get { return priority_; }
            set {
                priority_ = value;
                /*
                List<SpritePart> siblings = new List<SpritePart>();
                Transform parent = transform.parent;
                int count = parent.childCount;
                for ( int i = 0; i < count; ++i ) {
                    var part = parent.GetChild( i ).GetComponent<SpritePart>();
                    if ( part != null ) {
                        siblings.Add( part );
                    }
                }

                // 兄弟の並び替え
                siblings.Sort( ( l, r ) => l.priority_ - r.priority_ );
                count = siblings.Count;
                for ( int i = 0; i < count; ++i ) {
                    siblings[i].transform.SetSiblingIndex( i );
                }
                */
            }
        }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="root"></param>
        public void Setup( SpriteRoot root )
        {
            root_ = root;

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
            List<AttributeBase> attributes = keyFrames_[frame];

            foreach ( var attribute in attributes ) {
                //attribute.Update( this );
            }
        }

        /// <summary>
        /// セルマップの指定
        /// </summary>
        /// <param name="index"></param>
        /// <param name="mapIndex"></param>
        public void SetCellMap( int index, int mapIndex )
        {
        }

    }
}
