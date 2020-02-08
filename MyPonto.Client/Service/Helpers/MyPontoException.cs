using System;
using System.Linq;
using Tieno.MyPonto.Client.Model;

namespace Tieno.MyPonto.Client.Service
{
    public class MyPontoException : Exception
    {
        public MyPontoException(ErrorResponse errorResponse):base(
            String.Join(Environment.NewLine,errorResponse?.Errors?.Select(x => $"{x?.Code} - {x?.Detail}")))
        {
            this.ErrorResponse = errorResponse;
        }

        public ErrorResponse ErrorResponse { get; set; }
    }
}
