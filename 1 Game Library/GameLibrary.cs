using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
//using System.Management;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
//using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Crystal_Editor
{
    public partial class GameLibrary : Form
    {
        

        //public static string selectedFolder = "DummyTest (Error)";
        //public string dummySettings = File.ReadAllText(Properties.Settings.Default.settingBetaGamesFolder + "\\Persona 4 Golden\\Dummy Settings.json");


        public GameLibrary()
        {
            InitializeComponent();
            DictionaryOfStrings.CrystalPath = Assembly.GetEntryAssembly().Location;            
            DictionaryOfStrings.CrystalPath = Path.GetFullPath(Path.Combine(DictionaryOfStrings.CrystalPath, @"..\..\..\"));            
            ScanForWorkshops();

            //tabControlWorkshopInfo

            tabControlWorkshopInfo.Appearance = TabAppearance.FlatButtons;
            tabControlWorkshopInfo.ItemSize = new Size(0, 1);
            tabControlWorkshopInfo.SizeMode = TabSizeMode.Fixed;

            foreach (TabPage tab in tabControlWorkshopInfo.TabPages)
            {
                tab.Text = "";
            }


        }

        private void button1_Click(object sender, EventArgs e) //Button: Launch Workshop
        {
            ModdingWorkshop f2 = new ModdingWorkshop();
            f2.Show();
            //this.Close();
        }
        
        

        private void ScanForWorkshops() 
        {
            libraryTreeView.Nodes.Clear();

            if (Directory.Exists(DictionaryOfStrings.CrystalPath + "\\Workshops"))
            {
                string[] allWorkshopsPathsArray = Directory.GetDirectories(DictionaryOfStrings.CrystalPath + "\\Workshops");
                string[] allWorkshopNamesArray;

                foreach (var WorkshopPath in allWorkshopsPathsArray)
                {
                                       
                    libraryTreeView.Nodes.Add(File.ReadAllText(WorkshopPath + "\\Game Name.txt"));
                }
            }
            //Make this line a warning popup that directory does not exist
        }

                        

        public static string libraryNodeName = "";

        private void libraryTreeView_AfterSelect(object sender, TreeViewEventArgs e) //TreeView: After selecting a node
        {
            libraryNodeName = libraryTreeView.SelectedNode.Text;            

            //Check if an Art Banner exists. Load it if it does.
            string checkFileExist = DictionaryOfStrings.CrystalPath + "\\Workshops\\" + libraryNodeName + "\\LibraryBannerArt.png";
            if (File.Exists(checkFileExist))
            {
                pictureBox1.Image = Image.FromFile(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + libraryNodeName + "\\LibraryBannerArt.png");
                

                
            }
            if (!File.Exists(checkFileExist))
            {
                pictureBox1.Image = Image.FromFile(DictionaryOfStrings.CrystalPath + "\\Other\\Images\\LibraryBannerArt.png");

            }

            labelEditorCount.Text = Convert.ToString(System.IO.Directory.GetDirectories(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + libraryNodeName + "\\Editors", "*", SearchOption.TopDirectoryOnly).Count());
            labelDocumentCount.Text = Convert.ToString(System.IO.Directory.GetDirectories(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + libraryNodeName + "\\Documentation", "*", SearchOption.TopDirectoryOnly).Count());
            labelToolCount.Text = Convert.ToString(System.IO.Directory.GetDirectories(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + libraryNodeName + "\\Tools", "*", SearchOption.TopDirectoryOnly).Count());
            labelGameRegion.Text = File.ReadAllText(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + GameLibrary.libraryNodeName + "\\Game Region.txt");
            labelBestEmulator.Text = File.ReadAllText(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + GameLibrary.libraryNodeName + "\\Best Emulator.txt");
            labelGamePlatform.Text = File.ReadAllText(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + GameLibrary.libraryNodeName + "\\Game Platform.txt");
            tabControlWorkshopInfo.SelectedTab = tabControlWorkshopInfo.TabPages["tabPagePatchNotes"];
            button1.Enabled = true;
            button1.BackColor = Color.FromArgb(35, 35, 35);
        }   

        private void buttonTutorial_Click(object sender, EventArgs e) //Button: Tutorial
        {
            Tutorial f2 = new Tutorial();
            f2.Show();
        }

        
        
                
        private void button5_Click(object sender, EventArgs e) //Button: Create Workshop
        {
            ClearInfo();
            tabControlWorkshopInfo.SelectedTab = tabControlWorkshopInfo.TabPages["tabPageWorkshopMaker"];
            button4.Text = "Create Workshop";
        }

        private void buttonEditWorkshop_Click(object sender, EventArgs e) //Button: Edit Workshop
        {
            ClearInfo();
            tabControlWorkshopInfo.SelectedTab = tabControlWorkshopInfo.TabPages["tabPageWorkshopMaker"];
            button4.Text = "Save Workshop";
            richTextBoxGameName.Text = libraryNodeName;
            comboBoxGamePlatform.Text = File.ReadAllText(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + libraryNodeName + "\\Game Platform.txt");
            comboBoxGameRegion.Text = File.ReadAllText(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + libraryNodeName + "\\Game Region.txt");
            comboBoxBestEmulator.Text = File.ReadAllText(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + libraryNodeName + "\\Best Emulator.txt");



        }

        private void ClearInfo() 
        {
            richTextBoxGameName.Text = null;
            comboBoxGamePlatform.Text = null;
            comboBoxGameRegion.Text = null;
            comboBoxBestEmulator.Text = null;
            labelGamePlatform.Text = null;
            labelGameRegion.Text = null;
            labelBestEmulator.Text = null;
        }

        private void button4_Click(object sender, EventArgs e) //Button: (Tab) WorkshopMaker
        {
            if (button4.Text == "Create Workshop")
            {
                Directory.CreateDirectory(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + richTextBoxGameName.Text);
                Directory.CreateDirectory(DictionaryOfStrings.CrystalPath + "\\Settings\\Workshops\\" + richTextBoxGameName.Text);
                Directory.CreateDirectory(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + richTextBoxGameName.Text + "\\Documentation");
                Directory.CreateDirectory(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + richTextBoxGameName.Text + "\\Editors");
                Directory.CreateDirectory(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + richTextBoxGameName.Text + "\\Tools");
                File.WriteAllText(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + richTextBoxGameName.Text + "\\Game Name.txt", richTextBoxGameName.Text); //Overwrites, Or creates file if it does not exist. Needs location permissions for admin folders.
                SaveWorkshop();
                
            }
            if (button4.Text == "Save Workshop")
            {

                
                //labelGameRegion.Text = File.ReadAllText(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + GameLibrary.libraryNodeName + "\\Game Region.txt");
                //labelBestEmulator.Text = File.ReadAllText(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + GameLibrary.libraryNodeName + "\\Best Emulator.txt");
                //labelGamePlatform.Text = File.ReadAllText(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + GameLibrary.libraryNodeName + "\\Game Platform.txt");
                
                string oldFolderName = DictionaryOfStrings.CrystalPath + "\\Workshops\\" + libraryNodeName;
                string newFolderName = DictionaryOfStrings.CrystalPath + "\\Workshops\\" + richTextBoxGameName.Text;
                string oldSettingsFolderName = DictionaryOfStrings.CrystalPath + "\\Settings\\Workshops\\" + libraryNodeName;
                string newSettingsFolderName = DictionaryOfStrings.CrystalPath + "\\Settings\\Workshops\\" + richTextBoxGameName.Text;
                try
                {
                    Directory.Move(oldFolderName, newFolderName);
                    Directory.Move(oldSettingsFolderName, newSettingsFolderName);
                }
                catch (IOException exp)
                {
                    Console.WriteLine(exp.Message);
                }

                File.WriteAllText(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + richTextBoxGameName.Text + "\\Game Name.txt", richTextBoxGameName.Text); //Overwrites, Or creates file if it does not exist. Needs location permissions for admin folders.

                SaveWorkshop();
                button1.Enabled = false;
                button1.BackColor = Color.Gray;
            }
        }

        private void SaveWorkshop() 
        {
            File.WriteAllText(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + richTextBoxGameName.Text + "\\Game Platform.txt", comboBoxGamePlatform.Text); //Overwrites, Or creates file if it does not exist. Needs location permissions for admin folders.
            File.WriteAllText(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + richTextBoxGameName.Text + "\\Game Region.txt", comboBoxGameRegion.Text); //Overwrites, Or creates file if it does not exist. Needs location permissions for admin folders.
            File.WriteAllText(DictionaryOfStrings.CrystalPath + "\\Workshops\\" + richTextBoxGameName.Text + "\\Best Emulator.txt", comboBoxBestEmulator.Text); //Overwrites, Or creates file if it does not exist. Needs location permissions for admin folders.
            tabControlWorkshopInfo.SelectedTab = tabControlWorkshopInfo.TabPages["tabPagePatchNotes"];
            ScanForWorkshops();
        }

        private void buttonCancel_Click(object sender, EventArgs e)//Button: (Tab) Cancel (Aka go to Patchnotes tab)
        {
            tabControlWorkshopInfo.SelectedTab = tabControlWorkshopInfo.TabPages["tabPagePatchNotes"];
        }
        //System.IO.Path.GetDirectoryName(Application.ExecutablePath);

    }
}
