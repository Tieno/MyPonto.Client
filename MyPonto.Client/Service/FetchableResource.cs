namespace Tieno.MyPonto.Client.Service
{
    public abstract class FetchableResource
    {
        internal MyPontoService _service;

        public void Bind(MyPontoService service)
        {
            this._service = service; 
        }
    } 
}