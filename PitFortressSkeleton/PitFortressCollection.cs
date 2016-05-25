namespace PitFortress
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Classes;
    using Helpers;
    using Interfaces;
    using Wintellect.PowerCollections;

    public class PitFortressCollection : IPitFortress
    {
        private int minionId;
        private int mineId;

        private readonly OrderedBag<Minion> minions;
        private readonly OrderedMultiDictionary<int, Minion> minionsByRange; 
        private readonly Dictionary<string, Player> players;
        private readonly OrderedBag<Player> playerScoreboard;
        private readonly OrderedBag<Mine> mines; 

        public PitFortressCollection()
        {
            this.minions = new OrderedBag<Minion>();
            this.minionsByRange = new OrderedMultiDictionary<int, Minion>(true);
            this.players = new Dictionary<string, Player>();
            this.playerScoreboard = new OrderedBag<Player>();
            this.mines = new OrderedBag<Mine>();

            this.minionId = 1;
            this.mineId = 1;
        }

        public int PlayersCount => this.players.Count;

        public int MinionsCount => this.minions.Count;

        public int MinesCount => this.mines.Count;

        public void AddPlayer(string name, int mineRadius)
        {
            if (this.players.ContainsKey(name))
            {
                throw new ArgumentException();
            }

            var player = new Player(name, mineRadius);

            this.players.Add(name, player);
            this.playerScoreboard.Add(player);
        }

        public void AddMinion(int xCoordinate)
        {
            Validators.ValidateRange(xCoordinate, 0, 1000000);
            var minion = new Minion(xCoordinate, this.minionId);

            this.minions.Add(minion);
            this.minionsByRange[xCoordinate].Add(minion);
            this.minionId++;
        }

        public void SetMine(string playerName, int xCoordinate, int delay, int damage)
        {
            if (!this.players.ContainsKey(playerName))
            {
                throw new ArgumentException();
            }

            var player = this.players[playerName];
            Validators.ValidateRange(xCoordinate, 0, 1000000);
            Validators.ValidateRange(delay, 0, 10000);
            Validators.ValidateRange(damage, 0, 100);

            var mine = new Mine(this.mineId, player, xCoordinate, delay, damage);

            this.mines.Add(mine);
            this.mineId++;
        }

        public IEnumerable<Minion> ReportMinions()
        {
            return this.minions;
        }

        public IEnumerable<Player> Top3PlayersByScore()
        {
            if (this.PlayersCount < 3)
            {
                throw new ArgumentException();
            }

            return this.playerScoreboard.Take(3);
        }

        public IEnumerable<Player> Min3PlayersByScore()
        {
            if (this.PlayersCount < 3)
            {
                throw new ArgumentException();
            }

            return this.playerScoreboard.Reverse().Take(3);
        }

        public IEnumerable<Mine> GetMines()
        {
            return this.mines;
        }

        public void PlayTurn()
        {
            var minesToExplode = new List<Mine>();

            foreach (var mine in this.mines)
            {
                mine.Delay--;
                if (mine.Delay <= 0)
                {
                    minesToExplode.Add(mine);

                    var minionsInRangeOfMine =
                        this.minionsByRange.Range(
                            mine.XCoordinate - mine.Player.Radius,
                            true,
                            mine.XCoordinate + mine.Player.Radius,
                            true)
                            .SelectMany(pair => pair.Value);

                    foreach (var minion in minionsInRangeOfMine)
                    {
                        if (minion.Health > 0)
                        {
                            minion.Health -= mine.Damage;
                            if (minion.Health <= 0)
                            {
                                var killer = mine.Player;
                                this.playerScoreboard.Remove(killer);
                                killer.Score++;
                                this.playerScoreboard.Add(killer);

                                this.minions.Remove(minion);
                            }
                        }
                    }
                }
            }

            foreach (var mine in minesToExplode)
            {
                this.mines.Remove(mine);
            }
        }
    }
}
