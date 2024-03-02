namespace Farm.DTOS
{
    public class ProduceRequestDTO
    {
        public int? ProduceId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Quantity { get; set; }
        public int? Status { get; set; }
        public DateTime? PlantingDate { get; set; }
        public DateTime? ExpectedHarvestDate { get; set; }
        public DateTime? ActualHarvestDate { get; set; }
        public int? GardenId { get; set; }

        public List<string> ValidateInput(bool IsUpdate)
        {
            var errors = new List<string>();
            if (IsUpdate && ProduceId == null)
                errors.Add("Produce Id is required!");
            else if (!IsUpdate)
                ProduceId = null;

            if (String.IsNullOrEmpty(Name))
                errors.Add("Title is required!");
            else if (Name.Length > 250)
                errors.Add("Length of Title must be <= 250");

            if (String.IsNullOrEmpty(Description))
                errors.Add("Type is required!");
            else if (Description.Length > 250)
                errors.Add("Length of Type must be <= 250");          

            if (PlantingDate == null)
                errors.Add("PusblishedDate is required!");

            if (Quantity == null)
                errors.Add("Quantity is required!");

            if (Status == null)
                errors.Add("Status is required!");

            if (ExpectedHarvestDate == null)
                errors.Add("ExpectedHarvestDate is required!");

            if (ActualHarvestDate == null)
                errors.Add("ActualHarvestDate is required!");

            if (GardenId == null)
                errors.Add("GardenId is required!");

            return errors;
        }
    }
}
