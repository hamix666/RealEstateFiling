using RealEstateFiling.Services;
using RealEstateFilingWin.Models.FIL;

namespace RealEstateFilingWin.Services.FIL
{
    public class OwnerServices : RepoBase<Owners>, IOwnerServices
    {
        public OwnerServices(RealEstateContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
