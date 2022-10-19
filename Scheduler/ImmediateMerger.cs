using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    public class ImmediateMerger : Merger
    {

        //Această tehnică implică orice pereche de instrucţiuni
        //care au valori imediate pe post
        //de al doilea operand sursă
        public bool IsMergeCase(Instructiune i1, Instructiune i2)
        {
            if (i1.Operanzi.Count == 2 && i2.Operanzi.Count == 2)
            {
                if (Instructiune.EsteOperandulValoareImediata(i1.Operanzi[2])
                 && Instructiune.EsteOperandulValoareImediata(i2.Operanzi[2]))
                {
                    return true;
                }
            }
            return false;
            throw new NotImplementedException();
        }

        //SUB R3, R6, #3
        //ADD R4, R3, #1 /* instrucţiunea care se infiltrează */
        //SUB R3, R6, #3; ADD R4, R6, #-2
        public bool Merge(ref Instructiune i1,ref Instructiune i2)
        {
            bool seSuprascrieInstructiunea = false;
            switch(i1.Nume)
            {
                case "ADD":
                    {
                        double result;
                        result = int.Parse(i1.Operanzi[1]) + int.Parse(i2.Operanzi[1]);
                        i2.Operanzi[2] = result.ToString();
                        break;
                    }
                case "SUB":
                    {
                        double result;
                        result = int.Parse(i1.Operanzi[1]) - int.Parse(i2.Operanzi[1]);
                        i2.Operanzi[2] = result.ToString();
                        break;
                    }
                case "MUL":
                    {
                        double result;
                        result = int.Parse(i1.Operanzi[1]) * int.Parse(i2.Operanzi[1]);
                        i2.Operanzi[2] = result.ToString();
                        break;
                    }
                case "DIV":
                    {
                        double result;
                        result = int.Parse(i1.Operanzi[1]) / int.Parse(i2.Operanzi[1]);
                        i2.Operanzi[2] = result.ToString();
                        break;
                    }
            }
            return seSuprascrieInstructiunea;
        }
    }
}
