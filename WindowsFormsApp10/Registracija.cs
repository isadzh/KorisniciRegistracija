using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp10
{
    public partial class Registracija : Form
    {
        private Korisnik korisnik;
        public bool Edit { get; set; }

        public Registracija()
        {
            InitializeComponent();
            korisnik = new Korisnik();
        }

        public Registracija(Korisnik korisnik) : this()
        {
            this.korisnik = korisnik;
            UcitajPodatkeOKorisniku();
            Edit = true;
        }

        private void UcitajPodatkeOKorisniku()
        {
            try
            {
                txtIme.Text = korisnik.Ime;
                txtPrezime.Text = korisnik.Prezime;
                txtKorisnickoIme.Text = korisnik.KorisnickoIme;
                txtLozinka.Text = korisnik.Lozinka;
                pbSlikaKorisnika.Image = ImageHelper.FromByteToImage(korisnik.Slika);
                cbAktivan.Checked = korisnik.Aktivan;
            }
            catch ( Exception ex)
            {

                MessageBox.Show(ex.Message+" "+ex.InnerException?.Message);
            }

        }

        private void Registracija_Load(object sender, EventArgs e)
        {
            txtLozinka.Text = GetLozinka(8);
        }

        private string GetLozinka(int brojZnakova)
        {
            string novaLozinka = string.Empty;
            string dozvoljeniZnakovi = "12abc34def56ghijk7lmn8oprst9uvz";
            Random r = new Random();
            for (int i = 0; i < brojZnakova; i++)
            {
                novaLozinka += dozvoljeniZnakovi[r.Next(0, dozvoljeniZnakovi.Length)];
            }
            return novaLozinka;
        }

        private void pbSecureIcon_Click(object sender, EventArgs e)
        {
            char prazan = new char();
            if (txtLozinka.PasswordChar == prazan)
                txtLozinka.PasswordChar = '*';
            else
                txtLozinka.PasswordChar = prazan;
        }

        private void txtIme_TextChanged(object sender, EventArgs e)
        {
            txtKorisnickoIme.Text = $"{txtIme.Text.ToLower()}.{txtPrezime.Text.ToLower()}";
        }

        private void txtPrezime_TextChanged(object sender, EventArgs e)
        {
            txtKorisnickoIme.Text = $"{txtIme.Text.ToLower()}.{txtPrezime.Text.ToLower()}";
        }

        private void btnDodajSliku_Click(object sender, EventArgs e)
        {
            try
            {
                if (ofdUcitajSliku.ShowDialog() == DialogResult.OK)
                {
                    string putanjaDoSlike = ofdUcitajSliku.FileName;
                    Image slika = Image.FromFile(putanjaDoSlike);
                    pbSlikaKorisnika.Image = slika;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Greska ->{ex.Message}");
            }
        }

        private void btnSpasi_Click(object sender, EventArgs e)
        {
            if (ValidirajUnos())
            {
                korisnik.Ime = txtIme.Text;
                korisnik.Prezime = txtPrezime.Text;
                korisnik.KorisnickoIme = txtKorisnickoIme.Text;
                korisnik.Lozinka = txtLozinka.Text;
                korisnik.Slika = ImageHelper.FromImageToByte(pbSlikaKorisnika.Image);
                korisnik.Aktivan = cbAktivan.Checked;
                if (!Edit)
                {
                    Baza.DB.Korisnici.Add(korisnik);
                    Baza.DB.SaveChanges();
                    MessageBox.Show("Registracija je uspjesna");
                }
                else
                {
                    Baza.DB.Entry(korisnik).State = EntityState.Modified;
                    Baza.DB.SaveChanges();
                    MessageBox.Show("Editovanje je uspjesno");
                }

                DialogResult = DialogResult.OK;
                Close();
            }
           
        }

        private bool ValidirajUnos()
        {
            if (pbSlikaKorisnika.Image == null)
            {
                err.SetError(pbSlikaKorisnika, Validator.porukaObaveznaVrijednost);
                return false;
            }
            else
                err.Clear();
            return Validator.ObaveznoPolje(txtIme, err, Validator.porukaObaveznaVrijednost) &&
                Validator.ObaveznoPolje(txtPrezime, err, Validator.porukaObaveznaVrijednost);
        }
    }
}
