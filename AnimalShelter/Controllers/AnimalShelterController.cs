using System;
using System.Collections.Generic;
using System.Net;
using AnimalShelter.Domain;
using AnimalShelter.Domain.Entities;
using AnimalShelter.Domain.Interfaces;
using AnimalShelter.Domain.Requests;
using Microsoft.AspNetCore.Mvc;

namespace AnimalShelter.Controllers
{
    [ApiController]
    [Route("animalShelter/")]
    public class AnimalShelterController : ControllerBase
    {
        private IStorageService _animalStorage;
        private IOperationId _operationId;

        public AnimalShelterController(IStorageService animalStorage, IOperationId operationId)
        {
            _animalStorage = animalStorage;
            _operationId = operationId;
        }

        [Route("putDogForAdoption")]
        [HttpPost]
        public void PutDogForAdoption([FromBody] PutAnimalForAdoptionRequest request)
        {
            Console.WriteLine(_operationId.GetOperationId());

            var dog = new Dog(request.Name, request.Weight, request.Breed);
            _animalStorage.PutForAdoption(dog);
        }

        [Route("putCatForAdoption")]
        [HttpPost]
        public void PutCatForAdoption([FromBody] PutAnimalForAdoptionRequest request)
        {
            Console.WriteLine(_operationId.GetOperationId());

            var cat = new Cat(request.Name, request.Weight, request.Breed);
            _animalStorage.PutForAdoption(cat);
        }

        [Route("putPussInBootsForAdoption")]
        [HttpPost]
        public System.Web.Mvc.HttpStatusCodeResult PutPussInBootsForAdoption()
        {
            Console.WriteLine(_operationId.GetOperationId());

            return new System.Web.Mvc.HttpStatusCodeResult(HttpStatusCode.BadRequest, "Puss In Boots is an independant cat and can not be put for adoption");
        }

        [Route("getAnimalList")]
        [HttpGet]
        public Dictionary<string, IAnimal> GetAnimals()
        {
            Console.WriteLine(_operationId.GetOperationId());

            return _animalStorage.GetAnimalList();
        }


        [Route("getCatDetails")]
        [HttpGet]
        public Cat GetCatDetails([FromBody] BaseAnimalRequest request)
        {
            Console.WriteLine(_operationId.GetOperationId());

            var cat = _animalStorage.GetAnimalDetails(request.AnimalId) as Cat;
            return cat;
        }

        [Route("getDogDetails")]
        [HttpGet]
        public Dog GetDogDetails([FromBody] BaseAnimalRequest request)
        {
            Console.WriteLine(_operationId.GetOperationId());

            var dog = _animalStorage.GetAnimalDetails(request.AnimalId) as Dog;
            return dog;
        }

        /// <summary>
        /// Idempotent operation.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("adopt")]
        [HttpPost]
        public System.Web.Mvc.HttpStatusCodeResult Adopt([FromBody] BaseAnimalRequest request)
        {
            Console.WriteLine(_operationId.GetOperationId());

            var animal = _animalStorage.Remove(request.AnimalId);

            if (animal == null)
            {
                return new System.Web.Mvc.HttpStatusCodeResult(HttpStatusCode.NoContent, "Animal with such ID does not stay in the shelter");
            }

            return new System.Web.Mvc.HttpStatusCodeResult(HttpStatusCode.OK, "Congratulations! You have just adopted " + animal.Name);
        }

        [Route("talk")]
        [HttpPost]
        public string TalkWithAnAnimal([FromBody] BaseAnimalRequest request)
        {
            Console.WriteLine(_operationId.GetOperationId());

            var animal = _animalStorage.GetAnimalDetails(request.AnimalId);
            return animal?.Speak();
        }

        [Route("feedATreat")]
        [HttpPost]
        public System.Web.Mvc.HttpStatusCodeResult FeedATreat([FromBody] BaseAnimalRequest request)
        {
            Console.WriteLine(_operationId.GetOperationId());

            var animal = _animalStorage.GetAnimalDetails(request.AnimalId);

            if (animal == null)
            {
                return new System.Web.Mvc.HttpStatusCodeResult(HttpStatusCode.NoContent, "Animal with such ID does not stay in the shelter");
            }

            //Animal should not be adopted or talked with while he's eating.
            //Lock block added to avoid race condition.
            lock (animal)
            {
                animal.FeedATreat();

                _animalStorage.UpdateAnimal(request.AnimalId, animal);
            }

            return new System.Web.Mvc.HttpStatusCodeResult(HttpStatusCode.OK, "You have just fed " + animal.Name + " a treat! Now his weight is " + animal.Weight);
        }

        [Route("feedSpoiledFish")]
        [HttpPost]
        public System.Web.Mvc.HttpStatusCodeResult FeedASpoiledFish([FromBody] BaseAnimalRequest request)
        {
            Console.WriteLine(_operationId.GetOperationId());

            var animal = _animalStorage.GetAnimalDetails(request.AnimalId);

            if (animal == null)
            {
                return new System.Web.Mvc.HttpStatusCodeResult(HttpStatusCode.NoContent, "Animal with such ID does not stay in the shelter");
            }

            var isAlive = animal.FeedASpoiledFish();

            if (!isAlive)
            {
                _animalStorage.Remove(request.AnimalId);
            }

            return new System.Web.Mvc.HttpStatusCodeResult(HttpStatusCode.OK, "You have just fed " + animal.Name + " a spoiled fish! " + (isAlive ? "But he is alive." : "Now he is dead"));
        }
    }
}
