namespace AnimalShelter.Domain.Entities
{
    public class Animal
    {
        public string Name { get; set; }
        public int Weight { get; set; }

        /// <summary>
        /// Animal.Speak made virtual, since implementation might be different for various animals.
        /// </summary>
        /// <returns></returns>
        public virtual string Speak()
        {
            return "I'm an animal, but I can speak English";
        }

        public void Feed()
        {
            Weight += 1;
        }
    }
}
