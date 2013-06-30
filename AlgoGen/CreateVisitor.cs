using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlgoGen.Expressions;

namespace AlgoGen
{
    public class CreateVisitor : ExprVisitor
    {
        StringBuilder _b;
        int _currentLevel;
        double _x, _y, _t;

        public CreateVisitor(double x, double y, double t)
        {
            _b = new StringBuilder();
            _x = x;
            _y = y;
            _t = t;
        }

        public Expr Build()
        {
            //Expr left = new IfExpr(new )
            return new BinaryOperatorExpr(
                new BinaryOperatorExpr(
                    new ConstantExpr( _y ), new ConstantExpr( _t ), BinaryOperatorExpr.BinaryOperatorType.Minus ),
                    new ConstantExpr( 2 ),
                    BinaryOperatorExpr.BinaryOperatorType.Plus );
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
