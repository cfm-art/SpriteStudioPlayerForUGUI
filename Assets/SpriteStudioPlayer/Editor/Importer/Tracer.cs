using UnityEngine;

namespace a.spritestudio.editor
{
    /// <summary>
    /// ログ
    /// </summary>
    public class Tracer
    {
        /// <summary>
        /// 有効無効
        /// </summary>
        public static bool enable = false;

        /// <summary>
        /// ログ
        /// </summary>
        /// <param name="message"></param>
        public static void Log( object message )
        {
            if ( enable ) {
                Debug.Log( message );
            }
        }

        /// <summary>
        /// ログ
        /// </summary>
        /// <param name="message"></param>
        public static void LogWarning( object message )
        {
            if ( enable ) {
                Debug.LogWarning( message );
            }
        }
    }
}
