using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp10
{
    public class Validator
    {
        public const string porukaObaveznaVrijednost = "Obavezna vrijednost";
        public static bool ObaveznoPolje(TextBox textBox,ErrorProvider err,string poruka)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                err.SetError(textBox, poruka);
                return false;
            }
            err.Clear();
            return true;
        }
    }
}
