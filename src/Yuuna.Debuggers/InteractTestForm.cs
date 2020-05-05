
namespace Yuuna.TextDebugger
{
    using Newtonsoft.Json;
    using RestSharp;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class InteractTestForm : Form
    {
        private readonly ListBox _history;
        private readonly TextBox _input;
        private readonly RestClient client;

        public InteractTestForm()
        {
            this.InitializeComponent();
            this.Text = "Yuuna's Debugger Form";
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MaximumSize = new Size(320, 640);
            this.Opacity = 0.8;
            this.DoubleBuffered = true;
            this.BackColor = Color.White;

            var rx = Screen.PrimaryScreen.WorkingArea;
            this.Location = new Point(rx.Width - this.Size.Width, rx.Height - this.Size.Height);
            this.StartPosition = FormStartPosition.Manual;

            this.Move += delegate
            {
                this.Visible = false;
                this.Location = new Point(rx.Width - this.Size.Width, rx.Height - this.Size.Height);
                this.Visible = true;
            };


            _history = new ListBox();
            _history.HorizontalScrollbar = true;
            _history.Dock = DockStyle.Fill;
           // _history.Enabled = false;
            
            _history.BorderStyle = BorderStyle.None;

            this.Controls.Add(_history);

            _input = new TextBox();
            _input.Dock = DockStyle.Bottom;
            _input.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    var req = this._input.Text;
                    this._input.Clear();

                    if (string.IsNullOrWhiteSpace(req))
                        return;

                    if (req.StartsWith('/'))
                    {
                        switch (req.AsSpan(1).ToString())
                        {
                            case "cls":
                            case "clear":
                                {
                                    _history.Items.Clear();
                                    break;
                                }

                            default:
                                break;
                        }
                        return;
                    }

                    this._history.Items.Add("Me:  " + req);

                    int visibleItems = _history.ClientSize.Height / _history.ItemHeight;
                    _history.TopIndex = Math.Max(_history.Items.Count - visibleItems + 1, 0);

                    if (TrySend(req, out var resp))
                        this._history.Items.Add("Bot: " + resp.message);
                    else
                        this._history.Items.Add("Error: " + resp.message); 
                    
                    visibleItems = _history.ClientSize.Height / _history.ItemHeight;
                    _history.TopIndex = Math.Max(_history.Items.Count - visibleItems + 1, 0);

                    _history.SelectedItem = null;
                }
            };
            this.Controls.Add(_input);


            this.client = new RestClient("http://localhost:5000/");
            client.Timeout = 3000;
        }

        private bool TrySend(string text, out (string mood, string message) m)
        {
            try
            {
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", "{\n    \"text\": \"" + text + "\"\n}", ParameterType.RequestBody);
                var response = client.Execute(request);
                var result = JsonConvert.DeserializeAnonymousType(response.Content, new
                {
                    Success = default(bool),
                    Message = default(string),
                    Mood = default(string),
                    Text = default(string),
                });
                if(result is null)
                {
                    throw new Exception("無法處理請求，Yuuna的核心系統可能尚未開啟");
                }

                m = (result.Mood, result.Message);
                return true;
            }
            catch(Exception e)
            {
                m = ("Sad", e.Message);
            }
            return false;
        }
    }
}
