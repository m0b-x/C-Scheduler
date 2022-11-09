using System.Text;

namespace Scheduler
{
    public class MovMerger : Merger
    {

        //prima instructiune mov:,a doua x
        //se presupune ca exista RAW intre ele
        public bool Merge(ref Instructiune i1, ref Instructiune i2)
        {
            bool seSuprascrieOInstructiune = false;

            if (i1.Nume.ToLower().Equals("eq"))
            {

                //EQ B1, R0, R0
                //BT B1, label /* instrucţiunea care se infiltrează */
                //BRA label

                //TODO:NU AI FACUTB
                if (i2.Nume.ToLower().Equals("bt"))
                {
                    if (i1.Operanzi[2].Equals(i1.Operanzi[3]))
                    {
                        string label = i2.Operanzi[1];
                        List<string> operanzi = new(1);
                        Instructiune iNoua = new()
                        {
                            Nume = "Bra",
                            Operanzi = operanzi
                        };
                        i2 = iNoua;
                        seSuprascrieOInstructiune = true;
                        return seSuprascrieOInstructiune;
                    }
                }
                //EQ B3, R0, R0 /* B3 := true */
                //TB3 ADD R10, R11, R12 /* instrucţiunea care se infiltrează */
                //=>EQ B3, R0, R0; ADD R10, R11, R12
                else
                {
                    string[] operanziDinNume = i2.Nume.Split();
                    List<string> operanziNoi = new();
                    operanziNoi.AddRange(operanziDinNume);
                    operanziNoi.RemoveAt(0);
                    operanziNoi.AddRange(i2.Operanzi);

                    Instructiune iNoua = new()
                    {
                        Nume = operanziNoi[0]
                    };
                    operanziNoi.RemoveAt(0);
                    iNoua.Operanzi = operanziNoi;
                    i2 = iNoua;
                    return seSuprascrieOInstructiune;
                }
            }
            if (i1.Nume.ToLower().Equals("NE"))
            {

                //NE B1, R0, R0
                //BT B1, label /* instrucţiunea care se infiltrează */
                //BRA label
                if (i2.Nume.ToLower().Equals("bt"))
                {
                    if (i1.Operanzi[2].Equals(i1.Operanzi[3]))
                    {
                        string label = i2.Operanzi[1];
                        List<string> operanzi = new(1);
                        Instructiune iNoua = new()
                        {
                            Nume = "Bra",
                            Operanzi = operanzi
                        };
                        i2 = iNoua;
                        seSuprascrieOInstructiune = true;
                        return seSuprascrieOInstructiune;
                    }
                }
                //NE B3, R0, R0 /* B3 := true */
                //TB3 ADD R10, R11, R12 /* instrucţiunea care se infiltrează */
                //=>NE B3, R0, R0; ADD R10, R11, R12
                else
                {
                    i2.Nume = i2.Operanzi[0];
                    i2.Operanzi[0] = i2.Operanzi[1];
                    i2.Operanzi[1] = i2.Operanzi[2];
                    i2.Operanzi[2] = i2.Operanzi[3];
                    i2.Operanzi.RemoveAt(4);
                    return seSuprascrieOInstructiune;
                }
            }
            else if (i1.Nume.ToLower().Equals("mov"))
            {
                //MOV B1, B2
                //TB1 LD R4, (R0, R6) /* instrucţiunea care se infiltrează */ Secvenţa combinată:
                //MOV B1, B2; TB2 LD R4, (R0, R6)
                if (i2.Nume.ToLower().Contains("tb"))
                {
                    StringBuilder numeNou = new(i2.Nume);
                    numeNou[2] = i1.Operanzi[1][1];
                    i2.Nume = numeNou.ToString();
                }
                switch (i2.Nume.ToLower())
                {
                    //MOV R6, R7
                    //ADD R3, R6, R5
                    //=>MOV R6, R7; ADD R3, R7, R5
                    //sau
                    //MOV R6, R7
                    //ADD R3, R5, R6
                    //=>MOV R6, R7; ADD R3, R5, R7
                    case "add":
                        {
                            if (!Instructiune.EsteOperandulValoareImediata(i1.Operanzi[1]))
                            {
                                if (i2.Operanzi[1].Equals(i1.Operanzi[0]))
                                {
                                    i2.Operanzi[1] = i1.Operanzi[1];
                                }
                                if (i2.Operanzi[2].Equals(i1.Operanzi[0]))
                                {
                                    i2.Operanzi[2] = i1.Operanzi[1];
                                }
                                return seSuprascrieOInstructiune;
                            }
                            else
                            {
                                //MOV R6, #4
                                //ADD R3, R6, #5
                                //=>MOV R6, #4; MOV R3, #9
                                if (Instructiune.EsteOperandulValoareImediata(i2.Operanzi[2]))
                                {
                                    int suma = int.Parse(i1.Operanzi[1].Remove(0, 1) + int.Parse(i2.Operanzi[2].Remove(0, 1)));

                                    string operandDestinatie = i2.Operanzi[0];

                                    List<string> operanziNoi = new();
                                    operanziNoi.Add(operandDestinatie);
                                    operanziNoi.Add(Instructiune.SimbolValoareImediata + suma);

                                    Instructiune i2Noua = new()
                                    {
                                        Nume = i1.Nume,
                                        Operanzi = operanziNoi
                                    };
                                    i2 = i2Noua;
                                    return seSuprascrieOInstructiune;

                                }
                                //MOV R6, #4
                                //ADD R3, #5, R6
                                //=>MOV R6, #4; MOV R3, #9
                                else if (Instructiune.EsteOperandulValoareImediata(i2.Operanzi[1]))
                                {
                                    int suma = int.Parse(i1.Operanzi[1].Remove(0, 1) + int.Parse(i2.Operanzi[1].Remove(0, 1)));

                                    string operandDestinatie = i2.Operanzi[0];

                                    List<string> operanziNoi = new(2);
                                    operanziNoi.Add(operandDestinatie);
                                    operanziNoi.Add(Instructiune.SimbolValoareImediata + suma);

                                    Instructiune i2Noua = new()
                                    {
                                        Nume = i1.Nume,
                                        Operanzi = operanziNoi
                                    };
                                    i2 = i2Noua;
                                    return seSuprascrieOInstructiune;
                                }
                                else
                                {
                                    i2.Operanzi[1].Replace(i1.Operanzi[0], i1.Operanzi[1]);
                                }
                            }
                            break;
                        }
                    //MOV R3, #0
                    //ST (R1, R2), R3 /* instrucţiunea care se infiltrează */ Secvenţa modificată:
                    //=>MOV R3, #0; ST (R1, R2), R0
                    //sau
                    case "st":
                        {

                            for (int i = 0; i < i2.Operanzi.Count; i++)
                            {
                                i2.Operanzi[i] = i2.Operanzi[i].Replace(i1.Operanzi[0], i1.Operanzi[1]);
                            }
                            return seSuprascrieOInstructiune;
                        }
                    //MOV R4, #4
                    //GT B1, R4, R3 
                    //=>MOV R4, #4; LTE B1 R3, #4
                    case "gt":
                        {

                            for (int i = 0; i < i2.Operanzi.Count; i++)
                            {
                                i2.Operanzi[i] = i2.Operanzi[i].Replace(i1.Operanzi[0], i1.Operanzi[1]);
                            }
                            i2.Nume = "LTE";
                            (i2.Operanzi[1], i2.Operanzi[2]) = (i2.Operanzi[2], i2.Operanzi[1]);
                            return seSuprascrieOInstructiune;
                        }
                    case "lt":
                        {

                            for (int i = 0; i < i2.Operanzi.Count; i++)
                            {
                                i2.Operanzi[i] = i2.Operanzi[i].Replace(i1.Operanzi[0], i1.Operanzi[1]);
                            }
                            i2.Nume = "GTE";
                            (i2.Operanzi[1], i2.Operanzi[2]) = (i2.Operanzi[2], i2.Operanzi[1]);
                            return seSuprascrieOInstructiune;
                        }
                    case "lte":
                        {

                            for (int i = 0; i < i2.Operanzi.Count; i++)
                            {
                                i2.Operanzi[i] = i2.Operanzi[i].Replace(i1.Operanzi[0], i1.Operanzi[1]);
                            }
                            i2.Nume = "GT";
                            (i2.Operanzi[1], i2.Operanzi[2]) = (i2.Operanzi[2], i2.Operanzi[1]);
                            return seSuprascrieOInstructiune;
                        }
                    case "gte":
                        {

                            for (int i = 0; i < i2.Operanzi.Count; i++)
                            {
                                i2.Operanzi[i] = i2.Operanzi[i].Replace(i1.Operanzi[0], i1.Operanzi[1]);
                            }
                            i2.Nume = "LT";
                            (i2.Operanzi[1], i2.Operanzi[2]) = (i2.Operanzi[2], i2.Operanzi[1]);
                            return seSuprascrieOInstructiune;
                        }
                    case "gtu":
                        {

                            for (int i = 0; i < i2.Operanzi.Count; i++)
                            {
                                i2.Operanzi[i] = i2.Operanzi[i].Replace(i1.Operanzi[0], i1.Operanzi[1]);
                            }
                            i2.Nume = "LTU";
                            (i2.Operanzi[1], i2.Operanzi[2]) = (i2.Operanzi[2], i2.Operanzi[1]);
                            return seSuprascrieOInstructiune;
                        }
                    case "gts":
                        {

                            for (int i = 0; i < i2.Operanzi.Count; i++)
                            {
                                i2.Operanzi[i] = i2.Operanzi[i].Replace(i1.Operanzi[0], i1.Operanzi[1]);
                            }
                            i2.Nume = "LTS";
                            (i2.Operanzi[1], i2.Operanzi[2]) = (i2.Operanzi[2], i2.Operanzi[1]);
                            return seSuprascrieOInstructiune;
                        }
                    case "ltu":
                        {

                            for (int i = 0; i < i2.Operanzi.Count; i++)
                            {
                                i2.Operanzi[i] = i2.Operanzi[i].Replace(i1.Operanzi[0], i1.Operanzi[1]);
                            }
                            i2.Nume = "GTU";
                            (i2.Operanzi[1], i2.Operanzi[2]) = (i2.Operanzi[2], i2.Operanzi[1]);
                            return seSuprascrieOInstructiune;
                        }
                    case "lts":
                        {

                            for (int i = 0; i < i2.Operanzi.Count; i++)
                            {
                                i2.Operanzi[i] = i2.Operanzi[i].Replace(i1.Operanzi[0], i1.Operanzi[1]);
                            }
                            i2.Nume = "GTS";
                            (i2.Operanzi[1], i2.Operanzi[2]) = (i2.Operanzi[2], i2.Operanzi[1]);
                            return seSuprascrieOInstructiune;
                        }
                    //MOV B1, B2
                    //BT B1, label /* instrucţiunea care se infiltrează */
                    //=>MOV B1, B2; BT B2, label
                    case "bt":
                        {
                            i2.Operanzi[1] = i1.Operanzi[1];
                            return seSuprascrieOInstructiune;
                        }
                }
            }
            return seSuprascrieOInstructiune;
        }

        public bool IsMergeCase(Instructiune i1, Instructiune i2)
        {
            if (i1.Nume.ToLower().Equals("mov"))
            {
                return true;
            }
            if (i1.Nume.ToLower().Equals("eq") || i1.Nume.ToLower().Equals("ne"))
            {
                if (i1.Operanzi[1].Equals(i1.Operanzi[2]))
                {
                    return true;
                }
            }
            return false;
            throw new NotImplementedException();
        }
    }
}
