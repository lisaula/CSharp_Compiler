using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class IDState : State
    {

        private Dictionary<string, TokenType> reservedWordsDict;
        public IDState(string name, bool isInital, bool isFinal) : base(name, isInital, isFinal)
        {
            reservedWordsDict = new Dictionary<string, TokenType>();
            initReservedWords();
        }

        private void initReservedWords()
        {
            reservedWordsDict["class"] = TokenType.RW_CLASS;
            reservedWordsDict["public"] = TokenType.RW_PUBLIC;
            reservedWordsDict["protected"] = TokenType.RW_PROTECTED;
            reservedWordsDict["private"] = TokenType.RW_PRIVATE;
            reservedWordsDict["abstract"] = TokenType.RW_ABSTRACT;
            reservedWordsDict["bool"] = TokenType.RW_BOOL;
            reservedWordsDict["int"] = TokenType.RW_INT;
            reservedWordsDict["double"] = TokenType.RW_DOUBLE;
            reservedWordsDict["var"] = TokenType.RW_VAR;
            reservedWordsDict["float"] = TokenType.RW_FLOAT;
            reservedWordsDict["decimal"] = TokenType.RW_DECIMAL;
            reservedWordsDict["char"] = TokenType.RW_CHAR;
            reservedWordsDict["string"] = TokenType.RW_STRING;
            reservedWordsDict["override"] = TokenType.RW_OVERRIDE;
            reservedWordsDict["void"] = TokenType.RW_VOID;
            reservedWordsDict["new"] = TokenType.RW_NEW;
            reservedWordsDict["true"] = TokenType.LIT_BOOL;
            reservedWordsDict["false"] = TokenType.LIT_BOOL;
            reservedWordsDict["as"] = TokenType.RW_AS;
            reservedWordsDict["is"] = TokenType.RW_IS;
            reservedWordsDict["while"] = TokenType.RW_WHILE;
            reservedWordsDict["for"] = TokenType.RW_FOR;
            reservedWordsDict["foreach"] = TokenType.RW_FOREACH;
            reservedWordsDict["in"] = TokenType.RW_IN;
            reservedWordsDict["using"] = TokenType.RW_USING;
            reservedWordsDict["namespace"] = TokenType.RW_NAMEESPACE;
            reservedWordsDict["if"] = TokenType.RW_IF;
            reservedWordsDict["else"] = TokenType.RW_ELSE;
            reservedWordsDict["switch"] = TokenType.RW_SWITCH;
            reservedWordsDict["case"] = TokenType.RW_CASE;
            reservedWordsDict["static"] = TokenType.RW_STATIC;
            reservedWordsDict["sizeof"] = TokenType.RW_SIZEOF;
            //reservedWordsDict["type"] = TokenType.RW_TYPE;
            reservedWordsDict["enum"] = TokenType.RW_ENUM;
            reservedWordsDict["interface"] = TokenType.RW_INTERFACE;
            reservedWordsDict["virtual"] = TokenType.RW_VIRTUAL;
            reservedWordsDict["base"] = TokenType.RW_BASE;
            reservedWordsDict["this"] = TokenType.RW_THIS;
            reservedWordsDict["do"] = TokenType.RW_DO;
            reservedWordsDict["break"] = TokenType.RW_BREAK;
            reservedWordsDict["continue"] = TokenType.RW_CONTINUE;
            reservedWordsDict["return"] = TokenType.RW_RETURN;
            reservedWordsDict["default"] = TokenType.RW_DEFAULT;
            reservedWordsDict["Dictionary"] = TokenType.RW_NULL;
            reservedWordsDict["null"] = TokenType.RW_NULL;
        }

        public override Token makeToken(string lexema, int lexemaRow, int lexemaColumn)
        {
            try
            {
                TokenType token_type = reservedWordsDict[lexema];
                return new Token(
                        token_type,
                        lexema,
                        lexemaRow,
                        lexemaColumn
                        );
            }
            catch(KeyNotFoundException e)
            {
                return new Token(
                        TokenType.ID,
                        lexema,
                        lexemaRow,
                        lexemaColumn
                        );
            }
        }
    }
}
