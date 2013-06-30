using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlgoGen.Expressions;

namespace AlgoGen
{
    public class EvalVisitor : ExprVisitor
    {
        StringBuilder _b;
        int _currentLevel;

        public EvalVisitor()
        {
            _b = new StringBuilder();
        }

        internal protected override void VisitNegate( NegateExpr e )
        {
            _b.Append( ' ', _currentLevel ).Append( "Negate" ).AppendLine();
            _currentLevel++;
            Visit( e.Expr );
            _currentLevel--;
        }

        internal protected override void VisitConstant( ConstantExpr e )
        {
            _b.Append( ' ', _currentLevel ).Append( e.Value ).AppendLine();
            /*_currentLevel++;
            Visit(e.Value);
            _currentLevel--;*/
        }

        internal protected override void VisitBinaryOperator( BinaryOperatorExpr e )
        {
            _b.Append( ' ', _currentLevel ).Append( e.OperatorType ).AppendLine();
            _currentLevel++;
            Visit( e.Left );
            Visit( e.Right );
            _currentLevel--;
        }

        internal protected override void VisitIf( IfExpr e )
        {
            _b.Append( ' ', _currentLevel ).Append( "If" ).AppendLine();
            Visit( e.Condition );

            _currentLevel++;
            _b.Append( ' ', _currentLevel ).Append( "then" ).AppendLine();
            Visit( e.WhenTrue );
            if( e.WhenFalse != null )
            {
                _b.Append( ' ', _currentLevel ).Append( "else" ).AppendLine();
                Visit( e.WhenFalse );
            }
            _currentLevel--;
        }

        public override string ToString()
        {
            return _b.ToString();
        }
    }
}
