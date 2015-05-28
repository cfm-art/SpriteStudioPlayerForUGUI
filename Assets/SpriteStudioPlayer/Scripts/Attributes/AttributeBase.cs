using UnityEngine;
using System.Collections;

namespace a.spritestudio.attribute
{
    /// <summary>
    /// キーフレームの基本
    /// </summary>
    public abstract class AttributeBase
        : ScriptableObject
    {
        protected AttributeBase()
        {
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="part"></param>
        public void Do( SpritePart part )
        {
            OnUpdate( part );
        }

        /// <summary>
        /// 処理
        /// </summary>
        /// <param name="part"></param>
        protected abstract void OnUpdate( SpritePart part );
    }
}
