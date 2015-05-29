using System.Xml;

namespace a.spritestudio.editor.xml
{
    /// <summary>
    /// XMLの属性の読み込み
    /// </summary>
    public class AttributeReader
    {
        /// <summary>
        /// 
        /// </summary>
        private XmlAttribute attribute_;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attribute"></param>
        public AttributeReader( XmlAttribute attribute )
        {
            if ( attribute == null ) { throw new System.NullReferenceException( "attribute is null." ); }
            attribute_ = attribute;
        }

        /// <summary>
        /// intで取得
        /// </summary>
        /// <returns></returns>
        public int AtInteger()
        {
            int result;
            int.TryParse( attribute_.Value, out result );
            return result;
        }

        /// <summary>
        /// floatで取得
        /// </summary>
        /// <returns></returns>
        public float AtFloat()
        {
            float result;
            float.TryParse( attribute_.Value, out result );
            return result;
        }

        /// <summary>
        /// boolで取得
        /// </summary>
        /// <returns></returns>
        public bool AtBoolean()
        {
            return NodeReader.IsTrue( attribute_.Value );
        }

        /// <summary>
        /// stringで取得
        /// </summary>
        /// <returns></returns>
        public string AtText()
        {
            return attribute_.Value;
        }
    }
}
