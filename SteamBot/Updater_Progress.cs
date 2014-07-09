using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using Ionic.Zip;
using System.Threading;
using SteamBot;
using MetroFramework.Forms;

namespace MistClient
{
    public partial class Updater_Progress : MetroForm
    {
        Updater updater;
        Log log;
        string fileSize;

        public Updater_Progress(Updater updater, Log log)
        {
            InitializeComponent();
            this.updater = updater;
            this.log = log;
            Util.LoadTheme(this, this.Controls);
        }

        private void Updater_Progress_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            log.Info("[UPDATER] Beginning the update process...");
            if (File.Exists("Update.zip"))
            {
                File.Delete("Update.zip");
            }
            // the URL to download the file from
            string sUrlToReadFileFrom = "http://jzhang.net/mist/releases/Mist_latest.zip";
            // the path to write the file to
            string sFilePathToWriteFileTo = "Update.zip";
            // first, we need to get the exact size (in bytes) of the file we are downloading
            Uri url = new Uri(sUrlToReadFileFrom);
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
            response.Close();
            if (response == null || response.StatusCode != HttpStatusCode.OK)
            {
                log.Error("[UPDATER] Error contacting the update server! Aborting.");
                MetroFramework.MetroMessageBox.Show(this, "Could not contact the update server. Please try again later.",
                                    "Connection Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error,
                                    MessageBoxDefaultButton.Button1);
                this.Close();
                return;
            }
            //string fileName = "Mist.exe";
            //string oldFileName = "Mist.exe.old";
            //File.Move(fileName, oldFileName);
            // gets the size of the file in bytes
            Int64 iSize = response.ContentLength;
            double sizeMB = (double)iSize / 1048576;
            fileSize = sizeMB.ToString("0.00");
            if (fileSize != null)
            {
                metroLabel1.Text += " (" + fileSize + " MB)";
            }
            // keeps track of the total bytes downloaded so we can update the progress bar
            Int64 iRunningByteTotal = 0;
            // use the webclient object to download the file
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                // open the file at the remote URL for reading
                using (System.IO.Stream streamRemote = client.OpenRead(new Uri(sUrlToReadFileFrom)))
                {
                    // using the FileStream object, we can write the downloaded bytes to the file system
                    using (Stream streamLocal = new FileStream(sFilePathToWriteFileTo, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        // loop the stream and get the file into the byte buffer
                        int iByteSize = 0;
                        byte[] byteBuffer = new byte[1024];
                        while ((iByteSize = streamRemote.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                        {
                            // write the bytes to the file system at the file path specified
                            streamLocal.Write(byteBuffer, 0, iByteSize);
                            iRunningByteTotal += iByteSize;
                            // calculate the progress out of a base "100"
                            double dIndex = (double)(iRunningByteTotal);
                            double dTotal = (double)iSize;
                            double dProgressPercentage = (dIndex / dTotal);
                            int iProgressPercentage = (int)(dProgressPercentage * 100);
                            // update the progress bar
                            backgroundWorker1.ReportProgress(iProgressPercentage);
                        }
                        // clean up the file stream
                        streamLocal.Close();
                    }
                    // close the connection to the remote server
                    streamRemote.Close();
                }
            }            
            if (!File.Exists("Update.zip"))
            {
                log.Error("[UPDATER] The file has failed to download.");
                MetroFramework.MetroMessageBox.Show(this, "The file has failed to download.",
                                    "Downloading Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error,
                                    MessageBoxDefaultButton.Button1);
                this.Close();
                return;
            }
            using (var zip = Ionic.Zip.ZipFile.Read("Update.zip"))
            {
                log.Info("[UPDATER] Extracting zip file...");
                if (File.Exists("Mist.exe"))
                {
                    log.Info("[UPDATER] Renamed Mist.exe to prepare for deletion.");
                    File.Move("Mist.exe", "Mist.exe.old");
                }
                foreach (ZipEntry ex in zip)
                {
                    foreach (var file in Directory.GetFiles(Path.Combine(Application.StartupPath, "lib")))
                    {
                        string fileName = file;
                        if (fileName.EndsWith(ex.FileName.Replace('/', '\\')))
                        {
                            File.Move(file, file + ".old");
                            log.Info("[UPDATER] Renaming file " + file + " to prepare for deletion.");
                        }
                    }
                    log.Info("[UPDATER] Extracting " + ex.FileName + "...");
                    try
                    {
                        ex.Extract(Application.StartupPath, ExtractExistingFileAction.OverwriteSilently);
                    }
                    catch (Exception err)
                    {
                        log.Error("[UPDATER] Error extracting: " + err.Message);
                    }
                }
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progress_download.Value = e.ProgressPercentage;
            
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                log.Error("[UPDATER] " + e.Error.Message);
                if (File.Exists("Mist.exe.old"))
                {
                    File.Move("Mist.exe.old", "Mist.exe");
                }
            }
            try
            {
                File.Delete("Update.zip");
                File.Delete("Mist.exe.PendingOverwrite");
                File.Delete("Mist.exe.tmp");
                File.Delete("Mist.exe.old");
            }
            catch
            {

            }
            log.Success("[UPDATER] Mist has been successfully updated!");
            MetroFramework.MetroMessageBox.Show(this, "Mist has been successfully updated!",
                                    "Success",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information,
                                    MessageBoxDefaultButton.Button1);
            this.Close();
            updater.CloseUpdater();
        }

        private void Updater_Progress_FormClosed(object sender, FormClosedEventArgs e)
        {
            updater.CloseUpdater();
        }
    }
}
