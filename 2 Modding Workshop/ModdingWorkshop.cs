using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
//using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Crystal_Editor._3_Editor_Board;


namespace Crystal_Editor
{
    public partial class ModdingWorkshop : Form
    {
        DictionaryOfStrings dictionary = new DictionaryOfStrings();
        public int i = 0;
        public int i2 = 0;
        int countDirectories = 0;
        List<string> ListOfEditorNames = new List<string>();
        List<string> ListOfDocumentNames = new List<string>();
        List<Button> ListOfDocumentButtons = new List<Button>();
        string CurrentDocumentMode = ""; //Used for Edit/Save button, and swapping between the modes.
        string SelectedDocument = ""; //when the user clicks on a document, it becomed the selected document.
        string SelectedDocumentName = ""; //when the user clicks on a document, it becomes the selected document name.
        int DySpace = 35;



        string SelectedEditor = "";
        List<Button> ListOfEditorButtons = new List<Button>();
        List<List<Control>> ListOfLists = new List<List<Control>>();
        public List<Control> ListEdit0 = new List<Control>();
        public List<Control> ListEdit1 = new List<Control>();
        public List<Control> ListEdit2 = new List<Control>();
        public List<Control> ListEdit3 = new List<Control>();
        int ListE = 0;


        public ModdingWorkshop()
        {
            InitializeComponent();
            ListE = 0;
            SelectedEditor = "EditorHome";

            richTextBoxDocumentName.Hide();
            pictureBoxDiscord.Image = Image.FromFile(DictionaryOfStrings.CrystalPath + "\\Other\\Images\\DiscordLogo.png");

            //This counts how many editor folders there are, we later display this as buttons to open those editors.
            countDirectories = System.IO.Directory.GetDirectories(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + GameLibrary.libraryNodeName + "\\Editors", "*", SearchOption.TopDirectoryOnly).Count();
            panelCore.BackColor = Color.FromArgb(32,32,32);

            //string checkFileExist = DictionaryOfStrings.CrystalPath + "\\Workshops\\" + GameLibrary.libraryNodeName + "\\LibraryBanner.png";
            //if (File.Exists(checkFileExist))
            //{


            //}
            //if (!File.Exists(checkFileExist))
            //{

            //}

            CreateEditors();
            StartDocuments();


            
        }

        private void StartDocuments() 
        {
            i2 = 0;
            CreateDocumentation();
            ListOfDocumentButtons.Sort((x, y) => String.Compare(x.Name, y.Name)); //Sort the Document Buttons list by Order
        }
                

        public static string savePath = "";



        // //////////////// START OF GENERATORS ////////////////////////////////////////////////////////

        private void CreateEditors() 
        {
            string[] subdirs = Directory.GetDirectories(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + GameLibrary.libraryNodeName + "\\Editors");
            dictionary.Ex = 8;
            dictionary.Ey = 8;
            for (int i = 0; i < subdirs.Length; i++)
            {
                
                ListOfEditorNames.Add(Path.GetFileName(subdirs[i]).ToString());
                CreateEditors2();
            }
            //CreateEditors2();

        }
        public void CreateEditors2() 
        {
            

            Button newButton = new Button();
            
            newButton.Location = new Point(dictionary.Ex, dictionary.Ey);
            newButton.Size = new Size(190, 30);            
            dictionary.Ey = dictionary.Ey + 35;
            newButton.Name = ListOfEditorNames[i];
            newButton.Text = ListOfEditorNames[i];
            newButton.BackColor = Color.FromArgb(38, 38, 38);
            newButton.ForeColor = Color.White;
            newButton.FlatStyle = FlatStyle.Flat;
            newButton.FlatAppearance.BorderColor = Color.FromArgb(150, 150, 150);
            newButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 30, 30);
            newButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(45, 45, 45);
            newButton.Click += delegate
            {
                // Your code                

                //var _form = new Form1();************************************************************************************************************************************************************************************************************************************************************************************************************************************************************
                DictionaryOfStrings.SelectedEditorDirectory = DictionaryOfStrings.CrystalPath + "\\Workshops\\" + GameLibrary.libraryNodeName + "\\Editors\\" + newButton.Name; //in real setup, final addition is 'Selected Editor Name' aka the folder.
                //var loadTest = _form.LoadEditorFully();
            };

            panelEditors.Controls.Add(newButton);
            i = i + 1;
        }


