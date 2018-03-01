using CodeGen.Generation;

namespace CodeGen
{
    public class CodeGenerator
    {
        public Code Generate(Expression expression)
        {
            return expression.Code;
        }        
    }
}