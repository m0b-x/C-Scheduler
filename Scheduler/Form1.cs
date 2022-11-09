namespace Scheduler
{
    public partial class SchedulerForm : Form
    {
        Scheduler scheduler = new();
        Color colorEnabled = System.Drawing.Color.Green;
        Color colorDisabled = System.Drawing.Color.Red;

        bool textIsLoaded = false;

        bool movMergingIsEnabled = false;
        bool movReabsorbtionIsEnabled = false;
        bool immediateMergingIsEnabled = false;

        string fileIsLoadedString = "Fisierul a fost incarcat";
        string fileContentRaw;
        string[] separatori = new string[] { "\n", "\r\n" };
        string[] fileContent;
        string filePath;


        List<Instructiune> instructiuni = new();

        ParserInstructiuni parserInstructiuni = new();
        MovMerger movMerger = new();
        MovReabsorber movReabsorber = new();
        ImmediateMerger immediateMerger = new();
        public SchedulerForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void labelMovMerging_Click(object sender, EventArgs e)
        {
            if (movMergingIsEnabled == false)
            {
                labelMovMerging.BackColor = colorEnabled;
                movMergingIsEnabled = true;
            }
            else
            {
                labelMovMerging.BackColor = colorDisabled;
                movMergingIsEnabled = false;
            }
        }

        private void labelImmediateMerging_Click(object sender, EventArgs e)
        {
            if (immediateMergingIsEnabled == false)
            {
                labelImmediateMerging.BackColor = colorEnabled;
                immediateMergingIsEnabled = true;
            }
            else
            {
                labelImmediateMerging.BackColor = colorDisabled;
                immediateMergingIsEnabled = false;
            }
        }

        private void labelMovReabsorbtion_Click(object sender, EventArgs e)
        {
            if (movReabsorbtionIsEnabled == false)
            {
                labelMovReabsorbtion.BackColor = colorEnabled;
                movReabsorbtionIsEnabled = true;
            }
            else
            {
                labelMovReabsorbtion.BackColor = colorDisabled;
                movReabsorbtionIsEnabled = false;
            }
        }

        private void buttonLoadFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Text Files (*.txt)|*.txt|Trace Files (*.trc)|*.trc|Ins Files (*.ins)|*.ins|All Files (*.*)|*.*";
                openFileDialog.FilterIndex = 4;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;

                    Stream fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new(fileStream))
                    {
                        fileContentRaw = reader.ReadToEnd();
                        richBoxCodInitial.Text = fileContentRaw;

                        fileContentRaw = ParserInstructiuni.StergeComentarile(fileContentRaw);

                        fileContent = fileContentRaw.Split(separatori, StringSplitOptions.RemoveEmptyEntries);

                        textIsLoaded = true;
                        loadedFileLabel.BackColor = colorEnabled;
                        loadedFileLabel.Text = fileIsLoadedString;

                        parserInstructiuni.ParseazaInstructiuni(fileContent);
                        int i = 0;
                        List<Instructiune> instructiuniNoi = new();

                        List<int> liniiSchimbate = new();
                        while (i < parserInstructiuni.Instructiuni.Count() - 1)
                        {
                            if (Instructiune.EsteEticheta(parserInstructiuni.Instructiuni[i]) ||
                                Instructiune.EsteDirectivaASCII(parserInstructiuni.Instructiuni[i]))
                            {

                                instructiuniNoi.Add(parserInstructiuni.Instructiuni[i]);
                            }
                            else if (Instructiune.EsteDependintaRAW(parserInstructiuni.Instructiuni[i], parserInstructiuni.Instructiuni[i + 1]))
                            {
                                if (movMergingIsEnabled &&
                                    movMerger.IsMergeCase(parserInstructiuni.Instructiuni[i], parserInstructiuni.Instructiuni[i + 1]))
                                {
                                    Instructiune i1 = parserInstructiuni.Instructiuni[i];
                                    Instructiune i2 = parserInstructiuni.Instructiuni[i + 1];
                                    /*
                                    Debug.WriteLine("Mov Merge");
                                    Instructiune.Afiseaza(i1);
                                    Instructiune.Afiseaza(i2);
                                    Debug.WriteLine("=>");
                                    */
                                    movMerger.Merge(ref i1, ref i2);
                                    instructiuniNoi.Add(i1);
                                    //instructiuniNoi.Add(i2);
                                    parserInstructiuni.Instructiuni[i] = i1;
                                    parserInstructiuni.Instructiuni[i + 1] = i2;
                                    /*
                                    Instructiune.Afiseaza(i1);
                                    Instructiune.Afiseaza(i2);
                                    Debug.WriteLine("");
                                    */
                                    liniiSchimbate.Add(i);
                                    liniiSchimbate.Add(i + 1);
                                    i++;
                                }
                                else
                                    if (immediateMergingIsEnabled &&
                                    immediateMerger.IsMergeCase(parserInstructiuni.Instructiuni[i], parserInstructiuni.Instructiuni[i + 1]))
                                {
                                    Instructiune i1 = parserInstructiuni.Instructiuni[i];
                                    Instructiune i2 = parserInstructiuni.Instructiuni[i + 1];
                                    /*
                                    Debug.WriteLine("Immediate Merge");
                                    Instructiune.Afiseaza(i1);
                                    Instructiune.Afiseaza(i2);
                                    Debug.WriteLine("=>");
                                    */
                                    immediateMerger.Merge(ref i1, ref i2);
                                    instructiuniNoi.Add(i1);
                                    parserInstructiuni.Instructiuni[i] = i1;
                                    parserInstructiuni.Instructiuni[i + 1] = i2;
                                    /*
                                    Instructiune.Afiseaza(i1);
                                    Instructiune.Afiseaza(i2);
                                    Debug.WriteLine("");
                                    */
                                    liniiSchimbate.Add(i);
                                    liniiSchimbate.Add(i + 1);
                                    i++;
                                }
                                else
                                    if (movReabsorbtionIsEnabled &&
                                    movReabsorber.IsMergeCase(parserInstructiuni.Instructiuni[i], parserInstructiuni.Instructiuni[i + 1]))
                                {
                                    Instructiune i1 = parserInstructiuni.Instructiuni[i];
                                    Instructiune i2 = parserInstructiuni.Instructiuni[i + 1];
                                    /*
                                    Debug.WriteLine("Mov Reabsorber");
                                    Instructiune.Afiseaza(i1);
                                    Instructiune.Afiseaza(i2);
                                    Debug.WriteLine("=>");
                                    */
                                    movReabsorber.Merge(ref i1, ref i2);
                                    instructiuniNoi.Add(i1);
                                    parserInstructiuni.Instructiuni[i] = i1;
                                    parserInstructiuni.Instructiuni[i + 1] = i2;
                                    /*
                                    Instructiune.Afiseaza(i1);
                                    Instructiune.Afiseaza(i2);
                                    Debug.WriteLine("");
                                    */
                                    liniiSchimbate.Add(i);
                                    liniiSchimbate.Add(i + 1);
                                    i++;
                                }
                            }
                            else
                            {
                                instructiuniNoi.Add(parserInstructiuni.Instructiuni[i]);
                            }
                            i++;

                        }
                        System.Text.StringBuilder textVechi = new();
                        foreach (Instructiune instr in instructiuniNoi)
                        {
                            string stringInstr;
                            if (Instructiune.EsteEticheta(instr) && !Instructiune.EsteDirectivaASCII(instr))
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

                        System.Text.StringBuilder textNou = new();
                        textNou.Append(@"{\rtf1\ansi");
                        int index = 0;
                        foreach (string? linie in richBoxCodFinal.Lines)
                        {
                            if (liniiSchimbate.Contains(index))
                            {
                                string linieSchimbata = @"\b" + @linie + @"\b0";
                                textNou.Append(@linieSchimbata);
                                textNou.AppendLine(@"\line");
                            }
                            else
                            {
                                textNou.Append(@linie);
                                textNou.AppendLine(@"\line");
                            }
                            index++;
                        }
                        textNou.Append("}");
                        richBoxCodFinal.Rtf = textNou.ToString();
                    }
                }
            }
        }

    }
}