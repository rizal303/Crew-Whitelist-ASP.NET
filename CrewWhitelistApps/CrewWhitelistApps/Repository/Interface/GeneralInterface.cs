
namespace CrewWhitelistApps.Repository.Interface
{
    interface GeneralInterface<obj>
    {
        bool save(obj o);
        bool edit(obj o);
        bool delete(obj o);
    }
}
