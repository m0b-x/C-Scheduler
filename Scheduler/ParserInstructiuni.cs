using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
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
        static char[] caractereSpatii = { ' ', '\t' };

        public List<Instructiune>Instructiuni
        {
            get { return instructiuni; }
            set { instructiuni = value; }
        }

        public void ParseazaInstructiuni(string[] fileContent)
        {
            foreach (string linie in fileContent)
            {
                if (!String.IsNullOrWhiteSpace(linie) && linie.Any(x => !char.IsLetter(x)))
                {
                    if (!EsteLiniaEticheta(linie) && !EsteDirectivaASCII(linie))
                    {
                        string linieFaraSpatii = linie.Trim(caractereSpatii);
                        Debug.WriteLine(linieFaraSpatii);
                        var date = linieFaraSpatii.Split(separatorInstructiuneOperator,2);
                        Instructiune instructiune = new Instructiune()
                        {
                            Nume = date[0]
                        };
                        var operatori = date[1].Split(separatorOperatori);
                        foreach(var op in operatori)
                        {
                            instructiune.Operanzi.Add(op.Trim());
                        }

                        instructiuni.Add(instructiune);
                    }
                    else if(EsteLiniaEticheta(linie) || EsteDirectivaASCII(linie))
                    {
                        string linieFaraSpatii = linie.Trim(caractereSpatii);
                        Instructiune instructiune = new Instructiune()
                        {
                            Nume = linieFaraSpatii
                        };
                        instructiuni.Add(instructiune);
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
