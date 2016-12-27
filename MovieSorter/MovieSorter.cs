using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Text.RegularExpressions;


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


        private void Counter()
        {
            if (MatchList.Count > 1 || MatchList.Count == 0) Count.Text = MatchList.Count + @" Items in MatchList";
            else Count.Text = MatchList.Count + @" Item in MatchList";
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
            Query();
        }

        private List<string> MatchFiles = new List<string>();
        private List<string> IgnoreFiles = new List<string>();

        private List<string> MatchList
        {
            get { return MatchFiles; }
            set { MatchFiles = value; }
        }

        private List<string> IgnoreList
        {
            get { return IgnoreFiles; }
            set { IgnoreFiles = value; }
        }


        private void Query()
        {
            var filters =
                new Regex("(CAMRip|CAM|TS|TELESYNC|PDVD|PTVD|PPVRip|SCR|SCREENER|DVDSCR|DVDSCREENER|BDSCR|R4|R5|R5LINE|R5.LINE|DVD|DVD5|DVD9|DVDRip|DVDR|TVRip|DSR|PDTV|SDTV|HDTV|HDTVRip|DVB|DVBRip|DTHRip|VODRip|VODR|BDRip|BRRip|BR.Rip|BluRay|Blu.Ray|BD|BDR|BD25|BD50|3D.BluRay|3DBluRay|3DBD|Remux|BDRemux|BR.Scr|BR.Screener|HDDVD|HDRip|WorkPrint|VHS|VCD|TELECINE|WEBRip|WEB.Rip|WEBDL|WEB.DL|WEBCap|WEB.Cap|ithd|iTunesHD|Laserdisc|AmazonHD|NetflixHD|NetflixUHD|VHSRip|LaserRip|URip|UnknownRip|MicroHD|WP|TC|PPV|DDC|R5.AC3.5.1.HQ|DVD-Full|DVDFull|Full-Rip|FullRip|DSRip|SATRip|BD5|BD9|Extended|Uncensored|Remastered|Unrated|Uncut|IMAX|(Ultimate.)?(Director.?s|Theatrical|Ultimate|Final|Rogue|Collectors|Special|Despecialized).(Cut|Edition|Version)|((H|HALF|F|FULL)[^\\p{Alnum}]{0,2})?(SBS|TAB|OU)|DivX|Xvid|AVC|(x|h)[.]?(264|265)|HEVC|3ivx|PGS|MP[E]?G[45]?|MP[34]|(FLAC|AAC|AC3|DD|MA).?[2457][.]?[01]|[26]ch|(Multi.)?DTS(.HD)?(.MA)?|FLAC|AAC|AC3|TrueHD|Atmos|[M0]?(420|480|720|1080|1440|2160)[pi]|(?<=[-.])(420|480|720|1080|2D|3D)|10.?bit|(24|30|60)FPS|Hi10[P]?|[a-z]{2,3}.(2[.]0|5[.]1)|(19|20)[0-9]+(.)S[0-9]+(?!(.)?E[0-9]+)|(?<=\\d+)v[0-4]|CD\\d+|3D|2D)");
            var baseURL = "http://www.omdbapi.com/";
            listView1.Items.Clear();
            var match = new List<string>();
            var ignore = new List<string>();

            MatchList = match;
            IgnoreList = ignore;


            if (Directory.Exists(Source_dir.Text))
            {
                var allfiles = GetFiles(Source_dir.Text, "*.*");
                var i = 1;
                foreach (var name in allfiles)
                {
                    var Dir = Path.GetDirectoryName(name);
                    if (!(match.Contains(Dir)) && !(ignore.Contains(Dir)) && !(match.Contains(name)))
                    {
                        var extension = Path.GetExtension(name);
                        if (extension == null) continue;
                        var ext = extension.ToLower();
                        if (ext.Equals(".mp4") || ext.Equals(".avi") || ext.Equals(".mkv"))
                        {
                            var file = Path.GetFileNameWithoutExtension(name);
                            var SP = new Regex("[sS][0-9]{2}[eE][0-9]{2}");
                            if (SP.IsMatch(file))
                            {
                                var S_pattern = "[sS][0-9]{2}";
                                var S = Regex.Match(file, S_pattern);
                                var Series = S.Value;
                                Series = Series.Replace("S", " ");
                                SP = new Regex("[sS][0-9]{2}[eE][0-9]{2}.*");
                                var EP = SP.Replace(file, string.Empty);
                                var uri = baseURL + "?t=" + EP + "&Season=" + Series + "&r=json";
                                WebRequest request = WebRequest.Create(uri);
                                request.Credentials = CredentialCache.DefaultCredentials;
                                WebResponse response = request.GetResponse();
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
                                    match.Add(Dir);
                                    progressBar1.Visible = true;
                                    progressBar1.Minimum = 0;
                                    progressBar1.Maximum = match.Count;
                                    progressBar1.Value = 0;
                                    progressBar1.Step = 1;
                                }
                                else ignore.Add(Dir);
                            }

                            else
                            {
                                {
                                    var M_pattern = "[0-9]{4}";
                                    var M = Regex.Match(file, M_pattern);
                                    var year = int.Parse(M.Value);
                                    if (year >= 1900 && year < 2100)
                                    {
                                        if (year == 2016) match.Add(file);
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
                                        var uri = baseURL + "?s=" + tmpName + "&type=movie&r=json";
                                        WebRequest request = WebRequest.Create(uri);
                                        request.Credentials = CredentialCache.DefaultCredentials;
                                        WebResponse response = request.GetResponse();
                                        // Get the stream containing content returned by the server.
                                        var dataStream = response.GetResponseStream();
                                        // Open the stream using a StreamReader for easy access.
                                        var reader = new StreamReader(dataStream);
                                        // Read the content.
                                        var responseFromServer = reader.ReadToEnd();
                                        // Display the content.
                                        dynamic result = JsonConvert.DeserializeObject<dynamic>(responseFromServer);
                                        foreach (var item in result)
                                        {
                                            if(item.Title.Equals(tmpName) && item.Type.Equals("movie"))
                                                if (item.Year.Equals("2016")) match.Add(name);
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
                Counter();
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

    }
}
