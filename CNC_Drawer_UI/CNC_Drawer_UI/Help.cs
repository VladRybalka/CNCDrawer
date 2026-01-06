using System;
using System.IO;
using System.Windows.Forms;

namespace CNC_Drawer_UI
{
    public partial class Help : Form
    {
        string language = "eng";
        string path;

        public Help()
        {
            InitializeComponent();
        }

        private void btn_En_Click(object sender, EventArgs e)
        {
            // Change language on English.
            language = "eng";
            btn_En.Enabled = false;
            btn_Ukr.Enabled = true;
            SetTextsInTextBoxes();
        }

        private void btn_Ukr_Click(object sender, EventArgs e)
        {
            // Change language on Ukrainian.
            language = "ukr";
            btn_Ukr.Enabled = false;
            btn_En.Enabled = true;
            SetTextsInTextBoxes();
        }

        private void SetTextsInTextBoxes()
        {
            // Indents for correct output
            int cutLengthCom = 5;
            int cutLengthBrowse = 10;
            int cutLengthZoom = 8;
            int cutLengthFilter = 10;
            int cutLengthStart = 9;

            if (language == "eng")
            {
                string text;

                // Read File with Help on English.
                using(StreamReader reader = new StreamReader(path + "\\src\\Help_texts\\Help_Eng.txt"))
                {
                    text = reader.ReadToEnd();
                }

                // Splits into tabs.
                string[] paragraphs = text.Split(new string[] { "\t" }, StringSplitOptions.None);

                // Changes the explanation text in labels.
                lbl_Com.Text = paragraphs[0].Substring(cutLengthCom, paragraphs[0].Length - cutLengthCom);
                lbl_Browse.Text = paragraphs[1].Substring(cutLengthBrowse, paragraphs[1].Length - cutLengthBrowse);
                lbl_Zoom.Text = paragraphs[2].Substring(cutLengthZoom, paragraphs[2].Length - cutLengthZoom);
                lbl_Filter.Text = paragraphs[3].Substring(cutLengthFilter, paragraphs[3].Length - cutLengthFilter);
                lbl_Start.Text = paragraphs[4].Substring(cutLengthStart, paragraphs[4].Length - cutLengthStart);
            }
            else if(language == "ukr")
            {
                string text;

                // Read File with Help on Ukrainian.
                using (StreamReader reader = new StreamReader(path + "\\src\\Help_texts\\Help_Ukr.txt"))
                {
                    text = reader.ReadToEnd();
                }

                // Splits into tabs.
                string[] paragraphs = text.Split(new string[] { "\t" }, StringSplitOptions.None);

                // Changes the explanation text in labels.
                lbl_Com.Text = paragraphs[0].Substring(cutLengthCom, paragraphs[0].Length - cutLengthCom);
                lbl_Browse.Text = paragraphs[1].Substring(cutLengthBrowse, paragraphs[1].Length - cutLengthBrowse);
                lbl_Zoom.Text = paragraphs[2].Substring(cutLengthZoom, paragraphs[2].Length - cutLengthZoom);
                lbl_Filter.Text = paragraphs[3].Substring(cutLengthFilter, paragraphs[3].Length - cutLengthFilter);
                lbl_Start.Text = paragraphs[4].Substring(cutLengthStart, paragraphs[4].Length - cutLengthStart);
            }
        }

        private void Help_Load(object sender, EventArgs e)
        {
            var p = new DirectoryInfo(Directory.GetCurrentDirectory());
            path = p.Parent.Parent.FullName;
            Icon = new System.Drawing.Icon(path + "\\src\\Icons\\Help.ico");

            SetTextsInTextBoxes();
        }
    }
}
