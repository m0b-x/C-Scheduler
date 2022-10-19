using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    public class MovMerger : Merger
    {

        //prima instructiune mov:,a doua x
        //se presupune ca exista RAW intre ele
        public bool Merge(ref Instructiune i1,ref Instructiune i2)
        {
            bool seSuprascrieOInstructiune = false;

            if (i1.Nume.ToLower().Equals("eq"))
            {

                //EQ B1, R0, R0
                //BT B1, label /* instrucţiunea care se infiltrează */
                //BRA label
                if (i2.Nume.ToLower() == "bt")
                {
                    if (i1.Operanzi[2].Equals(i1.Operanzi[3]))
                    {
                        string label = i2.Operanzi[1];
                        List<string> operanzi = new(1);
                        Instructiune iNoua = new Instructiune()
                        {
                            Nume = "Bra",
                            Operanzi = operanzi
                        };
                        seSuprascrieOInstructiune = true;
                        return seSuprascrieOInstructiune;
                    }
                }
                //EQ B3, R0, R0 /* B3 := true */
                //TB3 ADD R10, R11, R12 /* instrucţiunea care se infiltrează */
                //=>EQ B3, R0, R0; ADD R10, R11, R12
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
                                Debug.WriteLine("Caz 1 Add");
                                if (i2.Operanzi[1] == i1.Operanzi[0])
                                {
                                    i2.Operanzi[1] = i1.Operanzi[1];
                                }
                                if (i2.Operanzi[2] == i1.Operanzi[0])
                                {
                                    i2.Operanzi[2] = i1.Operanzi[1];
                                }
                                Instructiune.Afiseaza(i2);
                                return seSuprascrieOInstructiune;
                            }
                            else
                            {
                                Debug.WriteLine("Caz 2 Add");
                                //MOV R6, #4
                                //ADD R3, R6, #5
                                //=>MOV R6, #4; MOV R3, #9
                                if (Instructiune.EsteOperandulValoareImediata(i2.Operanzi[2]))
                                {
                                    int suma = int.Parse(i1.Operanzi[1].Remove(0, 1) + int.Parse(i2.Operanzi[2].Remove(0, 1)));

                                    string operandDestinatie = i2.Operanzi[0];

                                    List<string> operanziNoi = new List<string>(2);
                                    operanziNoi.Add(operandDestinatie);
                                    operanziNoi.Add(Instructiune.SimbolValoareImediata + suma);

                                    Instructiune i2Noua = new Instructiune()
                                    {
                                        Nume = i1.Nume,
                                        Operanzi = operanziNoi
                                    };
                                    Instructiune.Afiseaza(i2Noua);
                                    return seSuprascrieOInstructiune;

                                }
                                //MOV R6, #4
                                //ADD R3, #5, R6
                                //=>MOV R6, #4; MOV R3, #9
                                else if (Instructiune.EsteOperandulValoareImediata(i2.Operanzi[1]))
                                {
                                    Debug.WriteLine("Caz 3 Add");
                                    int suma = int.Parse(i1.Operanzi[1].Remove(0, 1) + int.Parse(i2.Operanzi[1].Remove(0, 1)));

                                    string operandDestinatie = i2.Operanzi[0];

                                    List<string> operanziNoi = new List<string>(2);
                                    operanziNoi.Add(operandDestinatie);
                                    operanziNoi.Add(Instructiune.SimbolValoareImediata + suma);

                                    Instructiune i2Noua = new Instructiune()
                                    {
                                        Nume = i1.Nume,
                                        Operanzi = operanziNoi
                                    };
                                    Instructiune.Afiseaza(i2Noua);
                                    return seSuprascrieOInstructiune;
                                }
                            }
                            break;
                        }
                    //MOV R3, #0
                    //ST (R1, R2), R3 /* instrucţiunea care se infiltrează */ Secvenţa modificată:
                    //=>MOV R3, #0; ST (R1, R2), R0
                    case "store":
                        {
                            Debug.WriteLine("Caz Store");
                            if (Instructiune.EsteOperandulValoareImediata(i1.Operanzi[1]))
                            {
                                string operandDestinatie = i2.Operanzi[0];

                                List<string> operanziNoi = new List<string>(2);
                                operanziNoi.Add(operandDestinatie);
                                operanziNoi.Add(i1.Operanzi[1]);

                                Instructiune i2Noua = new Instructiune()
                                {
                                    Nume = i2.Nume,
                                    Operanzi = operanziNoi
                                };
                                Instructiune.Afiseaza(i2Noua);
                                return seSuprascrieOInstructiune;
                            }
                            break;
                        }
                    //MOV R4, #4
                    //GT B1, R4, R3 
                    //=>MOV R4, #4; LTE B1 R3, #4
                    case "gt":
                        {
                            if (Instructiune.EsteOperandulValoareImediata(i1.Operanzi[1]))
                            {
                                if (i2.Operanzi[1].Equals(i1.Operanzi[0]))
                                {
                                    string operandDestinatie = i2.Operanzi[0];
                                    string operandSursa1 = i2.Operanzi[2];
                                    string operandSursa2 = i1.Operanzi[1];

                                    List<string> operanziNoi = new(2);
                                    operanziNoi.Add(operandDestinatie);
                                    operanziNoi.Add(operandSursa1);
                                    operanziNoi.Add(operandSursa2);

                                    Instructiune instructiuneNoua = new Instructiune()
                                    {
                                        Nume = "LTE",
                                        Operanzi = operanziNoi
                                    };
                                    Instructiune.Afiseaza(instructiuneNoua);
                                    return seSuprascrieOInstructiune;
                                }
                            }
                            break;
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
            return true;
            throw new NotImplementedException();
        }
    }
}
