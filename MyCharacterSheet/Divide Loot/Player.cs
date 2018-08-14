namespace MyCharacterSheet.Divide_Loot
{
    public class Player
    {

        #region Members

        public int[] currency = new int[5];

        #endregion

        #region Constructors

        /// =========================================
        /// Player()
        /// =========================================
        public Player(string name)
        {
            Name = name;
        }

        /// =========================================
        /// Player()
        /// =========================================
        public Player(int cp, int sp, int ep, int gp, int pp)
        {
            currency[4] = cp;
            currency[3] = sp;
            currency[2] = ep;
            currency[1] = gp;
            currency[0] = pp;
        }

        #endregion

        #region Accessors

        public string Name
        {
            get;
            set;
        }

        public double Total
        {
            get
            {
                return (Copper / 100.0) + (Silver / 10.0) + (Electrum / 5.0) + Gold + (Platinum * 10.0);
            }
        }

        public int Copper
        {
            get
            {
                return currency[4];
            }
            set
            {
                currency[4] = value;
            }
        }

        public int Silver
        {
            get
            {
                return currency[3];
            }
            set
            {
                currency[3] = value;
            }
        }

        public int Electrum
        {
            get
            {
                return currency[2];
            }
            set
            {
                currency[2] = value;
            }
        }

        public int Gold
        {
            get
            {
                return currency[1];
            }
            set
            {
                currency[1] = value;
            }
        }

        public int Platinum
        {
            get
            {
                return currency[0];
            }
            set
            {
                currency[0] = value;
            }
        }

        #endregion

    }
}
