﻿using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Scheduler
{
    public class Instructiune
    {
        public static int NrInstructiuni = 0;
        public static string SimbolValoareImediata = "#";
        public static char SimbolEticheta = ':';

        private string numeInstructiune;
        private int nrInstructiune;
        private List<string> operanzi = new List<string>();

        public int NrInstructiune
        {
            get { return nrInstructiune; }
        }
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
            if (operanzi.Count == 0 || Nume.Contains(SimbolEticheta))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Instructiune()
        {
            nrInstructiune = NrInstructiuni;
            NrInstructiuni++;
        }

        public static bool EsteOperandulValoareImediata(string operatorCaString)
        {
            if (operatorCaString.Contains(SimbolValoareImediata))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool EsteOperandulCuOffsetRegistrii(string operatorCaString)
        {
            var operatorFaraSpatii = operatorCaString.Trim();
            if (operatorFaraSpatii.StartsWith("(") && operatorFaraSpatii.Any(x => !char.IsLetter(x)))
                return true;
            return false;
        }
        public static bool EsteDependintaRAW(Instructiune i1, Instructiune i2)
        {
            string operandScrisI1 = i1.Operanzi[0].ToLower();

            bool suntRegistriiFaraOffset = true;
            //al doilea operand
            for (int i = 1; i < i2.operanzi.Count; i++)
            {
                if (EsteRegistruCuOffset(i2.operanzi[i]))
                {
                    suntRegistriiFaraOffset = false;
                    break;
                }
            }
            if(suntRegistriiFaraOffset == true)
            {
                for (int i = 1; i < i2.operanzi.Count; i++)
                {
                    if(i2.operanzi[i].ToLower().Equals(operandScrisI1))
                    {
                        return true;
                    }
                }
            }
            else
            {
                for (int i = 1; i < i2.operanzi.Count; i++)
                {
                    if (EsteOperandulCuOffsetRegistrii(i2.operanzi[i]))
                    {
                        string operandPrelucrat = i2.operanzi[i].ToLower().Trim();
                        operandPrelucrat = Regex.Replace(operandPrelucrat, "[()]", "");
                        var operanziDinOffset = operandPrelucrat.Split(",");
                        if (operanziDinOffset[0].Equals(operandScrisI1))
                        {
                            return true;
                        }
                        if(operanziDinOffset.Count()>1)
                        if(operanziDinOffset[1].Equals(operandScrisI1))
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (i2.operanzi[i].ToLower().Equals(operandScrisI1))
                        {
                            return true;
                        }
                    }
                }
            }
            if (i1.Nume.ToLower().Equals("eq") || i1.Nume.ToLower().Equals("ne"))
            {
                if (("t" + i1.Operanzi[0].ToLower()).Equals(i2.Nume.ToLower()))
                    return true;
            }
            if (("t" + i1.Operanzi[0].ToLower()).Equals(i2.Nume.ToLower()))
            {
                return true;
            }
            return false;
        }

        public static bool EsteRegistruCuOffset(string operand)
        {
            if(operand.Contains(')') || operand.Contains('('))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void Afiseaza(Instructiune i1)
        {
            string stringDeAfisat = i1.Nume + " ";
            foreach (var op in i1.Operanzi)
            {
                stringDeAfisat += op + ",";
            }
            stringDeAfisat = stringDeAfisat.Remove(stringDeAfisat.Length - 1);
            Debug.WriteLine(stringDeAfisat);
        }
    }
}
