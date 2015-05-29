using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace a.spritestudio.editor
{
    /// <summary>
    /// SSPJをインポートするためのアセット
    /// </summary>
    public class SSPJImportTool
        : ScriptableObject
    {
        /// <summary>
        /// sspjに登録されているアニメーション一覧
        /// </summary>
        [System.Serializable]
        public class TargetAnimation
        {
            /// <summary>
            /// 
            /// </summary>
            [SerializeField]
            private string name_;

            /// <summary>
            /// ssaeのファイル名
            /// </summary>
            [SerializeField]
            private string file_;

            /// <summary>
            /// ssae内のアニメーション名
            /// </summary>
            [SerializeField]
            private string animation_;

            /// <summary>
            /// インポートするかどうか
            /// </summary>
            [SerializeField]
            private bool isImport_;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="name"></param>
            /// <param name="animation"></param>
            internal TargetAnimation( string name, string animation )
            {
                name_ = name + "/" + animation;
                file_ = name;
                animation_ = animation;
                isImport_ = false;
            }

            /// <summary>
            /// 
            /// </summary>
            internal string File
            {
                get { return file_; }
            }

            /// <summary>
            /// 
            /// </summary>
            internal string Animation
            {
                get { return animation_; }
            }

            /// <summary>
            /// 
            /// </summary>
            internal bool IsImport
            {
                get { return isImport_; }
                set { isImport_ = value; }
            }
        }

        /// <summary>
        /// ファイル名
        /// </summary>
        [SerializeField]
        private string fileName_;

        /// <summary>
        /// セルマップ名
        /// </summary>
        [SerializeField]
        private List<string> cellMaps_;

        /// <summary>
        /// アニメーションたち
        /// </summary>
        [SerializeField]
        private List<TargetAnimation> animations_;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static SSPJImportTool Create( string name )
        {
            var self = ScriptableObject.CreateInstance<SSPJImportTool>();
            self.name = name;
            self.fileName_ = name;
            self.cellMaps_ = new List<string>();
            self.animations_ = new List<TargetAnimation>();
            return self;
        }

        /// <summary>
        /// セル追加
        /// </summary>
        /// <param name="name"></param>
        public void AddCell( string name )
        {
            cellMaps_.Add( name );
        }

        /// <summary>
        /// アニメーション追加
        /// </summary>
        /// <param name="name"></param>
        /// <param name="animation"></param>
        public void AddAnimation( string name, string animation )
        {
            animations_.Add( new TargetAnimation( name, animation ) );
        }

        /// <summary>
        /// ファイルのフルパス
        /// </summary>
        public string FullPath
        {
            get {
                return string.Format( "{0}/{1}.sspj",
                    System.IO.Path.GetDirectoryName( UnityEditor.AssetDatabase.GetAssetPath( this ) ),
                    FileName );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string FileName
        {
            get { return fileName_; }
        }

        /// <summary>
        /// 
        /// </summary>
        public ReadOnlyCollection<string> CellMaps
        {
            get { return cellMaps_.AsReadOnly(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public ReadOnlyCollection<TargetAnimation> Animations
        {
            get { return animations_.AsReadOnly(); }
        }

        /// <summary>
        /// インポート対象
        /// </summary>
        public TargetAnimation[] Targets
        {
            get
            {
                return (from x in animations_ where x.IsImport select x).ToArray();
            }
        }
    }
}
