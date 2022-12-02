using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Controls;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.DataFormats;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Crystal_Editor._3_Editor_Board
{
    public partial class EditorBoard : Form
    {

        byte[] HexFile; //This will be the entire file that we are editing.
        List<Control> EntryList = new List<Control>(); //A list of all textboxes we make, that the user uses to edit the hex values.
        List<Control> TextBoxlist = new List<Control>(); //A list of all textboxes we make, that the user uses to edit the hex values.
        int HexOffset; //The table of hex data starts THIS many bytes into the file. 
        int HexWidth;  //The table of hex data is this many bytes wide per row.
        int HexRowByte;//The exact byte were looking at in a row of the table, is this many bytes into that row.

        public EditorBoard()
        {
            InitializeComponent();
            richTextBoxWidth.Text = "3"; //Used temporarily in loading a dummy file.
            LoadDummyFile();


        }

        private void enemyTree_AfterSelect(object sender, TreeViewEventArgs e)
        {

            richTextBox1.Text = HexFile[HexOffset + (CollectionTree.SelectedNode.Index * HexWidth) + 0].ToString("D");//#1 is offset, #2 is Row Width, #3 is byte in row.
            richTextBox2.Text = HexFile[HexOffset + (CollectionTree.SelectedNode.Index * HexWidth) + 1].ToString("D");//Value 1 is distance / offset from the start of the file to the start of the array ("Start" in 010) (The first byte is 0) 
            richTextBox3.Text = HexFile[HexOffset + (CollectionTree.SelectedNode.Index * HexWidth) + 2].ToString("D");
            labelSelectedName.Text = CollectionTree.SelectedNode.Text;
            GiveEntrysHex2Dec();
        }

        private void button2_Click(object sender, EventArgs e) //Button: Save Dummy File
        {
            int ListInt = 0;

            foreach (Control TextBox in TextBoxlist)
            {
                Byte.TryParse(TextBoxlist[ListInt].Text, out byte value8);
                {
                    ByteManager.ByteWriter(value8, HexFile, HexOffset + (CollectionTree.SelectedNode.Index * HexWidth) + ListInt);
                } //First 1 byte save

                ListInt++;

            }
        }

        private void buttonHex_Click(object sender, EventArgs e) //Button: Load Dummy File
        {
            LoadDummyFile();
        }

        private void LoadDummyFile() 
        {
            string hexpath = "E:\\D Clone\\Crystal Editor Project\\Crystal Editor\\bin\\HexFiles\\HexDummy1";  //This defines the path as a string, so i can refer to it by this string/name instead of the full path every time
            int hexlength = (int)(new FileInfo(hexpath).Length);   //The leagth of the array?
            HexFile = File.ReadAllBytes(hexpath);  //loads an array with whatever is in the path


            CollectionTree.Nodes.Add("0 Knight Fencer"); //Used temporarily in loading a dummy file. Later this would be loaded from a file on the PC.
            CollectionTree.Nodes.Add("1 Knight Guard");
            CollectionTree.Nodes.Add("2 Knight Warrior");
            CollectionTree.Nodes.Add("HIDDEN Knight Fencer H");
            CollectionTree.Nodes.Add("3 Knight Fencer A");

            TreeNodeCollection nodeCollect = CollectionTree.Nodes;
            CollectionTree.SelectedNode = nodeCollect[0];
            HexWidth = Convert.ToInt16(richTextBoxWidth.Text); //Used temporarily in loading a dummy file. Later this would be loaded from a file on the PC.
            //MakeTextBoxes();
            MakeNewEntry();
        }

        















        private void MakeNewEntry() //Right now this just makes the entrys, and adds a NameBox and a TextBox
        {
            int YPos = 0;
            //var i = HexWidth;
            for (int i = 0; i < HexWidth; i++)
            {
                Panel newEntry = new Panel();
                //panel1

                newEntry.Location = new Point(8, YPos + 8); //dictionary.Ex, dictionary.Ey
                newEntry.Size = new Size(250, 37);
                YPos = YPos + 42;
                newEntry.BackColor = Color.FromArgb(48, 48, 48);
                newEntry.ForeColor = Color.White;
                newEntry.Font = new Font(newEntry.Font.FontFamily, 13, FontStyle.Regular);
                newEntry.Click += delegate
                {
                    // Code goes here
                    
                };
                tabControl1.TabPages[0].Controls.Add(newEntry);
                //panel8.Controls.Add(newEntry);

                EntryList.Add(newEntry);
            }

            MakeEntryNameBoxes();
            MakeEntryTextBoxes();

        }


        private void MakeEntryNameBoxes()
        {
            int EntryCount = 0;

            foreach (Control EntryPanel in EntryList)
            {
                
                TextBox newTextBox = new TextBox();
                
                newTextBox.Location = new Point(7, 7); //dictionary.Ex, dictionary.Ey
                newTextBox.Size = new Size(100, 24);
                //newTextBox.Name = ListOfEditorNames[i];
                //newTextBox.Text = HexFile[0 + (enemyTree.SelectedNode.Index * HexWidth) + 0].ToString("D"); //#1 is offset, #2 is Row Width, #3 is byte in row.
                newTextBox.BackColor = Color.FromArgb(38, 38, 38);
                newTextBox.ForeColor = Color.White;
                newTextBox.BorderStyle= BorderStyle.None;
                newTextBox.Font = new Font(newTextBox.Font.FontFamily, 13, FontStyle.Regular);
                //newTextBox.FlatStyle = FlatStyle.Flat;
                //newTextBox.FlatAppearance.BorderColor = Color.FromArgb(150, 150, 150);
                //newTextBox.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 30, 30);
                //newTextBox.FlatAppearance.MouseOverBackColor = Color.FromArgb(45, 45, 45);
                newTextBox.Click += delegate
                {
                    // Your code               

                    
                };

                //panel1.Controls.Add(newTextBox);
                EntryList[EntryCount].Controls.Add(newTextBox);
                newTextBox.Text = "Entry " + EntryCount.ToString();
                EntryCount++;
                //TextBoxlist.Add(newTextBox);

                //richTextBox4.AppendText("\nHeyo");
                //TextBoxlist[EntryCount].Text = HexFile[0 + (enemyTree.SelectedNode.Index * HexWidth) + EntryCount].ToString("D"); //#1 is offset, #2 is Row Width, #3 is byte in row.
                //EntryCount++;

            }

        }


        private void MakeEntryTextBoxes() 
        {
            int EntryCount = 0;

            foreach (Control EntryPanel in EntryList)
            {
                
                TextBox newTextBox = new TextBox();
                
                newTextBox.Location = new Point(128, 3); //dictionary.Ex, dictionary.Ey
                newTextBox.Size = new Size(100, 24);
                //newTextBox.Name = ListOfEditorNames[i];
                //newTextBox.Text = HexFile[0 + (enemyTree.SelectedNode.Index * HexWidth) + 0].ToString("D"); //#1 is offset, #2 is Row Width, #3 is byte in row.
                newTextBox.BackColor = Color.FromArgb(38, 38, 38);
                newTextBox.ForeColor = Color.White;
                newTextBox.Font = new Font(newTextBox.Font.FontFamily, 13, FontStyle.Regular);
                //newTextBox.FlatStyle = FlatStyle.Flat;
                //newTextBox.FlatAppearance.BorderColor = Color.FromArgb(150, 150, 150);
                //newTextBox.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 30, 30);
                //newTextBox.FlatAppearance.MouseOverBackColor = Color.FromArgb(45, 45, 45);
                newTextBox.Click += delegate
                {
                    // Your code 
                    
                };

                //panel1.Controls.Add(newTextBox);
                EntryList[EntryCount].Controls.Add(newTextBox);
                EntryCount++;
                TextBoxlist.Add(newTextBox);



                //richTextBox4.AppendText("\nHeyo");
                //TextBoxlist[EntryCount].Text = HexFile[0 + (enemyTree.SelectedNode.Index * HexWidth) + EntryCount].ToString("D"); //#1 is offset, #2 is Row Width, #3 is byte in row.
                //EntryCount++;

            }

        }









        private void button3_Click(object sender, EventArgs e) //Button Add Tab
        {
            TabPage tp = new TabPage("New Tab");
            tabControl1.TabPages.Add(tp);
        }

        private void button10_Click(object sender, EventArgs e) //Button Rename Current Tab
        {
            tabControl1.SelectedTab.Text = richTextBox8.Text;
        }




        private void button1_Click(object sender, EventArgs e) //Button: Set Entrys
        {
            //TextBoxlist[0].Text = HexFile[HexOffset + (enemyTree.SelectedNode.Index * HexWidth) + 0].ToString("D"); //#1 is offset, #2 is Row Width, #3 is byte in row.
            //TextBoxlist[1].Text = HexFile[HexOffset + (enemyTree.SelectedNode.Index * HexWidth) + 1].ToString("D"); //#1 is offset, #2 is Row Width, #3 is byte in row.
            //TextBoxlist[2].Text = HexFile[HexOffset + (enemyTree.SelectedNode.Index * HexWidth) + 2].ToString("D"); //#1 is offset, #2 is Row Width, #3 is byte in row.
            GiveEntrysHex2Dec();
        }

        private void GiveEntrysHex2Dec() 
        {
            int ListInt = 0;

            foreach (Control TextBox in TextBoxlist)
            {
                //richTextBox4.AppendText("\nHeyo");
                TextBoxlist[ListInt].Text = HexFile[HexOffset + (CollectionTree.SelectedNode.Index * HexWidth) + ListInt].ToString("D"); //#1 is offset, #2 is Row Width, #3 is byte in row.
                ListInt++;

            }
        }

        

        



        private void button8_Click(object sender, EventArgs e) //Button Make Editor XML
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("    ");
            settings.CloseOutput = true;
            settings.OmitXmlDeclaration = true;
            using (XmlWriter writer = XmlWriter.Create("Editor.xml", settings))
            {
                writer.WriteStartElement("BoardInfo");
                writer.WriteStartElement("EditorForm");
                writer.WriteStartElement("Tabs");

                //TabPage page = tabControl1.SelectedTab;
                //var controls = page.Controls;
                //var Pages = tabControl1.TabPages;
                foreach (TabPage tab in tabControl1.TabPages)
                {
                    writer.WriteElementString("Tab", tab.Text);
                }
                
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteStartElement("Entry");
                writer.WriteElementString("Name", "Entry ");
                writer.WriteElementString("Folder", "0");
                writer.WriteElementString("Order", "2");
                writer.WriteElementString("ByteSize", "1");
                writer.WriteElementString("DataType", "Number");
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.Flush();
            }
        }

        private void button9_Click(object sender, EventArgs e) //Button Load Editor XML
        {
            XmlDocument EditXml = new XmlDocument();
            EditXml.Load(DictionaryOfStrings.CrystalPath + "Debug\\net7.0-windows\\Editor.xml");
            XmlNodeList Tab = EditXml.GetElementsByTagName("Tab");
            XmlNodeList Name = EditXml.GetElementsByTagName("Name");
            richTextBoxLoadXML.Text = "Name: " + Tab[0].InnerText;
            richTextBox9.Text = "Name: " + Name[0].InnerText;


            tabControl1.TabPages[0].Text = Tab[0].InnerText;
            textBox2.Text = Tab.Count.ToString();
            for (int i = Tab.Count - 1; i > 0;) 
            {
                TabPage tp = new TabPage(Tab[i].InnerText);
                tabControl1.TabPages.Add(tp);
                i--;
                //textBox2.Text = "HELPPP";
            }
        }

        
    }
}
