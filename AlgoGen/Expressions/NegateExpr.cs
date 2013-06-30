using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlgoGen;

namespace AlgoGen.Expressions
{
    public class NegateExpr : Expr
    {
        public NegateExpr( Expr expr )
            : base( ExprType.Negate )
        {
            Expr = expr;
        }

        public Expr Expr { get; private set; }

        public override void DumpTreeView( int level, StringBuilder b )
        {
            b.Append( ' ', level * 2 )
            .Append( "Negate" )
            .AppendLine();
            Expr.DumpTreeView( level + 1, b );
        }

        public override void Accept( ExprVisitor v )
        {
            v.VisitNegate( this );
        }
    }
}
