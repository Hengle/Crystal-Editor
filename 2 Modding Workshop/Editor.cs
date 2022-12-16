using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crystal_Editor
{
    public class Core
    {
        //Fields
        List<Page> PageList = new List<Page>();
        //List<...> TreeList = new List<...>;   --> add later, figure out Tree ADT
        Panel CorePanel = new Panel();
        //Contructor
        public Core(String name)
        {
            CorePanel.Name = "Core" + name;
            CorePanel.Dock = DockStyle.Fill;
            CorePanel.BackColor = Color.FromArgb(38, 88, 38); //Light green for debugging
            CorePanel.ForeColor = Color.Black;
        }
        //Methods
        public Panel GetPanel() 
        {
            return CorePanel;
        }
    }

    public class Page
    {
        //Fields
        List<Row> RowList = new List<Row>();
        Panel PagePanel = new Panel();
        //Constructor
        public Page()
        {
            //BackColor = Color.FromArgb(48, 98, 48); //Green for debugging
        }
        //Methods
        public Panel GetPanel()
        {
            return PagePanel;
        }
    }

    public class Row
    {
        //Fields
        List<Column> ColumnList = new List<Column>();
        Panel RowPanel = new Panel();
        //Constructor
        public Row()
        {
            //BackColor = Color.FromArgb(38, 38, 88); //Light Blue for debugging
        }
        //Methods
        public Panel GetPanel()
        {
            return RowPanel;
        }
    }

    public class Column
    {
        //Fields
        List<Entry> EntryList = new List<Entry>();
        Panel ColumnPanel = new Panel();
        //Constructor
        public Column()
        {
            //BackColor = Color.FromArgb(88, 38, 38); //Light Red for debugging
        }
        //Methods
    }

    public class Entry
    {
        //Fields
        String EntryType = "";
        int[] position;
        int ByteSize = 0;
        int ByteOffset = 0;
        // consider using polymorphism to control entry types?? (EntryNumber: Entry ...)
        Panel EntryPanel = new Panel();
        //Constructor
        public Entry(String EntryType, int x, int y, int ByteSize, int ByteOffset)
        {
            this.EntryType = EntryType;
            this.position = new int[2];
            this.position[0] = x;
            this.position[1] = y;
            this.ByteSize = ByteSize;
            this.ByteOffset = ByteOffset;
        }
        //Methods
    }
}
