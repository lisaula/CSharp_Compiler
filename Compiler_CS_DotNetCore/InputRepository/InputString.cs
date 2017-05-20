namespace Compiler
{
    public class InputString : IInput
    {
        public string initialInput { get; set; }

        public InputString(string input)
        {            
            this.initialInput = input;
            this.rowCount = 1;
            this.colCount = 1;
            this.currentChar = 0;
        }

        public int currentChar { get; set; }

        public int colCount { get; set; }

        public int rowCount { get; set; }

        public Symbol GetNextSymbol()
        {
            if (currentChar < initialInput.Length)
            {
                if (initialInput[currentChar] == '\n')
                {
                    ++rowCount;
                    colCount = 1;
                }
                var returnSymbol = new Symbol(
                    initialInput[currentChar++],
                    rowCount,
                    colCount++);
                return returnSymbol;
            }

            return new Symbol('\0', rowCount, colCount);
        }
    }
}