using Compiler.Tree;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace Compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            var txt = System.IO.File.ReadAllText(@"G:\2017\2do tri\Compi\Compiler_CS_DotNetCore\UnitTestProject\ParsersTests\testFiles\compis_frag.txt");
            var s = @"
private class ClasePrivada {
    public Constructor(int a ) : base(a){
        int x = y = z += t;
        bool boleano = ((5+3) * 8+5 / 10 %20 );
        if(x is int){
            
        }
    }
    private Constructor2 (int a ){
        int.TryParse(x);
        for(int i =0; i< 10; ++i){
            System.Console.Out.WriteLine(""Hola people"");
        }
        Clase x = new Clase();
    }
}
";
            var inputString = new InputString(s);
            var lexer = new LexicalAnalyzer(inputString);
            var parser = new Parser(lexer);
            CompilationNode tree;
            try
            {
                tree = parser.parse();
                SerializeTree(tree);
            }
            catch(System.Exception e)
            {
                System.Console.Out.WriteLine(e);
            }
            System.Console.ReadKey();
        }

        private static void SerializeTree(CompilationNode tree)
        {
            System.Type[] types = { typeof(UsingNode), typeof(NamespaceNode), typeof(EnumDefinitionNode)
            , typeof(EnumNode), typeof(InterfaceNode), typeof(ClassDefinitionNode), typeof(FieldNode)
            , typeof(MethodNode), typeof(ConstructorNode),typeof(ConstructorInitializerNode), typeof(IdentifierNode), typeof(Token)
            , typeof(ExpressionNode), typeof(Parameter), typeof(ModifierNode), typeof(PrimitiveType), typeof(DictionaryTypeNode)
            , typeof(LiteralNode), typeof(StatementExpressionNode), typeof(FunctionCallExpression), typeof(AccessMemory)
            , typeof(ReferenceAccessNode), typeof(ForStatementNode), typeof(ForeachStatementNode), typeof(WhileStatementNode)
            , typeof(DoStatementNode), typeof(IfStatementNode),typeof(SwitchStatementNode) , typeof(BodyStatement), typeof(LocalVariableDefinitionNode)
            , typeof(EmbeddedStatementNode), typeof(IdentifierTypeNode), typeof(ClassInstantiation), typeof(ArrayInstantiation), typeof(ConditionExpression)
            , typeof(AssignmentNode), typeof(PostAdditiveExpressionNode), typeof(UnaryExpressionNode), typeof(PreExpressionNode),
            typeof(VoidTypeNode), typeof(ArrayAccessNode), typeof(ParenthesizedExpressionNode), typeof(ArithmeticExpression), typeof(VarType)
            , typeof(BinaryExpression), typeof(TernaryExpressionNode), typeof(JumpStatementNode), typeof(CastingExpressionNode)
            , typeof(InlineExpressionNode),typeof(SubExpression), typeof(SumExpression), typeof(MultExpression), typeof(DivExpression), typeof(LogicalExpression),
            typeof(RelationalExpression), typeof(EqualityExpression), typeof(ModExpression), typeof(IsExpression)};
            var serializer = new XmlSerializer(typeof(CompilationNode),types);
            var logPath = System.IO.Path.GetTempFileName();
            var logFile = System.IO.File.Create("prueba_expr.xml");
            var writer = new System.IO.StreamWriter(logFile);
            serializer.Serialize(writer, tree);
        }
    }
}
