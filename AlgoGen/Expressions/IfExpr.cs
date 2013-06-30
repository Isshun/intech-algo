using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlgoGen;

namespace AlgoGen.Expressions
{
    public class IfExpr : Expr
    {
        public IfExpr( Expr condition, Expr whenTrue, Expr whenFalse = null )
            : base( ExprType.If )
        {
            if( condition == null ) throw new ArgumentNullException( "condition" );
            if( whenTrue == null ) throw new ArgumentNullException( "whenTrue" );
            Condition = condition;
            WhenTrue = whenTrue;
            WhenFalse = whenFalse;
        }

        public Expr Condition { get; private set; }
        public Expr WhenTrue { get; private set; }
        public Expr WhenFalse { get; private set; }

        public override void DumpTreeView( int level, StringBuilder b )
        {
            b.Append( ' ', level * 2 )
            .Append( "If" )
            .AppendLine();
            Condition.DumpTreeView( level + 1, b );
            b.Append( ' ', level * 2 )
            .Append( "Then" )
            .AppendLine();
            WhenTrue.DumpTreeView( level + 1, b );

            if( WhenFalse != null )
            {
                b.Append( ' ', level * 2 )
                .Append( "Then" )
                .AppendLine();
                WhenFalse.DumpTreeView( level + 1, b );
            }
        }

        public override void Accept( ExprVisitor v )
        {
            v.VisitIf( this );
        }
    }
}
