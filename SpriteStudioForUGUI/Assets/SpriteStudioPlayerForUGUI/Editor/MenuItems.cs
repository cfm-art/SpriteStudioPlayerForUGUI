using UnityEditor;

namespace a.spritestudio.editor
{
    /// <summary>
    /// メニューに出す項目
    /// </summary>
    public class MenuItems
    {
        /// <summary>
        /// プリファレンスのタグ
        /// </summary>
        private static class Tags
        {
            /// <summary>
            /// 自動インポート
            /// </summary>
            internal const string kAutoImport = "assfugui-ai";

            /// <summary>
            /// ログ
            /// </summary>
            internal const string kImportLog = "assfugui-il";

            /// <summary>
            /// ログレベル
            /// </summary>
            internal const string kLogLevel = "assfugui-ll";

            /// <summary>
            /// 出力先
            /// </summary>
            internal const string kExportPath = "assfugui-ep";
        }

        /// <summary>
        /// メニューの順番
        /// </summary>
        private enum Order
        {
            kAutoImport = 1,
            kAutoImportOn,
            kAutoImportOff,

            kImportLog,
            kImportLogOn,
            kImportLogOff,

            kLogLevel,
            kLogLevelInfomation,
            kLogLevelWarning,
            kLogLevelError,
            kLogLevelFatal,

            kSetExportPath,
        }

        #region 自動インポートの設定

        /// <summary>
        /// 自動インポート
        /// </summary>
        public static bool AutoImport
        {
            get { return EditorPrefs.GetBool( Tags.kAutoImport, false ); }
            set { EditorPrefs.SetBool( Tags.kAutoImport, value ); }
        }

        /// <summary>
        /// 自動インポート有り
        /// </summary>
        [MenuItem( "SpriteStudioForUGUI/自動インポート/On", false, (int) Order.kAutoImportOn )]
        public static void EnableAutoImport()
        {
            AutoImport = true;
        }

        /// <summary>
        /// 自動インポート有り
        /// </summary>
        [MenuItem( "SpriteStudioForUGUI/自動インポート/On", true )]
        public static bool IsValidEnableAutoImport()
        {
            return !AutoImport;
        }

        /// <summary>
        /// 自動インポートなし
        /// </summary>
        [MenuItem( "SpriteStudioForUGUI/自動インポート/Off", false, (int) Order.kAutoImportOff )]
        public static void DisableAutoImport()
        {
            AutoImport = false;
        }

        /// <summary>
        /// 自動インポートなし
        /// </summary>
        [MenuItem( "SpriteStudioForUGUI/自動インポート/Off", true )]
        public static bool IsValidDisableAutoImport()
        {
            return AutoImport;
        }
        #endregion

        #region インポートログの有無
        /// <summary>
        /// ログ
        /// </summary>
        public static bool ImportLog
        {
            get { return EditorPrefs.GetBool( Tags.kImportLog, false ); }
            set { EditorPrefs.SetBool( Tags.kImportLog, value ); }
        }

        /// <summary>
        /// ログ有り
        /// </summary>
        [MenuItem( "SpriteStudioForUGUI/インポートログ/On", false, (int) Order.kImportLogOn )]
        public static void EnableImportLog()
        {
            ImportLog = true;
        }

        /// <summary>
        ///ログ有り
        /// </summary>
        [MenuItem( "SpriteStudioForUGUI/インポートログ/On", true )]
        public static bool IsValidEnableImportLog()
        {
            return !ImportLog;
        }

        /// <summary>
        /// ログなし
        /// </summary>
        [MenuItem( "SpriteStudioForUGUI/インポートログ/Off", false, (int) Order.kImportLogOff )]
        public static void DisableImportLog()
        {
            ImportLog = false;
        }

        /// <summary>
        /// ログなし
        /// </summary>
        [MenuItem( "SpriteStudioForUGUI/インポートログ/Off", true )]
        public static bool IsValidDisableImportLog()
        {
            return ImportLog;
        }
        #endregion

        #region ログレベル

        /// <summary>
        /// 順番
        /// </summary>
        private enum LogLevelOrder
        {
            kInformation = Order.kLogLevelInfomation,
            kWarning = Order.kLogLevelWarning,
            kError = Order.kLogLevelError,
            kFatal = Order.kLogLevelFatal,
        }

