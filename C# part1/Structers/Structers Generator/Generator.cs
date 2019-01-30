using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profiling
{
    class Generator
    {
        enum Type {Struct,Class}
        public static int ArrayLength { get; private set; } = Constants.FieldCounts.Count;

        public static string GenerateDeclarations()
        {
            var result = new StringBuilder();
            for (var i = 0; i < ArrayLength; i++)
            {
                var num = Constants.FieldCounts.ElementAt(i);
                CreateDeclaration(Type.Struct, num, result);
                CreateDeclaration(Type.Class, num, result);
            }
            return result.ToString();
        }

        private static void CreateDeclaration(Type type, int num, StringBuilder result)
        {       
            result.AppendFormat("{0} {1}{2}\n", type.ToString().ToLower(), type.ToString()[0], num);
            result.Append("{\n");
            for (var i = 0; i < num; i++)
                result.AppendFormat("byte Value{0}; ", i);
            result.Append("\n}\n\n");
        }

        public static string GenerateArrayRunner()
        {
            var result = new StringBuilder();
            result.Append("public class ArrayRunner : IRunner\n{\n");
            for (var i = 0; i < ArrayLength; i++)
            {
                var num = Constants.FieldCounts.ElementAt(i);
                result.AppendFormat(
                    "void PC{0}()\n{{\n" +
                    "var array = new C{0}[Constants.ArraySize];\n" +
                    "for (int i = 0; i < Constants.ArraySize; i++) array[i] = new C{0}();\n}}\n" +
                    "void PS{0}()\n{{\n" +
                    "var array = new S{0}[Constants.ArraySize];\n}}\n", num);
            }
            MakeCallPart(result);
            result.Append("}");
            return result.ToString();
        }

        public static void MakeCallPart(StringBuilder result)
        {
            result.Append("public void Call(bool isClass, int size, int count)\n{\n");
            for (var i = 0; i < ArrayLength; i++)
            {
                var num = Constants.FieldCounts.ElementAt(i);
                result.AppendFormat(
                    "if (isClass && size == {0})\n{{\n" +
                    "for (int i = 0; i < count; i++) PC{0}();\n" +
                    "return;\n}}\n" +
                    "if (!isClass && size == {0})\n{{\n" +
                    "for (int i = 0; i < count; i++) PS{0}();\n" +
                    "return;\n}}\n"
                    , num);
            }
            result.Append("throw new ArgumentException();");
            result.Append("\n}\n");
        }

        public static string GenerateCallRunner()
        {
            throw new NotImplementedException();
        }
    }
}