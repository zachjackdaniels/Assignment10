using Assignment10.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment10.Components
{
    public class BowlerTeamViewComponent : ViewComponent
    {
        private BowlingLeagueContext _context;
        public BowlerTeamViewComponent(BowlingLeagueContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            return View(_context.Teams
                .Distinct()
                .OrderBy(x => x.TeamName));
        }
    }
}
