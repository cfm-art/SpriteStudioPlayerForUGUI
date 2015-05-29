using UnityEngine;
using System.Collections;

namespace a.spritestudio.attribute
{
    /// <summary>
    /// ユーザー定義情報の通知
    /// </summary>
    public class UserDataNotifier
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isVisible"></param>
        public static AttributeBase Create( string message )
        {
            var self = new AttributeBase( AttributeBase.Target.kUserNotify, null, null, null, new string[] { message } );
            return self;
        }

        /// <summary>
        /// 処理
        /// </summary>
        /// <param name="part"></param>
        public static void OnUpdate( SpritePart part, AttributeBase attribute )
        {
            part.Root.NotifyUserData( part, attribute.@string( 0 ) );
        }
    }
}
