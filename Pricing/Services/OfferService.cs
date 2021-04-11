using Pricing.Models;
using Pricing.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pricing.Services
{
    public class OfferService : IOfferService
    {
        private readonly IOfferRepository _OfferRepository;

        public OfferService(IOfferRepository OfferRepository)
        {
            _OfferRepository = OfferRepository;
        }

        public async Task<IReadOnlyCollection<Offer>> GetAll()
        {
            return await _OfferRepository.GetAllAsync();
        }
    }
}