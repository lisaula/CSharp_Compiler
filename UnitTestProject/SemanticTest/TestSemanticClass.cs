using Compiler_CS_DotNetCore.Semantic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestProject.SemanticTest
{
    [TestClass]
    public class ClassTestSemantic
    {
        [TestMethod]
        public void TestInterfaceWithInheritance()
        {
            List<string> paths = null;
            API api = new API(paths);
            var text = @"
public interface Types{
			
}
namespace Figuras{
	public class Circulo : Figura{

	}
	namespace insideFiguras{
		public interface Types{
			
		}
	}
}
public interface myInterface : Types{
	void getNombre(int index);
	void getNompre(string name);
	int setIndex(int index);
}
";
            var input = new Compiler.InputString(text);
            var semanticEvaluator = new Evaluator(paths, api, input);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestInterfaceWithRedundantInheritance()
        {
            List<string> paths = null;
            API api = new API(paths);
            var text = @"
public interface Types{
			
}
namespace Figuras{
	public class Circulo : Figura{

	}
	namespace insideFiguras{
		public interface Types{
			
		}
	}
}
public interface myInterface : Types, Types{
	void getNombre(int index);
	void getNompre(string name);
	int setIndex(int index);
}
";
            var input = new Compiler.InputString(text);
            var semanticEvaluator = new Evaluator(paths, api, input);
        }
    }
}
