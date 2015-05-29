using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;

namespace a.spritestudio.editor.xml
{
    /// <summary>
    /// ノードの配列
    /// </summary>
    public class NodeListReader
    {
        private XmlNodeList nodes_;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodes"></param>
        public NodeListReader( XmlNodeList nodes )
        {
            if ( nodes == null ) { throw new System.NullReferenceException( "nodes is null." ); }
            nodes_ = nodes;
        }

        /// <summary>
        /// intで取得
        /// </summary>
        /// <returns></returns>
        public ReadOnlyCollection<int> AtInteger()
        {
            List<int> results = new List<int>();
            foreach ( XmlNode node in nodes_ ) {
                results.Add( new NodeReader( node ).AtInteger() );
            }
            return results.AsReadOnly();
        }

        /// <summary>
        /// intの配列で取得
        /// </summary>
        /// <param name="separator"></param>
        /// <returns></returns>
        public ReadOnlyCollection<int[]> AtIntegers( char separator )
        {
            List<int[]> results = new List<int[]>();
            foreach ( XmlNode node in nodes_ ) {
                results.Add( new NodeReader( node ).AtIntegers( separator ) );
            }
            return results.AsReadOnly();
        }

        /// <summary>
        /// floatで取得
        /// </summary>
        /// <returns></returns>
        public ReadOnlyCollection<float> AtFloat()
        {
            List<float> results = new List<float>();
            foreach ( XmlNode node in nodes_ ) {
                results.Add( new NodeReader( node ).AtFloat() );
            }
            return results.AsReadOnly();
        }

        /// <summary>
        /// floatの配列で取得
        /// </summary>
        /// <param name="separator"></param>
        /// <returns></returns>
        public ReadOnlyCollection<float[]> AtFloats( char separator )
        {
            List<float[]> results = new List<float[]>();
            foreach ( XmlNode node in nodes_ ) {
                results.Add( new NodeReader( node ).AtFloats( separator ) );
            }
            return results.AsReadOnly();
        }

        /// <summary>
        /// boolで取得
        /// </summary>
        /// <returns></returns>
        public ReadOnlyCollection<bool> AtBoolean()
        {
            List<bool> results = new List<bool>();
            foreach ( XmlNode node in nodes_ ) {
                results.Add( new NodeReader( node ).AtBoolean() );
            }
            return results.AsReadOnly();
        }

        /// <summary>
        /// boolの配列で取得
        /// </summary>
        /// <param name="separator"></param>
        /// <returns></returns>
        public ReadOnlyCollection<bool[]> AtBooleans( char separator )
        {
            List<bool[]> results = new List<bool[]>();
            foreach ( XmlNode node in nodes_ ) {
                results.Add( new NodeReader( node ).AtBooleans( separator ) );
            }
            return results.AsReadOnly();
        }

        /// <summary>
        /// 文字列で取得
        /// </summary>
        /// <returns></returns>
        public ReadOnlyCollection<string> AtText()
        {
            List<string> results = new List<string>();
            foreach ( XmlNode node in nodes_ ) {
                results.Add( new NodeReader( node ).AtText() );
            }
            return results.AsReadOnly();
        }

        /// <summary>
        /// 文字列の配列で取得
        /// </summary>
        /// <param name="separator"></param>
        /// <returns></returns>
        public ReadOnlyCollection<string[]> AtTexts( char separator )
        {
            List<string[]> results = new List<string[]>();
            foreach ( XmlNode node in nodes_ ) {
                results.Add( new NodeReader( node ).AtTexts( separator ) );
            }
            return results.AsReadOnly();
        }

        /// <summary>
        /// ノード全て取得
        /// </summary>
        public ReadOnlyCollection<NodeReader> Nodes
        {
            get {
                List<NodeReader> result = new List<NodeReader>();
                foreach ( XmlNode node in nodes_ ) {
                    result.Add( new NodeReader( node ) );
                }
                return result.AsReadOnly();
            }
        }

        /// <summary>
        /// foreach用
        /// </summary>
        /// <returns></returns>
        public IEnumerator<NodeReader> GetEnumerator()
        {
            return Nodes.GetEnumerator();
            
        }
    }
}
