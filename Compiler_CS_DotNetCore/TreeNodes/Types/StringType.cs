namespace Compiler.Tree
{
    public class StringType : PrimitiveType
    {
        public StringType(Token token) : base(token)
        {
        }

        public StringType()
        {

        }
        public override string ToString()
        {
            return this.GetType().Name;
        }
    }
}