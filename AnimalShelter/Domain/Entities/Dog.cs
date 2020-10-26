using AnimalShelter.Domain.Interfaces;

namespace AnimalShelter.Domain.Entities
{
    [System.Runtime.Serialization.DataContract(Name = "Dog")]
    public class Dog : Animal, IAnimal
    {
        public string Breed { get; set; }
        private bool _isAlive;

        public Dog(string name, int weight, string breed)
        {
            Name = name;
            Weight = weight;
            Breed = breed;

            _isAlive = true;
        }

        public override sealed string Speak()
        {
            return "Wuf wuf";
        }

        public void FeedATreat()
        {
            Weight += 2;
        }

        public bool FeedASpoiledFish()
        {
            _isAlive = false;
            return _isAlive;
        }
    }
}
