using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Class1
{
    internal class Master : ObservableCollection<Мастер>
    {
        public Master()
        {
            var queryKlient = from q in tkmEntities7.GetContext().Мастер
                              select q;
            foreach (Мастер q in queryKlient)
            {
                this.Add(q);
            }
        }

    }
}
