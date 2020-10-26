using AnimalShelter.Domain.Entities;
using AnimalShelter.Domain.Interfaces;
using System.Collections.Generic;

namespace AnimalShelter.Domain
{
    public interface IStorageService
    {
        void PutForAdoption(IAnimal animal);
        Dictionary<string, IAnimal> GetAnimalList();
        IAnimal GetAnimalDetails(string id);
        IAnimal Remove(string id);
        void UpdateAnimal(string id, IAnimal animal);
    }
}