        //
        //
        ///////////////////////// DOCUMENTATION  V   ////////////// EDITORS ^   ///////////////////////////////
        //
        //


        private void CreateDocumentation()
        {
            string[] Docs = Directory.GetDirectories(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + GameLibrary.libraryNodeName + "\\Documentation");
            dictionary.Ex = 6;
            dictionary.Ey = 6;
            ListOfDocumentNames.Clear();
            ListOfDocumentButtons.Clear();
            panelDocumentation.Controls.Clear();
            for (int i2 = 0; i2 < Docs.Length; i2++)
            {
                           
                ListOfDocumentNames.Add(Path.GetFileName(Docs[i2]).ToString());
                CreateDocumentation2();
            }
            

        }
        public void CreateDocumentation2()
        {


            Button newButton = new Button();


            newButton.Text = ListOfDocumentNames[i2];
            newButton.Name = File.ReadAllText(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + GameLibrary.libraryNodeName + "\\Documentation\\" + newButton.Text + "\\Order.txt");
            dictionary.Ey = Convert.ToInt16(newButton.Name);
            newButton.Location = new Point(dictionary.Ex, ((Convert.ToInt16(newButton.Name) * DySpace) + 8));
            newButton.Size = new Size(205, 30);
            dictionary.Ey = dictionary.Ey + 35;
            newButton.BackColor = Color.FromArgb(38, 38, 38);
            newButton.ForeColor = Color.White;
            newButton.FlatStyle = FlatStyle.Flat;
            newButton.FlatAppearance.BorderColor = Color.FromArgb(150, 150, 150);
            newButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 30, 30);
            newButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(45, 45, 45);
            newButton.Click += delegate
            {
                // Your code                

                richTextBoxDocumentation.Text = File.ReadAllText(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + GameLibrary.libraryNodeName + "\\Documentation\\" + newButton.Text + "\\Text.txt");
                DocumentReset();
                SelectedDocument = newButton.Text;
                SelectedDocumentName = newButton.Name;                
            };
            
