using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace Scheduler
{
    public class ParserInstructiuni
    {

        ArrayList instructiuni = new ArrayList();

        char separatorInstructiuneOperator = ' ';
        char separatorOperatori = ',';
        string marcajInceputComentariu = "/*";
        string marcajSfarsitComentariu = "*/";
        char simbolEticheta = ':';

        ArrayList Instructiuni
        {
            get { return instructiuni; }
            set { instructiuni = value; }
        }

        public void ParseazaInstructiuni(string[] fileContent)
        {
            foreach (string linie in fileContent)
            {
                if (!EsteLiniaEticheta(linie))
                {
                    var date = linie.Split(separatorInstructiuneOperator);
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
        private bool EsteLiniaEticheta(string linie)
        {
            if (linie.IndexOf(simbolEticheta) == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static string StergeComentarile(string fileContentRaw)
        {
            var re = @"(@(?:""[^""]*"")+|""(?:[^""\n\\]+|\\.)*""|'(?:[^'\n\\]+|\\.)*')|//.*|/\*(?s:.*?)\*/";
            return Regex.Replace(fileContentRaw, re, "$1");
        }
    }
}
