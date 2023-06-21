using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Class1
{
    internal class Nomenklatura : ObservableCollection<Номенклатура>
    {
        public Nomenklatura()
        {
            var queryKlient = from q in tkmEntities7.GetContext().Номенклатура
                              select q;
            foreach (Номенклатура q in queryKlient)
            {
                this.Add(q);
            }
        }

    }
}
