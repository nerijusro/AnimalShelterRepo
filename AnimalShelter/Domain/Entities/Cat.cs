using AnimalShelter.Domain.Interfaces;

namespace AnimalShelter.Domain.Entities
{
    public class Cat : Animal, IAnimal
    {
        private bool _isAlive;
        public string Breed { get; set; }
        public int LivesRemaining { get; set; }

        public Cat(string name, int weight, string breed)
        {
            Name = name;
            Weight = weight;
            Breed = breed;
            LivesRemaining = 9;

            _isAlive = true;
        }

        public override string Speak()
        {
            return "Meow";
        }

        public void FeedATreat()
        {
            Weight += 1;
        }

        public bool FeedASpoiledFish()
        {
            LivesRemaining -= 1;

            if (LivesRemaining == 0)
            {
                _isAlive = false;
            }

            return _isAlive;
        }
    }
}
