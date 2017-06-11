using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compiler.Tree;
using Compiler_CS_DotNetCore.Semantic;

namespace Compiler
{
    public partial class Parser
    {
        private List<Statement> optional_statement_list()
        {
            DebugInfoMethod("optional_statement_list");
            TokenType[] nuevo = {
                TokenType.RW_VAR, TokenType.OPEN_CURLY_BRACKET,TokenType.END_STATEMENT,
                TokenType.RW_IF, TokenType.RW_SWITCH, TokenType.RW_WHILE, 
                TokenType.RW_DO, TokenType.RW_FOR, TokenType.RW_FOREACH,
                TokenType.RW_BREAK, TokenType.RW_CONTINUE, TokenType.RW_RETURN
            };
            if (pass(nuevo.Concat(typesOptions).Concat(unaryExpressionOptions).Concat(unaryOperatorOptions).
                Concat(literalOptions).ToArray()
                ))
            {
                return statement_list();
            }
            else
            {
                DebugInfoMethod("epsilon");
                return new List<Statement>();
            }
        }

        private List<Statement> statement_list()
        {
            DebugInfoMethod("statement_list");
            var stmnt = statement();
            var list = optional_statement_list();
            list.Insert(0, stmnt);
            return list;
        }

        private Statement statement()
        {
            DebugInfoMethod("statement "+look_ahead.Count());
            TokenType[] nuevo = { TokenType.RW_VAR };
            TokenType[] embedded= {
                TokenType.OPEN_CURLY_BRACKET,TokenType.END_STATEMENT,
                TokenType.RW_IF, TokenType.RW_SWITCH, TokenType.RW_WHILE,
                TokenType.RW_DO, TokenType.RW_FOR, TokenType.RW_FOREACH,
                TokenType.RW_BREAK, TokenType.RW_CONTINUE, TokenType.RW_RETURN
            };

            while (pass(typesOptions.Concat(nuevo).ToArray()))
            {
                addLookAhead(lexer.getNextToken());
                if (look_ahead[look_ahead.Count() - 1].type == TokenType.OP_DOT)
                {
                    addLookAhead(lexer.getNextToken());
                }
                else
                    break;
            }
            int index;
            int index2=0;
            Token placeholder = current_token;
            if (pass(typesOptions.Concat(nuevo).ToArray()))
            {
                index = look_ahead.Count() - 1;
                placeholder = look_ahead[index];
                addLookAhead(lexer.getNextToken());
                index2 = look_ahead.Count() - 1;
                DebugInfoMethod("PH: " + placeholder.type+" "+ look_ahead[index2].type);
            }
            if ( 
                (pass(typesOptions.Concat(nuevo).ToArray()) &&
                (placeholder.type == TokenType.ID
                || placeholder.type == TokenType.OP_LESS_THAN
                || 
                (placeholder.type == TokenType.OPEN_SQUARE_BRACKET
                && (look_ahead[index2].type == TokenType.CLOSE_SQUARE_BRACKET
                || look_ahead[index2].type == TokenType.OP_COMMA) ))) 
               )
            {
                var local = local_variable_declaration();
                if (!pass(TokenType.END_STATEMENT))
                    throwError("end statement ';'");
                consumeToken();
                return local;
            }
            else if(pass(embedded.Concat(unaryExpressionOptions).Concat(unaryOperatorOptions).
                Concat(literalOptions).Concat(primitiveTypes).ToArray()
                ))
            {
                return embedded_statement();
            }
            else
            {
                throwError("local or embedded statement con " + current_token.type);
                return null;
            }
        }

        private EmbeddedStatementNode embedded_statement()
        {
            DebugInfoMethod("embedded_statement");
            if (pass(TokenType.OPEN_CURLY_BRACKET, TokenType.END_STATEMENT))
            {
                return maybe_empty_block();
            }else if (pass(unaryExpressionOptions.Concat(unaryOperatorOptions).Concat(literalOptions).Concat(primitiveTypes).ToArray())) 
            {
                var stmt = statement_expression();
                if (!pass(TokenType.END_STATEMENT))
                    throwError("end statement ';'");
                consumeToken();
                return stmt;
            }
            else if(pass(TokenType.RW_IF, TokenType.RW_SWITCH))
            {
                return selection_statement();
            }else if(pass(TokenType.RW_WHILE, TokenType.RW_DO, TokenType.RW_FOR
                , TokenType.RW_FOREACH))
            {
                return iteration_statement();
            }else if(pass(TokenType.RW_BREAK, TokenType.RW_CONTINUE, TokenType.RW_RETURN))
            {
                var jump =  jump_statement();
                if (!pass(TokenType.END_STATEMENT))
                    throwError("end statement ';'");
                consumeToken();
                return jump;
            }
            else
            {
                throwError("block, statement, selection,iteration, or jump statement");
                return null;
            }
        }
        private LocalVariableDefinitionNode local_variable_declaration()
        {
            DebugInfoMethod("local_variable_declaration");
            TokenType[] nuevo = { TokenType.RW_VAR };
            if (!pass(typesOptions.Concat(nuevo).ToArray()))
                throwError("a type");
            TypeDefinitionNode type = null; 
            if (pass(TokenType.RW_VAR)) {
                type = new VarType(current_token);
                consumeToken();
            } else {
                type =  types();
            }
            var local = new LocalVariableDefinitionNode();
            var variables = variable_declarator_list(null,null,type);
            foreach (FieldNode f in variables)
            {
                if (local.variable.ContainsKey(f.id.token.lexema))
                    throw new SemanticException("Local variable " + f.id.token.lexema + " already exist in current context",f.id.token);
                local.variable[f.id.token.lexema] = f;
            }
            return local;
        } 
    }
}
