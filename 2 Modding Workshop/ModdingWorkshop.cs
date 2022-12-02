using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Crystal_Editor._3_Editor_Board;
//using Newtonsoft.Json;


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


        public ModdingWorkshop()
        {
            InitializeComponent();

            richTextBoxDocumentName.Hide();
            pictureBoxDiscord.Image = Image.FromFile(DictionaryOfStrings.CrystalPath + "\\Other\\Images\\DiscordLogo.png");

            //This counts how many editor folders there are, we later display this as buttons to open those editors.
            countDirectories = System.IO.Directory.GetDirectories(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + GameLibrary.libraryNodeName + "\\Editors", "*", SearchOption.TopDirectoryOnly).Count();
            

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
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    
}
