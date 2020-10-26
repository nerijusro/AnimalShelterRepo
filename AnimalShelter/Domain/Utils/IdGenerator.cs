using AnimalShelter.Domain.Interfaces;

namespace AnimalShelter.Domain.Utils
{
    public class IdGenerator : IIdGenerator
    {
        private int _idCount;

        public IdGenerator()
        {
            _idCount = 0;
        }

        public string GetId()
        {
            _idCount += 1;
            return _idCount.ToString();
        }
    }
}
