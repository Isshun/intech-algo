using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlgoGen.Expressions;

namespace AlgoGen
{
    public class ExprVisitor
    {
        public void Visit( Expr e )
        {
            e.Accept( this );
        }

        internal protected virtual void VisitConstant( ConstantExpr e ) { }
        internal protected virtual void VisitBinaryOperator( BinaryOperatorExpr e ) { }
        internal protected virtual void VisitNegate( NegateExpr e ) { }
        internal protected virtual void VisitIf( IfExpr e ) { }
    }
}
