using UnityEngine;
using System.Collections;

namespace a.spritestudio.attribute
{
    /// <summary>
    /// 可視状態の更新
    /// </summary>
    public class VisibilityUpdater
        : AttributeBase
    {
        /// <summary>
        /// 可視
        /// </summary>
        private bool isVisible_;

        /// <summary>
        /// 処理
        /// </summary>
        /// <param name="part"></param>
        protected override void OnUpdate( SpritePart part )
        {
            part.gameObject.SetActive( isVisible_ );
        }
    }
}
