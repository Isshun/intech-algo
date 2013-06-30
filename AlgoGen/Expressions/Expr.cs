using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgoGen
{
    public abstract class Expr
    {
        protected Expr( ExprType type )
        {
            ExprType = type;
        }

        public ExprType ExprType { get; private set; }

        public abstract void DumpTreeView( int level, StringBuilder b );

        public abstract void Accept( ExprVisitor v );
    }
}
