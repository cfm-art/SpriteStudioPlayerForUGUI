using System.Xml;

using a.spritestudio.editor.xml;

namespace a.spritestudio.editor
{
    /// <summary>
    /// .ssaeのインポート
    /// </summary>
    public class SSAEImporter
    {
        /// <summary>
        /// 
        /// </summary>
        public SSAEImporter()
        {
        }

        /// <summary>
        /// インポート
        /// </summary>
        /// <param name="fil"></param>
        public void Import( string fileName )
        {
            var xml = new XmlDocument();
            xml.Load( fileName );


        }
    }
}
