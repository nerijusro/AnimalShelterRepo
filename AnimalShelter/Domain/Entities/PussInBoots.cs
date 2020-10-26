namespace AnimalShelter.Domain.Entities
{
    public class PussInBoots : Cat
    {
        public PussInBoots(int weight) : base("PussInBoots", weight, "Whatever his breed is")
        {
        }
        
        /// <summary>
        /// Animal.Speak sealed, since there is no way someone else could put words in Puss In Boots's mouth.
        /// </summary>
        /// <returns></returns>
        public sealed override string Speak() 
        {
            return "Meow, but in Spanish";
        }
    }
}
