using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RaceBet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaceBet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RaceManagementController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get(int? matchid) //this is the main function that returns all the odds of all matches or of a specific match
        {
            try
            {
                using (var db = new Context())
                {
                    var matchodds = (from x in db.MatchOdds
                                     where matchid == null || (matchid != null && x.MatchId == matchid)
                                     select new
                                     {
                                         x.MatchId,
                                         x.Match.Description,
                                         x.Match.MatchDateTime,
                                         x.Match.TeamA,
                                         x.Match.TeamB,
                                         x.Match.Sport,
                                         x.Specifier,
                                         x.Odd
                                     }).OrderBy(x => x.MatchId).ToList();

                    var matchoddsdetails = matchodds.Select(x => new
                    {
                        x.Description,
                        MatchDate = x.MatchDateTime.ToString("dd/MM/yyyy"),
                        MatchTime = x.MatchDateTime.ToString("HH:mm"),
                        x.TeamA,
                        x.TeamB,
                        Sport = x.Sport.ToString(),
                        x.Specifier,
                        x.Odd
                    });

                    return StatusCode(StatusCodes.Status200OK, new { matchoddsdetails = matchoddsdetails });
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetMatches")]
        public ActionResult GetMatches()
        {
            try
            {
                using (var db = new Context())
                {
                    var matches = (from x in db.Matches
                                     select new
                                     {
                                         x.ID,
                                         x.Description,
                                         x.MatchDateTime,
                                         x.TeamA,
                                         x.TeamB,
                                         x.Sport
                                     }).ToList();

                    var matchdetails = matches.Select(x => new
                    {
                        x.ID,
                        x.Description,
                        MatchDate = x.MatchDateTime.ToString("dd/MM/yyyy"),
                        MatchTime = x.MatchDateTime.ToString("HH:mm"),
                        x.TeamA,
                        x.TeamB,
                        Sport = x.Sport.ToString()
                    });

                    return StatusCode(StatusCodes.Status200OK, new { matchdetails = matchdetails });
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        [HttpPost("AddMatch")]
        public ActionResult AddMatch(Match match)
        {
            try
            {
                using (var db = new Context())
                {
                    db.Matches.Add(match);
                    db.SaveChanges();

                    return StatusCode(StatusCodes.Status200OK, new { matchid = match.ID });
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        [HttpPost("UpdateMatch")]
        public ActionResult UpdateMatch(Match match)
        {
            try
            {
                using (var db = new Context())
                {
                    var dbmatch=db.Matches.FirstOrDefault(x => x.ID == match.ID);
                    if (dbmatch != null)
                    {
                        dbmatch.Description = match.Description;
                        dbmatch.MatchDateTime = match.MatchDateTime;
                        dbmatch.TeamA = match.TeamA;
                        dbmatch.TeamB = match.TeamB;
                        dbmatch.Sport = match.Sport;

                        db.SaveChanges();

                        return StatusCode(StatusCodes.Status200OK);
                    }
                    else
                        return StatusCode(StatusCodes.Status400BadRequest, "you have to provide a valid id");


                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        [HttpDelete("DeleteMatch")]
        public ActionResult DeleteMatch(int matchid)
        {
            try
            {
                using (var db = new Context())
                {
                    var dbmatch = db.Matches.FirstOrDefault(x => x.ID == matchid);
                    if (dbmatch != null)
                    {
                        db.Matches.Remove(dbmatch);

                        db.SaveChanges();
                        return StatusCode(StatusCodes.Status200OK);
                    }
                    else
                        return StatusCode(StatusCodes.Status400BadRequest, "you have to provide a valid id");

                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        [HttpGet("GetMatchOdds")]
        public ActionResult GetMatchOdds()
        {
            try
            {
                using (var db = new Context())
                {
                    var matcheodds = (from x in db.MatchOdds
                                   select new
                                   {
                                       x.ID,
                                       x.MatchId,
                                       x.Specifier,
                                       x.Odd
                                   }).ToList();

                    return StatusCode(StatusCodes.Status200OK, new { matcheodds = matcheodds });
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        [HttpPost("AddMatchOdd")]
        public ActionResult AddMatchOdd(MatchOdd matchodd)
        {
            try
            {
                using (var db = new Context())
                {
                    db.MatchOdds.Add(matchodd);
                    db.SaveChanges();

                    return StatusCode(StatusCodes.Status200OK, new { matchoddid = matchodd.ID });
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message+" "+e.InnerException.Message);
            }

        }

        [HttpPost("UpdateMatchOdd")]
        public ActionResult UpdateMatchOdd(MatchOdd matchOdd)
        {
            try
            {
                using (var db = new Context())
                {
                    var dbmatchodd = db.MatchOdds.FirstOrDefault(x => x.ID == matchOdd.ID);
                    if (dbmatchodd != null)
                    {
                        dbmatchodd.MatchId = matchOdd.MatchId;
                        dbmatchodd.Specifier = matchOdd.Specifier;
                        dbmatchodd.Odd = matchOdd.Odd;

                        db.SaveChanges();
                        return StatusCode(StatusCodes.Status200OK);
                    }
                    else
                        return StatusCode(StatusCodes.Status400BadRequest, "you have to provide a valid id");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        [HttpDelete("DeleteMatchOdd")]
        public ActionResult DeleteMatchOdd(int matchoddid)
        {
            try
            {
                using (var db = new Context())
                {
                    var dbmatchodd = db.MatchOdds.FirstOrDefault(x => x.ID == matchoddid);
                    if (dbmatchodd != null)
                    {
                        db.MatchOdds.Remove(dbmatchodd);

                        db.SaveChanges();
                        return StatusCode(StatusCodes.Status200OK);
                    }
                    else
                        return StatusCode(StatusCodes.Status400BadRequest, "you have to provide a valid id");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

    }

}
