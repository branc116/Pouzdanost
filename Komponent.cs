using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;

namespace Pouzdanost
{
    public partial class Komponent : Control
    {
        protected double _R;
        public double R
        {
            get
            {
                return _R;
            }
            set
            {
                _R = value;
                Invalidate();
            }
        }
        protected Size s;
        public enum selectedState
        {
            front,
            value,
            name,
            end,
            none
        }
        protected selectedState _selected;
        public selectedState selected
        {
            get
            {
                return _selected;
            }
            set
            {
                if (_selected == value)
                    _selected = selectedState.none;
                else
                    _selected = value;
                Invalidate();
            }
        }
        public enum tip
        {
            start,
            midle,
            end
        }
        public tip Tip;
        Point truLoc;
        public double zoom;

        protected Point _downLoc;
        public Point downLoc
        {
            get
            {
                return _downLoc;
            }
            set
            {
                _downLoc = value;
            }
        }
        public Point upLoc
        {
            set
            {
                Point v = new Point(Location.X + value.X - downLoc.X, Location.Y + value.Y - downLoc.Y);

                v.X = (v.X / 50) * 50;
                v.Y = (v.Y / 50) * 50;
                
                v.X=Math.Min(Math.Max(0, v.X), Parent.Size.Width);
                v.Y = Math.Min(Math.Max(0, v.Y), Parent.Size.Height);
                Location = v;
                Invalidate();
            }
        }
        public event EventHandler end;
        public event EventHandler front;
        public List<string> roditelji;
        public List<string> djeca;
        public enum VrstaSpoja
        {
            serijski,
            paralelni
        };
        public Komponent()
        {
            
        }
        public Komponent(Point a, tip b)
        {
            InitializeComponent();
            Location = a;
            zoom = 1;
            Tip = b;
            s = new Size((int)(zoom*100),(int)(zoom* 66));
            selected = selectedState.none;
            Size = s;
            Name = "K1";
            roditelji = new List<string>();
            djeca = new List<string>();
            if (b == tip.end)
                Name = "E";
            else if (b == tip.start)
                Name = "S";
            
            
        }
        public Komponent(Komponent a, Komponent b, VrstaSpoja spoj )
        {
            Name = "K" + a.Name.Substring(1) + "," + b.Name.Substring(1);
            djeca = b.djeca;
            roditelji = a.roditelji;
            Tag = new Form1.spremi(a,b,spoj);
            InitializeComponent();
            if (spoj == VrstaSpoja.serijski)
                Location = new Point((a.Location.X + b.Location.X) / 2, (a.Location.Y + b.Location.Y) / 2);
            else
                Location = a.Location;
            zoom = 1;
            Tip = tip.midle;
            s = new Size((int)(zoom * 100), (int)(zoom * 66));
            selected = selectedState.none;
            Size = s;
            if (spoj == VrstaSpoja.serijski)
                R = Math.Round(a.R * b.R, 4);
            else
                R = Math.Round(1 - ((1 - a.R) * (1 - b.R)),4);
            R = Math.Min(0.9999, R);
            if (spoj == VrstaSpoja.paralelni)
            {
                for (int i = 0; i < a.roditelji.Count; i++)
                {
                    Komponent temp = (Komponent)a.Parent.Controls[a.roditelji[i]];
                    temp.djeca.Remove(a.Name);
                    temp.djeca.Remove(b.Name);
                    temp.djeca.Add(Name);
                }
                for (int i = 0; i < a.djeca.Count; i++)
                {
                    Komponent temp = (Komponent)b.Parent.Controls[b.djeca[i]];
                    temp.roditelji.Remove(a.Name);
                    temp.roditelji.Remove(b.Name);
                    temp.roditelji.Add(Name);
                }
            }
            else
            {
                for (int i = 0; i < a.roditelji.Count; i++)
                {
                    Komponent temp = (Komponent)a.Parent.Controls[a.roditelji[i]];
                    temp.djeca.Remove(a.Name);
                    temp.djeca.Add(Name);
                }
                for (int i = 0; i < b.djeca.Count; i++)
                {
                    Komponent temp = (Komponent)b.Parent.Controls[b.djeca[i]];
                    temp.roditelji.Remove(b.Name);
                    temp.roditelji.Add(Name);
                }
            }

            a.Dispose();
            b.Dispose();
        }
        public Komponent(Form1.spremi a)
        {
            InitializeComponent();
            Location = a.firstLocation;
            zoom = 1;
            Tip = a.vrstaKomponente;
            s = new Size((int)(zoom * 100), (int)(zoom * 66));
            selected = selectedState.none;
            Size = s;
            Name = a.firstName;
            roditelji = a.parent;
            R = a.firstValue;
            Tag = a.firstTag;
            djeca = a.child;
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics g = CreateGraphics();
            if (Tip == tip.midle)
            {
                g.DrawRectangle(new Pen(Color.Black, 3), 0, 0, Size.Width, Size.Height);
                Rectangle front = new Rectangle(1, 1, (int)(Size.Width * 0.5f), (int)(Size.Height - 2));
                Rectangle end = new Rectangle((int)(Size.Width * 0.5f), 1, Size.Width - (int)(Size.Width * 0.5f) - 1, Size.Height - 1);
                if (selected == selectedState.front)
                    g.FillRectangle(Brushes.Gray, front);
                if (selected == selectedState.end)
                    g.FillRectangle(Brushes.Gray, end);
                Font my = new Font("Arial", 0.2f * Height, FontStyle.Regular, GraphicsUnit.Pixel);
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                g.DrawString(Name, my, Brushes.Black, new Rectangle(0,0,Width,Height), sf);
              
            }else if (Tip == tip.start)
            {
                Size = new Size((int)(20 * zoom), (int)(20 * zoom));
                Rectangle r = new Rectangle(0, 0, Size.Width-1, Size.Height-1);
                g.DrawEllipse(Pens.Black, r);
                if (selected == selectedState.end)
                    g.FillEllipse(Brushes.Gray, r);
            }
            else if (Tip == tip.end)
            {
                Size = new Size((int)(20 * zoom), (int)(20 * zoom));
                Rectangle r = new Rectangle(0, 0, Size.Width-1, Size.Height-1);
                g.DrawRectangle(Pens.Black, r);
                if (selected == selectedState.front)
                    g.FillRectangle(Brushes.Gray, r);
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button.ToString() == "Left")
                downLoc = e.Location;
            BringToFront();
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button.ToString() == "Left")
                upLoc = e.Location;
            else if (e.Button == MouseButtons.Right)
                if (Tip == tip.midle)

