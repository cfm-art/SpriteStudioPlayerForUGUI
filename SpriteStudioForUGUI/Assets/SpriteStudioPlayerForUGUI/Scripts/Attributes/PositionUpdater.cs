using UnityEngine;
using System.Collections;

namespace a.spritestudio.attribute
{
    /// <summary>
    /// 座標の変更
    /// </summary>
    public class PositionUpdater
    {
        public const int kTargetX = 0;
        public const int kTargetY = 1;
        public const int kTargetZ = 2;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public static AttributeBase Create( int target, float value )
        {
            var self = new AttributeBase(
                target == kTargetX
                    ? AttributeBase.Target.kPositionX
                    : target == kTargetY
                        ? AttributeBase.Target.kPositionY
                        : AttributeBase.Target.kPositionZ,
                null, new float[] { value }, null );
            return self;
        }

        /// <summary>
        /// 処理
        /// </summary>
        /// <param name="part"></param>
        public static void OnUpdateX( SpritePart part, AttributeBase attribute )
        {
            var position = part.transform.localPosition;
            position.x = attribute.@float( 0 );
            part.transform.localPosition = position;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="part"></param>
        /// <param name="attribute"></param>
        public static void OnUpdateY( SpritePart part, AttributeBase attribute )
        {
            var position = part.transform.localPosition;
            position.y = attribute.@float( 0 );
            part.transform.localPosition = position;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="part"></param>
        /// <param name="attribute"></param>
        public static void OnUpdateZ( SpritePart part, AttributeBase attribute )
        {
            var position = part.transform.localPosition;
            position.z = attribute.@float( 0 );
            part.transform.localPosition = position;
        }
    }
}
