using System;
using System.Windows.Forms;

namespace FORMClubRota
{
    public partial class Form1 : Form
    {
        private Socio[] eleS = new Socio[1000];
        private int num = 0;

        public Form1()
        {
            InitializeComponent();

            num = 0;
            eleS[0].tessera = "AA111111";
            eleS[0].nome = "Marco";
            eleS[0].cognome = "Rossi";
            eleS[0].luogo = "Milano";
            eleS[0].indirizzo = "Via B. 116";
            eleS[0].nascita = DateTime.Parse("16/6/2001");
            eleS[0].iscrizione = DateTime.Parse("1/1/2000");
            eleS[0].ver = DateTime.Parse("20/12/2020");
            eleS[0].quota = 1000;
            num++;
            eleS[1].tessera = "BB222222";
            eleS[1].nome = "Stephan";
            eleS[1].cognome = "Verdi";
            eleS[1].luogo = "Roma";
            eleS[1].indirizzo = "Via G. 34";
            eleS[1].nascita = DateTime.Parse("19/10/1998");
            eleS[1].iscrizione = DateTime.Parse("1/1/2021");
            eleS[1].ver = DateTime.Parse("4/9/2019");
            eleS[1].quota = 100;
            num++;
            eleS[2].tessera = "CC333333";
            eleS[2].nome = "Nicole";
            eleS[2].cognome = "Pozzi";
            eleS[2].luogo = "Torino";
            eleS[2].indirizzo = "Via L. 22";
            eleS[2].nascita = DateTime.Parse("7/5/1978");
            eleS[2].iscrizione = DateTime.Parse("8/7/2014");
            eleS[2].ver = DateTime.Parse("17/9/2020");
            eleS[2].quota = 10;
            num++;
        }

        private void btnInserisci_Click(object sender, EventArgs e)
        {
            if (num > 1000)
            {
                MessageBox.Show("Errore di elenco!", "Errore");
                return;
            }

            if (string.IsNullOrEmpty(txtNome.Text) || (string.IsNullOrEmpty(txtCognome.Text) || string.IsNullOrEmpty(txtIndirizzo.Text) || string.IsNullOrEmpty(txtLuogo.Text) || string.IsNullOrEmpty(txtQuota.Text)))
            {
                MessageBox.Show("Inserire i dati mancanti!", "Errore");
                return;
            }

            Socio nuovoS = default(Socio);

            if (!(txtTessera.Text.Length == 8))
            {
                MessageBox.Show("Il Numero di Tessera deve essere composto da 8 caratteri!");
                return;
            }

            DateTime today = DateTime.Today;

            if ((today.Year - dtpNascita.Value.Year) < 18)
            {
                MessageBox.Show("Non è possibile iscriversi avendo meno di 18 anni!", "Errore");
                return;
            }

            nuovoS.tessera = txtTessera.Text;
            nuovoS.nome = txtNome.Text;
            nuovoS.cognome = txtCognome.Text;
            nuovoS.indirizzo = txtIndirizzo.Text;
            nuovoS.luogo = txtLuogo.Text;
            nuovoS.nascita = DateTime.Parse(dtpNascita.Text);
            nuovoS.iscrizione = DateTime.Parse(dtpRegistrazione.Text);
            nuovoS.ver = DateTime.Parse(dtpUltimoVer.Text);
            nuovoS.quota = decimal.Parse(txtQuota.Text);

            eleS[num] = nuovoS;
            num++;

            txtTessera.Clear();
            txtNome.Clear();
            txtCognome.Clear();
            txtIndirizzo.Clear();
            txtLuogo.Clear();
            txtQuota.Clear();
            dtpNascita.CustomFormat = "1/1/2000";
            dtpRegistrazione.CustomFormat = "1/1/2000";
            dtpUltimoVer.CustomFormat = "1/1/2000";
        }

        private void btnVisualizza_Click(object sender, EventArgs e)
        {
            int x = 0;

            ListViewItem riga = default(ListViewItem);
            listView1.Items.Clear();

            while (x < num)
            {
                riga = new ListViewItem(new string[]
                   {  eleS[x].tessera,
                   eleS[x].nome,
                   eleS[x].cognome,
                   eleS[x].indirizzo,
                   eleS[x].luogo,
                   eleS[x].nascita.ToString(),
                   eleS[x].iscrizione.ToString(),
                    eleS[x].ver.ToString(),
                   eleS[x].quota.ToString()});
                listView1.Items.Add(riga);
                x = x + 1; ;
            }
        }

