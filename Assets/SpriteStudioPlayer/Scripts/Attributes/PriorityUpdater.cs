using UnityEngine;
using System.Collections;

namespace a.spritestudio.attribute
{
    /// <summary>
    /// 優先度の設定
    /// </summary>
    public class PriorityUpdater
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static AttributeBase Create( int value )
        {
            var self = new AttributeBase( AttributeBase.Target.kPriority, new int[] { value }, null, null );
            return self;
        }

        /// <summary>
        /// 処理
        /// </summary>
        /// <param name="part"></param>
        public static void OnUpdate( SpritePart part, AttributeBase attribute )
        {
            part.Priority = attribute.@int( 0 );
        }
    }
}
