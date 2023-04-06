using ProductionAccounting.DAL.Entities;
using ProductionAccounting.Models.Base;
using System;

namespace ProductionAccounting.Models
{
    public class OperationModel : Model
    {
        private CoefficientModel _coefficient;
        public CoefficientModel Coefficient
        {
            get { return _coefficient; }
            set { _coefficient = value; OnPropertyChanged(nameof(Coefficient));
                OnPropertyChanged(nameof(Price));
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            var o = obj as OperationModel;
            return Name == o.Name 
                && Cost == o.Cost
                && Coefficient.Equals(o.Coefficient)
                && OperationDuration == o.OperationDuration;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Coefficient, this.Cost, Name, OperationDuration);
        }

        private decimal _cost;
        public decimal Cost
        {
            get { return _cost; }
            set 
            {
                _cost = value; OnPropertyChanged(nameof(Cost)); 
                OnPropertyChanged(nameof(Price));
            }
        }

        private int _operationDuration;
        public int OperationDuration
        {
            get { return _operationDuration; }
            set { _operationDuration = value; OnPropertyChanged(nameof(OperationDuration)); OnPropertyChanged(nameof(Price)); }
        }


        private decimal _price;
        public decimal Price
        {
            get
            {   
                _price = Cost* OperationDuration *Coefficient.CoefficientValue;
                return _price;
            }
            set
            {
                _price = value; OnPropertyChanged(nameof(Price));  
            }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        public OperationModel()
        { }

        public OperationModel(Operation orm)
        {
            Coefficient = new CoefficientModel(orm.OperationСoefficient);
            Cost = orm.Cost;
            OperationDuration = orm.OperationDuration;
            Id = orm.Id;
            Name = orm.Name;
        }

        public Operation MapToOrm()
        {
            return new Operation()
            {
                Id = Id,
                OperationDuration = OperationDuration,
                Name = Name,
                Cost = Cost,
                OperationСoefficient = Coefficient.MapToOrm()
            };
        }

        public Operation MapToOrm(Operation operation)
        {
            operation.Name = Name;
            operation.Cost = Cost;
            operation.OperationDuration = OperationDuration;
            //operation.OperationСoefficient = Coefficient.MapToOrm();
            return operation;
        }

        public override object Clone()
        {
            var model = new OperationModel();
            model.Id = Id;
            model.Cost = Cost;
            model.OperationDuration = OperationDuration;
            model.Name = Name;
            model.Coefficient = (CoefficientModel)Coefficient.Clone();
            return model;
        }

        public override string ToString()
        {
            //return $"{Name}. Стоимость: {string.Format("{0:F2}", Cost)}. Длительность: {OperationDuration}. Коэф: {string.Format("{0:F2}", Coefficient.CoefficientValue)}";
            return $"{Name}. Коэф: {string.Format("{0:F2}", Coefficient.CoefficientValue)}";

        }
    }
}
