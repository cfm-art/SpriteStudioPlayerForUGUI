using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using a.spritestudio.editor.xml;
using a.spritestudio.attribute;

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
            /// <summary>
            /// 同じ数値か
            /// </summary>
            /// <param name="v"></param>
            /// <returns></returns>
            public virtual bool IsSameValue( ValueBase v )
            {
                return false;
            }
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
                    Tracer.Log( "error occurs:" + GetType().Name + "\n" + root.Raw.InnerXml );
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
        public virtual bool IsInterpolation { get { return true; } }

        /// <summary>
        /// キー間の補間
        /// </summary>
        /// <param name="left"></param>
        /// <param name="leftKey"></param>
        /// <param name="right"></param>
        /// <param name="rightKey"></param>
        /// <returns></returns>
        protected virtual ReadOnlyCollection<ValueBase> Interpolate( ValueBase left, int leftKey, ValueBase right, int rightKey )
        {
            return null;
        }

        /// <summary>
        /// エンジン側のキーフレームを生成する
        /// </summary>
        /// <param name="part">対象パーツ</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual AttributeBase CreateKeyFrame( SpritePart part, ValueBase value )
        {
            throw new System.NotImplementedException( GetType().Name );
        }

        /// <summary>
        /// エンジン側のキーフレームを全て生成する
        /// </summary>
        /// <param name="totalFrames"></param>
        /// <returns></returns>
        public ReadOnlyCollection<AttributeBase> CreateKeyFrames( SpritePart part, int totalFrames )
        {
            List<ValueBase> results = new List<ValueBase>( new ValueBase[totalFrames] );
            int finalIndex = -1;
            foreach ( var value in values_ ) {
                if ( results.Count < value.Key + 1 ) {
                    ExpandList( results, value.Key + 1 );
                }
                results[value.Key] = value.Value;
                if ( finalIndex < value.Key ) {
                    finalIndex = value.Key;
                }
            }
            if ( IsInterpolation ) {
                // 補間フレームの生成
                for ( int i = 0; i < totalFrames - 1; ++i ) {
                    if ( results[i] != null && results[i + 1] == null ) {
                        // iがnullでなくて、次のフレームが飛んでいる場合は補間対象
                        bool isInterpolated = false;
                        for ( int j = i + 2; j < totalFrames; ++j ) {
                            if ( results[j] != null ) {
                                // 補間フレームを生成する
                                var interpolated = Interpolate( results[i], i, results[j], j );
                                for ( int k = 1; k <= interpolated.Count; ++k ) {
                                    results[i + k] = interpolated[k - 1];
                                }

                                // 次はこのフレームから補間できないか調べる
                                i = j - 1;
                                isInterpolated = true;
                                break;
                            }
                        }
                        if ( !isInterpolated ) {
                            // 次フレームがもう無いので終わり
                            break;
                        }
                    }
                }
            }
            // 最終フレームは入れておく
            results[totalFrames - 1] = results[finalIndex];

            // 同一数値のキーを間引く
            for ( int i = 0; i < results.Count - 2; ++i ) {
                if ( results[i] == null ) { continue; }
                for ( int j = i + 1; j < results.Count - 1; ++j ) {
                    if ( results[j] == null ) { continue; }
                    if ( results[i].IsSameValue( results[j] ) ) {
                        results[j] = null;
                    } else {
                        i = j - 1;
                        break;
                    }
                }
            }

            // エンジン側のクラスに一括変換
            return (from o in results.AsReadOnly() select o != null ? CreateKeyFrame( part, o ) : null).ToList().AsReadOnly();
        }

        /// <summary>
        /// リストの大きさを大きくする
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="size"></param>
        private static void ExpandList<T>( List<T> list, int size )
        {
            while ( list.Count < size ) {
                list.Add( default( T ) );
            }
        }

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
