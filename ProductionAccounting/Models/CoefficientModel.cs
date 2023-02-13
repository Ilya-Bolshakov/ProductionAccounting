using ProductionAccounting.DAL.Entities;
using ProductionAccounting.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionAccounting.Models
{
    public class CoefficientModel : Model
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        public decimal _coefficientValue;
        public decimal CoefficientValue
        {
            get { return _coefficientValue; }
            set { _coefficientValue = value; OnPropertyChanged(nameof(CoefficientValue)); }
        }


        public CoefficientModel()
        { }

        public CoefficientModel(OperationСoefficient orm)
        {
            Name = orm.Name;
            Id = orm.Id;
            CoefficientValue = orm.CoefficientValue;
        }

        public OperationСoefficient MapToOrm()
        {
            OperationСoefficient c = new OperationСoefficient();
            c.Id = Id;
            c.Name = Name;
            c.CoefficientValue = CoefficientValue;
            return c;
        }

        public override object Clone()
        {
            CoefficientModel model = new CoefficientModel();
            model.Name = Name;
            model.CoefficientValue = CoefficientValue;
            model.Id = Id;
            return model;
        }

        public override string ToString()
        {
            return $"{Name}, Значение: {CoefficientValue}";
        }
    }
}
