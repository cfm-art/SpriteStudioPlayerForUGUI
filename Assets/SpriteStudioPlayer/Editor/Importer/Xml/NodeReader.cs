using System.Xml;

namespace a.spritestudio.editor.xml
{
    /// <summary>
    /// ノードのデータ取り出し
    /// </summary>
    public class NodeReader
    {
        /// <summary>
        /// 
        /// </summary>
        private XmlNode node_;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public NodeReader( XmlNode node )
        {
            if ( node == null ) { throw new System.NullReferenceException( "node is null." ); }
            node_ = node;
        }

        /// <summary>
        /// ノードの検索
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static NodeReader findFirst( XmlDocument xml, string tag )
        {
            var node = xml.SelectSingleNode( tag );
            return node != null ? new NodeReader( node ) : null;
        }

        /// <summary>
        /// 子供を1つ取得
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public NodeReader Child( string tag )
        {
            return new NodeReader( node_.SelectSingleNode( tag ) );
        }

        /// <summary>
        /// 子供を全て取得
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public NodeListReader Children( string tag )
        {
            return new NodeListReader( node_.SelectNodes( tag ) );
        }

        /// <summary>
        /// 同階層の次のノード
        /// </summary>
        /// <returns></returns>
        public NodeReader Next()
        {
            return new NodeReader( node_.NextSibling );
        }

        /// <summary>
        /// intで取得
        /// </summary>
        /// <returns></returns>
        public int AtInteger()
        {
            int result;
            int.TryParse( node_.InnerText, out result );
            return result;
        }

        /// <summary>
        /// 子供からintで取得
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public int AtInteger( string tag )
        {
            return Child( tag ).AtInteger();
        }

        /// <summary>
        /// intの配列で取得
        /// </summary>
        /// <param name="separator"></param>
        /// <returns></returns>
        public int[] AtIntegers( char separator )
        {
            string v = node_.InnerText;
            string[] tokens = v.Split( separator );
            int[] results = new int[tokens.Length];

            for ( int i = 0; i < tokens.Length; ++i ) {
                int.TryParse( tokens[i], out results[i] );
            }
            return results;
        }

        /// <summary>
        /// 子供からintの配列で取得
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public int[] AtIntegers( string tag, char separator )
        {
            return Child( tag ).AtIntegers( separator );
        }

        /// <summary>
        /// floatで取得
        /// </summary>
        /// <returns></returns>
        public float AtFloat()
        {
            float result;
            float.TryParse( node_.InnerText, out result );
            return result;
        }

        /// <summary>
        /// 子供からfloatで取得
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public float AtFloat( string tag )
        {
            return Child( tag ).AtFloat();
        }

        /// <summary>
        /// floatの配列で取得
        /// </summary>
        /// <param name="separator"></param>
        /// <returns></returns>
        public float[] AtFloats( char separator )
        {
            string v = node_.InnerText;
            string[] tokens = v.Split( separator );
            float[] results = new float[tokens.Length];

            for ( int i = 0; i < tokens.Length; ++i ) {
                float.TryParse( tokens[i], out results[i] );
            }
            return results;
        }

        /// <summary>
        /// 子供からfloatの配列で取得
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public float[] AtFloats( string tag, char separator )
        {
            return Child( tag ).AtFloats( separator );
        }

        /// <summary>
        /// boolで取得
        /// </summary>
        /// <returns></returns>
        public bool AtBoolean()
        {
            string v = node_.InnerText;
            return IsTrue( v );
        }

        /// <summary>
        /// 子供からboolで取得
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public bool AtBoolean( string tag )
        {
            return Child( tag ).AtBoolean();
        }

        /// <summary>
        /// boolの配列で取得
        /// </summary>
        /// <param name="separator"></param>
        /// <returns></returns>
        public bool[] AtBooleans( char separator )
        {
            string v = node_.InnerText;
            string[] tokens = v.Split( separator );
            bool[] results = new bool[tokens.Length];
            for ( int i = 0; i < tokens.Length; ++i ) {
                results[i] = IsTrue( tokens[i] );
            }
            return results;
        }

        /// <summary>
        /// boolの配列で取得
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public bool[] AtBooleans( string tag, char separator )
        {
            return Child( tag ).AtBooleans( separator );
        }

        /// <summary>
        /// 文字列がtrue相当か判定
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        private static bool IsTrue( string v )
        {
            if ( v == null ) { return false; }
            if ( "true".Equals( v ) ) { return true; }
            if ( "1".Equals( v ) ) { return true; }
            return false;
        }

        /// <summary>
        /// 文字列で取得
        /// </summary>
        /// <returns></returns>
        public string AtText()
        {
            return node_.InnerText;
        }

        /// <summary>
        /// 文字列で取得
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public string AtText( string tag )
        {
            return Child( tag ).AtText();
        }

        /// <summary>
        /// 文字列の配列で取得
        /// </summary>
        /// <param name="separator"></param>
        /// <returns></returns>
        public string[] AtTexts( char separator )
        {
            return node_.InnerText.Split( separator );
        }

        /// <summary>
        /// 文字列の配列で取得
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public string[] AtTexts( string tag, char separator )
        {
            return Child( tag ).AtTexts( separator );
        }
    }
}
