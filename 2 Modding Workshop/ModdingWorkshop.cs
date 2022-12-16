using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Linq;
//using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;


//using Crystal_Editor._3_Editor_Board; //NO ERROR? NOW THERE IS?

//private void button14_Click(object sender, EventArgs e) //Button Alpha
//{
//    EditorBoard f2 = new EditorBoard();
//    f2.Show();
//}


namespace Crystal_Editor
{
    public partial class ModdingWorkshop : Form
    {
        //DictionaryOfStrings dictionary = new DictionaryOfStrings();        
        int NoSave = 0; //Used to make sure only the first selection of a document doesn't save.
        List<string> ListOfDocumentNames = new List<string>();
        List<Button> ListOfDocumentButtons = new List<Button>();        
        string SelectedDocument = ""; //when the user clicks on a document, it becomed the selected document.
        string SelectedDocumentName = ""; //when the user clicks on a document, it becomes the selected document name.
        int PixelsBetweenDocuments = 29;
        Button SelectedDoc;
        string SelectedEditor = "";
        
        List<Button> ListOfEditorButtons = new List<Button>();
        string HomeCoreName = "Home"; //The name of the Home's Core Panel (So its easy to change globally)


        public ModdingWorkshop()
        {
            InitializeComponent();
                        
            SelectedEditor = HomeCoreName;
            richTextBoxHexWidth.Text = "3";
            pictureBoxDiscord.Image = Image.FromFile(DictionaryOfStrings.CrystalPath + "\\Other\\Images\\DiscordLogo.png");

            LoadDocumentation();                        
        }
        

        private void LoadDocumentation()
        {
                        
            string[] DocNames = Directory.GetDirectories(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + GameLibrary.libraryNodeName + "\\Documentation");
            ListOfDocumentNames.Clear();
            ListOfDocumentButtons.Clear();
            panelDocumentation.Controls.Clear();

            for (int i2 = 0; i2 < DocNames.Length; i2++)
            {
                           
                ListOfDocumentNames.Add(Path.GetFileName(DocNames[i2]).ToString());
                Button DocumentButton = new Button();


                DocumentButton.Text = ListOfDocumentNames[i2];
                DocumentButton.Name = File.ReadAllText(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + GameLibrary.libraryNodeName + "\\Documentation\\" + DocumentButton.Text + "\\Order.txt");
                
                DocumentButton.Location = new Point(0, ((Convert.ToInt16(DocumentButton.Name) * PixelsBetweenDocuments) + 0));

                DocumentButton.Size = new Size(216, 30);
                DocumentButton.BackColor = Color.FromArgb(38, 38, 38);
                DocumentButton.ForeColor = Color.White;
                DocumentButton.FlatStyle = FlatStyle.Flat;
                DocumentButton.FlatAppearance.BorderColor = Color.FromArgb(00, 00, 00); //I want this brighter when moused over?
                DocumentButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 30, 30);
                DocumentButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(45, 45, 45);
                //DocumentButton.Dock = DockStyle.Top;

                DocumentButton.Click += delegate
                {
                    SaveDoc();
                    DocumentButton.BackColor = Color.FromArgb(33, 33, 33);                                                      
                    SelectedDoc = DocumentButton;
                    richTextBoxDocumentation.Text = File.ReadAllText(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + GameLibrary.libraryNodeName + "\\Documentation\\" + DocumentButton.Text + "\\Text.txt");
                    SelectedDocument = DocumentButton.Text;
                    SelectedDocumentName = DocumentButton.Name;
                    richTextBoxDocumentName.Text = DocumentButton.Text;

                };

                ListOfDocumentButtons.Add(DocumentButton);
                panelDocumentation.Controls.Add(DocumentButton);
            }
            ListOfDocumentButtons.Sort((x, y) => String.Compare(x.Name, y.Name)); //Sort the Document Buttons list by Order
            //Does not affect display order on launch, only affects future moving of things up/down because it does so by list ID

        }

        private void SaveDoc() //Button: Save Document
        {
            if (NoSave == 1) 
            {
                SelectedDoc.BackColor = Color.FromArgb(38, 38, 38);
                string OldDocumentDirName = DictionaryOfStrings.CrystalPath + "\\Workshops\\" + GameLibrary.libraryNodeName + "\\Documentation\\" + SelectedDocument;
                string NewDocumentDirName = DictionaryOfStrings.CrystalPath + "\\Workshops\\" + GameLibrary.libraryNodeName + "\\Documentation\\" + richTextBoxDocumentName.Text;
                try
                {
                    Directory.Move(OldDocumentDirName, NewDocumentDirName);
                }
                catch (IOException exp)
                {
                    Console.WriteLine(exp.Message);
                }
                SelectedDocument = richTextBoxDocumentName.Text;


                File.WriteAllText(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + GameLibrary.libraryNodeName + "\\Documentation\\" + richTextBoxDocumentName.Text + "\\Text.txt", richTextBoxDocumentation.Text); //Overwrites, Or creates file if it does not exist. Needs location permissions for admin folders.
                SelectedDoc.Text = richTextBoxDocumentName.Text;
            }

            NoSave = 1;
        }
        
