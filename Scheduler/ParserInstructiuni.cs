using System.Text.RegularExpressions;

namespace Scheduler
{
    public class ParserInstructiuni
    {

        List<Instructiune> instructiuni = new();

        char separatorInstructiuneOperator = ' ';
        char separatorOperatori = ',';
        string marcajInceputComentariu = "/*";
        string marcajSfarsitComentariu = "*/";
        char simbolEticheta = ':';
        char simbolASCII = '.';
        static char[] caractereSpatii = { ' ', '\t' };

        public List<Instructiune> Instructiuni
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
                        string[] date = linieFaraSpatii.Split(separatorInstructiuneOperator, 2);
                        Instructiune instructiune = new()
                        {
                            Nume = date[0]
                        };
                        string[] operatori = date[1].Split(separatorOperatori);
                        foreach (string op in operatori)
                        {
                            instructiune.Operanzi.Add(op.Trim());
                        }

                        instructiuni.Add(instructiune);
                    }
                    else
                    {
                        if (EsteDirectivaASCII(linie))
                        {
                            string linieFaraSpatii = linie.Trim(caractereSpatii);
                            string[] date = linieFaraSpatii.Split(separatorInstructiuneOperator, 2);
                            Instructiune instructiune = new()
                            {
                                Nume = date[0],
                                Operanzi = new() { date[1] }
                            };
                            instructiuni.Add(instructiune);
                        }
                        else
                        {
                            string linieFaraSpatii = linie.Trim(caractereSpatii);
                            Instructiune instructiune = new()
                            {
                                Nume = linieFaraSpatii
                            };
                            instructiuni.Add(instructiune);
                        }
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
            string re = @"(@(?:""[^""]*"")+|""(?:[^""\n\\]+|\\.)*""|'(?:[^'\n\\]+|\\.)*')|//.*|/\*(?s:.*?)\*/";
            return Regex.Replace(fileContentRaw, re, "$1");
        }
    }
}
