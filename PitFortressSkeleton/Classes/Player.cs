namespace PitFortress.Classes
{
    using System;
    using Helpers;
    using Interfaces;

    public class Player : IPlayer
    {
        private int radius;

        public Player(string name, int mineRadius)
        {
            this.Name = name;
            this.Radius = mineRadius;
        }

        public string Name { get; private set; }

        public int Score { get; set; }

        public int Radius
        {
            get
            {
                return this.radius;
            }

            private set
            {
                Validators.ValidateMinValue(value, 0);
                this.radius = value;
            }
        }

        public int CompareTo(Player other)
        {
            var compareTo = -this.Score.CompareTo(other.Score);
            if (compareTo == 0)
            {
                compareTo = string.Compare(other.Name, this.Name, StringComparison.Ordinal);
            }

            return compareTo;
        }
    }
}
