using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp10
{
    public class RegistracijaContext : DbContext
    {
        public RegistracijaContext() :base("PutanjaDoBaze")
        {

        }
        public DbSet<Korisnik> Korisnici { get; set; }
    }
}
