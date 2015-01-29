using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NTH.Framework.Windows.Interaction;

namespace NTH.Framework.Demo
{
    public partial class MainWindow : Form
    {
        private readonly KeyboardHook _hook = new KeyboardHook();
        public MainWindow()
        {
            InitializeComponent();

            var f4 = new Hotkey(Windows.Interaction.ModifierKeys.Win, Keys.F4);
            f4.KeyPressed += (s, e) => MessageBox.Show("Lol " + f4);
            var f5 = new Hotkey(Windows.Interaction.ModifierKeys.None, Keys.F5);
            f5.KeyPressed += (s, e) => MessageBox.Show("Lol f5");

            _hook.RegisterHotkey(f4);
            _hook.RegisterHotkey(f5);
            _hook.UnregisterHotkey(f4);
        }
    }
}
