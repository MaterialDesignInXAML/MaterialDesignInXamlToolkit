using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace MaterialDesignDemo.WinForms
{
    public partial class Form1 : Form
    {
        private readonly ElementHost _elementHost;

        public Form1()
        {
            InitializeComponent();

            _elementHost = new()
            {
                Dock = System.Windows.Forms.DockStyle.Fill,
                Location = new Point(0, 0),
            };
            _elementHost.HostContainer.Children.Add(new MdixContent());
            Controls.Add(_elementHost);
        }
    }
}
