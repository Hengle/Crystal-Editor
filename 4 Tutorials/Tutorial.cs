using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crystal_Editor
{
    public partial class Tutorial : Form
    {
        public Tutorial()
        {
            InitializeComponent();

            tabControlTutorials.Appearance = TabAppearance.FlatButtons;
            tabControlTutorials.ItemSize = new Size(0, 1);
            tabControlTutorials.SizeMode = TabSizeMode.Fixed;
            foreach (TabPage tab in tabControlTutorials.TabPages)
            {
                tab.Text = "";
            }

            tabControlTrees.Appearance = TabAppearance.FlatButtons;
            tabControlTrees.ItemSize = new Size(0, 1);
            tabControlTrees.SizeMode = TabSizeMode.Fixed;
            foreach (TabPage tab in tabControlTrees.TabPages)
            {
                tab.Text = "";
            }

            tabControlDictionary.Appearance = TabAppearance.FlatButtons;
            tabControlDictionary.ItemSize = new Size(0, 1);
            tabControlDictionary.SizeMode = TabSizeMode.Fixed;
            foreach (TabPage tab in tabControlDictionary.TabPages)
            {
                tab.Text = "";
            }

        }

        private void buttonBasic_Click(object sender, EventArgs e)
        {
            tabControlTrees.SelectedTab = tabControlTrees.TabPages["tabPageBasic"];
        }

        private void buttonAdvanced_Click(object sender, EventArgs e)
        {
            tabControlTrees.SelectedTab = tabControlTrees.TabPages["tabPageAdvanced"];
        }

        private void buttonOther_Click(object sender, EventArgs e)
        {
            tabControlTrees.SelectedTab = tabControlTrees.TabPages["tabPageOther"];
        }

        private void buttonDictionary_Click(object sender, EventArgs e)
        {
            tabControlTrees.SelectedTab = tabControlTrees.TabPages["tabPageDictionary"];
        }

        private void treeViewBasic_AfterSelect(object sender, TreeViewEventArgs e)
        {
            tabControlTutorials.SelectedTab = tabControlTutorials.TabPages[treeViewBasic.SelectedNode.Name];
        }

        private void treeViewDictionary_AfterSelect(object sender, TreeViewEventArgs e)
        {
            tabControlDictionary.SelectedTab = tabControlDictionary.TabPages[treeViewDictionary.SelectedNode.Name];
        }



        private void MouseEnter(object sender, EventArgs e)
        {
            (sender as Label).ForeColor = Color.FromArgb(65, 85, 255);
            
        }

        private void MouseLeave(object sender, EventArgs e)
        {
            (sender as Label).ForeColor = Color.FromArgb(70, 165, 255); 


        }



        private void InfoEditor(object sender, EventArgs e)
        {
            tabControlDictionary.SelectedTab = tabControlDictionary.TabPages["tabEditor"];
        }

        private void InfoMod(object sender, EventArgs e)
        {
            tabControlDictionary.SelectedTab = tabControlDictionary.TabPages["tabMod"];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CheckKeyword("test", Color.Purple, 0);
            //this
        }

        private void CheckKeyword(string word, Color color, int startIndex)
        {
            if (this.richTextBox1.Text.Contains(word))
            {
                int index = -1;
                int selectStart = this.richTextBox1.SelectionStart;

                while ((index = this.richTextBox1.Text.IndexOf(word, (index + 1))) != -1)
                {
                    this.richTextBox1.Select((index + startIndex), word.Length);
                    this.richTextBox1.SelectionColor = color;
                    this.richTextBox1.Select(selectStart, 0);
                    this.richTextBox1.SelectionColor = Color.White;
                }

                
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            CheckKeyword("test", Color.Purple, 0);
            //this.Che
        }
    }
}
