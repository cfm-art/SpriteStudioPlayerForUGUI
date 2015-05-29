using UnityEngine;
using System.Collections;

namespace a.spritestudio.attribute
{
    /// <summary>
    /// UVの変更
    /// </summary>
    public class TextureCoordUpdater
    {
        public const int kTargetS = 0;
        public const int kTargetT = 1;
        public const int kTargetU = 2;
        public const int kTargetV = 3;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public static AttributeBase Create( int target, float value )
        {
            var self = new AttributeBase( 
                target == kTargetS
                    ? AttributeBase.Target.kUVS
                    : target == kTargetT
                        ? AttributeBase.Target.kUVT
                        : target == kTargetU
                            ? AttributeBase.Target.kUVU
                            : AttributeBase.Target.kUVV,
               null, new float[] { value }, null );
            return self;
        }

        /// <summary>
        /// 処理
        /// </summary>
        /// <param name="part"></param>
        public static void OnUpdateS( SpritePart part, AttributeBase attribute )
        {
            float value = attribute.@float( 0 );
            part.UpdateTexCoordS( value );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="part"></param>
        /// <param name="attribute"></param>
        public static void OnUpdateT( SpritePart part, AttributeBase attribute )
        {
            float value = attribute.@float( 0 );
            part.UpdateTexCoordT( value );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="part"></param>
        /// <param name="attribute"></param>
        public static void OnUpdateU( SpritePart part, AttributeBase attribute )
        {
            float value = attribute.@float( 0 );
            part.UpdateTexCoordU( value );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="part"></param>
        /// <param name="attribute"></param>
        public static void OnUpdateV( SpritePart part, AttributeBase attribute )
        {
            float value = attribute.@float( 0 );
            part.UpdateTexCoordV( value );
        }
    }
}
