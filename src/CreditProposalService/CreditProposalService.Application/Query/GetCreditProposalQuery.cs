using CreditProposalService.Domain.Entities;
using MediatR;

namespace CreditProposalService.Application.Query;

public record GetCreditProposalQuery() : IRequest<IEnumerable<CreditProposal>>;