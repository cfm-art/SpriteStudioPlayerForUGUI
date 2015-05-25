using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml;

using a.spritestudio.editor.xml;
using a.spritestudio.types;

namespace a.spritestudio.editor
{
    /// <summary>
    /// プロジェクトファイルのインポート
    /// </summary>
    public class SSPJImporter
    {
        /// <summary>
        /// インポート結果
        /// </summary>
        public struct Information
        {
            public int fps;
            public int frameCount;
            public SortMode sortMode;
            public float pivotX;
            public float pivotY;
            public ReadOnlyCollection<string> cellMaps;
            public ReadOnlyCollection<string> animePacks;

            public override string ToString()
            {
                return string.Format( "fps={0},\nframe={1},\nsortMode={2},\npivot=[{3}, {4}],\ncellMaps=({5}),\nanimePacks=({6})",
                    fps, frameCount, sortMode, pivotX, pivotY,
                    string.Join( ";", cellMaps.ToArray() ),
                    string.Join( ";", animePacks.ToArray() ) );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public SSPJImporter()
        {
        }

        /// <summary>
        /// インポート
        /// </summary>
        /// <param name="fileName"></param>
        public Information Import( string fileName )
        {
            var xml = new XmlDocument();
            xml.Load( fileName );
            
            // 各種データを取得
            var animeSettings = NodeReader.findFirst( xml, "SpriteStudioProject/animeSettings" );
            int fps = animeSettings.AtInteger( "fps" );
            int frameCount = animeSettings.AtInteger( "frameCount" );
            string sortMode = animeSettings.AtText( "sortMode" );
            float[] pivot = animeSettings.AtFloats( "pivot", ' ' );

            var cellMaps = NodeReader.findFirst( xml, "SpriteStudioProject/cellmapNames" );
            var cellMapNames = cellMaps.Children( "value" ).AtText();

            var animePacks = NodeReader.findFirst( xml, "SpriteStudioProject/animepackNames" );
            var animePackNames = animePacks.Children( "value" ).AtText();

            return new Information() {
                fps = fps,
                frameCount = frameCount,
                sortMode = SortModeOpeartor.FromString( sortMode ),
                pivotX = pivot[0],
                pivotY = pivot[1],
                cellMaps = cellMapNames,
                animePacks = animePackNames,
            };
        }

        /// <summary>
        /// 移動
        /// </summary>
        /// <param name="oldFile"></param>
        /// <param name="newFile"></param>
        public void Move( string oldFile, string newFile )
        {
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="fileName"></param>
        public void Delete( string fileName )
        {
        }
    }
}
