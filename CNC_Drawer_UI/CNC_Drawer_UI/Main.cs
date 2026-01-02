using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace CNC_Drawer_UI
{
    public partial class Main : Form
    {
        string path;
        Process process;

        Help help_form;

        float zoom = 1.0f;
        int last_zoom = 1;
        Image img;

        public Main()
        {
            InitializeComponent();
        }

        #region -==- Timers -==-

        // Check COM ports timer.
        private void timerCOMPortUpdate_Tick(object sender, EventArgs e)
        {
            GetComPortsAsynk();
        }

        #endregion

        #region -==- Buttons -==-

        // Browse button click.
        private async void btn_browse_Click(object sender, EventArgs e)
        {
            // Select image file.
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    path = fileDialog.FileName;
                }
            }

            last_zoom = 1;
            numUpDownZoom.Value = 1;

            // Image proccesing and display.
            SendRequestAndTakeImageAsync();
        }

        // Start button click.
        private async void btn_start_Click(object sender, EventArgs e)
        {
            timerCOMPortUpdate.Stop();
            using (HttpClient client = new HttpClient())
            {
                // Send start command, COM port and waits for the end.
                await client.GetAsync($"http://127.0.0.1:5000/start/{cBCom.Text}");
            }
            timerCOMPortUpdate.Start();
        }

        // Help button click.
        private void btn_help_Click(object sender, EventArgs e)
        {
            help_form = new Help();
            help_form.ShowDialog();
        }

        // Exit button click.
        private void btn_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        #region -==- numUpDownZoom Events -==-

        // If key pressed in zoom.
        private void numUpDownZoom_KeyDown(object sender, KeyEventArgs e)
        {
            // If Enter key pressed.
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    SendRequestAndTakeImageAsync();
                    break;
            }
        }

        #endregion

        #region -==- numUpDownFilter events -==-

        // If key pressed in filter.
        private void numUpDownFilter_KeyDown(object sender, KeyEventArgs e)
        {
            // If Enter key pressed.
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    SendRequestAndTakeImageAsync();
                    break;
            }
        }

        #endregion

        #region -==- Form Events -==-

        private void Form1_Load(object sender, EventArgs e)
        {
            // Start COM port update timer.
            timerCOMPortUpdate.Start();

            // Add mouse scroll event.
            panel1.MouseWheel += Mouse_Scroll;
            var p = new DirectoryInfo(Directory.GetCurrentDirectory());    // Get current directory.
            Icon = new Icon(p.Parent.Parent.FullName + "\\src\\Icons\\Main.ico");    // Set form icon.
#if !DEBUG

            // Start server process.
            string pathServer = p.Parent.Parent.Parent.Parent.FullName + "\\CNC_Drawer_BL\\exe\\server.exe";
            process = new Process();
            process.StartInfo.FileName = pathServer;
            process.Start();

#endif

            path = p.Parent.Parent.FullName + "\\src\\default.png";
            SendRequestAndTakeImageAsync();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
#if !DEBUG

            // Close all server processes.
            foreach (var proc in Process.GetProcessesByName("server"))
            {
                proc.Kill();
                proc.WaitForExit();
            }
#endif
        }

        #endregion

        #region -==- User Methods -==-

        private async void SendRequestAndTakeImageAsync()
        {
            // Image byte array.
            byte[] image;

            // Build the request URL.
            string request = $"http://127.0.0.1:5000/open_image/{WebUtility.UrlEncode(path)}";
            request += $"/{(byte)numUpDownZoom.Value}";
            request += $"/{(byte)numUpDownFilter.Value}";

            // Send request and get image.
            using (HttpClient client = new HttpClient())
            {
                image = await client.GetByteArrayAsync(request);
            }

            // Check if image is too big.
            if (Encoding.UTF8.GetString(image) == "0")
            {
                MessageBox.Show("Error: Image is too big", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                numUpDownZoom.Value = last_zoom;
                return;
            }
            last_zoom = (int)numUpDownZoom.Value;

            // Display image in PictureBox.
            using (MemoryStream ms = new MemoryStream(image))
            {
                Image img = null;
                img = Image.FromStream(ms);

                pictureBox1.Image = img;
            }

            // Change picturbox image.
            img = pictureBox1.Image;

            // Update picturebox size depending on zoom.
            pictureBox1.Width = (int)(img.Width * zoom);
            pictureBox1.Height = (int)(img.Height * zoom);

            // Get time.
            string time;
            request = "http://127.0.0.1:5000/time";
            using (HttpClient client = new HttpClient())
            {
                time = await client.GetStringAsync(request);
            }
            lbl_time.Text = time;
        }

        private async void GetComPortsAsynk()
        {
            string coms;    // JSON string of COM ports.
            using (HttpClient ht = new HttpClient())
            {
                coms = await ht.GetStringAsync("http://127.0.0.1:5000/com");    // Get COM ports from server.
            }

            // Convert JSON string to string array.
            string[] a = JsonConvert.DeserializeObject<string[]>(coms);

            bool changed = true;    // Flag indicate if list COM ports changed.
            foreach (string com in cBCom.Items)
            {
                // Checks if the list has been modified.
                if (!(a.Contains(com)))
                {
                    changed = true;
                    break;
                }
            }

            // Update COM port list if changed.
            if (changed)
            {
                cBCom.Items.Clear();
                foreach (string com in a)
                {
                    cBCom.Items.Add(com);
                }

                if (a.Length > 0) cBCom.SelectedIndex = 0;
                else cBCom.Text = "";
            }
        }

        #endregion

        #region -==- Used Events -==-

        private void Mouse_Scroll(object sender, MouseEventArgs e)
        {
            // Get mouse position.
            Point mousePos = e.Location;

            // Update zoom value.
            if (e.Delta > 0) 
            {
                if(zoom + 0.2 < 5f)
                {
                    zoom += 0.2f;
                }
            } 
            else
            {
                if (zoom - 0.2 > 0.2f)
                {
                    zoom -= 0.2f;
                }
            }

            // Update picturebox size depending on zoom.
            pictureBox1.Width = (int)(img.Width * zoom);
            pictureBox1.Height = (int)(img.Height * zoom);
        }

        #endregion
    }
}
