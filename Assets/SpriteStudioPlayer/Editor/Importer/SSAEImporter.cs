using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Xml;

using a.spritestudio.editor.xml;
using a.spritestudio.types;

namespace a.spritestudio.editor
{
    /// <summary>
    /// .ssaeのインポート
    /// </summary>
    public class SSAEImporter
    {
        /// <summary>
        /// Settingsの上書き
        /// </summary>
        public struct OverrideSettings
        {
            public int? fps;
            public int? frameCount;
            public SortMode? sortMode;
            public float? pivotX;
            public float? pivotY;

            public override string ToString()
            {
                return string.Format( "fps={0}, frameCount={1}, sortMode={2}, pivot=[{3},{4}]",
                    fps.HasValue ? fps.Value.ToString() : "none",
                    frameCount.HasValue ? frameCount.Value.ToString() : "none",
                    sortMode.HasValue ? sortMode.Value.ToString() : "none",
                    pivotX.HasValue ? pivotX.Value.ToString() : "none",
                    pivotY.HasValue ? pivotY.Value.ToString() : "none" );
            }
        }

        /// <summary>
        /// 各パーツの情報
        /// </summary>
        public class Part
        {
            public string name;
            public int index;
            public int parent;
            public NodeType type;
            public BoundsType boundsType;   // 謎
            public InheritType inheritType; // 謎
            //謎：ineheritRates
            public AlphaBlendType blendType;
            public bool show;
            public string expandAttribute;  // 謎
            public string expandChildren;   // 謎

            public Part( NodeReader node )
            {
                name = node.AtText( "name" );
                index = node.AtInteger( "arrayIndex" );
                parent = node.AtInteger( "parentIndex" );
                type = NodeTypeOperator.FromString( node.AtText( "type" ) );
                boundsType = BoundsTypeOperator.FromString( node.AtText( "boundsType" ) );
                inheritType = InheritTypeOperator.FromString( node.AtText( "inheritType" ) );
                //inheritRates
                blendType = AlphaBlendTypeOperator.FromString( node.AtText( "alphaBlendType" ) );
                show = node.AtBoolean( "show" );
                expandAttribute = node.AtText( "expandAttribute" );
                expandChildren = node.AtText( "expandChildren" );
            }

            public override string ToString()
            {
                return string.Format( "name={0}, index={1}, parent={2}, type={3}, boundsType={4}, inheritType={5}, blendType={6}, show={7}, expandAttribute={8}, expandChildren={9}",
                    name, index, parent, type,
                    boundsType, inheritType, blendType,
                    show, expandAttribute, expandChildren );
            }
        }

        /// <summary>
        /// 各パーツのアニメーション情報
        /// </summary>
        public class PartAnimation
        {
            public string partName;

            public ReadOnlyCollection<SpriteAttribute> attributes;

            public PartAnimation( NodeReader node )
            {
                partName = node.AtText( "partName" );
                var children = node.Child( "attributes" ).Children( "attribute" );

                var results = new List<SpriteAttribute>();
                foreach ( var child in children ) {
                    string tag = child.Attribute( "tag" ).AtText();
                    var targetType = Type.GetType( "a.spritestudio.editor.attribute." + tag );
                    var attribute = (SpriteAttribute) Activator.CreateInstance( targetType );
                    attribute.Setup( child );
                    results.Add( attribute );
                }
                attributes = results.AsReadOnly();
            }

            public override string ToString()
            {
                return string.Format( "partName={0}, attributes={1}",
                    partName,
                    string.Join( ";\n", (from o in attributes select "\t{" + o.ToString() + "}").ToArray() ) );
            }
        }

        /// <summary>
        /// アニメーション情報
        /// </summary>
        public class Animation
        {
            public string name;
            public OverrideSettings settings;
            public ReadOnlyCollection<PartAnimation> parts;

            public Animation( NodeReader node )
            {
                name = node.AtText( "name" );
                if ( node.AtBoolean( "overrideSettings" ) ) {   
                    settings = CreateOverrideSettings( node.Child( "settings" ) );
                } else {
                    settings = new OverrideSettings();
                }

                var parts = node.Child( "partAnimes" ).Children( "partAnime" );
                List<PartAnimation> results = new List<PartAnimation>();
                foreach ( var part in parts ) {
                    results.Add( new PartAnimation( part ) );
                }
                this.parts = results.AsReadOnly();
            }

            public override string ToString()
            {
                return string.Format( "name={0}, settings={1}, parts={2}",
                    name, settings.ToString(),
                    string.Join( ";\n", (from o in parts select "\t{" + o.ToString() + "}").ToArray() ) );
            }
        }

        /// <summary>
        /// インポート結果
        /// </summary>
        public struct Information
        {
            public OverrideSettings settings;
            public ReadOnlyCollection<string> cellMapNames;
            public ReadOnlyCollection<Part> parts;
            public ReadOnlyCollection<Animation> animations;

            public override string ToString()
            {
                return string.Format( "settings={{{0}}},\ncellMapNames={1},\nparts=(\n{2}\n),\nanimations=(\n{3}\n)",
                    settings.ToString(),
                    string.Join( ";", cellMapNames.ToArray() ),
                    string.Join( ";\n", (from o in parts select "\t{" + o.ToString() + "}").ToArray() ),
                    string.Join( ";\n", (from o in animations select "\t{" + o.ToString() + "}").ToArray() ) );
            }
        }

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
        public Information Import( string fileName )
        {
            var xml = new XmlDocument();
            xml.Load( fileName );

            // 上書き設定の読み込み
            OverrideSettings overrideSettings = CreateOverrideSettings( NodeReader.findFirst( xml, "SpriteStudioAnimePack/settings" ) );

            // パーツ情報
            var partsNode = NodeReader.findFirst( xml, "SpriteStudioAnimePack/Model/partList" ).Children( "value" );
            var parts = from p in partsNode.Nodes select new Part( p );

            // セルの指定
            var cellMapNames = NodeReader.findFirst( xml, "SpriteStudioAnimePack/cellmapNames" ).Children( "value" );

            // アニメーション情報
            var animeNode = NodeReader.findFirst( xml, "SpriteStudioAnimePack/animeList" ).Children( "anime" );
            var animes = from a in animeNode.Nodes select new Animation( a );

            return new Information() {
                settings = overrideSettings,
                cellMapNames = cellMapNames.AtText(),
                parts = parts.ToList().AsReadOnly(),
                animations = animes.ToList().AsReadOnly(),
            };
        }

        /// <summary>
        /// 上書き設定の生成
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        private static OverrideSettings CreateOverrideSettings( NodeReader settings )
        {
            OverrideSettings overrideSettings = new OverrideSettings();
            {
                var fps = settings.ChildOrNull( "fps" );
                var frameCount = settings.ChildOrNull( "frameCount" );
                var sortMode = settings.ChildOrNull( "sortMode" );
                var pivot = settings.ChildOrNull( "pivot" );

                overrideSettings.fps = fps != null ? (int?) fps.AtInteger() : null;
                overrideSettings.frameCount = frameCount != null ? (int?) frameCount.AtInteger() : null;
                overrideSettings.sortMode = sortMode != null ? (SortMode?) SortModeOpeartor.FromString( sortMode.AtText() ) : null;
                if ( pivot != null ) {
                    float[] pivots = pivot.AtFloats( ' ' );
                    overrideSettings.pivotX = pivots[0];
                    overrideSettings.pivotY = pivots[1];
                } else {
                    overrideSettings.pivotX = null;
                    overrideSettings.pivotY = null;
                }
            }
            return overrideSettings;
        }
    }
}
