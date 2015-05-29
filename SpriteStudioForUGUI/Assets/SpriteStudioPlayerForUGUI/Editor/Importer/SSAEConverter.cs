﻿using System.Collections.Generic;
using UnityEngine;

using a.spritestudio;

namespace a.spritestudio.editor
{
    /// <summary>
    /// .ssaeのインポートした情報をエンジン側のクラスへ変換する
    /// </summary>
    public class SSAEConverter
    {
        public class Result
        {
            public List<GameObject> animations;
        }

        /// <summary>
        /// ssaeのパース結果を変換して、GameObjectを生成
        /// </summary>
        /// <param name="projectInformation"></param>
        /// <param name="animationData"></param>
        /// <param name="cellMaps"></param>
        /// <param name="materials"></param>
        /// <returns></returns>
        public Result Convert( SSPJImporter.Information projectInformation,
                SSAEImporter.Information animationData,
                List<CellMap> cellMaps,
                Dictionary<types.AlphaBlendType, Material> materials )
        {
            List<GameObject> results = new List<GameObject>( animationData.animations.Count );

            try {
                // 使用するセル
                List<CellMap> requiredCellMap = new List<CellMap>();
                foreach ( var cell in animationData.cellMapNames ) {
                    var cellName = System.IO.Path.GetFileNameWithoutExtension( cell );
                    var found = cellMaps.Find( ( o ) => o.name == cellName );
                    if ( found == null ) {
                        foreach ( var o in cellMaps ) {
                            UnityEngine.Debug.Log( o.name );
                        }
                        throw new System.ArgumentException( "Cell '" + cell + "' dose not find." );
                    }
                    Tracer.Log( found );
                    requiredCellMap.Add( found );
                }

                // アニメーションの生成
                foreach ( var anime in animationData.animations ) {
                    var o = new GameObject( anime.name, typeof( SpriteRoot ), typeof( RectTransform ) );
                    results.Add( o );

                    var root = o.GetComponent<SpriteRoot>();
                    InitializeRoot( root, projectInformation, anime.settings, requiredCellMap );

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

                            // パーツの初期化
                            Material material;
                            materials.TryGetValue( part.blendType, out material );
                            sp.Setup( root, part.type, material );

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

                return new Result() {
                    animations = results,
                };
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

        /// <summary>
        /// ルートの初期化
        /// </summary>
        /// <param name="root"></param>
        /// <param name="information"></param>
        /// <param name="settings"></param>
        /// <param name="requiredCellMap"></param>
        private static void InitializeRoot( SpriteRoot root, SSPJImporter.Information information,
           SSAEImporter.OverrideSettings settings,
            List<CellMap> requiredCellMap )
        {
            root.Setup( Pick( information.fps, settings.fps ),
                    Pick( information.frameCount, settings.frameCount ),
                    Pick( information.pivotX, settings.pivotX ),
                    Pick( information.pivotY, settings.pivotY ) );
            root.SetupSpriteHolder();
            root.SetupCellMaps( requiredCellMap );
        }

        /// <summary>
        /// Nullableのうち有効なものから1つ取り出す
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="defaultValue"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        private static T Pick<T>( T defaultValue, params T?[] values )
            where T : struct
        {
            for ( int i = 0; i < values.Length; ++i ) {
                T? t = values[i];
                if ( t.HasValue ) {
                    return t.Value;
                }
            }
            return defaultValue;
        }
    }
}
