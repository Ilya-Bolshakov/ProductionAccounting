using System.Collections.Generic;

namespace ProductionAccounting.Models
{
    public class TabelModel
    {
        public List<OperationModel> Operations { get; set; }
        public string Name { get; set; }

        public TabelModel(ProductModel product)
        {
            Name = product.Name;
            Operations = new List<OperationModel>(product.Operations);
        }
    }
}
