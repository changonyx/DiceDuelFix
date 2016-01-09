using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuneBet
{
    public class DiceDuel
    {
        private string clientHash;
        private string serverHash;
        private string secretHash;
        private string fullHash;
        private long betAmount = 2500;
        private bool above = true;
        private string Owner;
        private string Staker;
        private int OwnersRoll;
        private int StakersRoll;

        public DiceDuel()
        { }


        public DiceDuel(string clientHash, string serverHash, string secretHash)
        {
            this.clientHash = clientHash;
            this.serverHash = serverHash;
            this.secretHash = secretHash;
            this.fullHash = clientHash + serverHash + secretHash;

            //Add Code to DB HERE
        }

        public DiceDuel(string clientHash, string serverHash, string secretHash, long betAmount, bool above, string OwnerID)
        {
            this.clientHash = clientHash;
            this.serverHash = serverHash;
            this.secretHash = secretHash;
            this.fullHash = clientHash + serverHash + secretHash;
            this.betAmount = betAmount;
            this.above = above;
            this.Owner = OwnerID;
            this.OwnersRoll = RollDice();
            //Add Code to DB HERE
        }

        public string StakeOwner(string clientHash, string serverHash, string StakerID)
        {
            string winner = "Tie";
            this.Staker = StakerID;

            this.StakersRoll = new DiceDuel(clientHash, serverHash, secretHash).RollDice();
            //Check if it was a Tie and return "Tie" is so.
            if (OwnersRoll == StakersRoll)
                return winner;

            //Conditional on Die number preference
            if (above)
            {
                //Highest Roll Wins.
                winner = OwnersRoll > StakersRoll ? Owner : Staker;
            }
            else
            {
                //Lowest Roll Wins.
                winner = OwnersRoll < StakersRoll ? Owner : Staker;
            }

            //Add Code to DB HERE

            return winner;
        }

        public int RollDice()
        {
            int dieFace;

            Random rnd = new Random(int.Parse(fullHash));

            // Mod to die face
            dieFace = (rnd.Next() % 6) + 1;
            
            return dieFace;
        }

        /// <summary>
        /// Generates a random Alphanumeric string of whatever length
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
