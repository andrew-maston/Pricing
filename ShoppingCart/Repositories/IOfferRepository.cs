using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingCart
{
    public interface IOfferRepository
    {
        public Task<IReadOnlyCollection<Offer>> GetAllAsync();
    }
}