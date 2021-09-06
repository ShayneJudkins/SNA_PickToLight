using PickToLightData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PickToLightBanderDisplay_TestHarness
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            //ScanOWK scanOWK;
            //using (var ctx = new PickToLightEntities())
            //{
            //    scanOWK = (from s in ctx.ScanOWKs
            //               where s.ID == idScanOWK
            //               select s).Single<ScanOWK>();
            //}
            //return scanOWK;
        }

        private void comboBander_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateComboOWK();
        }

        private void populateComboOWK()
        {
            using (var ctx = new PickToLightEntities())
            {
                string whichBander = comboBander.SelectedText;
                var scanOWKs = (from s in ctx.ScanOWKs
                                where s.ScannedBy == whichBander
                                orderby s.ID descending
                                select s).Take(100).ToList();
                if (scanOWKs != null)
                {
                    //comboPickStartingOWK.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.scanOWKBindingSource, "ID", true));
                    //comboPickStartingOWK.DataSource = this.scanOWKBindingSource;
                    comboPickStartingOWK.DataSource = scanOWKs;
                    comboPickStartingOWK.DisplayMember = "ID";
                    comboPickStartingOWK.ValueMember = "ID";
                }
            }
        }
    }
}
