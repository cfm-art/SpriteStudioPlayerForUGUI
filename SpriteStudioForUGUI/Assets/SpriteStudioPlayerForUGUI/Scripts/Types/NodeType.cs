namespace a.spritestudio.types
{
    public enum NodeType
    {
        kNull,
        kNormal,
        kInstance
    }

    public static class NodeTypeOperator
    {
        public static NodeType FromString( string v )
        {
            switch ( v ) {
                case "null":
                    return NodeType.kNull;

                case "instance":
                    return NodeType.kInstance;

                default:
                    return NodeType.kNormal;
            }
        }
    }
}
