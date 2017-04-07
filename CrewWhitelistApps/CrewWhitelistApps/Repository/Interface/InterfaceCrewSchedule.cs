using CrewWhitelistApps.Models;
using System.Collections.Generic;

namespace CrewWhitelistApps.Repository.Interface
{
    interface InterfaceCrewSchedule : GeneralInterface<CrewScheduleModel>
    {
        List<CrewScheduleModel> getCrewByName();
        List<CrewScheduleModel> getAllCrewSchedule();
        List<CrewScheduleModel> getByDaygetAllCrewSchedule();
    }
}
