using UnityEngine;
using System.Text;

namespace a.spritestudio.editor
{

    /// <summary>
    /// ログレベルをintで
    /// </summary>
    internal static class LevelToInt
    {
        public static int ToInt( this Tracer.Level self )
        {
            return (int) self;
        }
    }

    /// <summary>
    /// ログ
    /// </summary>
    public class Tracer
    {
        /// <summary>
        /// ログレベル
        /// </summary>
        public enum Level
        {
            kInfomation,
            kWarning,
            kError,
            kFatal
        }

        /// <summary>
        /// 有効無効
        /// </summary>
        public static bool enable = true;

        /// <summary>
        /// ログ
        /// </summary>
        private static StringBuilder log_;

        /// <summary>
        /// 
        /// </summary>
        private static Level level_;

        /// <summary>
        /// 
        /// </summary>
        public static void Startup( Level level = Level.kWarning )
        {
            log_ = new StringBuilder( 4096 );
            level_ = level;
        }

        /// <summary>
        /// 出力
        /// </summary>
        public static void Dump()
        {
            if ( log_.Length > 0 ) {
                Debug.Log( log_ );
            }
        }

        /// <summary>
        /// ログ
        /// </summary>
        /// <param name="message"></param>
        public static void Log( object message, Level level = Level.kInfomation )
        {
            if ( enable ) {
                //Debug.Log( message );
                if ( level_.ToInt() <= level.ToInt() ) {
                    log_.Append( message );
                    log_.AppendLine();
                }
            }
        }

        /// <summary>
        /// ログ
        /// </summary>
        /// <param name="message"></param>
        public static void LogWarning( object message, Level level = Level.kWarning )
        {
            if ( enable ) {
                //Debug.LogWarning( message );
                if ( level_.ToInt() <= level.ToInt() ) {
                    log_.Append( "<color=red>" );
                    log_.Append( message );
                    log_.Append( "</color>" );
                    log_.AppendLine();
                }
            }
        }
    }
}