        private void button16_Click(object sender, EventArgs e) //Button: Save as New Document
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
                
            }

            LoadDocumentation();
        }



        // ///////////////////////////Document Movement Controls v///////////////////

        private void button7_Click(object sender, EventArgs e) //Button Down (Move Documenet)
        {
            if (SelectedDocumentName != Convert.ToString(ListOfDocumentButtons.Count() - 1)) //  "0"
            {
                int DOne = Convert.ToInt16(ListOfDocumentButtons[Convert.ToInt16(SelectedDocumentName)].Name);
                int DTwo = DOne + 1;
                Swap(DOne, DTwo);
                SelectedDocumentName = (DOne + 1).ToString();
                //ListOfDocumentButtons[0].Text = "Some text";

            }

        }
        private void button6_Click(object sender, EventArgs e) //Button Up (Move Documenet)
        {
            if (SelectedDocumentName != "0") 
            {
                int DTwo = Convert.ToInt16(ListOfDocumentButtons[Convert.ToInt16(SelectedDocumentName)].Name);
                int DOne = DTwo - 1;
                Swap(DOne, DTwo);
                SelectedDocumentName = (DTwo - 1).ToString();
            }
            
        }        

        private void Swap(int DOne, int DTwo) 
        {
            
            ListOfDocumentButtons[DOne].Location = new Point(0, (DOne * PixelsBetweenDocuments) + 0 + PixelsBetweenDocuments);
            ListOfDocumentButtons[DTwo].Location = new Point(0, (DTwo * PixelsBetweenDocuments) + 0 - PixelsBetweenDocuments);
            System.IO.File.WriteAllText(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + GameLibrary.libraryNodeName + "\\Documentation\\" + ListOfDocumentButtons[DOne].Text + "\\Order.txt", (DOne + 1).ToString()); //Overwrites, Or creates file if it does not exist. Needs location permissions for admin folders.
            System.IO.File.WriteAllText(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + GameLibrary.libraryNodeName + "\\Documentation\\" + ListOfDocumentButtons[DTwo].Text + "\\Order.txt", (DTwo - 1).ToString()); //Overwrites, Or creates file if it does not exist. Needs location permissions for admin folders.
            ListOfDocumentButtons[DOne].Name = (DOne + 1).ToString();
            ListOfDocumentButtons[DTwo].Name = (DTwo - 1).ToString();            
            ListOfDocumentButtons.Sort((x, y) => String.Compare(x.Name, y.Name)); //Sort the Document Buttons list by Order
        }

        
        

        // ///////////////////////////////v Top Bar stuff v  - - - ^ Documentation Stuff ^ /////////////////////////////////

        
        private void pictureBoxDiscord_Click(object sender, EventArgs e) //Image Button: Discord
        {
            //System.Diagnostics.Process.Start("https://discord.gg/mhrZqjRyKx");
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
            yourProcess.StartInfo.FileName = @DictionaryOfStrings.CrystalPath + "\\Tools\\General\\Text Core - N++\\notepad++.exe";
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
            yourProcess.StartInfo.FileName = @DictionaryOfStrings.CrystalPath + "\\Tools\\General\\Hex Core - HxD\\HxD64.exe";
            yourProcess.Start();
        }

        private void button5_Click(object sender, EventArgs e) //Button: (Tool) Hex Core 010
        {
            Process yourProcess = new Process();
            yourProcess.StartInfo.FileName = @DictionaryOfStrings.CrystalPath + "\\Tools\\General\\Hex Core - 010\\010EditorPortable.exe";
            yourProcess.Start();
        }





















        // //////////////////////////////////////////////////////////////////////////////////////////
        // //////////////////////////////////////////////////////////////////////////////////////////
        // //////////////////////////////////////////////////////////////////////////////////////////
        //
        //
        //
        // //////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////Core Controls/////////////////////////////////////////
        // //////////////////////////////////////////////////////////////////////////////////////////
        //
        //
        //
        // //////////////////////////////////////////////////////////////////////////////////////////
        // //////////////////////////////////////////////////////////////////////////////////////////
        // //////////////////////////////////////////////////////////////////////////////////////////




        private void button3_Click(object sender, EventArgs e) //Button Home
        {
            PanelHide();
            SelectedEditor = HomeCoreName;
            PanelShow();
            //CoreHome
        }

        private void button15_Click(object sender, EventArgs e) //Button Hide Selected Core
        {
            PanelHide();
        }

        private void PanelHide() 
        {
            Controls.Find(SelectedEditor, true)[0].Hide();
        }

        private void PanelShow()
        {
            Controls.Find(SelectedEditor, true)[0].Show();
        }

        
        
        /////////////////XML EXAMPLE////////////////////
        private void buttonSaveWorkshop_Click(object sender, EventArgs e)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("    ");
            settings.CloseOutput = true;
            settings.OmitXmlDeclaration = true;
            using (XmlWriter writer = XmlWriter.Create(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + GameLibrary.libraryNodeName + "\\Core.xml", settings))
            {
                writer.WriteStartElement("Root");//Start Root
                  writer.WriteStartElement("Furrys");
                    writer.WriteElementString("Tubble", "Furry");
                    writer.WriteElementString("Dawn", "Furry");
                  writer.WriteEndElement();
                  writer.WriteStartElement("Toys");
                    writer.WriteElementString("Hood", "Yes");
                    writer.WriteElementString("Bone", "Yes");
                    writer.WriteElementString("Chocolate", "No");
                  writer.WriteEndElement();
                  writer.WriteStartElement("Thing1");
                    writer.WriteStartElement("Thing2");
                      writer.WriteElementString("Thingis", "Cotton");
                    writer.WriteEndElement();
                  writer.WriteEndElement();
                writer.WriteEndElement(); //End Root               

                writer.Flush(); //Ends the XML File
            }
        }


        //private void LoadXML()  //Example of loading a XML file
        //{
        //    XmlDocument EditXml = new XmlDocument();
        //    EditXml.Load(DictionaryOfStrings.CrystalPath + "Debug\\net7.0-windows\\Core.xml");
        //    XmlNodeList Page = EditXml.GetElementsByTagName("Page");
        //    XmlNodeList Name = EditXml.GetElementsByTagName("Name");
        //    //richTextBoxLoadXML.Text = "Name: " + Tab[0].InnerText;
        //    //richTextBox9.Text = "Name: " + Name[0].InnerText;


        //    //SelectSingleNode
        //    //tabControl1.TabPages[0].Text = EditXml.SelectSingleNode("EditBoard/TabPage[0]/Name").InnerText;
        //    //richTextBoxLoadXML.Text = EditXml.SelectSingleNode("/EditBoard/TabPage[1]/Name").InnerText;
        //    richTextBoxLoadXML.Text = EditXml.SelectSingleNode("/EditBoard/TabPage").LocalName;
        //    //richTextBoxLoadXML.Text = EditXml.SelectSingleNode("/EditBoard/11111").LocalName;
        //    //textBox2.Text = Page.Count.ToString();
        //    //for (int i = Page.Count - 1; i > 0;)
        //    //{
        //    //    TabPage tp = new TabPage(Page[i].InnerText);
        //    //    tabControl1.TabPages.Add(tp);
        //    //    i--;
        //    //    //textBox2.Text = "HELPPP";
        //    //}

        //}

        // //////////////////////////////////////////////////////////////////////////////////////////
        // //////////////////////////////////////////////////////////////////////////////////////////
        // //////////////////////////////////////////////////////////////////////////////////////////
        //
        //
        //
        //
        // //////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////TUBBLE THINGS////////////////////////////////////////
        // //////////////////////////////////////////////////////////////////////////////////////////
        //
        //
        //
        //
        // //////////////////////////////////////////////////////////////////////////////////////////
        // //////////////////////////////////////////////////////////////////////////////////////////
        // //////////////////////////////////////////////////////////////////////////////////////////

        //List<Button> ListOfDocumentButtons = new List<Button>();
        //Dictionary<string, string> capitalOf = new Dictionary<string, string>();
        //capitalOf.Add("Japan", "Tokio");

        // Loop over the dictionary and output the results to the console
        //foreach (KeyValuePair<string, string> combi in capitalOf)
        //{
        // Console.WriteLine("The capital of " + combi.Key + " is " + combi.Value);
        //}


        List<Button> EditorList = new List<Button>();
        Dictionary<string, Panel> EditorData = new Dictionary<string, Panel>();



        //=============CURRENT GOALS=================
        //Clicking editor button hides current Core + opens selected editor Core
        //
        //
        //panelEditorList      Name of the panel the Core buttons are docked into

        private void buttonCreateTubbleEditor_Click(object sender, EventArgs e) //Button: New Tubble Core
        {
            string buttonName = CreateNewEditorSelectionButton(); //Grabs the button name / What user puts in textbox and puts it in buttonName
            //EditorData[buttonName] = CreateNewEditorPanel(); //Adds a Ilist to the mega dictionary, with the name of the editor (aka button.Name) as the key. (AKA users textbox text)
            Core EditorNew = new Core(richTextBox1.Text);
            panelCore.Controls.Add(EditorNew.GetPanel());
            EditorData[buttonName] = EditorNew.GetPanel();
            //CreateNewDummyLabel();
        }


        private String CreateNewEditorSelectionButton()
        {
            Button editorSelectionButton = new Button();
            
            editorSelectionButton.Text = richTextBox1.Text;
            editorSelectionButton.Name = "Core" + richTextBox1.Text;
            editorSelectionButton.Dock = DockStyle.Top;
            editorSelectionButton.ForeColor = Color.FromArgb(224, 224, 224);
            editorSelectionButton.BackColor = Color.FromArgb(35, 35, 35);
            editorSelectionButton.FlatStyle = FlatStyle.Flat;
            editorSelectionButton.FlatAppearance.BorderColor = Color.FromArgb(150, 150, 150);
            editorSelectionButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 30, 30);
            editorSelectionButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(45, 45, 45);
            editorSelectionButton.Size = new Size(10, 35);            
            

            editorSelectionButton.Click += delegate
            {

                //PanelSwap();

                //Hide Current Core
                //  Controls.Find(SelectedEditor + "Panel", true)[0].Hide();
                //  Controls.Find(SelectedEditor + "Panel", true)[0].Show();

                //button -> dictionary -> Core in dictionary (Show this)
                //SelectedEditor = (Becomes Current Core)
                if (SelectedEditor != HomeCoreName)
                {
                    EditorData[SelectedEditor].Hide();
                }
                else 
                {
                    Controls.Find(HomeCoreName, true)[0].Hide();
                }
                SelectedEditor = editorSelectionButton.Name;
                EditorData[editorSelectionButton.Name].Show();
                //PanelHide();
                //SelectedEditor = editorSelectionButton.Name;
                //PanelShow();
            };
            //Hide "Current Core"
            //button click sets current core from dictionary
            //Show "Current Core"

            if (SelectedEditor != HomeCoreName) 
            {
                EditorData[SelectedEditor].Hide();
            }
            else
            {
                Controls.Find(HomeCoreName, true)[0].Hide();
            }


            SelectedEditor = editorSelectionButton.Name;
            ListOfEditorButtons.Add(editorSelectionButton);            

            panelEditorList.Controls.Add(editorSelectionButton);
            editorSelectionButton.BringToFront();
            EditorList.Add(editorSelectionButton);

            return editorSelectionButton.Name;
        }

        private Panel CreateNewEditorPanel()
        {
            Panel newPanel = new Panel();

            //newPanel.Text = ListOfDocumentNames[i2];
            //newPanel.Text = richTextBox1.Text;
            newPanel.Name = "Core" + richTextBox1.Text + "Panel";
            newPanel.Dock = DockStyle.Fill;
            newPanel.BackColor = Color.FromArgb(38, 88, 38);
            newPanel.ForeColor = Color.Black;
            newPanel.Click += delegate
            {
                // Your code                

                //richTextBoxDocumentation.Text = File.ReadAllText(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + GameLibrary.libraryNodeName + "\\Documentation\\" + newPanel.Text + "\\Text.txt");
                //DocumentReset();
                SelectedEditor = newPanel.Text;
                //SelectedDocumentName = newPanel.Name;
            };

            //ListOfDocumentButtons.Add(newPanel);
            panelCore.Controls.Add(newPanel);
            //panelCore.Controls.C
            //i2 = i2 + 1;

            return newPanel;

        }

        private void CreateNewDummyLabel()
        {
            Label newLabel = new Label();

            newLabel.Text = richTextBox1.Text + " Core";
            newLabel.Font = new Font("Microsoft Sans Serif", 13);
            newLabel.Location = new Point(850, 300);
            var thinggg = Controls.Find("Core" + richTextBox1.Text + "Panel", true);
            thinggg[0].Controls.Add(newLabel);


        }



        
        // //////////////////////////////////////////////////////////////////////////////////////////
        // //////////////////////////////////////////////////////////////////////////////////////////
        // //////////////////////////////////////////////////////////////////////////////////////////
        // //////////////////////////////////////////////////////////////////////////////////////////
        // //////////////////////////////////////////////////////////////////////////////////////////
        // //////////////////////////////////////////////////////////////////////////////////////////
        // ///////////////////////////////OLD REFERENCE MATERIAL/////////////////////////////////////
        // //////////////////////////////////////////////////////////////////////////////////////////
        // //////////////////////////////////////////////////////////////////////////////////////////
        // //////////////////////////////////////////////////////////////////////////////////////////
        // //////////////////////////////////////////////////////////////////////////////////////////
        // //////////////////////////////////////////////////////////////////////////////////////////
        // //////////////////////////////////////////////////////////////////////////////////////////
        


        //PanelHide();
        //MakeEditorButtonNew();
        //MakeEditorPanelNew();
        //MakeDummyLabel();
        //MakeEditorLeftSidebarNew();
        //MakeEditorCollectionTreeNew();
        //MakeEditorPageNew();
        //MakeEditorRowNew();
        //MakeEditorColumnNew();


        //DocumentButton.ForeColor = Color.FromArgb(224,224,224);
        //    DocumentButton.BackColor = Color.FromArgb(35,35,35);
        //    DocumentButton.FlatStyle= FlatStyle.Flat;
        //    DocumentButton.FlatAppearance.BorderColor = Color.FromArgb(150, 150, 150);
        //    DocumentButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(30,30,30);
        //    DocumentButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(45, 45, 45);


        //newTree.ForeColor = Color.FromArgb(224, 224, 224);
        //newTree.BackColor = Color.FromArgb(35, 35, 35);
        //newTree.Size = new Size(200,500);
        //newTree.Font = new Font("Segoe UI", 13);
        //newTree.HotTracking = true;
        //newTree.HideSelection = false;
        //newTree.ShowLines = false;
        //newTree.ShowRootLines= false;
        //newTree.FullRowSelect = true;

        //newTree.AfterSelect += delegate
        //{
        //    GiveEntrysHex2Dec();
        //};

        //Unused, but kept to preserve the ExampleLoadFile and Hex2Dec Examples
        byte[] HexFile; //This will be the entire file that we are editing.
        int HexWidth;
        int HexOffset = 0;
        List<Control> TextBoxlist = new List<Control>();

        private void ExampleLoadFile() 
        {
            string hexpath = DictionaryOfStrings.CrystalPath + "\\HexFiles\\HexDummy10x10";  //This defines the path as a string, so i can refer to it by this string/name instead of the full path every time
            int hexlength = (int)(new FileInfo(hexpath).Length);   //The leagth of the array?
            HexFile = File.ReadAllBytes(hexpath);  //loads an array with whatever is in the path
            var CollectionTree = Controls.Find(SelectedEditor + "Panel" + "Tree", true)[0] as TreeView;
            CollectionTree.Nodes.Clear();
            CollectionTree.Nodes.Add("0 Knight Fencer"); //Used temporarily in loading a dummy file. Later this would be loaded from a file on the PC.
            CollectionTree.Nodes.Add("1 Knight Guard");
            CollectionTree.Nodes.Add("2 Knight Warrior");
            CollectionTree.Nodes.Add("HIDDEN Knight Fencer H");
            CollectionTree.Nodes.Add("3 Knight Fencer A");

            TreeNodeCollection nodeCollect = CollectionTree.Nodes;
            CollectionTree.SelectedNode = nodeCollect[0];

            HexWidth = Convert.ToInt16(richTextBoxHexWidth.Text); //Used temporarily in loading a dummy file. Later this would be loaded from a file on the PC.


            
        }

        private void GiveEntrysHex2Dec()
        {
            var CollectionTree = Controls.Find(SelectedEditor + "Panel" + "Tree", true)[0] as TreeView;
            int ListInt = 0;
            //HexWidth = 1;

            

            foreach (Control TextBox in TextBoxlist)
            {
                //richTextBox4.AppendText("\nHeyo");
                TextBoxlist[ListInt].Text = HexFile[HexOffset + (CollectionTree.SelectedNode.Index * HexWidth) + ListInt].ToString("D"); //#1 is offset, #2 is Row Width, #3 is byte in row.
                ListInt++;


                //bug is caused by it selecting from latest textboxlist, instead of from a list from a spelected editor.
            }
        }











        private void button3_Click_1(object sender, EventArgs e) //Button: New Class Test
        {
            //im using this to practise moving info between classes.
            //It Doesn't do anything relevant right now.
            Documents CSDocuments = new Documents();
            CSDocuments.TextClass(richTextBoxNewClass);
            CSDocuments.TextClass2(); //(richTextBoxNewClass);
        }

















        // More new stuff

    }


}
