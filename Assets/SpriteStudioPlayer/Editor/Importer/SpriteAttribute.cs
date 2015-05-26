using System.Collections.Generic;
using System.Collections.ObjectModel;
using a.spritestudio.editor.xml;

namespace a.spritestudio.editor
{
    /// <summary>
    /// アニメーション情報
    /// </summary>
    public abstract class SpriteAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public abstract class ValueBase
        {
        }

        /// <summary>
        /// キーフレーム
        /// </summary>
        private SortedDictionary<int, ValueBase> values_;

        /// <summary>
        /// 
        /// </summary>
        public SpriteAttribute()
        {
            values_ = new SortedDictionary<int, ValueBase>();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="node"></param>
        public void Setup( NodeReader root )
        {
            foreach ( var node in root.Children( "key" ) ) {
                var key = node.Attribute( "time" ).AtInteger();
                var value = node.Child( "value" );
                try {
                    values_.Add( key, CraeteValue( node, value ) );
                } catch {
                    UnityEngine.Debug.Log( "error occurs:" + GetType().Name + "\n" + root.Raw.InnerXml );
                    throw;
                }
            }
        }

        /// <summary>
        /// キーフレーム生成
        /// </summary>
        /// <param name="key">キー</param>
        /// <param name="node">value</param>
        protected abstract ValueBase CraeteValue( NodeReader key, NodeReader node );

        /// <summary>
        /// 補間の有無
        /// </summary>
        protected virtual bool IsInterpolation { get { return true; } }

        /// <summary>
        /// 自分と次のキーの間を補間
        /// </summary>
        /// <param name="next"></param>
        /// <returns></returns>
        protected virtual ReadOnlyCollection<SpriteAttribute> Interpolate( SpriteAttribute next )
        {
            return null;
        }

        /// <summary>
        /// エンジン側のキーフレームを生成する
        /// </summary>
        /// <returns></returns>
        //public abstract 未定 CreateKeyFrame();

        public override string ToString()
        {
            var text = new System.Text.StringBuilder();
            text.Append( GetType().Name );
            text.Append( ":\n" );

            foreach ( var o in values_ ) {
                text.Append( o.Key );
                text.Append( " = {" );
                text.Append( o.Value.ToString() );
                text.Append( "}\n" );
            }

            return text.ToString();
        }
    }
}
