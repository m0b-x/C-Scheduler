using System;
using System.Diagnostics;

namespace Scheduler
{
    public class Instructiune
    {
        public static string SimbolValoareImediata = "#";

        private string numeInstructiune;
        private List<string> operanzi = new List<string>();

        public int NumarOperanzi
        {
            get { return operanzi.Count; }
        }
        public string Nume
        {
            get { return numeInstructiune; }
            set { numeInstructiune = value; }
        }
        public List<string> Operanzi
        {
            get { return operanzi; }
            set { operanzi = value; }
        }

        public bool EsteEticheta()
        {
            if (operanzi.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool EsteOperandulValoareImediata(string operatorCaString)
        {
            if (operatorCaString[0].Equals(SimbolValoareImediata))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool EsteDependintaRAW(Instructiune i1, Instructiune i2)
        {
            foreach(string operand in i1.operanzi)
            {
                if (EsteRegistru(operand) == false)
                    return false;
            }
            foreach (string operand in i2.operanzi)
            {
                if (EsteRegistru(operand) == false)
                    return false;
            }

            return false;
        }

        public static bool EsteRegistru(string operand)
        {
            if (operand.Length == 2)
            {
                if (operand[0].ToString().ToLower().Equals("r"))
                {
                    var isNumeric = int.TryParse(operand[1].ToString(), out _);
                    if(isNumeric)
                    {
                        return true;
                    }
                }
            }
            else if (operand.Length == 3)
            {

                if (operand[0].ToString().ToLower().Equals("r"))
                {
                    var isNumeric = int.TryParse(operand.Substring(operand.Length - 2), out _);
                    if (isNumeric)
                    {
                        return true;
                    }
                }
            }
            else
            {
                return false;
            }
            return false;
        }
        public static void Afiseaza(Instructiune i1)
        {
            Debug.Write(i1.Nume + " ");
            foreach (var op in i1.Operanzi)
            {
                Debug.Write(op + " ");
            }
            Debug.WriteLine("");
        }
    }
}
