namespace PitFortress.Classes
{
    using Interfaces;

    public class Minion : IMinion
    {
        public Minion(int xCoordinate, int id)
        {
            this.XCoordinate = xCoordinate;
            this.Id = id;
            this.Health = 100;
        }

        public int Id { get; private set; }

        public int XCoordinate { get; set; }

        public int Health { get; set; }

        public int CompareTo(Minion other)
        {
            var compareTo = this.XCoordinate.CompareTo(other.XCoordinate);
            if (compareTo == 0)
            {
                compareTo = this.Id.CompareTo(other.Id);
            }

            return compareTo;
        }
    }
}
