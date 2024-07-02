using CustomControls.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WindowsUtilityApp;

namespace YourNamespace
{
    public partial class MainForm : Form
    {
        private List<ApplicationInfo> appList;

        public MainForm()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            string url = "https://raw.githubusercontent.com/username/repository/branch/filename.xlsx"; // GitHub'daki dosya URL'si
            string downloadPath = Path.Combine(Application.StartupPath, "list.xlsx"); // Uygulamanın çalıştığı dizine kaydet

            FileDownloader downloader = new FileDownloader();
            string filePath = downloader.DownloadFile(url, downloadPath);

            ExcelReader reader = new ExcelReader();
            appList = reader.ReadExcel(filePath);

            int yPos = 10;
            foreach (var app in appList)
            {
                ToggleButton toggleButton = new ToggleButton
                {
                    Name = $"toggle_{app.Id}",
                    Location = new System.Drawing.Point(10, yPos),
                    Width = 50
                };

                Label label = new Label
                {
                    Name = $"label_{app.Id}",
                    Location = new System.Drawing.Point(70, yPos + 5),
                    Text = app.Name,
                    Width = 200
                };

                this.Controls.Add(toggleButton);
                this.Controls.Add(label);

                yPos += 40;
            }
        }

        private List<ApplicationInfo> GetSelectedApps()
        {
            List<ApplicationInfo> selectedApps = new List<ApplicationInfo>();

            foreach (Control control in this.Controls)
            {
                if (control is ToggleButton toggleButton && toggleButton.Checked)
                {
                    string id = toggleButton.Name.Split('_')[1];
                    var app = appList.FirstOrDefault(a => a.Id == id);
                    if (app != null)
                    {
                        selectedApps.Add(app);
                    }
                }
            }

            return selectedApps;
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            var selectedApps = GetSelectedApps();
            WingetInstaller installer = new WingetInstaller();
            installer.InstallApps(selectedApps);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Form Load logic here if needed
        }
    }
}
