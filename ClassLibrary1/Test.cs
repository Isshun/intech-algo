using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using AlgoGen;

namespace AlgoGen.Test
{
    [TestFixture]
    public class Test
    {

        [Test]
        public void Create()
        {
            var createVisitor = new CreateVisitor(42, 10, 8);

            var expr = createVisitor.Build();

            var evalVisitor = new EvalVisitor();
            evalVisitor.Visit( expr );
            System.Console.WriteLine(evalVisitor);
        }
    }
}
