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
            if (MatchList.Count > 1 || MatchList.Count == 0) Count.Text = MatchList.Count + @" Items in the MatchList";
            else Count.Text = MatchList.Count + @" Item in the MatchList";
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
                    try
                    {
                        var extension = Path.GetExtension(name);
                        if (extension == null) continue;
                        var ext = extension.ToLower();
                        if (!(MatchList.Contains(Path.GetDirectoryName(name))) && !(IgnoreFiles.Contains(Path.GetDirectoryName(name))))
                        {
                            if (ext.Equals(".mp4") || ext.Equals(".avi") || ext.Equals(".mkv"))
                            {
                                var file = Path.GetFileNameWithoutExtension(name);
                                var SP = new Regex("[sS][0-9]{2}[eE][0-9]{2}");
                                if(SP.IsMatch(file))
                                {
                                    var S_pattern = "[sS][0-9]{2}";
                                    var S = Regex.Match(file, S_pattern);
                                    var Series = S.Value;
                                    Series = Series.Replace("S", " ");
                                    SP = new Regex("[sS][0-9]{2}[eE][0-9]{2}.*");
                                    var Name = SP.Replace(file, string.Empty);
                                    //MessageBox.Show(Name);
                                    //MessageBox.Show(Series);
                                    var uri = baseURL + "?t=" + Name + "&Season=" + Series + "&r=json";
                                    // Create a request for the URL. 
                                    WebRequest request = WebRequest.Create(uri);
                                    // If required by the server, set the credentials.
                                    request.Credentials = CredentialCache.DefaultCredentials;
                                    // Get the response.
                                    WebResponse response = request.GetResponse();
                                    // Display the status.
                                    //Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                                    // Get the stream containing content returned by the server.
                                    var dataStream = response.GetResponseStream();
                                    // Open the stream using a StreamReader for easy access.
                                    var reader = new StreamReader(dataStream);
                                    // Read the content.
                                    var responseFromServer = reader.ReadToEnd();
                                    // Display the content.
                                    dynamic result = JsonConvert.DeserializeObject<dynamic>(responseFromServer);
                                    var Episodes = result.Episodes;
                                    // Clean up the streams and the response.
                                    reader.Close();
                                    response.Close();

                                    if (!MatchList.Contains(name) && !MatchList.Contains(name.ToLower()))
                                    {
                                        if (TestYear(Episodes))
                                            {
                                            match.Add(name);
                                            progressBar1.Visible = true;
                                            progressBar1.Minimum = 0;
                                            progressBar1.Maximum = match.Count;
                                            progressBar1.Value = 0;
                                            progressBar1.Step = 1;
                                        }

                                    }
                                }
                            }
                        }
                    }

                    catch //(Exception e)
                    {

                    }
                }


                foreach (var film in match)
                {
                    var co = i++;
                    Add("", co.ToString(), Path.GetDirectoryName(film));

                }
                listView1.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
                listView1.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.HeaderSize);
                listView1.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.ColumnContent);
                listView1.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.HeaderSize);
                Counter();
            }

            else MessageBox.Show(@"No such Folder");

        }

        private bool TestYear(dynamic episodes)
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