        private void btnCancella_Click(object sender, EventArgs e)
        {
            bool found = default;
            string tesseraCanc = "";
            tesseraCanc = txtCancella.Text;

            if (string.IsNullOrEmpty(txtCancella.ToString()))
            {
                MessageBox.Show("Inserire un Numero di Tessera!", "Errore");
                return;
            }

            if (num == 0)
            {
                MessageBox.Show("Non ci sono elementi da cancellare!", "Errore");
                return;
            }

            if (num > 1000)
            {
                MessageBox.Show("Errore di elenco!", "Errore");
                return;
            }

            found = Class1.CancellaID(eleS, ref num, tesseraCanc);

            if (found)
            {
                MessageBox.Show("Elemento cancellato!");
                txtCancella.Clear();
                return;
            }
            else
            {
                MessageBox.Show("Elemento non cancellato!");
                txtCancella.Clear();
                return;
            }
        }

        private void btnVisualizzaSorted_Click(object sender, EventArgs e)
        {
            Class1.SelectionSortAlfabetico(eleS, num); // Ordine Alfabetico

            decimal media = default;
            media = Class1.MediaQuote(eleS, num); // Media

            int x = 0;

            DateTime oggi = DateTime.Today;

            ListViewItem riga2 = default(ListViewItem);
            listView2.Items.Clear();

            while (x < num)
            {
                if (eleS[x].quota < media && (oggi.Year - eleS[x].iscrizione.Year) > 1)
                {
                    riga2 = new ListViewItem(new string[]
                           {  eleS[x].tessera,
                   eleS[x].nome,
                   eleS[x].cognome,
                   eleS[x].indirizzo,
                   eleS[x].luogo,
                   eleS[x].nascita.ToString(),
                   eleS[x].iscrizione.ToString(),
                   eleS[x].ver.ToString(),
                   eleS[x].quota.ToString()});
                    listView2.Items.Add(riga2);
                }
                x = x + 1;
            }
        }

        private void btnModifica_Click(object sender, EventArgs e)
        {
            int k = 0;

            if (string.IsNullOrEmpty(txtModiTessera.Text) || string.IsNullOrEmpty(txtModiNome.Text) || string.IsNullOrEmpty(txtModiCogn.Text) || string.IsNullOrEmpty(txtModiIndirizzo.Text) || string.IsNullOrEmpty(txtModiLuogo.Text) || string.IsNullOrEmpty(dtpModiNascita.Text) || string.IsNullOrEmpty(dtpModiRegis.Text) || string.IsNullOrEmpty(dtpModiVer.Text) || string.IsNullOrEmpty(txtModiQuota.Text))
            {
                MessageBox.Show("Inserire i dati mancanti!", "Errore");
                return;
            }

            k = Class1.Cerca(eleS, num, txtCerca.Text);

            if (k == -1)
            {
                MessageBox.Show("Dato non trovato");
                return;
            }

            DateTime today2 = DateTime.Today;

            if ((today2.Year - dtpModiNascita.Value.Year) < 18)
            {
                MessageBox.Show("Non è possibile essere iscritti al Club essendo minorenni!", "Errore");
                return;
            }

            Socio modiS = default(Socio);

            modiS.tessera = txtModiTessera.Text;
            modiS.nome = txtModiNome.Text;
            modiS.cognome = txtModiCogn.Text;
            modiS.indirizzo = txtModiIndirizzo.Text;
            modiS.luogo = txtModiLuogo.Text;
            modiS.nascita = DateTime.Parse(dtpModiNascita.Text);
            modiS.iscrizione = DateTime.Parse(dtpModiRegis.Text);
            modiS.ver = DateTime.Parse(dtpModiVer.Text);
            modiS.quota = decimal.Parse(txtModiQuota.Text);

            eleS[k] = modiS;

            txtModiTessera.Clear();
            txtModiNome.Clear();
            txtModiCogn.Clear();
            txtModiIndirizzo.Clear();
            txtModiLuogo.Clear();
            txtModiQuota.Refresh();

            btnModifica.Enabled = true;
        }

