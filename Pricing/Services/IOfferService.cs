using Pricing.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pricing.Services
{
    /// <summary>
    /// Retrieves offers
    /// </summary>
    public interface IOfferService
    {
        /// <summary>
        /// Retrieves collection of offers
        /// </summary>
        /// <returns>IReadOnlyCollection of Offer</returns>
        public Task<IReadOnlyCollection<Offer>> GetAll();
    }
}