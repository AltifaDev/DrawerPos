using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerPos.Shared
{
    public class WeeklyMonthlyRevenueDto
    {
        public List<DailyRevenueDto> LastWeekRevenue { get; set; }
        public List<DailyRevenueDto> ThisMonthRevenue { get; set; }
    }
}