        /// <summary>
        /// ログレベル
        /// </summary>
        public static Tracer.Level LogLevel
        {
            get { return Tracer.IntToLevel( EditorPrefs.GetInt( Tags.kLogLevel, Tracer.Level.kWarning.ToInt() ) ); }
            set { EditorPrefs.SetInt( Tags.kLogLevel, value.ToInt() ); }
        }

        /// <summary>
        /// レベルの上の線
        /// </summary>
        [MenuItem( "SpriteStudioForUGUI/インポートログ/", false, (int) Order.kLogLevel )]
        public static void LogLevelLine() { }

        /// <summary>
        /// レベルの順番
        /// </summary>
        [MenuItem( "SpriteStudioForUGUI/インポートログ/レベル/", true, (int) Order.kLogLevel )]
        public static void LogLevelsOrder() { }

        /// <summary>
        /// ログレベル：情報
        /// </summary>
        [MenuItem( "SpriteStudioForUGUI/インポートログ/レベル/情報", false, (int) LogLevelOrder.kInformation )]
        public static void LogLevelInformation()
        {
            LogLevel = Tracer.Level.kInfomation;
        }

        /// <summary>
        /// ログレベル：情報の有効状態
        /// </summary>
        [MenuItem( "SpriteStudioForUGUI/インポートログ/レベル/情報", true )]
        public static bool IsValidLogLevelInformation()
        {
            return LogLevel != Tracer.Level.kInfomation;
        }

        /// <summary>
        /// ログレベル：警告
        /// </summary>
        [MenuItem( "SpriteStudioForUGUI/インポートログ/レベル/警告", false, (int) LogLevelOrder.kWarning )]
        public static void LogLevelWarning()
        {
            LogLevel = Tracer.Level.kWarning;
        }

        /// <summary>
        /// ログレベル：警告の有効状態
        /// </summary>
        [MenuItem( "SpriteStudioForUGUI/インポートログ/レベル/警告", true )]
        public static bool IsValidLogLevelWarning()
        {
            return LogLevel != Tracer.Level.kWarning;
        }

        /// <summary>
        /// ログレベル：エラー
        /// </summary>
        [MenuItem( "SpriteStudioForUGUI/インポートログ/レベル/エラー", false, (int) LogLevelOrder.kError )]
        public static void LogLevelError()
        {
            LogLevel = Tracer.Level.kError;
        }

        /// <summary>
        /// ログレベル：エラーの有効状態
        /// </summary>
        [MenuItem( "SpriteStudioForUGUI/インポートログ/レベル/エラー", true )]
        public static bool IsValidLogLevelError()
        {
            return LogLevel != Tracer.Level.kError;
        }

        /// <summary>
        /// ログレベル：致命
        /// </summary>
        [MenuItem( "SpriteStudioForUGUI/インポートログ/レベル/致命的なエラー", false, (int) LogLevelOrder.kFatal )]
        public static void LogLevelFatal()
        {
            LogLevel = Tracer.Level.kFatal;
        }

        /// <summary>
        /// ログレベル：致命の有効状態
        /// </summary>
        [MenuItem( "SpriteStudioForUGUI/インポートログ/レベル/致命的なエラー", true )]
        public static bool IsValidLogLevelFatal()
        {
            return LogLevel != Tracer.Level.kFatal;
        }

        #endregion

        #region 出力先の指定
        /// <summary>
        /// 初期の出力先
        /// </summary>
        private const string kDefaultPath = "Assets/Exports/";

        /// <summary>
        /// 出力先
        /// </summary>
        public static string ExportPath
        {
            get
            {
                string path = EditorPrefs.GetString( Tags.kExportPath, kDefaultPath );
                if ( path.Length == 0 || path[path.Length - 1] != '/' ) {
                    path = path + '/';
                }
                return path;
            }
            set { EditorPrefs.SetString( Tags.kExportPath, value ); }
        }

        /// <summary>
        /// 出力先の指定
        /// </summary>
        [MenuItem( "SpriteStudioForUGUI/出力先の設定", false, (int) Order.kSetExportPath )]
        public static void SetExportPath()
        {
            string result = EditorUtility.OpenFolderPanel( "出力先", ExportPath, "" );
            if ( result != null ) {
                ExportPath = UnityEditor.FileUtil.GetProjectRelativePath( result );
                Selection.activeObject = null;
            }
        }

        #endregion
    }
}
