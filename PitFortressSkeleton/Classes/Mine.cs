namespace PitFortress.Classes
{
    using Interfaces;

    public class Mine : IMine
    {
        public Mine(int id, Player player, int xCoordinate, int delay, int damage)
        {
            this.Player = player;
            this.XCoordinate = xCoordinate;
            this.Delay = delay;
            this.Damage = damage;
            this.Id = id;
        }

        public int Id { get; private set; }

        public Player Player { get; private set; }

        public int XCoordinate { get; set; }

        public int Delay { get; set; }

        public int Damage { get; set; }

        public int CompareTo(Mine other)
        {
            var compareTo = this.Delay.CompareTo(other.Delay);
            if (compareTo == 0)
            {
                compareTo = this.Id.CompareTo(other.Id);
            }

            return compareTo;
        }
    }
}
