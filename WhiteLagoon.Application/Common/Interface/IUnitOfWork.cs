

namespace WhiteLagoon.Application.Common.Interface
{
    public interface IUnitOfWork
    {
        IVillaNumberRepository VillaNumber { get; }

        IVillaRepository Villa { get; }


    }
}
