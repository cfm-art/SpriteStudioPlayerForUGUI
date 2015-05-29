using System.Linq;
using System.Xml;
using System.Collections.ObjectModel;

using a.spritestudio.editor.xml;

namespace a.spritestudio.editor
{
    /// <summary>
    /// .ssceのインポート
    /// </summary>
    public class SSCEImporter
    {
        /// <summary>
        /// セルマップの情報
        /// </summary>
        public class Cell
        {
            public readonly string name;
            public readonly float[] uv;
            public readonly float[] pivot;
            public readonly bool rotated;
            public readonly int[] size;

            public Cell( NodeReader node, int textureWidth, int textureHeight )
            {
                name = node.AtText( "name" );
                int[] pos = node.AtIntegers( "pos", ' ' );
                size = node.AtIntegers( "size", ' ' );
                pivot = node.AtFloats( "pivot", ' ' );
                rotated = node.AtBoolean( "rotated" );

                float s = pos[0] / (float) textureWidth;
                float t = 1f - (pos[1] / (float) textureHeight);
                float u = s + (size[0] / (float) textureWidth);
                float v = t - (size[1] / (float) textureHeight);
                uv = new float[4] { s, v, u, t };
            }

            public override string ToString()
            {
                return string.Format( "name={0}, uv=[{1},{2},{3},{4}], pivot=[{5},{6}], rotated={7}",
                    name, uv[0], uv[1], uv[2], uv[3],
                    pivot[0], pivot[1], rotated );
            }
        }

        /// <summary>
        /// インポート結果
        /// </summary>
        public struct Information
        {
            public string name;
            public string imagePath;
            public UnityEngine.TextureWrapMode wrapMode;
            public UnityEngine.FilterMode filterMode;
            public ReadOnlyCollection<Cell> cellMaps;

            public override string ToString()
            {
                return string.Format( "name={0},\nimagePath={1},\nwrap={2},\nfilter={3},\ncellmaps=(\n{4}\n)",
                    name, imagePath,
                    wrapMode, filterMode,
                    string.Join( ";\n", (from c in cellMaps select "\t{" + c.ToString() + "}").ToArray() ) );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public SSCEImporter()
        {
        }

        /// <summary>
        /// インポート
        /// </summary>
        /// <param name="fil"></param>
        public Information Import( string fileName )
        {
            var xml = new XmlDocument();
            xml.Load( fileName );

            // 基本設定
            string name = NodeReader.findFirst( xml, "SpriteStudioCellMap/name" ).AtText();
            string imagePath = NodeReader.findFirst( xml, "SpriteStudioCellMap/imagePath" ).AtText();
            string wrapMode = NodeReader.findFirst( xml, "SpriteStudioCellMap/wrapMode" ).AtText();
            string filterMode = NodeReader.findFirst( xml, "SpriteStudioCellMap/filterMode" ).AtText();
            int[] pixelSize = NodeReader.findFirst( xml, "SpriteStudioCellMap/pixelSize" ).AtIntegers( ' ' );

            // セル情報
            var cells = NodeReader.findFirst( xml, "SpriteStudioCellMap/cells" ).Children( "cell" );
            var convertedCells = from o in cells.Nodes select new Cell( o, pixelSize[0], pixelSize[1] );

            return new Information() {
                name = name,
                imagePath = imagePath,
                wrapMode = convertWrapMode( wrapMode ),
                filterMode = convertFilterMode( filterMode ),
                cellMaps = convertedCells.ToList().AsReadOnly(),
            };
        }

        /// <summary>
        /// WrapModeの解決
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        private static UnityEngine.TextureWrapMode convertWrapMode( string mode )
        {
            switch ( mode ) {
                case "clamp":
                    return UnityEngine.TextureWrapMode.Clamp;
                case "loop":
                    return UnityEngine.TextureWrapMode.Repeat;
                case "once":
                    return UnityEngine.TextureWrapMode.Repeat;
                default:
                    return UnityEngine.TextureWrapMode.Clamp;
            }
        }

        /// <summary>
        /// FilterModeの解決
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        private static UnityEngine.FilterMode convertFilterMode( string mode )
        {
            switch ( mode ) {
                case "linear":
                    return UnityEngine.FilterMode.Bilinear;
                case "point":
                    return UnityEngine.FilterMode.Point;
                default:
                    return UnityEngine.FilterMode.Point;
            }
        }
    }
}
