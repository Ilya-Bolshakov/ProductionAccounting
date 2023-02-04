using ProductionAccounting.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionAccounting.DAL.Entities
{
    public class Product : NamedEntity
    {
        public virtual ICollection<Operation> Operations { get; set; }
        // количество операций для табеля 

        //инфа для табеля. 
        // название товара. 1 столбец название операций. 2 столбец стоимость операции УЖЕ ИТОГОВАя из таблицы операции. 3 длинная стррка для производителей. 4 для не табеля. итого количество операций.
    }
}
