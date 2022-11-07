using System;
using System.Diagnostics;

namespace Scheduler
{
    public partial class SchedulerForm : Form
    {
        Scheduler scheduler = new Scheduler();
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


        List<Instructiune> instructiuni = new List<Instructiune>();

        ParserInstructiuni parserInstructiuni = new ParserInstructiuni();
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
            if(immediateMergingIsEnabled == false)
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
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Text Files (*.txt)|*.txt|Trace Files (*.trc)|*.trc|Ins Files (*.ins)|*.ins|All Files (*.*)|*.*";
                openFileDialog.FilterIndex = 4;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;

                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
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
                        while (i < parserInstructiuni.Instructiuni.Count()-1)
                        {
                            if(parserInstructiuni.Instructiuni[i].EsteEticheta())
                            {

                                instructiuniNoi.Add(parserInstructiuni.Instructiuni[i]);
                                i++;
                                continue;
                            }
                            if (Instructiune.EsteDependintaRAW(parserInstructiuni.Instructiuni[i], parserInstructiuni.Instructiuni[i+1]))
                            {
                                if (movMergingIsEnabled &&
                                    movMerger.IsMergeCase(parserInstructiuni.Instructiuni[i], parserInstructiuni.Instructiuni[i + 1]))
                                {
                                    Instructiune i1 = parserInstructiuni.Instructiuni[i];
                                    Instructiune i2 = parserInstructiuni.Instructiuni[i + 1];
                                    Debug.WriteLine("Mov Merge");
                                    Instructiune.Afiseaza(i1);
                                    Instructiune.Afiseaza(i2);
                                    Debug.WriteLine("=>");
                                    movMerger.Merge(ref i1,ref i2);
                                    instructiuniNoi.Add(i1);
                                    instructiuniNoi.Add(i2);
                                    Instructiune.Afiseaza(i1);
                                    Instructiune.Afiseaza(i2);
                                    Debug.WriteLine("");
                                }
                                else
                                    if (immediateMergingIsEnabled &&
                                    immediateMerger.IsMergeCase(parserInstructiuni.Instructiuni[i], parserInstructiuni.Instructiuni[i + 1]))
                                {
                                    Debug.WriteLine("Immediate Merge");
                                    Instructiune i1 = parserInstructiuni.Instructiuni[i];
                                    Instructiune i2 = parserInstructiuni.Instructiuni[i + 1];
                                    Instructiune.Afiseaza(i1);
                                    Instructiune.Afiseaza(i2);
                                    Debug.WriteLine("=>");
                                    immediateMerger.Merge(ref i1, ref i2);
                                    instructiuniNoi.Add(i1);
                                    instructiuniNoi.Add(i2);
                                    Instructiune.Afiseaza(i1);
                                    Instructiune.Afiseaza(i2);
                                    Debug.WriteLine("");
                                }
                                else
                                    if (movReabsorbtionIsEnabled &&
                                    movReabsorber.IsMergeCase(parserInstructiuni.Instructiuni[i], parserInstructiuni.Instructiuni[i + 1]))
                                {
                                    Debug.WriteLine("Mov Reabsorber");
                                    Instructiune i1 = parserInstructiuni.Instructiuni[i];
                                    Instructiune i2 = parserInstructiuni.Instructiuni[i + 1];
                                    Instructiune.Afiseaza(i1);
                                    Instructiune.Afiseaza(i1);
                                    Debug.WriteLine("=>");
                                    movReabsorber.Merge(ref i1, ref i2);
                                    instructiuniNoi.Add(i1);
                                    instructiuniNoi.Add(i2);
                                    Instructiune.Afiseaza(i1);
                                    Instructiune.Afiseaza(i2);
                                    Debug.WriteLine("");
                                }
                            }
                            else
                            {
                                instructiuniNoi.Add(parserInstructiuni.Instructiuni[i]);
                                instructiuniNoi.Add(parserInstructiuni.Instructiuni[i + 1]);
                            }
                            i+=2;

                        }
                        foreach (var instr in instructiuniNoi)
                        {
                            string stringInstr;
                            if (instr.EsteEticheta())
                                stringInstr = $"{instr.Nume} ";
                            else
                                stringInstr = $"\t{instr.Nume} ";

                            foreach (var op in instr.Operanzi)
                            {
                                stringInstr += $"{op},";
                            }
                            stringInstr = stringInstr.Remove(stringInstr.Length - 1);
                            stringInstr = stringInstr + "\n";
                            richBoxCodFinal.Text += stringInstr;

                        }
                    }
                }
            }
        }

    }
}