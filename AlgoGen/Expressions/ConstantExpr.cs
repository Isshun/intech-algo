using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlgoGen;

namespace AlgoGen.Expressions
{
    public class ConstantExpr : Expr
    {
        public ConstantExpr( double value )
            : base( ExprType.Constant )
        {
            Value = value;
        }

        public double Value { get; private set; }

        public override void DumpTreeView( int level, StringBuilder b )
        {
            b.Append( ' ', level * 2 )
            .Append( Value )
            .AppendLine();
        }

        public override void Accept( ExprVisitor v )
        {
            v.VisitConstant( this );
        }
    }
}
