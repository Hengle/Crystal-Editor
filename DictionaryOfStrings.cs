using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Crystal_Editor
{
    internal class DictionaryOfStrings
    {

        public string partyGame = "Darts"; //A test dummy, unused.
        public static string SelectedEditorDirectory = ""; //i forget, i think its used in workshop to set the users project directory.
        //public static string SelectedDocumentDirectory = ""; //i forget, i think its used in workshop to set the users project directory.
        public int Ex = 0; //Used for creating editor buttons in workshop, the X coordinate
        public int Ey = 0; //Used for creating editor buttons in workshop, the Y coordinate
        public static string CrystalPath = "";
        public static string CreateEditorFolder = "";
    }
}
