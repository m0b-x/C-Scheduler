using System.Diagnostics;

namespace Scheduler
{
    public partial class SchedulerForm : Form
    {
        private const string InceputFormatText = @"{\rtf1\ansi";
        private const string SfarsitFormatText = "}";
        private const string LinieNouaRichText = @"\line";
        private const string InceputBoldRichText = @"\b";
        private const string SfarsitBoldRichText = @"\b0";
        private const int IndexAllFiles = 4;
        private Scheduler scheduler = new();
        private Color culoareLabelActivat = System.Drawing.Color.Green;
        private Color culoareLabelDezctivat = System.Drawing.Color.Red;

        private bool seCereMovMerging = false;
        private bool seCereMovReabsorbtion = false;
        private bool seCereImmediateMerging = false;

        private string stringFisierIncarcat = "Fisierul a fost incarcat";
        private string continutFisierRaw;
        private string[] separatori = new string[] { "\n", "\r\n" };
        private string[] fileContent;
        private string filePath;

        private List<Instructiune> instructiuni = new();

        private ParserInstructiuni parserInstructiuni = new();
        private MovMerger movMerger = new();
        private MovReabsorber movReabsorber = new();
        private ImmediateMerger immediateMerger = new();

        public SchedulerForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void labelMovMerging_Click(object sender, EventArgs e)
        {
            if (seCereMovMerging == false)
            {
                labelMovMerging.BackColor = culoareLabelActivat;
                seCereMovMerging = true;
            }
            else
            {
                labelMovMerging.BackColor = culoareLabelDezctivat;
                seCereMovMerging = false;
            }
        }

        private void labelImmediateMerging_Click(object sender, EventArgs e)
        {
            if (seCereImmediateMerging == false)
            {
                labelImmediateMerging.BackColor = culoareLabelActivat;
                seCereImmediateMerging = true;
            }
            else
            {
                labelImmediateMerging.BackColor = culoareLabelDezctivat;
                seCereImmediateMerging = false;
            }
        }

        private void labelMovReabsorbtion_Click(object sender, EventArgs e)
        {
            if (seCereMovReabsorbtion == false)
            {
                labelMovReabsorbtion.BackColor = culoareLabelActivat;
                seCereMovReabsorbtion = true;
            }
            else
            {
                labelMovReabsorbtion.BackColor = culoareLabelDezctivat;
                seCereMovReabsorbtion = false;
            }
        }

