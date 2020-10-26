namespace AnimalShelter.Domain.Interfaces
{
    public interface IAnimal
    {
        string Name { get; set; }
        int Weight { get; set; }
        string Breed { get; set; }

        string Speak();
        void FeedATreat();
        bool FeedASpoiledFish();
    }
}
