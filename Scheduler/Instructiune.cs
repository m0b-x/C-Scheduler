using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    public class Instructiune
    {
        private string numeInstructiune;
        private List<string> operanzi = new List<string>();

        public int NumarOperanzi
        {
            get { return operanzi.Count; }
        }
        public string Nume
        {
            get { return numeInstructiune; }
            set {  numeInstructiune = value; }
        }
        public List<string> Operanzi
        {
            get { return operanzi; } 
            set { operanzi = value; }
        }
        
        public bool EsteEticheta()
        {
            if(operanzi.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
