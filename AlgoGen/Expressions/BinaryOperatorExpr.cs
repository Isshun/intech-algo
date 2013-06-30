using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlgoGen;

namespace AlgoGen.Expressions
{
    public class BinaryOperatorExpr : Expr
    {
        public enum BinaryOperatorType
        {
            None,
            Plus,
            Minus,
            Mult,
            Div
        }

        public BinaryOperatorExpr( Expr left, Expr right, BinaryOperatorType type )
            : base( ExprType.BinaryOperator )
        {
            OperatorType = type;
            Left = left;
            Right = right;
        }

        public BinaryOperatorType OperatorType { get; private set; }
        public Expr Left { get; private set; }
        public Expr Right { get; private set; }

        public override void DumpTreeView( int level, StringBuilder b )
        {
            b.Append( ' ', level * 2 )
            .Append( OperatorType )
            .AppendLine();
            Left.DumpTreeView( level + 1, b );
            Right.DumpTreeView( level + 1, b );
        }

        public override void Accept( ExprVisitor v )
        {
            v.VisitBinaryOperator( this );
        }
    }
}