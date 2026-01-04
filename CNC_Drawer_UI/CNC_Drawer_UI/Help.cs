using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            language = "eng";
            btn_En.Enabled = false;
            btn_Ukr.Enabled = true;
            SetTextsInTextBoxes();
        }

        private void btn_Ukr_Click(object sender, EventArgs e)
        {
            language = "ukr";
            btn_Ukr.Enabled = false;
            btn_En.Enabled = true;
            SetTextsInTextBoxes();
        }

        // TODO: Дописать про драйвер.
        private void SetTextsInTextBoxes()
        {
            int cutLengthCom = 5;
            int cutLengthBrowse = 10;
            int cutLengthZoom = 8;
            int cutLengthFilter = 10;
            int cutLengthStart = 9;

            if (language == "eng")
            {
                string text;
                using(StreamReader reader = new StreamReader(path + "\\src\\Help_texts\\Help_Eng.txt"))
                {
                    text = reader.ReadToEnd();
                }
                string[] paragraphs = text.Split(new string[] { "\t" }, StringSplitOptions.None);

                lbl_Com.Text = paragraphs[0].Substring(cutLengthCom, paragraphs[0].Length - cutLengthCom);
                lbl_Browse.Text = paragraphs[1].Substring(cutLengthBrowse, paragraphs[1].Length - cutLengthBrowse);
                lbl_Zoom.Text = paragraphs[2].Substring(cutLengthZoom, paragraphs[2].Length - cutLengthZoom);
                lbl_Filter.Text = paragraphs[3].Substring(cutLengthFilter, paragraphs[3].Length - cutLengthFilter);
                lbl_Start.Text = paragraphs[4].Substring(cutLengthStart, paragraphs[4].Length - cutLengthStart);
            }
            else if(language == "ukr")
            {
                string text;
                using (StreamReader reader = new StreamReader(path + "\\src\\Help_texts\\Help_Ukr.txt"))
                {
                    text = reader.ReadToEnd();
                }
                string[] paragraphs = text.Split(new string[] { "\t" }, StringSplitOptions.None);
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
