using UnityEngine;
using System.Collections;

namespace a.spritestudio.attribute
{
    /// <summary>
    /// 回転の変更
    /// </summary>
    public class RotationUpdater
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
                    ? AttributeBase.Target.kRotationX
                    : target == kTargetY
                        ? AttributeBase.Target.kRotationY
                        : AttributeBase.Target.kRotationZ,
                null, new float[] { value }, null );
            return self;
        }

        /// <summary>
        /// 処理
        /// </summary>
        /// <param name="part"></param>
        public static void OnUpdateX( SpritePart part, AttributeBase attribute )
        {
            var rotation = part.transform.localRotation;
            var angles = rotation.eulerAngles;
            angles.x = attribute.@float( 0 );
            rotation.eulerAngles = angles;
            part.transform.localRotation = rotation;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="part"></param>
        /// <param name="attribute"></param>
        public static void OnUpdateY( SpritePart part, AttributeBase attribute )
        {
            var rotation = part.transform.localRotation;
            var angles = rotation.eulerAngles;
            angles.y = attribute.@float( 0 );
            rotation.eulerAngles = angles;
            part.transform.localRotation = rotation;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="part"></param>
        /// <param name="attribute"></param>
        public static void OnUpdateZ( SpritePart part, AttributeBase attribute )
        {
            var rotation = part.transform.localRotation;
            var angles = rotation.eulerAngles;
            angles.z = attribute.@float( 0 );
            rotation.eulerAngles = angles;
            part.transform.localRotation = rotation;
        }
    }
}
