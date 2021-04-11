using Pricing.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pricing.Repositories
{
    public interface IOfferRepository
    {
        public Task<IReadOnlyCollection<Offer>> GetAllAsync();
    }
}