        private void buttonLoadFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Text Files (*.txt)|*.txt|Trace Files (*.trc)|*.trc|Ins Files (*.ins)|*.ins|All Files (*.*)|*.*";
                openFileDialog.FilterIndex = IndexAllFiles;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    Stream fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new(fileStream))
                    {
                        continutFisierRaw = reader.ReadToEnd();
                        richBoxCodInitial.Text = continutFisierRaw;

                        continutFisierRaw = ParserInstructiuni.StergeComentarile(continutFisierRaw);

                        fileContent = continutFisierRaw.Split(separatori, StringSplitOptions.RemoveEmptyEntries);

                        DezactiveazaOptiuniForm();

                        parserInstructiuni.ParseazaInstructiuni(fileContent);
                        int index = 0;
                        List<Instructiune> instructiuniNoi = new();


                        while (index < parserInstructiuni.Instructiuni.Count() - 1)
                        {
                            if (Instructiune.EsteEticheta(parserInstructiuni.Instructiuni[index]) ||
                                Instructiune.EsteDirectivaASCII(parserInstructiuni.Instructiuni[index]))
                            {
                                instructiuniNoi.Add(parserInstructiuni.Instructiuni[index]);
                            }
                            else if (Instructiune.EsteDependintaRAW(parserInstructiuni.Instructiuni[index], parserInstructiuni.Instructiuni[index + 1]))
                            {
                                if (seCereMovMerging &&
                                    movMerger.EsteMergeCase(parserInstructiuni.Instructiuni[index], parserInstructiuni.Instructiuni[index + 1]))
                                {
                                    Instructiune instrComentariu = new Instructiune() { Nume = "\n \\* s-a efectuat Mov Merge.*\\" };

                                    instructiuniNoi.Add(instrComentariu);

                                    Instructiune i1 = parserInstructiuni.Instructiuni[index];
                                    Instructiune i2 = parserInstructiuni.Instructiuni[index + 1];
                                    /*
                                    Debug.WriteLine("Mov Merge");
                                    Instructiune.Afiseaza(i1);
                                    Instructiune.Afiseaza(i2);
                                    Debug.WriteLine("=>");
                                    */
                                    movMerger.Merge(ref i1, ref i2);
                                    instructiuniNoi.Add(i1);
                                    instructiuniNoi.Add(i2);
                                    parserInstructiuni.Instructiuni[index] = i1;
                                    parserInstructiuni.Instructiuni[index + 1] = i2;
                                    /*
                                    Instructiune.Afiseaza(i1);
                                    Instructiune.Afiseaza(i2);
                                    Debug.WriteLine("");
                                    */

                                    Instructiune instrSfComentariu = new Instructiune() { Nume = "\\* sfarsit Merge.*\\ \n" };

                                    instructiuniNoi.Add(instrSfComentariu);


                                    index += 2;
                                }
                                else
                                    if (seCereImmediateMerging &&
                                    immediateMerger.EsteMergeCase(parserInstructiuni.Instructiuni[index], parserInstructiuni.Instructiuni[index + 1]))
                                {
                                    Instructiune instrComentariu = new Instructiune() { Nume = "\n \\* s-a efectuat Imm. Merge*\\" };

                                    instructiuniNoi.Add(instrComentariu);

                                    Instructiune i1 = parserInstructiuni.Instructiuni[index];
                                    Instructiune i2 = parserInstructiuni.Instructiuni[index + 1];
                                    /*
                                    Debug.WriteLine("Immediate Merge");
                                    Instructiune.Afiseaza(i1);
                                    Instructiune.Afiseaza(i2);
                                    Debug.WriteLine("=>");
                                    */
                                    immediateMerger.Merge(ref i1, ref i2);
                                    instructiuniNoi.Add(i1);
                                    instructiuniNoi.Add(i2);
                                    parserInstructiuni.Instructiuni[index] = i1;
                                    parserInstructiuni.Instructiuni[index + 1] = i2;
                                    /*
                                    Instructiune.Afiseaza(i1);
                                    Instructiune.Afiseaza(i2);
                                    Debug.WriteLine("");
                                    */

                                    Instructiune instrSfComentariu = new Instructiune() { Nume = "\\* sfarsit Merge.*\\ \n" };

                                    instructiuniNoi.Add(instrSfComentariu);
                                    index+= 2;
                                }
                                else
                                    if (seCereMovReabsorbtion &&
                                    movReabsorber.EsteMergeCase(parserInstructiuni.Instructiuni[index], parserInstructiuni.Instructiuni[index + 1]))
                                {

                                    Instructiune instrComentariu = new Instructiune() { Nume = "\n \\* s-a efectuat Mov Reabs.*\\" };

                                    instructiuniNoi.Add(instrComentariu);

                                    Instructiune i1 = parserInstructiuni.Instructiuni[index];
                                    Instructiune i2 = parserInstructiuni.Instructiuni[index + 1];
                                    /*
                                    Debug.WriteLine("Mov Reabsorber");
                                    Instructiune.Afiseaza(i1);
                                    Instructiune.Afiseaza(i2);
                                    Debug.WriteLine("=>");
                                    */
                                    movReabsorber.Merge(ref i1, ref i2);
                                    instructiuniNoi.Add(i1);
                                    instructiuniNoi.Add(i2);
                                    parserInstructiuni.Instructiuni[index] = i1;
                                    parserInstructiuni.Instructiuni[index + 1] = i2;

                                    Instructiune instrSfComentariu = new Instructiune() { Nume = "\\* sfarsit Merge.*\\ \n" };

                                    instructiuniNoi.Add(instrSfComentariu);
                                    /*
                                    Instructiune.Afiseaza(i1);
                                    Instructiune.Afiseaza(i2);
                                    Debug.WriteLine("");
                                    */
                                    index += 2;
                                }
                            }
                            else
                            {
                                instructiuniNoi.Add(parserInstructiuni.Instructiuni[index]);
                            }
                            index++;
                        }
                        AdaugaInstructiuniNoiInTextBox(instructiuniNoi);

                    }
                }
            }
        }

        private void DezactiveazaOptiuniForm()
        {
            labelFisierIncarcat.BackColor = culoareLabelActivat;
            labelFisierIncarcat.Text = stringFisierIncarcat;
            buttonIncarcaFisier.Enabled = false;
            labelMovMerging.Enabled = false;
            labelMovReabsorbtion.Enabled = false;
            labelImmediateMerging.Enabled = false;
        }

        //bug: cand scrii \0 sau \1 il ia ca un separator

        private void AdaugaInstructiuniNoiInTextBox(List<Instructiune> instructiuniNoi)
        {
            foreach (Instructiune instr in instructiuniNoi)
            {
                string stringInstr;
                if (Instructiune.EsteEticheta(instr) && !Instructiune.EsteDirectivaASCII(instr))
                {
                    stringInstr = $"{instr.Nume} ";
                }
                else if (Instructiune.EsteDirectivaASCII(instr))
                {
                    stringInstr = $"{instr.Nume} ";
                }
                else
                {
                    stringInstr = $"\t{instr.Nume} ";
                }

                foreach (string op in instr.Operanzi)
                {
                    stringInstr += $"{op},";
                }
                stringInstr = stringInstr.Remove(stringInstr.Length - 1);
                stringInstr = stringInstr + Environment.NewLine;
                richBoxCodFinal.Text += stringInstr;
            }
        }

        private void buttonRepornesteAplicatia_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}