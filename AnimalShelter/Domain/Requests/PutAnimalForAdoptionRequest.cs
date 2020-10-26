namespace AnimalShelter.Domain.Requests
{
    public class PutAnimalForAdoptionRequest
    {
        public string Name { get; set; }
        public int Weight { get; set; }
        public string Breed { get; set; }
    }
}
