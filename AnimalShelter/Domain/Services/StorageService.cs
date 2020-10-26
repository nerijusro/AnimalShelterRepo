using AnimalShelter.Domain.Interfaces;
using System.Collections.Generic;

namespace AnimalShelter.Domain.Services
{
    public class StorageService : IStorageService
    {
        private Dictionary<string, IAnimal> _animalRegistry;
        private IIdGenerator _idGenerator;

        public StorageService(IIdGenerator idGenerator)
        {
            _animalRegistry = new Dictionary<string, IAnimal>();
            _idGenerator = idGenerator;
        }

        public void PutForAdoption(IAnimal animal)
        {
            _animalRegistry.Add(animal.GetType().Name + "_" + _idGenerator.GetId(), animal);
        }

        public Dictionary<string, IAnimal> GetAnimalList()
        {
            return _animalRegistry;
        }

        public IAnimal GetAnimalDetails(string id)
        {
            IAnimal animal;
            _animalRegistry.TryGetValue(id, out animal);

            if (animal != null)
            {
                return animal;
            }

            return null;
        }

        public IAnimal Remove(string id)
        {
            IAnimal animal;
            _animalRegistry.TryGetValue(id, out animal);

            _animalRegistry.Remove(id);
            return animal;
        }

        public void UpdateAnimal(string id, IAnimal animal)
        {
            _animalRegistry[id] = animal;
        }
    }
}
