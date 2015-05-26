using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace a.spritestudio.editor.interpolater
{
    /// <summary>
    /// 補間
    /// </summary>
    public class Linear
        : Interpolater
    {
        /// <summary>
        /// 補間
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public override float Interpolate( float left, float right, float t )
        {
            return left + (right - left) * t;
        }
    }
}
