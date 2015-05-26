using UnityEngine;
using System.Collections.Generic;

namespace a.spritestudio
{
    /// <summary>
    /// 
    /// </summary>
    public class Utility
    {
        /// <summary>
        /// 3次元ベクトルのXだけ加算する
        /// </summary>
        /// <param name="original"></param>
        /// <param name="addition"></param>
        /// <returns></returns>
        public static Vector3 AppendX( Vector3 original, Vector2 addition )
        {
            return new Vector3( original.x + addition.x, original.y, original.z );
        }

        /// <summary>
        /// 3次元ベクトルのYだけ加算する
        /// </summary>
        /// <param name="original"></param>
        /// <param name="addition"></param>
        /// <returns></returns>
        public static Vector3 AppendY( Vector3 original, Vector2 addition )
        {
            return new Vector3( original.x, original.y + addition.y, original.z );
        }

        /// <summary>
        /// 3次元ベクトルのXYを加算する
        /// </summary>
        /// <param name="original"></param>
        /// <param name="addition"></param>
        /// <returns></returns>
        public static Vector3 AppendXY( Vector3 original, Vector2 addition )
        {
            return new Vector3( original.x + addition.x, original.y + addition.y, original.z );
        }
    }
}
