using ProductionAccounting.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionAccounting.Models
{
    public class EmployeeModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }


        public EmployeeModel MapToDto(Employee orm)
        {
            return new EmployeeModel()
            {
                Name = orm.Name,
                Surname = orm.Surname,
                Patronymic = orm.Patronymic,
            };
        }
    }
}
