using AnimalShelter.Domain.Interfaces;
using System;

namespace AnimalShelter.Domain.Utils
{
    public class OperationIdGenerator : IOperationId
    {
        private string _operationId;
        public OperationIdGenerator()
        {
            Random random = new Random();

            _operationId = random.Next(0, 9999).ToString("D4");
        }

        public string GetOperationId()
        {
            return _operationId;
        }
    }
}
