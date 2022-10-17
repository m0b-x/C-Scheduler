namespace Scheduler
{
    public partial class SchedulerForm : Form
    {
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
                openFileDialog.FilterIndex = 2;
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
                    }
                }
            }
        }

        private void buttonOptimize_Click(object sender, EventArgs e)
        {
            parserInstructiuni.ParseazaInstructiuni(fileContent);
            richBoxCodFinal.Text += fileContentRaw;
        }
    }
}