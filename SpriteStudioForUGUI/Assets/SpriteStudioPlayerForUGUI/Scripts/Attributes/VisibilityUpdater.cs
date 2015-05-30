using UnityEngine;
using System.Collections;

namespace a.spritestudio.attribute
{
    /// <summary>
    /// 可視状態の更新
    /// </summary>
    public class VisibilityUpdater
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isVisible"></param>
        public static AttributeBase Create( bool isVisible )
        {
            var self = new AttributeBase( AttributeBase.Target.kVisibility, null, null, new bool[] { isVisible } );
            return self;
        }

        /// <summary>
        /// 処理
        /// </summary>
        /// <param name="part"></param>
        public static void OnUpdate( SpritePart part, AttributeBase attribute )
        {
            part.IsVisible = attribute.@bool( 0 );
        }
    }
}
