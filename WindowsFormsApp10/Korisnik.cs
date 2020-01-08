using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp10
{
    [Table("Korisnik")]
    public class Korisnik
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public byte[] Slika { get; set; }
        public bool Aktivan { get; set; }

        public override string ToString()
        {
            return $"{Ime} {Prezime} ({KorisnickoIme})";
        }
    }
}
