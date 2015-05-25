namespace a.spritestudio.types
{
    public enum NodeType
    {
        kNull,
        kNormal
    }

    public static class NodeTypeOperator
    {
        public static NodeType FromString( string v )
        {
            switch ( v ) {
                case "null":
                    return NodeType.kNull;

                default:
                    return NodeType.kNormal;
            }
        }
    }
}
