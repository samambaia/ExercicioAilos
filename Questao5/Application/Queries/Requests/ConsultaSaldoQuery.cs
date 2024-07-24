﻿using MediatR;
using Questao5.Application.Queries.Responses;

namespace Questao5.Application.Queries.Requests
{
    public class ConsultaSaldoQuery : IRequest<SaldoResponse>
    {
        public string IdContaCorrente { get; set; }
    }
}
