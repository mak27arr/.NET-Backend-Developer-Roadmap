namespace Database.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Barcode { get; set; }
        public decimal Rate { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public virtual Category? Category { get; set; }
        public int CategoryInfoKey { get; set; }
        public string? CategoryName { get; set; }
        public ProductParamtr Parameter { get; set; }
        private SecretParamtr SecretParameter { get; set; }

    }
}
