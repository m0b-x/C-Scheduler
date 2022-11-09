using System.Text;

namespace Scheduler
{
    public class ImmediateMerger : Merger
    {

        //Această tehnică implică orice pereche de instrucţiuni
        //care au valori imediate pe post
        //de al doilea operand sursă
        public bool IsMergeCase(Instructiune i1, Instructiune i2)
        {
            if (i1.Operanzi.Count == 3 && i2.Operanzi.Count == 3)
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
        public bool Merge(ref Instructiune i1, ref Instructiune i2)
        {
            bool seSuprascrieInstructiunea = false;
            switch (i1.Nume.ToLower())
            {
                case "add":
                case "addv":
                case "addc":
                    {
                        double result;
                        result = int.Parse(i1.Operanzi[2].Substring(1)) + int.Parse(i2.Operanzi[2].Substring(1));
                        i2.Operanzi[2] = Instructiune.SimbolValoareImediata + result.ToString();
                        break;
                    }
                case "sub":
                case "subv":
                case "subc":
                    {
                        double result;
                        result = int.Parse(i1.Operanzi[2].Substring(1)) - int.Parse(i2.Operanzi[2].Substring(1));
                        i2.Operanzi[2] = Instructiune.SimbolValoareImediata + result.ToString();
                        break;
                    }
                case "mult":
                    {
                        double result;
                        result = int.Parse(i1.Operanzi[2].Substring(1)) * int.Parse(i2.Operanzi[2].Substring(1));
                        i2.Operanzi[2] = Instructiune.SimbolValoareImediata + result.ToString();
                        break;
                    }
                case "div":
                    {
                        double result;
                        result = int.Parse(i1.Operanzi[2].Substring(1)) / int.Parse(i2.Operanzi[2].Substring(1));
                        i2.Operanzi[2] = Instructiune.SimbolValoareImediata + result.ToString();
                        break;
                    }
                case "bic":
                    {
                        string op1Binar = Convert.ToString(int.Parse(i1.Operanzi[2].Substring(1)), 2);
                        string op2Binar = Convert.ToString(int.Parse(i2.Operanzi[2].Substring(1)), 2);

                        int dimensiuneMica = (op1Binar.Length >= op2Binar.Length) ? op2Binar.Length : op1Binar.Length;
                        int dimensiuneMare = (op1Binar.Length < op2Binar.Length) ? op2Binar.Length : op1Binar.Length;

                        string op2Complement2 = op2Binar;

                        op2Complement2 = op2Complement2.Replace('0', '2').Replace('1', '0').Replace('2', '1');
                        StringBuilder operatorBinar = new(dimensiuneMare);

                        string opBinarMaiMare = (op1Binar.Length.Equals(dimensiuneMare)) ? op1Binar : op2Complement2;

                        for (int i = 0; i < dimensiuneMica; i++)
                        {
                            string newBit = (op1Binar[i] & op2Complement2[i]).ToString();
                            operatorBinar[i] = newBit[0];
                        }
                        for (int i = dimensiuneMica - 1; i < dimensiuneMare; i++)
                        {
                            operatorBinar[i] = opBinarMaiMare[i];
                        }
                        i2.Operanzi[2] = Instructiune.SimbolValoareImediata + operatorBinar.ToString();
                        break;
                    }
                case "and":
                    {
                        string op1Binar = Convert.ToString(int.Parse(i1.Operanzi[2].Substring(1)), 2);
                        string op2Binar = Convert.ToString(int.Parse(i2.Operanzi[2].Substring(1)), 2);
                        int dimensiuneMica = (op1Binar.Length >= op2Binar.Length) ? op2Binar.Length : op1Binar.Length;
                        int dimensiuneMare = (op1Binar.Length < op2Binar.Length) ? op2Binar.Length : op1Binar.Length;
                        string opBinarMaiMare = (op1Binar.Length.Equals(dimensiuneMare)) ? op1Binar : op2Binar;
                        StringBuilder operatorBinar = new(dimensiuneMare);

                        for (int i = 0; i < dimensiuneMica; i++)
                        {
                            string newBit = (op1Binar[i] & op2Binar[i]).ToString();
                            operatorBinar[i] = newBit[0];
                        }
                        for (int i = dimensiuneMica - 1; i < dimensiuneMare; i++)
                        {
                            operatorBinar[i] = opBinarMaiMare[i];
                        }
                        i2.Operanzi[2] = Instructiune.SimbolValoareImediata + operatorBinar.ToString();
                        break;
                    }
                case "or":
                    {
                        string op1Binar = Convert.ToString(int.Parse(i1.Operanzi[2].Substring(1)), 2);
                        string op2Binar = Convert.ToString(int.Parse(i2.Operanzi[2].Substring(1)), 2);
                        int dimensiuneMica = (op1Binar.Length >= op2Binar.Length) ? op2Binar.Length : op1Binar.Length;
                        int dimensiuneMare = (op1Binar.Length < op2Binar.Length) ? op2Binar.Length : op1Binar.Length;
                        string opBinarMaiMare = (op1Binar.Length.Equals(dimensiuneMare)) ? op1Binar : op2Binar;
                        StringBuilder operatorBinar = new(dimensiuneMare);

                        for (int i = 0; i < dimensiuneMica; i++)
                        {
                            string newBit = (op1Binar[i] | op2Binar[i]).ToString();
                            operatorBinar[i] = newBit[0];
                        }
                        for (int i = dimensiuneMica - 1; i < dimensiuneMare; i++)
                        {
                            operatorBinar[i] = opBinarMaiMare[i];
                        }
                        i2.Operanzi[2] = Instructiune.SimbolValoareImediata + operatorBinar.ToString();
                        break;
                    }
                case "eor":
                    {
                        string op1Binar = Convert.ToString(int.Parse(i1.Operanzi[2].Substring(1)), 2);
                        string op2Binar = Convert.ToString(int.Parse(i2.Operanzi[2].Substring(1)), 2);
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
                        i2.Operanzi[2] = Instructiune.SimbolValoareImediata + operatorBinar.ToString();
                        break;
                    }
            }
            return seSuprascrieInstructiunea;
        }
    }
}
