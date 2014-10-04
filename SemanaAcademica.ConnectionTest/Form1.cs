using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SemanaAcademica.ConnectionTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            try
            {
                var entitie = new SemanaAcademicaEntities();
                textBox1.Text = entitie.Pessoa.Count().ToString() + " pessoa(s) - OK!";
            }
            catch (Exception e)
            {
                textBox1.Text = e.StackTrace;
            }
        }
    }
}
