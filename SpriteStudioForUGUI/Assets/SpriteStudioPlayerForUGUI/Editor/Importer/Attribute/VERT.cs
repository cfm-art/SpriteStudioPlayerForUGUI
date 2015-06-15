using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace a.spritestudio.editor.attribute
{
    public class VERT
        : SpriteAttribute
    {
        /// <summary>
        /// キーフレームの中身
        /// </summary>
        private class Value
            : SpriteAttribute.ValueBase
        {
            public float[] lt;
            public float[] rt;
            public float[] lb;
            public float[] rb;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="part"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override spritestudio.attribute.AttributeBase CreateKeyFrame( SpritePart part, SpriteAttribute.ValueBase value )
        {
            Value v = (Value) value;
            return spritestudio.attribute.VertexUpdater.Create( v.lt, v.rt, v.lb, v.rb );
        }

        /// <summary>
        /// キーフレーム生成
        /// </summary>
        /// <param name="key"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override SpriteAttribute.ValueBase CraeteValue( xml.NodeReader key, xml.NodeReader node )
        {
            return new Value() {
                lt = node.AtFloats( "LT", ' ' ),
                rt = node.AtFloats( "RT", ' ' ),
                lb = node.AtFloats( "LB", ' ' ),
                rb = node.AtFloats( "RB", ' ' ),
            };
        }

        /// <summary>
        /// 補間有り
        /// </summary>
        public override bool IsInterpolation
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// 補間
        /// </summary>
        /// <param name="left"></param>
        /// <param name="leftKey"></param>
        /// <param name="right"></param>
        /// <param name="rightKey"></param>
        /// <returns></returns>
        protected override ReadOnlyCollection<ValueBase> Interpolate(
            ValueBase left, int leftKey, ValueBase right, int rightKey )
        {
            Value leftValue = (Value) left;
            Value rightValue = (Value) right;

            int count = rightKey - leftKey - 1;
            List<ValueBase> results = new List<ValueBase>( count );

            // 間のフレームの補間を生み出す
            Interpolater interporator = Interpolater.GetInterpolater( "linear" );
            for ( int i = 0; i < count; ++i ) {
                float[] lt = interporator.Interpolate( leftValue.lt, rightValue.lt, leftKey, rightKey, i + leftKey + 1 );
                float[] rt = interporator.Interpolate( leftValue.rt, rightValue.rt, leftKey, rightKey, i + leftKey + 1 );
                float[] lb = interporator.Interpolate( leftValue.lb, rightValue.lb, leftKey, rightKey, i + leftKey + 1 );
                float[] rb = interporator.Interpolate( leftValue.rb, rightValue.rb, leftKey, rightKey, i + leftKey + 1 );
                results.Add( new Value() {
                    lt = lt,
                    rt = rt,
                    lb = lb,
                    rb = rb
                } );
            }
            return results.AsReadOnly();
        }
    }
}
