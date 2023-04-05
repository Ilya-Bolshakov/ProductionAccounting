using ProductionAccounting.DAL.Entities;
using ProductionAccounting.Models.Base;

namespace ProductionAccounting.Models
{
    public class OperationModel : Model
    {
        private CoefficientModel _coefficient;
        public CoefficientModel Coefficient
        {
            get { return _coefficient; }
            set { _coefficient = value; OnPropertyChanged(nameof(Coefficient)); }
        }

        private decimal _cost;
        public decimal Cost
        {
            get { return _cost; }
            set 
            {
                _cost = value; OnPropertyChanged(nameof(Cost)); 
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
            return $"{Name}. Стоимость: {string.Format("{0:F2}", Cost)}. Длительность: {OperationDuration}. Коэф: {string.Format("{0:F2}", Coefficient.CoefficientValue)}";
        }
    }
}
