using ConsoleApp.Context;
using ConsoleApp.Entities;
using ConsoleApp.Repositories;

namespace ConsoleApp.Repositories;

internal partial class AddressRepository : Repo<AddressEntity>
{
    public AddressRepository(DataContext context) : base(context)
    {
    }
}
