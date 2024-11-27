using MonsterHunterDLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//revision history
////// Mahan Poor Hamidian     2024/11/27       Created MenuForm 
///   
namespace MonsterHunterForms
{
    public partial class MenuForm : Form
    {
        Hunter hunter = new Hunter();
        public static Map map = new Map();
        public static List<string> mapFiles = map.mapNames; //store the mapnames in mapfiles
        public const string MAP_INDEX_ERROR = "Please Choose a map";


        public MenuForm()
        {
            InitializeComponent();
           
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;
            foreach(var eachFile in mapFiles) //it will loop through and assign each map name to a line in combobox 
            {
                cmbMapNames.Items.Add(eachFile);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            hunter.Name = txtName.Text;
            if (hunter.ValidationError  != "" )
            {
                lblError.Visible = true;

                lblError.Text = hunter.ValidationError;
                return;
            }
            if (cmbMapNames.SelectedIndex == -1)
            {
                lblError.Visible = true;
                lblError.Text = MAP_INDEX_ERROR;
                return;

            }
            string playerName = txtName.Text;
            string mapName = cmbMapNames.SelectedItem?.ToString();
            GameForm modalGameForm = new GameForm(playerName, mapName);

            modalGameForm.ShowDialog();

        }
        
    }
}
