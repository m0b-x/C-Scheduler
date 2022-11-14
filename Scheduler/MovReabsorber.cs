namespace Scheduler
{
    public class MovReabsorber : Merger
    {
        //Această tehnică implică orice pereche de instrucţiuni
        //care au valori imediate pe post
        //de al doilea operand sursă
        public bool EsteMergeCase(Instructiune i1, Instructiune i2)
        {
            //este RAW

            if (i2.Nume.ToLower().Equals("mov"))
            {
                if (i1.Nume.ToLower().Equals("ld") ||
                   i1.Nume.ToLower().Equals("st"))
                {
                    return false;
                }
                return true;
            }
            return false;
            throw new NotImplementedException();
        }

        //ADD R3, R4, R5
        //MOV R6, R3 /* instrucţiunea care se infiltrează */
        //ADD R3, R4, R5;
        //=>ADD R6, R4, R5
        public bool Merge(ref Instructiune i1, ref Instructiune i2)
        {
            bool seSuprascrieOInstructiune = true;

            List<string> operanziNoi = new(3);
            operanziNoi.AddRange(i1.Operanzi);
            operanziNoi[0] = i2.Operanzi[0];
            Instructiune instructiuneNoua = new()
            {
                Nume = i1.Nume,
                Operanzi = operanziNoi
            };
            i2 = instructiuneNoua;

            return seSuprascrieOInstructiune;
        }
    }
}