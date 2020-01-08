using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp10
{
    public class Baza
    {
        public static RegistracijaContext DB { get; } = new RegistracijaContext();
    }
}
