namespace Tieno.MyPonto.Client.Service
{
    public class SynchronizationType
    {
        private SynchronizationType(string value)
        {
            this.Value = value;
        }

        public override string ToString()
        {
            return Value;
        }

        public string Value { get; set; }

        public static SynchronizationType AccountDetails = new SynchronizationType("accountDetails");
        public static SynchronizationType AccountTransactions = new SynchronizationType("accountTransactions");
    }
}