using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.FileIO;
using SearchOption = System.IO.SearchOption;

namespace MovieSorter
{
    public partial class MovieSorter : Form
    {
        public MovieSorter()
        {
            InitializeComponent();
        }

        private void MovieSorter_Load(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("", 24);
            listView1.Columns.Add("#");
            listView1.Columns.Add("Folder");
            listView1.CheckBoxes = true;
        }


        private void Counter(List<string> match)
        {
            if (match.Count > 1 || match.Count == 0) Count.Text = match.Count + @" Items in MatchList";
            else Count.Text = match.Count + @" Item in MatchList";
        }

        private void Add(string box, string index, string path)
        {
            progressBar1.PerformStep();
            string[] row = { box, index, path };
            var item = new ListViewItem(row);
            listView1.Items.Add(item);

        }

        private void Browes_Source_Click(object sender, EventArgs e)
        {
            var fs = new FolderSelectDialog();
            var result = fs.ShowDialog();
            if (!result) return;
            Source_dir.Text = fs.FileName;
        }




        private void Query()
        {
            var filters =
                new Regex(
                    @"(CAMRip|CAM|TS|TELESYNC|PDVD|PTVD|PPVRip|SCR|SCREENER|DVDSCR|DVDSCREENER|BDSCR|R4|R5|R5LINE|R5.LINE|DVD|DVD5|DVD9|DVDRip|DVDR|TVRip|DSR|PDTV|SDTV|HDTV|HDTVRip|DVB|DVBRip|DTHRip|VODRip|VODR|BDRip|BRRip|BR.Rip|BluRay|Blu.Ray|BD|BDR|BD25|BD50|3D.BluRay|3DBluRay|3DBD|Remux|BDRemux|BR.Scr|BR.Screener|HDDVD|HDRip|WorkPrint|VHS|VCD|TELECINE|WEBRip|WEB.Rip|WEBDL|WEB.DL|WEBCap|WEB.Cap|ithd|iTunesHD|Laserdisc|AmazonHD|NetflixHD|NetflixUHD|VHSRip|LaserRip|URip|UnknownRip|MicroHD|WP|TC|PPV|DDC|R5.AC3.5.1.HQ|DVD-Full|DVDFull|Full-Rip|FullRip|DSRip|SATRip|BD5|BD9|Extended|Uncensored|Remastered|Unrated|Uncut|IMAX|(Ultimate.)?(Director.?s|Theatrical|Ultimate|Final|Rogue|Collectors|Special|Despecialized).(Cut|Edition|Version)|((H|HALF|F|FULL)[^\\p{Alnum}]{0,2})?(SBS|TAB|OU)|DivX|Xvid|AVC|(x|h)[.]?(264|265)|HEVC|3ivx|PGS|MP[E]?G[45]?|MP[34]|(FLAC|AAC|AC3|DD|MA).?[2457][.]?[01]|[26]ch|(Multi.)?DTS(.HD)?(.MA)?|FLAC|AAC|AC3|TrueHD|Atmos|[M0]?(420|480|720|1080|1440|2160)[pi]|(?<=[-.])(420|480|720|1080|2D|3D)|10.?bit|(24|30|60)FPS|Hi10[P]?|[a-z]{2,3}.(2[.]0|5[.]1)|(19|20)[0-9]+(.)S[0-9]+(?!(.)?E[0-9]+)|(?<=\\d+)v[0-4]|CD\\d+|3D|2D)");
            var baseURL = "http://www.omdbapi.com/";
            listView1.Items.Clear();
            var match = new List<string>();
            var ignore = new List<string>();

            if (Directory.Exists(Source_dir.Text))
            {
                var allfiles = GetFiles(Source_dir.Text, "*.*");
                var i = 1;
                foreach (var name in allfiles)
                {
                    var potential = new List<dynamic>();
                    var dir = Path.GetDirectoryName(name);
                    if (!(match.Contains(dir)) && !(ignore.Contains(dir)) && !(match.Contains(name)))
                    {
                        var extension = Path.GetExtension(name);
                        if (extension == null) continue;
                        var ext = extension.ToLower();
                        if (ext.Equals(".mp4") || ext.Equals(".avi") || ext.Equals(".mkv"))
                        {
                            var file = Path.GetFileNameWithoutExtension(name);
                            var sp = new Regex("[sS][0-9]{2}[eE][0-9]{2}");
                            if (sp.IsMatch(file))
                            {
                                var S_pattern = "[sS][0-9]{2}";
                                var s = Regex.Match(file, S_pattern);
                                var series = s.Value;
                                series = series.Replace("S", " ");
                                sp = new Regex("[sS][0-9]{2}[eE][0-9]{2}.*");
                                var ep = sp.Replace(file, string.Empty);
                                var uri = baseURL + "?t=" + ep + "&Season=" + series + "&r=json";
                                var request = WebRequest.Create(uri);
                                request.Credentials = CredentialCache.DefaultCredentials;
                                var response = request.GetResponse();
                                // Get the stream containing content returned by the server.
                                var dataStream = response.GetResponseStream();
                                // Open the stream using a StreamReader for easy access.
                                var reader = new StreamReader(dataStream);
                                // Read the content.
                                var responseFromServer = reader.ReadToEnd();
                                // Display the content.
                                dynamic result = JsonConvert.DeserializeObject<dynamic>(responseFromServer);
                                var episodes = result.Episodes;
                                // Clean up the streams and the response.
                                reader.Close();
                                response.Close();
                                if (TestYear(episodes))
                                {
                                    match.Add(dir);
                                    progressBar1.Visible = true;
                                    progressBar1.Minimum = 0;
                                    progressBar1.Maximum = match.Count;
                                    progressBar1.Value = 0;
                                    progressBar1.Step = 1;
                                }
                                else ignore.Add(dir);
                            }

                            else
                            {
                                {
                                    var mPattern = "[0-9]{4}";
                                    var m = Regex.Match(file, mPattern);
                                    int year;
                                    if (!m.Success) year = 0;
                                    else year = int.Parse(m.Value);
                                    if (year >= 1900 && year < 2100)
                                    {
                                        if (year == 2016) match.Add(name);
                                    }
                                    else
                                    {
                                        var tmp = file.Split('.');
                                        var tmpName = "";
                                        foreach (var item in tmp)
                                        {
                                            if (TestWord(item, filters)) tmpName += item + ".";
                                            else break;
                                        }
                                        tmpName = tmpName.Substring(0, tmpName.Length - 1);
                                        var uri = baseURL + "?s=" + tmpName + "&type=movie&r=json";
                                        tmpName = tmpName.Replace(".", " ");
                                        var request = WebRequest.Create(uri);
                                        request.Credentials = CredentialCache.DefaultCredentials;
                                        var response = request.GetResponse();
                                        var dataStream = response.GetResponseStream();
                                        var reader = new StreamReader(dataStream);
                                        var responseFromServer = reader.ReadToEnd();
                                        dynamic result = JsonConvert.DeserializeObject<dynamic>(responseFromServer);
                                        string tmpResult = result.ToString();
                                        response.Close();
                                        dataStream.Close();
                                        reader.Close();
                                        if (result.Response != "False")
                                        {
                                            tmpResult = tmpResult.Replace("\r\n", string.Empty);
                                            var start = tmpResult.IndexOf("[", StringComparison.Ordinal);
                                            tmpResult = tmpResult.Substring(start, tmpResult.Length - start);
                                            var end = tmpResult.LastIndexOf("]", StringComparison.Ordinal) + 1;
                                            tmpResult = tmpResult.Substring(0, end);
                                            //tmpResult = tmpResult.Remove(tmpResult.Trim().Length - 1);
                                            result = JsonConvert.DeserializeObject<dynamic>(tmpResult);
                                            foreach (var item in result)
                                            {
                                                if (item.Title.ToString().Equals(tmpName) &&
                                                    item.Type.ToString().Equals("movie")) potential.Add(item);
                                            }
                                            if (potential.Count == 1)
                                            {
                                                if (potential[0].Year.ToString() == "2016")
                                                    match.Add(name);
                                            }

                                            else if(potential.Count != 0)
                                            {
                                                var msg = new MsgBox();
                                                var imdburl = "http://www.imdb.com/title/";
                                                var x = 25;
                                                var y = 30;
                                                foreach (var potentialItem in potential)
                                                {

                                                    //imdbID = imdburl + potentialItem.imdbID.ToString();
                                                    msg.AddLabeles(imdburl + potentialItem.imdbID.ToString(), x, y);
                                                    y += 20;
                                                }
                                                msg.MyTitle = tmpName + " Conflicts";
                                                msg.ShowDialog();
                                                foreach (var selected in potential)
                                                {
                                                    if (selected.imdbID.ToString().Equals(msg.MyID))
                                                        if (selected.Year.ToString() == "2016")
                                                            match.Add(name);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }


                foreach (var film in match)
                {
                    var co = i++;
                    Add("", co.ToString(), film);

                }
                listView1.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
                listView1.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.HeaderSize);
                listView1.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.ColumnContent);
                listView1.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.HeaderSize);
                Counter(match);
            }

            else MessageBox.Show(@"No such Folder");

        }

        private bool TestWord(string arg, Regex filters)
        {
            if (filters.IsMatch(arg)) return false;
            return true;
        }

        private static bool TestYear(dynamic episodes)
        {
            foreach (var episode in episodes)
            {
                string releas = episode.Released.ToString();
                if (releas.Contains("2016")) return true;

            }
            return false;
        }

        private static IEnumerable<string> GetFiles(string path, string pattern)
        {
            var files = new List<string>();

            try
            {
                files.AddRange(Directory.GetFiles(path, pattern, SearchOption.TopDirectoryOnly));
                foreach (var directory in Directory.GetDirectories(path))
                    files.AddRange(GetFiles(directory, pattern));
            }
            catch
            {
                Console.WriteLine(@"Opps!");
            }

            return files;
        }

        private void Move_button_Click(object sender, EventArgs e)
        {
            if (Source_dir.Text == "")
            {
                errorProvider1.SetIconPadding(Move_button, -90);
                errorProvider1.SetError(Move_button, "Set Source dir first");
            }
            else if (Distension_dir.Text == "")
            {
                errorProvider1.SetIconPadding(Move_button, -90);
                errorProvider1.SetError(Move_button, "Set Distension dir first");
            }
            else
            {
                foreach (ListViewItem item in listView1.Items)
                {
                    if (!item.Checked) continue;
                    try
                    {
                        var baseDir = Source_dir.Text;
                        var source = item.SubItems[2].Text;
                        var size = baseDir.Length;
                        var part = source.Remove(0, size);
                        var dest = Distension_dir.Text + part;
                        var attr = File.GetAttributes(item.SubItems[2].Text);
                        if (attr != FileAttributes.Directory)
                        {
                            if (Directory.Exists(Path.GetDirectoryName(dest)))

                                //FileSystem.CopyFile(source, dest, UIOption.AllDialogs);
                                FileSystem.MoveFile(source, dest, UIOption.AllDialogs);


                            else
                            {
                                Directory.CreateDirectory(Path.GetDirectoryName(dest));
                                FileSystem.MoveFile(source, dest, UIOption.AllDialogs);
                            }
                        }
                        else
                        {
                            FileSystem.MoveDirectory(source, dest, UIOption.AllDialogs);
                            //DirectoryCopy(source, dest, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void Browes_Destination_Click(object sender, EventArgs e)
        {
            var fs = new FolderSelectDialog();
            var result = fs.ShowDialog();
            if (!result) return;
            Distension_dir.Text = fs.FileName;
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        private void Check_All_CheckedChanged(object sender, EventArgs e)
        {
            if (Check_All.Checked)
            {
                for (var i = 0; i < listView1.Items.Count; i++)
                {
                    listView1.Items[i].Checked = true;
                }
            }
            else
            {
                for (var i = 0; i < listView1.Items.Count; i++)
                {
                    listView1.Items[i].Checked = false;
                }
            }
        }

        private void Go_button_Click(object sender, EventArgs e)
        {
            Query();
        }

        private void Source_dir_TextChanged(object sender, EventArgs e)
        {
            if (Source_dir.Text == "" || Distension_dir.Text == "")
                Go_button.Enabled = false;
            else Go_button.Enabled = true;
        }

        private void Distension_dir_TextChanged(object sender, EventArgs e)
        {
            if (Source_dir.Text == "" || Distension_dir.Text == "")
                Go_button.Enabled = false;
            else Go_button.Enabled = true;
        }
    }
}