                    if (e.X < 0.4f * Size.Width)
                    {
                        selected = selectedState.front;
                        EventHandler a = front;
                        if (a != null)
                            a(this, EventArgs.Empty);
                    }
                    else if (e.X > 0.6f * Size.Width)
                    {
                        selected = selectedState.end;
                        EventHandler a = end;
                        if (a != null)
                            a(this, EventArgs.Empty);
                    }
                    else { }
                else if (Tip == tip.start)
                {
                    selected = selectedState.end;
                    EventHandler a = end;
                    if (a != null)
                        a(this, EventArgs.Empty);
                }
                else
                {
                    selected = selectedState.front;
                    EventHandler a = front;
                    if (a != null)
                        a(this, EventArgs.Empty);
                }

        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if ((e.KeyChar == Keys.Delete.ToString()[0] || e.KeyChar == Keys.Back.ToString()[0]) && selected != selectedState.none)
            {
                for (int i = 0; i < roditelji.Count; i++)
                {
                    ((Komponent)(Parent.Controls[roditelji[i]]))
                         .djeca
                         .Remove(Name);
                }
                for (int i = 0; i < djeca.Count; i++)
                {
                    ((Komponent)(Parent.Controls[djeca[i]]))
                         .roditelji
                         .Remove(Name);
                }
                Dispose();

            }
        }
        protected override void OnMouseHover(EventArgs e)
        {
            Label discreption = new Label();
            discreption.Name = "ShowR";
            discreption.Text = R.ToString() +  Environment.NewLine + "R= " + (R*100).ToString() + " %" ;
            if (Tag != null)
            {
                Form1.spremi s = (Form1.spremi)Tag;
                discreption.Text += Environment.NewLine;
                if (s.vrstaSpoja == VrstaSpoja.serijski)
                {
                    discreption.Text +=( Name + " = " + s.firstName + " * " + s.secondName);
                }
                else
                {
                    discreption.Text += (Name + " = 1 - [(1-" + s.firstName + ") * (1-" + s.secondName + ")]");
                }
            }

            discreption.SendToBack();
            discreption.Location = new Point(Location.X, Location.Y + Height);
            Parent.Controls.Add(discreption);
            discreption.BringToFront();
            discreption.BackColor = Color.Transparent;
            discreption.AutoSize = true;
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            try {
                Parent.Controls["ShowR"].Dispose();
            }catch
            { 
            }
        }
        protected override void OnLocationChanged(EventArgs e)
        {
            try
            {
                Parent.Controls["ShowR"].Location = new Point(Location.X, Location.Y + Height);
            }
            catch { }
            
        }
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (Tip == tip.midle)
            {
                if (e.Button == MouseButtons.Left)
                {
                    TextBox ChangeR = new TextBox();
                    ChangeR.Text = R.ToString();
                    ChangeR.Location = new Point(Location.X, Location.Y + Height);
                    Parent.Controls.Add(ChangeR);
                    ChangeR.BringToFront();
                    ChangeR.Focus();
                    ChangeR.LostFocus += ChangeR_LostFocus;
                    ChangeR.KeyPress += ChangeR_KeyPress;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    if (e.Location.X > Size.Width * 0.6)
                    {
                        for (; djeca.Count != 0;)
                        {
                            Komponent djete = (Komponent)Parent.Controls[djeca[0]];
                            djete.roditelji.Remove(Name);
                            djeca.RemoveAt(0);
                        }
                    }
                    else if (e.Location.X < Size.Width * 0.4)
                    {
                        for (; roditelji.Count != 0;)
                        {
                            Komponent roditelj = (Komponent)Parent.Controls[roditelji[0]];
                            roditelj.djeca.Remove(Name);
                            roditelji.RemoveAt(0);
                        }
                    }
                }
            }else if (Tip == tip.start)
            {
                for (; djeca.Count != 0;)
                {
                    Komponent djete = (Komponent)Parent.Controls[djeca[0]];
                    djete.roditelji.Remove(Name);
                    djeca.RemoveAt(0);
                }
            }else if (Tip == tip.end)
            {
                for (; roditelji.Count != 0;)
                {
                    Komponent roditelj = (Komponent)Parent.Controls[roditelji[0]];
                    roditelj.djeca.Remove(Name);
                    roditelji.RemoveAt(0);
                }
            }
        }

        private void ChangeR_KeyPress(object sender, KeyPressEventArgs e)
        {
            Console.WriteLine("Keypressed = '" + e.KeyChar.ToString() + "'");
            if (e.KeyChar == '\r')
            {
                Parent.Controls["UnFocus"].Focus();
            }
        }

        private void ChangeR_LostFocus(object sender, EventArgs e)
        {
            TextBox ChangeR = ((TextBox)sender);
            try
            {
                double vrj = Convert.ToDouble(ChangeR.Text);
                R = vrj;
                ChangeR.Dispose();
            }
            catch
            {
                SystemSounds.Beep.Play();
                ChangeR.Focus();
            }
        }
    }
}