        private void btnCerca_Click(object sender, EventArgs e)
        {
            int K = 0;

            if (string.IsNullOrEmpty(txtCerca.Text))
                return;

            K = Class1.Cerca(eleS, num, txtCerca.Text);

            if (K == -1)
            {
                MessageBox.Show("Dato non trovato");
                return;
            }

            txtModiTessera.Text = eleS[K].tessera;
            txtModiNome.Text = eleS[K].nome;
            txtModiCogn.Text = eleS[K].cognome;
            txtModiIndirizzo.Text = eleS[K].indirizzo;
            txtModiLuogo.Text = eleS[K].luogo;
            dtpModiNascita.Text = eleS[K].nascita.ToString();
            dtpModiRegis.Text = eleS[K].iscrizione.ToString();
            dtpModiVer.Text = eleS[K].ver.ToString();
            txtModiQuota.Text = eleS[K].quota.ToString();

            btnModifica.Enabled = true;
        }

        private void btnVisuCate_Click(System.Object sender, System.EventArgs e)
        {
            int x = 0;
            DateTime fondazione = DateTime.Parse("1/1/2000");

            ListViewItem riga3 = default(ListViewItem);
            listView3.Items.Clear();

            if (rdbtnSostenitori.Checked == true)
            {
                while (x < num)
                {
                    if (eleS[x].quota < 50)
                    {
                        riga3 = new ListViewItem(new string[]
                                  {  eleS[x].tessera,
                   eleS[x].nome,
                   eleS[x].cognome,
                   eleS[x].indirizzo,
                   eleS[x].luogo,
                   eleS[x].nascita.ToString(),
                   eleS[x].iscrizione.ToString(),
                   eleS[x].ver.ToString(),
                   eleS[x].quota.ToString()});
                        listView3.Items.Add(riga3);
                    }
                    x = x + 1;
                }
            }

            if (rdbtnOrdinari.Checked == true)
            {
                while (x < num)
                {
                    if (eleS[x].quota < 200)
                    {
                        riga3 = new ListViewItem(new string[]
                                  {  eleS[x].tessera,
                   eleS[x].nome,
                   eleS[x].cognome,
                   eleS[x].indirizzo,
                   eleS[x].luogo,
                   eleS[x].nascita.ToString(),
                   eleS[x].iscrizione.ToString(),
                   eleS[x].ver.ToString(),
                   eleS[x].quota.ToString()});
                        listView3.Items.Add(riga3);
                    }
                    x = x + 1;
                }
            }

            if (rdbtnBenemeriti.Checked == true)
            {
                while (x < num)
                {
                    if (eleS[x].quota > 200)
                    {
                        riga3 = new ListViewItem(new string[]
                                  {  eleS[x].tessera,
                   eleS[x].nome,
                   eleS[x].cognome,
                   eleS[x].indirizzo,
                   eleS[x].luogo,
                   eleS[x].nascita.ToString(),
                   eleS[x].iscrizione.ToString(),
                   eleS[x].ver.ToString(),
                   eleS[x].quota.ToString()});
                        listView3.Items.Add(riga3);
                    }
                    x = x + 1;
                }
            }

            if (rdbtnFondatori.Checked == true)
            {
                while (x < num)
                {
                    if (eleS[x].iscrizione == fondazione)
                    {
                        riga3 = new ListViewItem(new string[]
                                  {  eleS[x].tessera,
                   eleS[x].nome,
                   eleS[x].cognome,
                   eleS[x].indirizzo,
                   eleS[x].luogo,
                   eleS[x].nascita.ToString(),
                   eleS[x].iscrizione.ToString(),
                   eleS[x].ver.ToString(),
                   eleS[x].quota.ToString()});
                        listView3.Items.Add(riga3);
                    }
                    x = x + 1;
                }
            }
        }

        private void txtQuotaInserimento(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;
            if (!Char.IsDigit(chr) && chr != 0)
            {
                e.Handled = true;
                MessageBox.Show("Sono ammessi solo numeri in questo campo!", "Errore");
            }
        }

        private void txtInserimento(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
                DialogResult a = MessageBox.Show("Numeri non ammessi!", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}