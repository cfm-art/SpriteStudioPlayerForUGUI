using UnityEngine;
using System.Collections;

namespace a.spritestudio.attribute
{
    /// <summary>
    /// ユーザー定義情報の通知
    /// </summary>
    public class UserDataNotifier
    {
        public class UserData
        {
            public int integer;
            public Vector2 point;
            public Rect rect;
            public string text;

            public override string ToString()
            {
                return string.Format( "integer={0}, point={1}, rect={2}, text={3}", integer, point, rect, text );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isVisible"></param>
        public static AttributeBase Create( int? integer, float[] point, float[] rect, string message )
        {
            bool[] has = { integer.HasValue, point != null, rect != null, message != null };
            int[] iValues = new int[1];
            if ( integer.HasValue ) {
                iValues[0] = integer.Value;
            }
            var floats = new System.Collections.Generic.List<float>( 6 );
            if ( point != null ) {
                floats.AddRange( point );
            }
            if ( rect != null ) {
                floats.AddRange( rect );
            }
            string[] texts = null;
            if ( message != null ) {
                texts = new string[] { message };
            }
            var self = new AttributeBase( AttributeBase.Target.kUserNotify, iValues, floats.ToArray(), has, texts );
            return self;
        }

        /// <summary>
        /// 処理
        /// </summary>
        /// <param name="part"></param>
        public static void OnUpdate( SpritePart part, AttributeBase attribute )
        {
            bool hasInt = attribute.@bool( 0 );
            bool hasPoint = attribute.@bool( 1 );
            bool hasRect = attribute.@bool( 2 );
            bool hasString = attribute.@bool( 3 );

            Vector2 point = Vector2.zero;
            Rect rect = new Rect();

            int index = 0;

            if ( hasPoint ) {
                point = new Vector2( attribute.@float( 0 ), attribute.@float( 1 ) );
                index = 2;
            }
            if ( hasRect ) {
                rect = new Rect(
                        attribute.@float( index + 0 ),
                        attribute.@float( index + 1 ),
                        attribute.@float( index + 2 ),
                        attribute.@float( index + 3 ) );
            }

            var data = new UserData() {
                integer = attribute.@int( 0 ),
                text = hasString ? attribute.@string( 0 ) : null,
                point = point,
                rect = rect,
            };
            part.Root.NotifyUserData( part, data );
        }
    }
}