            ListOfDocumentButtons.Add(newButton);
            panelDocumentation.Controls.Add(newButton);
            i2 = i2 + 1;
        }



        // /////////////////  END OF GENERATORS ////////////////////////////////////



        
        

        private void button10_Click(object sender, EventArgs e) //Button: Create New Editor
        {
            
            Directory.CreateDirectory(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + GameLibrary.libraryNodeName + "\\Editors\\" + richTextBox1.Text);
            DictionaryOfStrings.CreateEditorFolder = DictionaryOfStrings.CrystalPath + "\\Workshops\\" + GameLibrary.libraryNodeName + "\\Editors\\" + richTextBox1.Text + "\\";
            
        }

        private void button2_Click(object sender, EventArgs e) //Button: New Document
        {
            button2.Hide(); 
            richTextBoxDocumentName.Show();
            DocumentEdit();
            richTextBoxDocumentName.Text = "New Document Name";
            CurrentDocumentMode = "SaveNew";
            richTextBoxDocumentName.ReadOnly = false;
            richTextBoxDocumentName.BackColor = Color.FromArgb(55, 55, 55);
        }

        private void buttonDocumentMode_Click(object sender, EventArgs e)
        {
            if (CurrentDocumentMode == "Edit") 
            {
                DocumentEdit();
                button2.Hide();
                richTextBoxDocumentName.Show();
            }
            else if (CurrentDocumentMode == "Save") 
            {
                DocumentSave();
            }
            else if (CurrentDocumentMode == "SaveNew")
            {
                DocumentSaveNew();
            }
        }

        private void DocumentEdit()
        {
            
            richTextBoxDocumentation.ReadOnly = false;
            richTextBoxDocumentation.BackColor = Color.FromArgb(45,45,45);
            CurrentDocumentMode = "Save";
            buttonDocumentMode.Text = "Save";

        }

        private void DocumentSave()
        {
            string oldDocumentName = DictionaryOfStrings.CrystalPath + "\\Workshops\\" + GameLibrary.libraryNodeName + "\\Documentation\\" + SelectedDocument;
            string newDocumentName = DictionaryOfStrings.CrystalPath + "\\Workshops\\" + GameLibrary.libraryNodeName + "\\Documentation\\" + richTextBoxDocumentName.Text;
            try
            {
                Directory.Move(oldDocumentName, newDocumentName);
            }
            catch (IOException exp)
            {
                Console.WriteLine(exp.Message);
            }
            SelectedDocument = richTextBoxDocumentName.Text;


            File.WriteAllText(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + GameLibrary.libraryNodeName + "\\Documentation\\" + richTextBoxDocumentName.Text + "\\Text.txt", richTextBoxDocumentation.Text); //Overwrites, Or creates file if it does not exist. Needs location permissions for admin folders.
            DocumentReset();
        }
        private void DocumentSaveNew() 
        {            
            //Make the OrderText
            //Clear the document list
            //remake the document list
            string dir = DictionaryOfStrings.CrystalPath + "\\Workshops\\" + GameLibrary.libraryNodeName + "\\Documentation\\" + richTextBoxDocumentName.Text;
            // If directory does not exist, create it
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);                
                File.WriteAllText(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + GameLibrary.libraryNodeName + "\\Documentation\\" + richTextBoxDocumentName.Text + "\\Text.txt", richTextBoxDocumentation.Text);
                File.WriteAllText(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + GameLibrary.libraryNodeName + "\\Documentation\\" + richTextBoxDocumentName.Text + "\\Order.txt", Convert.ToString(ListOfDocumentButtons.Count));
                richTextBoxDocumentName.Clear();

            }
            
            DocumentReset();
            StartDocuments();
        }

        private void DocumentReset() 
        {
            richTextBoxDocumentation.ReadOnly = true;
            richTextBoxDocumentation.BackColor = Color.FromArgb(35, 35, 35);
            CurrentDocumentMode = "Edit";
            buttonDocumentMode.Text = "Edit Document";
            button2.Show();
            richTextBoxDocumentName.Hide();
            panelDocumentation.Controls.Clear();
            StartDocuments();
        }



        int BOne = 0;
        int BTwo = 0;
        private void button6_Click(object sender, EventArgs e) //Button Up (Move Documenet)
        {
            if (SelectedDocumentName != "0") 
            {
                BTwo = Convert.ToInt16(ListOfDocumentButtons[Convert.ToInt16(SelectedDocumentName)].Name);
                BOne = BTwo - 1;
                Swap();
                SelectedDocumentName = (BTwo - 1).ToString();
            }
            
        }
        private void button7_Click(object sender, EventArgs e) //Button Down (Move Documenet)
        {
            if (SelectedDocumentName != Convert.ToString(ListOfDocumentButtons.Count()-1)) //  "0"
            {
                BOne = Convert.ToInt16(ListOfDocumentButtons[Convert.ToInt16(SelectedDocumentName)].Name);
                BTwo = BOne + 1;
                Swap();
                SelectedDocumentName = (BOne + 1).ToString();
                //ListOfDocumentButtons[0].Text = "Some text";

            }

        }

        private void Swap() 
        {
            
            ListOfDocumentButtons[BOne].Location = new Point(dictionary.Ex, (BOne * DySpace) + 8 + DySpace);
            ListOfDocumentButtons[BTwo].Location = new Point(dictionary.Ex, (BTwo * DySpace) + 8 - DySpace);
            System.IO.File.WriteAllText(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + GameLibrary.libraryNodeName + "\\Documentation\\" + ListOfDocumentButtons[BOne].Text + "\\Order.txt", (BOne + 1).ToString()); //Overwrites, Or creates file if it does not exist. Needs location permissions for admin folders.
            System.IO.File.WriteAllText(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + GameLibrary.libraryNodeName + "\\Documentation\\" + ListOfDocumentButtons[BTwo].Text + "\\Order.txt", (BTwo - 1).ToString()); //Overwrites, Or creates file if it does not exist. Needs location permissions for admin folders.
            ListOfDocumentButtons[BOne].Name = (BOne + 1).ToString();
            ListOfDocumentButtons[BTwo].Name = (BTwo - 1).ToString();            
            ListOfDocumentButtons.Sort((x, y) => String.Compare(x.Name, y.Name)); //Sort the Document Buttons list by Order
        }

        


        private void buttonProject_Click(object sender, EventArgs e) //Button: Set Project Folder
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.FileName = "Save Here"; // Feed a name to the dialog that appears.

            if (sf.ShowDialog() == DialogResult.OK)
            {
                // Now here's our save folder
                savePath = Path.GetDirectoryName(sf.FileName);
                // Do whatever

                using (StreamWriter writer = new StreamWriter(DictionaryOfStrings.CrystalPath + "\\Settings\\Workshops\\" + GameLibrary.libraryNodeName + "\\Project Location.txt", false)) //// true to append data to the file
                {
                    writer.Write(savePath);
                }
            }
                        

        }

        
        private void pictureBoxDiscord_Click(object sender, EventArgs e) //Image Button: Discord
        {
            System.Diagnostics.Process.Start("https://discord.gg/mhrZqjRyKx");
        }

        private void pictureBoxDiscord_MouseEnter(object sender, EventArgs e)
        {
            pictureBoxDiscord.BackColor = Color.FromArgb(80, 80, 80);
        }
        private void pictureBoxDiscord_MouseLeave(object sender, EventArgs e)
        {
            pictureBoxDiscord.BackColor = Color.Transparent;
        }

        private void buttonNotepadPlusPlus_Click(object sender, EventArgs e) //Button: (Tool) Notepad++
        {
            Process yourProcess = new Process();
            yourProcess.StartInfo.FileName = @DictionaryOfStrings.CrystalPath + "\\Tools\\General\\Text Editor - N++\\notepad++.exe";
            yourProcess.Start();
        }

        private void button8_Click(object sender, EventArgs e) //Button: (Tool) DS Buff
        {
            Process yourProcess = new Process();
            yourProcess.StartInfo.FileName = @DictionaryOfStrings.CrystalPath + "\\Tools\\Console\\Nintendo DS\\File Converter - DSBuff\\dsbuff.exe";
            yourProcess.Start();
        }

        private void button4_Click(object sender, EventArgs e) //Button: (Tool) HxD
        {
            Process yourProcess = new Process();
            yourProcess.StartInfo.FileName = @DictionaryOfStrings.CrystalPath + "\\Tools\\General\\Hex Editor - HxD\\HxD64.exe";
            yourProcess.Start();
        }

        private void button5_Click(object sender, EventArgs e) //Button: (Tool) Hex Editor 010
        {
            Process yourProcess = new Process();
            yourProcess.StartInfo.FileName = @DictionaryOfStrings.CrystalPath + "\\Tools\\General\\Hex Editor - 010\\010EditorPortable.exe";
            yourProcess.Start();
        }

        private void button14_Click(object sender, EventArgs e) //Button Alpha
        {
            EditorBoard f2 = new EditorBoard();
            f2.Show();
        }




























        byte[] HexFile; //This will be the entire file that we are editing.
        int HexWidth;
        int HexOffset = 0;
        List<Control> EntryList = new List<Control>(); //A list of all textboxes we make, that the user uses to edit the hex values.
        List<Control> TextBoxlist = new List<Control>(); //A list of all textboxes we make, that the user uses to edit the hex values.


        //
        //
        //
        ///////////////////////General Things////////////////////////
        //
        //
        //



        private void button3_Click(object sender, EventArgs e) //Button Home
        {
            PanelHide();
            SelectedEditor = "EditorHome";
            PanelShow();            
        }

        private void button15_Click(object sender, EventArgs e) //Button Hide Selected Editor
        {
            PanelHide();
        }

        private void PanelHide() 
        {
            Controls.Find(SelectedEditor + "Panel", true)[0].Hide();
        }

        private void PanelShow()
        {
            Controls.Find(SelectedEditor + "Panel", true)[0].Show();
        }

        private void button17_Click(object sender, EventArgs e) //Button: Load Editor?
        {

        }

        private void button3_Click_1(object sender, EventArgs e) //Button: Load File
        {
            string hexpath = DictionaryOfStrings.CrystalPath +  "\\HexFiles\\HexDummy1";  //This defines the path as a string, so i can refer to it by this string/name instead of the full path every time
            int hexlength = (int)(new FileInfo(hexpath).Length);   //The leagth of the array?
            HexFile = File.ReadAllBytes(hexpath);  //loads an array with whatever is in the path

            //if (richTextBox1.Text == "0") 
            //{
            //    Controls.ListEdit0[0].Nodes
            //}

            var CollectionTree = Controls.Find(SelectedEditor + "Panel" + "Tree", true)[0] as TreeView;
            //Controls[SelectedEditor + "Panel" + "Tree"].Controls.Nodes.Add("1 Knight Guard");
            //Controls[SelectedEditor + "Panel" + "Tree"].Nodes.Add("1 Knight Guard");

            //Controls.Find(SelectedEditor + "Panel" + "Tree", true)[0].Controls.Nodes.Add("1 Knight Guard");
            //Controls.Find(SelectedEditor + "Panel" + "Tree", true)[0].Nodes.Add("1 Knight Guard");
            //CollectionTree.Controls.Nodes.Add("0 Knight Fencer"); //Used temporarily in loading a dummy file. Later this would be loaded from a file on the PC.
            //Controls["EditorRedPanelTree"].Nodes.Add("1 Knight Guard");
            //Controls.CollectionTree.Nodes.Add("1 Knight Guard");
            //Controls.CollectionTree.Nodes.Add("1 Knight Guard");
            CollectionTree.Nodes.Add(richTextBox1.Text);



            //CollectionTree.Nodes.Add("2 Knight Warrior");
            //CollectionTree.Nodes.Add("HIDDEN Knight Fencer H");
            //CollectionTree.Nodes.Add("3 Knight Fencer A");

            //Controls["EditortreeePanelTree"].Nodes.Add("1 Knight Guard");

            TreeNodeCollection nodeCollect = CollectionTree.Nodes;
            CollectionTree.SelectedNode = nodeCollect[0];

            HexWidth = Convert.ToInt16(richTextBoxHexWidth.Text); //Used temporarily in loading a dummy file. Later this would be loaded from a file on the PC.
            //MakeTextBoxes();
            //MakeNewEntry();
        }

        //
        //
        //
        ///////////////////////New Things////////////////////////
        //
        //
        //

        private void button16_Click(object sender, EventArgs e) //Button Create New Editor (new)
        {
            if (ListE == 3)
            {
                ListOfLists.Add(ListEdit0);
                ListE++;
            }
            if (ListE == 2)
            {
                ListOfLists.Add(ListEdit0);
                ListE++;
            }
            if (ListE == 1)
            {
                ListOfLists.Add(ListEdit0);
                ListE++;
            }
            if (ListE == 0) 
            {
                ListOfLists.Add(ListEdit0);
                ListE++;
            }

            
            //List<Button> ListOfEditorButtons = new List<Button>();
            //List<List<string>> ListOfLists = new List<List<string>>();
            PanelHide();
            MakeEditorButtonNew();
            MakeEditorPanelNew();
            MakeDummyLabel();
            MakeEditorLeftSidebarNew();
            MakeEditorCollectionTreeNew();
            MakeEditorPageNew();
            HexWidth = Convert.ToInt16(richTextBoxHexWidth.Text); //Used temporarily in loading a dummy file. Later this would be loaded from a file on the PC.

            MakeNewEntry();
        }

        private void MakeEditorPanelNew() 
        {
            Panel newPanel = new Panel();

            //newPanel.Text = ListOfDocumentNames[i2];
            //newPanel.Text = richTextBox1.Text;
            newPanel.Name = "Editor" + richTextBox1.Text + "Panel";            
            newPanel.Dock= DockStyle.Fill;
            newPanel.BackColor = Color.FromArgb(38, 88, 38);
            newPanel.ForeColor = Color.Black;            
            newPanel.Click += delegate
            {
                // Your code                

                //richTextBoxDocumentation.Text = File.ReadAllText(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + GameLibrary.libraryNodeName + "\\Documentation\\" + newPanel.Text + "\\Text.txt");
                //DocumentReset();
                //SelectedDocument = newPanel.Text;
                //SelectedDocumentName = newPanel.Name;
            };

            //ListOfDocumentButtons.Add(newPanel);
            panelCore.Controls.Add(newPanel);
            //panelCore.Controls.C
            //i2 = i2 + 1;

        }

        private void MakeEditorButtonNew()
        {
            Button newButton = new Button();
            newButton.Text = richTextBox1.Text;
            newButton.Name = "Editor" + richTextBox1.Text;
            newButton.Dock = DockStyle.Top;
            newButton.ForeColor = Color.FromArgb(224,224,224);
            newButton.BackColor = Color.FromArgb(35,35,35);
            newButton.FlatStyle= FlatStyle.Flat;
            newButton.FlatAppearance.BorderColor = Color.FromArgb(150, 150, 150);
            newButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(30,30,30);
            newButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(45, 45, 45);
            newButton.Size = new Size(10,35);            
            panelEditorList.Controls.Add(newButton);
            

            newButton.Click += delegate
            {
                //SelectedEditor = newButton.Name;
                //PanelSwap();

                PanelHide();
                SelectedEditor = newButton.Name;
                PanelShow();
            };
            SelectedEditor = newButton.Name;
            ListOfEditorButtons.Add(newButton);
            newButton.BringToFront();

        }

        private void MakeDummyLabel() 
        {
            Label newLabel = new Label();

            newLabel.Text = richTextBox1.Text + " Editor";
            newLabel.Font = new Font("Microsoft Sans Serif", 13);
            newLabel.Location = new Point(850, 300);
            var thinggg = Controls.Find("Editor" + richTextBox1.Text + "Panel", true);
            thinggg[0].Controls.Add(newLabel);


        }


        private void MakeEditorLeftSidebarNew() 
        {
            Panel newPanel = new Panel();

            newPanel.Name = "Editor" + richTextBox1.Text + "Panel" + "LSB";
            newPanel.Dock = DockStyle.Left;
            newPanel.Size = new Size(250,1);
            newPanel.BackColor = Color.FromArgb(30, 0, 0);
            newPanel.ForeColor = Color.Black;
            newPanel.Click += delegate
            {
                // Your code                

                //richTextBoxDocumentation.Text = File.ReadAllText(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + GameLibrary.libraryNodeName + "\\Documentation\\" + newPanel.Text + "\\Text.txt");
                //DocumentReset();
                //SelectedDocument = newPanel.Text;
                //SelectedDocumentName = newPanel.Name;
            };

            //ListOfDocumentButtons.Add(newPanel);
            //panelCore.Controls.Add(newPanel);
            Controls.Find(SelectedEditor + "Panel", true)[0].Controls.Add(newPanel);
            //panelCore.Controls.C
            //i2 = i2 + 1;

        }


        private void MakeEditorCollectionTreeNew() 
        {

            TreeView newTree = new TreeView();
            //newTree.Text = richTextBox1.Text + "Wut";
            newTree.Name = SelectedEditor + "Panel" + "Tree";
            newTree.Dock = DockStyle.Top;
            newTree.ForeColor = Color.FromArgb(224, 224, 224);
            newTree.BackColor = Color.FromArgb(35, 35, 35);
            newTree.Size = new Size(200,500);
            newTree.Font = new Font("Segoe UI", 13);
            newTree.HotTracking = true;
            newTree.HideSelection = false;
            newTree.ShowLines = false;
            newTree.ShowRootLines= false;
            newTree.FullRowSelect = true;
            
            //newTree.FlatStyle = FlatStyle.Flat;
            //newTree.FlatAppearance.BorderColor = Color.FromArgb(150, 150, 150);
            //newTree.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 30, 30);
            //newTree.FlatAppearance.MouseOverBackColor = Color.FromArgb(45, 45, 45);
            //newTree.Size = new Size(10, 35);
            //panelEditorList.Controls.Add(newTree);
            //Controls.Find(SelectedEditor + "Panel", true)[0].Hide();
            Controls.Find(SelectedEditor + "Panel" + "LSB", true)[0].Controls.Add(newTree);

            newTree.Click += delegate
            {
                //SelectedEditor = newTree.Name;
                //PanelSwap();
                richTextBox2.Text = newTree.Name;


            };

            newTree.AfterSelect += delegate
            {
                GiveEntrysHex2Dec();
            };


            newTree.Nodes.Add("0 Knight Fencer"); //Used temporarily in loading a dummy file. Later this would be loaded from a file on the PC.
            newTree.Nodes.Add("1 Knight Guard");            
            newTree.Nodes.Add("2 Knight Warrior");
            newTree.Nodes.Add("HIDDEN Knight Fencer H");
            newTree.Nodes.Add("3 Knight Fencer A");
            //ListOfLists.Add(newTree);            
            if (ListE == 3)
            {
                ListEdit3.Add(new TreeView());
            }
            if (ListE == 2)
            {
                ListEdit2.Add(new TreeView());
            }
            if (ListE == 1)
            {
                ListEdit1.Add(new TreeView());
            }
            if (ListE == 0)
            {
                ListEdit0.Add(new TreeView());
            }
            //ListOfEditorButtons.Add(newTree);

        }



        private void MakeEditorPageNew() 
        {

            Panel newPanel = new Panel();

            newPanel.Name = "Editor" + richTextBox1.Text + "Panel" + "Page1";
            newPanel.Dock = DockStyle.Left;
            newPanel.Size = new Size(550, 1);
            newPanel.BackColor = Color.FromArgb(00, 30, 0);
            newPanel.ForeColor = Color.Black;
            newPanel.Click += delegate
            {
                // Your code                

                //richTextBoxDocumentation.Text = File.ReadAllText(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + GameLibrary.libraryNodeName + "\\Documentation\\" + newPanel.Text + "\\Text.txt");
                //DocumentReset();
                //SelectedDocument = newPanel.Text;
                //SelectedDocumentName = newPanel.Name;
            };

            //ListOfDocumentButtons.Add(newPanel);
            //panelCore.Controls.Add(newPanel);
            Controls.Find(SelectedEditor + "Panel", true)[0].Controls.Add(newPanel);
            newPanel.BringToFront();
            MakeEditorRowNew();
        }

        private void MakeEditorRowNew() 
        {
            Panel newPanel = new Panel();

            newPanel.Name = "Editor" + richTextBox1.Text + "Panel" + "Page1" + "Row1";
            newPanel.Dock = DockStyle.Top;
            newPanel.Size = new Size(0, 300);
            newPanel.BackColor = Color.FromArgb(00, 0, 60);
            newPanel.ForeColor = Color.Black;
            newPanel.Click += delegate
            {
                // Your code                

                //richTextBoxDocumentation.Text = File.ReadAllText(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + GameLibrary.libraryNodeName + "\\Documentation\\" + newPanel.Text + "\\Text.txt");
                //DocumentReset();
                //SelectedDocument = newPanel.Text;
                //SelectedDocumentName = newPanel.Name;
            };

            //ListOfDocumentButtons.Add(newPanel);
            //panelCore.Controls.Add(newPanel);
            Controls.Find(SelectedEditor + "Panel" + "Page1", true)[0].Controls.Add(newPanel);
            MakeEditorColumnNew();
        }

        private void MakeEditorColumnNew()
        {
            Panel newPanel = new Panel();

            newPanel.Name = "Editor" + richTextBox1.Text + "Panel" + "Page1" + "Row1" + "Column1";
            newPanel.Dock = DockStyle.Left;
            newPanel.Size = new Size(300, 0);
            newPanel.BackColor = Color.FromArgb(60, 00, 0);
            newPanel.ForeColor = Color.Black;
            newPanel.Click += delegate
            {
                // Your code                

                //richTextBoxDocumentation.Text = File.ReadAllText(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + GameLibrary.libraryNodeName + "\\Documentation\\" + newPanel.Text + "\\Text.txt");
                //DocumentReset();
                //SelectedDocument = newPanel.Text;
                //SelectedDocumentName = newPanel.Name;
            };

            //ListOfDocumentButtons.Add(newPanel);
            //panelCore.Controls.Add(newPanel);
            Controls.Find(SelectedEditor + "Panel" + "Page1" + "Row1", true)[0].Controls.Add(newPanel);

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
                Controls.Find("Editor" + richTextBox1.Text + "Panel" + "Page1" + "Row1" + "Column1", true)[0].Controls.Add(newEntry);
                //tabControl1.TabPages[0].Controls.Add(newEntry);
                //panel8.Controls.Add(newEntry);

                EntryList.Add(newEntry);

                richTextBox3.Text = "Entry trigger";
            }

            MakeEntryNameBoxes();
            MakeEntryNumberBoxes();
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
                newTextBox.BorderStyle = BorderStyle.None;
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
                richTextBox3.Text = "Namebox trigger";
            }

        }


        private void MakeEntryNumberBoxes()
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
                richTextBox3.Text = "Textbox trigger";
            }
            
        }

        private void GiveEntrysHex2Dec()
        {
            var CollectionTree = Controls.Find(SelectedEditor + "Panel" + "Tree", true)[0] as TreeView;
            string hexpath = DictionaryOfStrings.CrystalPath + "\\HexFiles\\HexDummy1";  //This defines the path as a string, so i can refer to it by this string/name instead of the full path every time
            int hexlength = (int)(new FileInfo(hexpath).Length);   //The leagth of the array?
            HexFile = File.ReadAllBytes(hexpath);  //loads an array with whatever is in the path
            int ListInt = 0;
            //HexWidth = 1;

            foreach (Control TextBox in TextBoxlist)
            {
                //richTextBox4.AppendText("\nHeyo");
                TextBoxlist[ListInt].Text = HexFile[HexOffset + (CollectionTree.SelectedNode.Index * HexWidth) + ListInt].ToString("D"); //#1 is offset, #2 is Row Width, #3 is byte in row.
                ListInt++;

            }
        }



    }

    
}
