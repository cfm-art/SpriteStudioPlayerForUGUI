using System.Collections.Generic;
using UnityEngine;

using a.spritestudio;

namespace a.spritestudio.editor
{
    /// <summary>
    /// .ssaeのインポートした情報をエンジン側のクラスへ変換する
    /// </summary>
    public class SSAEConverter
    {
        public List<GameObject> Convert( SSPJImporter.Information projectInformation, SSAEImporter.Information animationData, List<CellMap> cellMaps )
        {
            List<GameObject> results = new List<GameObject>( animationData.animations.Count );

            try {
                // 使用するセル
                List<CellMap> requiredCellMap = new List<CellMap>();
                foreach ( var cell in animationData.cellMapNames ) {
                    var cellName = System.IO.Path.GetFileNameWithoutExtension( cell );
                    var found = cellMaps.Find( ( o ) => o.name == cellName );
                    if ( found == null ) {
                        throw new System.ArgumentException( "Cell '" + cell + "' dose not find." );
                    }
                    Debug.Log( found );
                    requiredCellMap.Add( found );
                }

                // アニメーションの生成
                foreach ( var anime in animationData.animations ) {
                    var o = new GameObject( anime.name, typeof( SpriteRoot ) );
                    results.Add( o );

                    var root = o.GetComponent<SpriteRoot>();
                    root.SetupCellMaps( requiredCellMap );

                    SortedDictionary<int, GameObject> partsObjects = new SortedDictionary<int, GameObject>();

                    // パーツの生成
                    foreach ( var part in animationData.parts ) {
                        var p = new GameObject( part.name, typeof( SpritePart ) );
                        try {
                            var sp = p.GetComponent<SpritePart>();

                            // 親子の設定
                            if ( part.parent < 0 ) {
                                p.transform.SetParent( o.transform, false );
                            } else {
                                p.transform.SetParent( partsObjects[part.parent].transform, false );
                            }
                            partsObjects.Add( part.index, p );
                            sp.Setup( root );

                            // このパーツに対するアニメーションを探す
                            foreach ( var a in anime.parts ) {
                                if ( a.partName == part.name ) {
                                    MakeAttributes( sp, a );
                                    break;
                                }
                            }

                            // 0フレーム目で初期化
                            sp.SetFrame( 0 );
                        } catch {
                            // ヒエラルキーにGOが残らないようにする
                            GameObject.DestroyImmediate( p );
                            throw;
                        }
                    }
                }

                return results;
            } catch {
                // ヒエラルキーにGOが残らないようにする
                foreach ( var o in results ) {
                    GameObject.DestroyImmediate( o );
                }
                throw;
            }
        }

        /// <summary>
        /// キーフレーム生成
        /// </summary>
        /// <param name="part"></param>
        /// <param name="animation"></param>
        private void MakeAttributes( SpritePart part, SSAEImporter.PartAnimation animation )
        {
            foreach ( var attribute in animation.attributes ) {
                var generated = attribute.CreateKeyFrames( part, part.Root.TotalFrames );
                for ( int i = 0; i < generated.Count; ++i ) {
                    var key = generated[i];
                    if ( key != null ) {
                        part.AddKey( i, key );
                    }
                }
            }
        }
    }
}
