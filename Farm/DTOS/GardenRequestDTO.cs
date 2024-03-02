using Farm.Modelss;

namespace Farm.DTOS
{
    public class GardenRequestDTO
    {
        public int? GardenId { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public decimal? Area { get; set; }

        public List<string> ValidateInput(bool IsUpdate)
        {
            var errors = new List<string>();
            if (IsUpdate && GardenId == null)
                errors.Add("GardenId is required!");
            else if (!IsUpdate)
                GardenId = null;

            if (String.IsNullOrEmpty(Name))
                errors.Add("Title is required!");
            else if (Name.Length > 250)
                errors.Add("Length of Title must be <= 250");

            if (String.IsNullOrEmpty(Location))
                errors.Add("Location is required!");
            else if (Location.Length > 250)
                errors.Add("Length of Type must be <= 250");

            if (Area == null)
                errors.Add("Area is required!");
            return errors;
        }
    }
}
