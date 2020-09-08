namespace HZJ.ORnodeSqlExpression
{
    internal enum DBExpressionType
    {
        Table = 1000, // make sure these don't overlap with ExpressionType
        ClientJoin,
        Column,
        Select,
        Projection,
        Entity,
        Join,
        Aggregate,
        Scalar,
        Exists,
        In,
        Grouping,
        AggregateSubquery,
        IsNull,
        Between,
        RowCount,
        NamedValue,
        OuterJoined,
        Insert,
        Update,
        Delete,
        Batch,
        Function,
        Block,
        If,
        Declaration,
        Variable
    }
}
