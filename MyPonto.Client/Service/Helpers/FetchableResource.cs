namespace Tieno.MyPonto.Client.Service
{
    public abstract class FetchableResource
    {
        internal IMyPontoApi myPontoClient;

        public void Bind(IMyPontoApi pontoClient)
        {
            this.myPontoClient = pontoClient; 
        }
    } 
}