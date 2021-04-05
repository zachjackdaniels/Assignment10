using Assignment10.Models;
using Assignment10.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment10.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private BowlingLeagueContext _context { get; set; }

        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(string team, int pageNum = 0)
        {
            //initializes a nullable int to handle team id, given team name from viewcomponent
            int? teamId = null;

            if (team != null)
            {
                //grabs from team from context by team id
                string tName = _context.Teams.Where(x => x.TeamName == team)
                    .FirstOrDefault().TeamName;

                //inputs that into the viewdata to be pulled by the index
                ViewBag.TeamName = tName;

                //if team name (team) is not null then fill teamId in with correct team Id
                teamId = (int)_context.Teams.Where(x => x.TeamName == team).FirstOrDefault().TeamId;
            }

            int pageSize = 5;


            return View(new IndexViewModel
            {
                //selects all bowlers where team equals what is selected, or all
                Bowlers = (_context.Bowlers
                .FromSqlInterpolated($"SELECT * FROM Bowlers WHERE TeamId = {teamId} OR {teamId} IS NULL")
                .OrderBy(x => x.BowlerId)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToList()),

                PageNumberInfo = new PageNumberInfo
                {
                    NumItemsPerPage = pageSize,
                    CurrentPage = pageNum,

                    //if no team has been selected, then get the full count.
                    //Otherwise only count the number of bowlers from selected team
                    TotalNumItems = (team == null ? _context.Bowlers.Count() :
                        _context.Bowlers.Where(x => x.TeamId == teamId).Count())
                },

                Team = (team == null ? "" :
                    _context.Teams.Where(x => x.TeamName == team)
                    .FirstOrDefault().TeamName)
            });

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
