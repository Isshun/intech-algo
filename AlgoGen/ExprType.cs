using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoGen
{
    public enum ExprType
    {
        None,
        Negate,
        Constant,
        BinaryOperator,
        If,
        Variable,
        SyntaxError,
        CallFunc
    }
}