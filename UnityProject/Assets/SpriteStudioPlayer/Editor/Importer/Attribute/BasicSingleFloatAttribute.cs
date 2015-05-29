using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace a.spritestudio.editor.attribute
{
    public class BasicSingleFloatAttribute
        : SpriteAttribute
    {
        /// <summary>
        /// キーフレームの中身
        /// </summary>
        protected class Value
            : SpriteAttribute.ValueBase
        {
            public string ipType;
            public float value;

            public override string ToString()
            {
                return string.Format( "ipType={0}, value={1}", ipType, value );
            }

            /// <summary>
            /// 同設定か
            /// </summary>
            /// <param name="v"></param>
            /// <returns></returns>
            public override bool IsSameValue( ValueBase v )
            {
                float diff = System.Math.Abs( value - ((Value) v).value );
                return diff < float.Epsilon;
            }
        }

        /// <summary>
        /// キーフレーム生成
        /// </summary>
        /// <param name="key"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override SpriteAttribute.ValueBase CraeteValue( xml.NodeReader key, xml.NodeReader node )
        {
            var ipType = key.Attribute( "ipType" );
            return new Value() {
                ipType = ipType != null ? ipType.AtText() : "linear",
                value = node.AtFloat()
            };
        }

        /// <summary>
        /// 補間生成
        /// </summary>
        /// <param name="left"></param>
        /// <param name="leftKey"></param>
        /// <param name="right"></param>
        /// <param name="rightKey"></param>
        /// <returns></returns>
        protected override ReadOnlyCollection<ValueBase> Interpolate(
                ValueBase left, int leftKey,
                ValueBase right, int rightKey )
        {
            Value leftValue = (Value) left;
            Value rightValue = (Value) right;

            int count = rightKey - leftKey - 1;
            List<ValueBase> results = new List<ValueBase>( count );

            // 間のフレームの補間を生み出す
            Interpolater interporator = Interpolater.GetInterpolater( leftValue.ipType );
            for ( int i = 0; i < count; ++i ) {
                float value = interporator.Interpolate(
                        leftValue.value, rightValue.value,
                        leftKey, rightKey, i + leftKey + 1 );
                results.Add( new Value() {
                    ipType = leftValue.ipType,
                    value = value,
                } );
            }
            return results.AsReadOnly();
        }
    }
}
