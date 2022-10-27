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
                case "ADD" :
                case "ADDV":
                case "DADC":
                    {
                        double result;
                        result = int.Parse(i1.Operanzi[1]) + int.Parse(i2.Operanzi[1]);
                        i2.Operanzi[2] = result.ToString();
                        break;
                    }
                case "SUB":
                case "SUBV":
                case "SUBC":
                    {
                        double result;
                        result = int.Parse(i1.Operanzi[1]) - int.Parse(i2.Operanzi[1]);
                        i2.Operanzi[2] = result.ToString();
                        break;
                    }
                case "BIC":
                    {
                        string op1Binar = Convert.ToString(int.Parse(i1.Operanzi[1]), 2);
                        string op2Binar = Convert.ToString(int.Parse(i2.Operanzi[1]), 2);

                        int dimensiuneMica = (op1Binar.Length >= op2Binar.Length) ? op2Binar.Length : op1Binar.Length;
                        int dimensiuneMare = (op1Binar.Length < op2Binar.Length) ? op2Binar.Length : op1Binar.Length;

                        string op2Complement2 = op2Binar;

                        op2Complement2 = op2Complement2.Replace('0', '2').Replace('1', '0').Replace('2', '1');
                        StringBuilder operatorBinar = new(dimensiuneMare);
                        
                        string opBinarMaiMare = (op1Binar.Length.Equals(dimensiuneMare)) ? op1Binar : op2Complement2;

                        for (int i = 0; i < dimensiuneMica; i++)
                        {
                            var newBit = (op1Binar[i] & op2Complement2[i]).ToString();
                            operatorBinar[i] = newBit[0];
                        }
                        for (int i = dimensiuneMica - 1; i < dimensiuneMare; i++)
                        {
                            operatorBinar[i] = opBinarMaiMare[i];
                        }
                        i2.Operanzi[1] = operatorBinar.ToString();
                        break;
                    }
                case "AND":
                    {
                        string op1Binar = Convert.ToString(int.Parse(i1.Operanzi[1]), 2);
                        string op2Binar = Convert.ToString(int.Parse(i2.Operanzi[1]), 2);
                        int dimensiuneMica = (op1Binar.Length >= op2Binar.Length) ? op2Binar.Length : op1Binar.Length;
                        int dimensiuneMare = (op1Binar.Length < op2Binar.Length) ? op2Binar.Length : op1Binar.Length;
                        string opBinarMaiMare = (op1Binar.Length.Equals(dimensiuneMare)) ? op1Binar : op2Binar;
                        StringBuilder operatorBinar = new(dimensiuneMare);

                        for(int i=0;i < dimensiuneMica; i++)
                        {
                            var newBit = (op1Binar[i] & op2Binar[i]).ToString();
                            operatorBinar[i] = newBit[0];
                        }
                        for(int i=dimensiuneMica-1;i<dimensiuneMare;i++)
                        {
                            operatorBinar[i] = opBinarMaiMare[i];
                        }
                        i2.Operanzi[1] = operatorBinar.ToString();
                        break;
                    }
                case "OR":
                    {
                        string op1Binar = Convert.ToString(int.Parse(i1.Operanzi[1]), 2);
                        string op2Binar = Convert.ToString(int.Parse(i2.Operanzi[1]), 2);
                        int dimensiuneMica = (op1Binar.Length >= op2Binar.Length) ? op2Binar.Length : op1Binar.Length;
                        int dimensiuneMare = (op1Binar.Length < op2Binar.Length) ? op2Binar.Length : op1Binar.Length;
                        string opBinarMaiMare = (op1Binar.Length.Equals(dimensiuneMare)) ? op1Binar : op2Binar;
                        StringBuilder operatorBinar = new(dimensiuneMare);

                        for (int i = 0; i < dimensiuneMica; i++)
                        {
                            var newBit = (op1Binar[i] | op2Binar[i]).ToString();
                            operatorBinar[i] = newBit[0];
                        }
                        for (int i = dimensiuneMica - 1; i < dimensiuneMare; i++)
                        {
                            operatorBinar[i] = opBinarMaiMare[i];
                        }
                        i2.Operanzi[1] = operatorBinar.ToString();
                        break;
                    }
                case "EOR":
                    {
                        string op1Binar = Convert.ToString(int.Parse(i1.Operanzi[1]), 2);
                        string op2Binar = Convert.ToString(int.Parse(i2.Operanzi[1]), 2);
                        int dimensiuneMica = (op1Binar.Length >= op2Binar.Length) ? op2Binar.Length : op1Binar.Length;
                        int dimensiuneMare = (op1Binar.Length < op2Binar.Length) ? op2Binar.Length : op1Binar.Length;
                        string opBinarMaiMare = (op1Binar.Length.Equals(dimensiuneMare)) ? op1Binar : op2Binar;
                        StringBuilder operatorBinar = new(dimensiuneMare);

                        for (int i = 0; i < dimensiuneMica; i++)
                        {
                            operatorBinar[i] = (op1Binar[i].Equals(op2Binar[i])) ? '1' : '0';
                        }
                        for (int i = dimensiuneMica - 1; i < dimensiuneMare; i++)
                        {
                            operatorBinar[i] = opBinarMaiMare[i];
                        }
                        i2.Operanzi[1] = operatorBinar.ToString();
                        break;
                    }
            }
            return seSuprascrieInstructiunea;
        }
    }
}
