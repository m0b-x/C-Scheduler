using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace Scheduler
{
    public class ParserInstructiuni
    {

        List<Instructiune>instructiuni = new List<Instructiune>();

        char separatorInstructiuneOperator = ' ';
        char separatorOperatori = ',';
        string marcajInceputComentariu = "/*";
        string marcajSfarsitComentariu = "*/";
        char simbolEticheta = ':';
        char simbolASCII = '.';

        public List<Instructiune>Instructiuni
        {
            get { return instructiuni; }
            set { instructiuni = value; }
        }

        public void ParseazaInstructiuni(string[] fileContent)
        {
            foreach (string linie in fileContent)
            {
                if (!linie.Equals(String.Empty) && linie.Any(x => !char.IsLetter(x)))
                {
                    if (!EsteLiniaEticheta(linie) && !EsteDirectivaASCII(linie))
                    {
                       var date = linie.Trim().Split(separatorInstructiuneOperator,2);
                        Instructiune instructiune = new Instructiune()
                        {
                            Nume = date[0]
                        };
                        var operatori = date[1].Split(separatorOperatori);
                        instructiune.Operanzi.AddRange(operatori);

                        instructiuni.Add(instructiune);
                    }
                    else
                    {

                        var date = linie.Split(separatorInstructiuneOperator);
                        Instructiune instructiune = new Instructiune()
                        {
                            Nume = date[0]
                        };
                    }
                }
            }
        }
        private bool EsteDirectivaASCII(string linie)
        {
            if (linie.Contains(simbolASCII))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool EsteLiniaEticheta(string linie)
        {
            if (linie.Contains(simbolEticheta))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static string StergeComentarile(string fileContentRaw)
        {
            var re = @"(@(?:""[^""]*"")+|""(?:[^""\n\\]+|\\.)*""|'(?:[^'\n\\]+|\\.)*')|//.*|/\*(?s:.*?)\*/";
            return Regex.Replace(fileContentRaw, re, "$1");
        }
    }
}
