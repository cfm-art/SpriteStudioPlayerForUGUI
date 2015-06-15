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
        /// 整数からログレベルへ
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        public static Level IntToLevel( int no )
        {
            return (Level) no;
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
                Debug.Log( "SSPJImporter begins dump." );
                const int tail = (65000 / 6 - 6);
                if ( log_.Length > tail ) {
                    for ( int i = 0; i < log_.Length; ) {
                        int length = log_.Length - i;
                        if ( length > tail ) {
                            // 分割
                            bool isLog = false;
                            for ( int j = tail - 1; j > 0; --j ) {
                                if ( log_[i + j] == '\n' ) {
                                    Debug.Log( log_.ToString( i, j ) );
                                    i += j + 1;
                                    isLog = true;
                                    break;
                                }
                            }
                            if ( !isLog ) {
                                Debug.Log( log_.ToString( i, tail ) );
                                i += tail;
                            }
                        } else {
                            Debug.Log( log_.ToString( i, length ) );
                            break;
                        }
                    }
                } else {
                    Debug.Log( log_.ToString() );
                }
                Debug.Log( "end." );
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
