using CrewWhitelistApps.Models;
using System.Collections.Generic;

namespace CrewWhitelistApps.Repository.Interface
{
    interface InterfaceCrew : GeneralInterface<CrewModel>
    {
        List<CrewModel> getAllCrew();
    }
}
