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
