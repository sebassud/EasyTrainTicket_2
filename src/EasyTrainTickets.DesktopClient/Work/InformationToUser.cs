using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EasyTrainTickets.DesktopClient.Work
{
    public class InformationToUser
    {
        public string Message { get; set; }
        public Brush Color { get; set; }

        public static string ServerError
        {
            get
            {
                return "Przepraszamy ale serwer jest czasowo niedostępny. W tej chwili pracujemy nad tym.";
            }
        }
    }
}
