
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Pouzdanost
{
    [Serializable]
    public partial class Form1 : Form
    {
        
        [Serializable]
        public struct spremi
        {
            public string firstName, secondName;
            public Point firstLocation, secondLocatin;
            public double firstValue, secondValue;
            public object firstTag, secondTag;
            public Komponent.VrstaSpoja vrstaSpoja;
            public Komponent.tip vrstaKomponente;
            public List<string> parent, child;
            public spremi (Komponent a, Komponent b, Komponent.VrstaSpoja vrsta)
            {
                firstName = a.Name;
                secondName = b.Name;
                firstLocation = a.Location;
                secondLocatin = b.Location;
                firstValue = a.R;
                secondValue = b.R;
                firstTag = a.Tag;
                secondTag = b.Tag;
                vrstaSpoja = vrsta;
                vrstaKomponente = a.Tip;
                parent = child = null;
            }
            public spremi (Komponent a)
            {
                firstName = a.Name;
                secondName = null;
                firstLocation = a.Location;
                secondLocatin = Point.Empty; 
                firstValue = a.R;
                secondValue = 0;
                firstTag = a.Tag;
                secondTag = null;
                vrstaSpoja =Komponent.VrstaSpoja.serijski;
                vrstaKomponente = a.Tip;
                parent = a.roditelji;
                child = a.djeca;
            }
            public Komponent[] staro (Komponent k)
            {
                Komponent[] st = new Komponent[2]
                {
                    new Komponent(firstLocation,Komponent.tip.midle),
                    new Komponent(secondLocatin,Komponent.tip.midle)
                };
                st[0].Name = firstName;
                st[1].Name = secondName;
                st[0].R = firstValue;
                st[1].R = secondValue;
                if (vrstaSpoja == Komponent.VrstaSpoja.serijski)
                    st[0].djeca.Add(st[1].Name);
                else
                    st[0].djeca = k.djeca;
                st[0].roditelji = k.roditelji;
                st[1].djeca = k.djeca;
                if (vrstaSpoja == Komponent.VrstaSpoja.serijski)
                    st[1].roditelji.Add(st[0].Name);
                else
                    st[1].roditelji = k.roditelji;
                st[0].Tag = firstTag;
                st[1].Tag = secondTag;
                return st;
            }
            
        }
        protected string _selectedFront, _selectedEnd;
        public string selectFront
        {
            get
            {
                return _selectedFront;
            }
            set
            {
                if (_selectedFront != null && _selectedFront !=value)
                    ((Komponent)Graf.Controls[_selectedFront])
                        .selected = Komponent.selectedState.none;
                if (_selectedEnd != null)
                {
                    Komponent roditelj = (Komponent)Graf.Controls[ _selectedEnd];
                    Komponent djete = (Komponent)Graf.Controls[value];
                    if (!djete.roditelji.Contains(roditelj.Name) && value !=selectEnd)
                    {
                        djete.roditelji.Add(roditelj.Name);
                        roditelj.djeca.Add(djete.Name);
                        Invalidate();
                        Console.Write("Spojen " + _selectedEnd + " ->" + value + "\n");
                    }
                    if (_selectedFront != value)
                        _selectedFront = value;
                    else
                        _selectedFront = null;
                }

                else
                {
                    if (_selectedFront != value)
                        _selectedFront = value;
                    else
                        _selectedFront = null;
                }
                if (value == selectEnd)
                {
                    _selectedEnd = null;
                }
                Invalidate();
                Console.Write("Start= " + _selectedEnd + " End= " + _selectedFront + "\n");
            }
        }
        public string selectEnd
        {
            get
            {
                return _selectedEnd;
            }
            set
            {
                if (_selectedEnd != null && _selectedEnd != value)
                    ((Komponent)Graf.Controls[_selectedEnd])
                        .selected = Komponent.selectedState.none;
                if (_selectedFront != null)
                {
                    Komponent roditelj = (Komponent)Graf.Controls[value];
                    Komponent djete = (Komponent)Graf.Controls[_selectedFront];
                    if (!djete.roditelji.Contains(roditelj.Name) && value != selectFront)
                    {
                        djete.roditelji.Add(roditelj.Name);
                        roditelj.djeca.Add(djete.Name);
                        Invalidate();
                        Console.Write("Spojen " + value + " ->" + _selectedFront + "\n");
                    }
                    if (value != _selectedEnd)
                        _selectedEnd = value;
                    else
                        _selectedEnd = null;
                    
                }

                else
                {
                    if (value != _selectedEnd)
                        _selectedEnd = value;
                    else
                        _selectedEnd = null;
                }

                if (value == selectFront)
                {
                    _selectedFront = null;
                }
                Invalidate();
                Console.Write("Start= " + _selectedEnd + " End= " + _selectedFront + "\n");
            }
        }
        public List<string> postupak;
        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            Graf.Controls
                .Add(new Komponent(new Point(10, Graf
                .Height / 2), Komponent
                .tip
                .start));

            ((Komponent)Graf.Controls["S"])
                .end += OnEnd;
                
            Graf.Controls
                .Add(new Komponent(new Point(Graf
                .Width-30, Graf
                .Height / 2), Komponent
                .tip
                .end));
            ((Komponent)Graf.Controls["E"])
                .front += OnFront;
            postupak = new List<string>();

        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics g;
            Panel temp = (Panel)this.Controls["Graf"];
            g = temp.CreateGraphics();
            Graf.SuspendLayout();
            g.Clear(Color.Wheat);

            Size s = this.Controls["Graf"].Size;
            g.DrawRectangle(new Pen(Color.Black, 1f), 0, 0, s.Width, s.Height);
            Graf.ResumeLayout();
            for (int i = 0; i < Graf.Controls.Count; i++)
            {
                try {
                    if (Graf.Controls[i].GetType() == new Komponent().GetType())
                    {
                        Komponent t = (Komponent)Graf.Controls[i];
                        for (int j = 0; j < t.djeca.Count; j++)
                        {
                            Komponent tdjete = (Komponent)Graf.Controls[t.djeca[j]];
                            Point[] lin = new Point[4]
                            {
                                new Point(t.Location.X + t.Width,t.Location.Y+t.Height/2),
                                new Point(t.Location.X + t.Width + 5,t.Location.Y+t.Height/2),
                                new Point(t.Location.X + t.Width + 5,tdjete.Location.Y+tdjete.Height/2),
                                new Point(tdjete.Location.X,tdjete.Location.Y+tdjete.Height/2)
                            };
                            GraphicsPath pat = new GraphicsPath();
                            pat.AddLines(lin);
                            g.DrawPath(Pens.Black, pat);
                        }
                    }
                }catch
                {
                }
            }
            Graf = temp;
        }

        private void OnClickDodaj(object sender, EventArgs e)
        {
            if (postupak.Count == 0)
            {
                Komponent nova;
                nova = new Komponent(new Point(Graf.Controls.Count * 20, Graf.Controls.Count * 20), Komponent.tip.midle);
                try
                {
                    int maxy = 0;
                    for (int i = 0; i < Graf.Controls.Count; i++)
                    {
                        if (Graf.Controls[i].Name[0] == 'K')
                        {
                            maxy = Math
                                .Max(Convert
                                .ToInt32(Graf
                                .Controls[i]
                                .Name
                                .Substring(1)), maxy);
                        }
                    }
                    maxy++;
                    nova.Name = "K" + maxy.ToString();
                }
                catch
                {
                    nova.Name = "K1";
                }
                nova.R = 0.99;
                nova.end += OnEnd;
                nova.front += OnFront;
                Graf.Controls.Add(nova);
                nova.BringToFront();
            }
            else
            {
                MessageBox.Show("Nemre se dodati komponet u ovom stanju.\nMora se vratit u početno stanje kako bi se mogao dadati novi komponent!!!");
            }
        }

        private void OnFront(object sender, EventArgs e)
        {
            selectFront = ((Control)sender).Name;
        }

        private void spremiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try {
                Spremi.ShowDialog();
                Stream s = File.Open(Spremi.FileName, FileMode.CreateNew);
                var wr = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                List<spremi> spremljeno = new List<spremi>();
                foreach (Control c in Graf.Controls)
                {
                    if (c.GetType() == new Komponent().GetType())
                    {
                        spremljeno.Add(new spremi((Komponent)c));
                    }
                }
                wr.Serialize(s, spremljeno);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void otvoriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try {
                Otvori.ShowDialog();
                Stream read = File.Open(Otvori.FileName, FileMode.Open);
                List<spremi> sprem = new List<spremi>();
                var wr = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                sprem = (List<spremi>)wr.Deserialize(read);
                Graf.Controls.Clear();
                foreach (spremi s in sprem)
                {
                    Graf.Controls.Add(new Komponent(s));
                }
                UnFocus = new Label();
                this.UnFocus.Location = new System.Drawing.Point(0, 0);
                this.UnFocus.Name = "UnFocus";
                this.UnFocus.Size = new System.Drawing.Size(0, 0);
                this.UnFocus.TabIndex = 0;
                Graf.Controls.Add(UnFocus);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public bool SerijskiSpoj(Komponent a, Komponent b)
        {
            if (a.djeca.Count == 1 && b.roditelji.Count == 1)
                return true;
            else
                return false;
        }

        public bool comp(List<string> a, List<string> b)
        {
            if (a.Count != b.Count)
                return false;
            for (int i=0 ;i<a.Count;i++)
            {
                string temp = a[i];
                bool nasel = false;
                for (int j = 0; j < b.Count; j++)
                {
                    if (a[i] == b[j])
                    {
                        nasel = true;
                        break;
                    }
                    
                }
                if (!nasel)
                    return false;
            }
            return true;
        }
        public bool ParalelniSpoj(Komponent a, Komponent b)
        {
            List<string> roditeljPrvog, roditeljDrugog, djecaPrvog, djecaDrugog;
            roditeljPrvog = a.roditelji;
            roditeljPrvog.Remove(b.Name);
            roditeljDrugog = b.roditelji;
            roditeljDrugog.Remove(a.Name);
            djecaPrvog = a.djeca;
            djecaPrvog.Remove(b.Name);
            djecaDrugog = b.djeca;
            djecaDrugog.Remove(a.Name);
            if (comp(roditeljPrvog,roditeljDrugog)&&comp(djecaPrvog,djecaDrugog))
                return true;
            return false;
        }

        private void Next_Click(object sender, EventArgs e)
        {
            bool nasel = false;
            for (int i = 0; i < Graf.Controls.Count; i++)
            {
                if (nasel)
                    break;
                if (Graf.Controls[i].Name[0] == 'K')
                {
                    if (nasel)
                        break;
                    Komponent temp = (Komponent)Graf.Controls[i];
                    for (int j= 0; j < temp.djeca.Count;j++)
                    {
                        if (nasel)
                            break;
                        Komponent djete = (Komponent)Graf.Controls[temp.djeca[j]];

                        for (int k = 0; k < djete.roditelji.Count; k++)
                        {
                            if (nasel)
                                break;
                            Komponent rod = (Komponent)Graf.Controls[djete.roditelji[k]];
                            if (rod.Name != temp.Name && ParalelniSpoj(temp, rod)){
                                Komponent novi = new Komponent(temp, rod, Komponent.VrstaSpoja.paralelni);
                                Graf.Controls.Add(novi);
                                novi.BringToFront();
                                novi.end += OnEnd;
                                novi.front += OnFront;
                                postupak.Add(novi.Name);
                                nasel = true;
                                _selectedEnd = null;
                                _selectedFront = null;
                                break;
                            }
                        }
                        if (djete.Name[0] == 'K')
                        {
                            if (nasel)
                                break;
                            
                            if (SerijskiSpoj(temp, djete))
                            {
                                if (nasel)
                                    break;
                                Komponent novi = new Komponent(temp,djete,Komponent.VrstaSpoja.serijski);
                                Graf.Controls.Add(novi);
                                novi.BringToFront();
                                novi.end += OnEnd;
                                novi.front += OnFront;
                                postupak.Add(novi.Name);
                                nasel = true;
                                _selectedEnd = null;
                                _selectedFront = null;
                                break;
                            }
                        }

                    }
                }
            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            if (postupak.Count == 0)
                MessageBox.Show("Ovo je zadnje stanje");
            else
            {
                Komponent ZadnjiKomponent = (Komponent)Graf.Controls[postupak.Last<string>()];
                Komponent[] rod = ((spremi)ZadnjiKomponent.Tag).staro(ZadnjiKomponent);
                Graf.Controls.AddRange(rod);
                if (((spremi)ZadnjiKomponent.Tag).vrstaSpoja == Komponent.VrstaSpoja.serijski)
                {
                    for (int i = 0; i < rod[0].roditelji.Count; i++)
                    {
                        Komponent roditelj = (Komponent)Graf.Controls[rod[0].roditelji[i]];
                        roditelj.djeca.Remove(postupak.Last<string>());
                        roditelj.djeca.Add(rod[0].Name);
                    }
                    for (int i = 0; i < rod[1].djeca.Count; i++)
                    {
                        Komponent djete = (Komponent)Graf.Controls[rod[1].djeca[i]];
                        djete.roditelji.Remove(postupak.Last<string>());
                        djete.roditelji.Add(rod[1].Name);
                    }
                }
                else
                {
                    for (int i = 0; i < rod[0].roditelji.Count; i++)
                    {
                        Komponent roditelj = (Komponent)Graf.Controls[rod[0].roditelji[i]];
                        roditelj.djeca.Add(rod[0].Name);
                        roditelj.djeca.Add(rod[1].Name);
                        roditelj.djeca.Remove(postupak.Last<string>());
                    }
                    for (int i = 0; i < rod[0].djeca.Count; i++)
                    {
                        Komponent djete = (Komponent)Graf.Controls[rod[0].djeca[i]];
                        djete.roditelji.Add(rod[0].Name);
                        djete.roditelji.Add(rod[1].Name);
                        djete.roditelji.Remove(postupak.Last<string>());
                    }
                }
                rod[0].end += OnEnd;
                rod[0].front += OnFront;
                rod[1].end += OnEnd;
                rod[1].front += OnFront;
                
                rod[0].BringToFront();
                rod[1].BringToFront();
                ZadnjiKomponent.Dispose();
                postupak.RemoveAt(postupak.Count - 1);
                Invalidate();
                
            }
        }

        private void otvoriKonzoluToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("\n\n\n");
            foreach (Control C in Graf.Controls){
                if (C.GetType() == new Komponent().GetType())
                {
                    foreach (string s in ((Komponent)C).roditelji)
                        Console.Write(s + ", ");
                    Console.Write(" -> " + C.Name + " -> ");
                    foreach (string s in ((Komponent)C).djeca)
                        Console.Write(s + ", ");
                    Console.WriteLine();
                }
            }
            
        }

        private void noviToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graf.Controls.Clear();
            Graf.Controls
                .Add(new Komponent(new Point(10, Graf
                .Height / 2), Komponent
                .tip
                .start));

            ((Komponent)Graf.Controls["S"])
                .end += OnEnd;

            Graf.Controls
                .Add(new Komponent(new Point(Graf
                .Width - 30, Graf
                .Height / 2), Komponent
                .tip
                .end));
            ((Komponent)Graf.Controls["E"])
                .front += OnFront;
            postupak = new List<string>();
            UnFocus = new Label();
            this.UnFocus.Location = new System.Drawing.Point(0, 0);
            this.UnFocus.Name = "UnFocus";
            this.UnFocus.Size = new System.Drawing.Size(0, 0);
            this.UnFocus.TabIndex = 0;
            Graf.Controls.Add(UnFocus);
        }

        private void OnEnd(object sender, EventArgs e)
        {
            selectEnd = ((Control)sender).Name;
        }
    }
}
