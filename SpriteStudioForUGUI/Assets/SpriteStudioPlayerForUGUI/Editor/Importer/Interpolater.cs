﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace a.spritestudio.editor
{
    /// <summary>
    /// 補間
    /// </summary>
    public abstract class Interpolater
    {
        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<string, Interpolater> interpolater_ = new Dictionary<string, Interpolater>() {
            { "linear", new interpolater.Linear() }
        };

        /// <summary>
        /// 補間機の取得
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Interpolater GetInterpolater( string type )
        {
            Interpolater i;
            if ( interpolater_.TryGetValue( type.ToLower(), out i ) ) {
                return i;
            }
            return new interpolater.Linear();
        }

        /// <summary>
        /// 補間
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public abstract float Interpolate( float left, float right, float t );

        /// <summary>
        /// キーフレームを指定して補間
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="leftKey"></param>
        /// <param name="rightKey"></param>
        /// <param name="nowKey"></param>
        /// <returns></returns>
        public float Interpolate( float left, float right, int leftKey, int rightKey, int nowKey )
        {
            int diff = rightKey - leftKey;
            int t = nowKey - leftKey;
            return Interpolate( left, right, t / (float) diff );
        }

        /// <summary>
        /// 複数を纏めて補間
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="leftKey"></param>
        /// <param name="rightKey"></param>
        /// <param name="nowKey"></param>
        /// <returns></returns>
        public float[] Interpolate( float[] left, float[] right, int leftKey, int rightKey, int nowKey )
        {
            int diff = rightKey - leftKey;
            int t = nowKey - leftKey;

            float[] result = new float[left.Length];

            for ( int i = 0; i < left.Length; ++i ) {
                result[i] = Interpolate( left[i], right[i], t / (float) diff );
            }
            return result;
        }
    }
}
