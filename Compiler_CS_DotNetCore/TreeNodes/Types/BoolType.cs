namespace Compiler.Tree
{
    public class BoolType : PrimitiveType
    {
        public BoolType(Token token) : base(token)
        {
        }
        public BoolType()
        {

        }
        public override string ToString()
        {
            return this.GetType().Name;
        }
    }
}