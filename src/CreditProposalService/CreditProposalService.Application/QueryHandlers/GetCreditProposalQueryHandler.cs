using CreditProposalService.Application.Query;
using CreditProposalService.Domain.Entities;
using CreditProposalService.Domain.Interfaces;
using MediatR;

namespace CreditProposalService.Application.QueryHandlers;

public class GetCreditProposalQueryHandler : IRequestHandler<GetCreditProposalQuery, IEnumerable<CreditProposal>>
{
    private readonly ICreditProposalReader _reader;

    public GetCreditProposalQueryHandler(ICreditProposalReader reader)
    {
        _reader = reader;
    }

    public async Task<IEnumerable<CreditProposal>> Handle(GetCreditProposalQuery request, CancellationToken cancellationToken)
    {
        return await _reader.GetAsync(cancellationToken);
    }
}