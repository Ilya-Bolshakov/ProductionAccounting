using ProductionAccounting.DAL.Entities;
using ProductionAccounting.Models.Base;

namespace ProductionAccounting.Models
{
    public class OperationModel : Model
    {
        public CoefficientModel Coefficient { get; set; }
        public decimal Cost { get; set; }
        public int OperationDuration { get; set; }
        public decimal Price { get; private set; }
        public string Name { get; set; }

        public OperationModel()
        { }

        public OperationModel(Operation orm)
        {
            Coefficient = new CoefficientModel(orm.OperationСoefficient);
            Cost = orm.Cost;
            OperationDuration = orm.OperationDuration;
            Id = orm.Id;
            Price = Cost * OperationDuration * Coefficient.CoefficientValue;
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
    }
}
