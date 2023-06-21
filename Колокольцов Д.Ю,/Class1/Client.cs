using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Class1
{
    internal class Client : ObservableCollection<Клиент>
    {
        public Client()
        {
            var queryKlient = from q in tkmEntities7.GetContext().Клиент
                              select q;
            foreach (Клиент q in queryKlient)
            {
                this.Add(q);
            }
        }

    }
    
}
