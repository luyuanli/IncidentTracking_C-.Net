using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LL_Lab6.Models;

namespace LL_Lab6.ViewModels
{
    public class Repo_Team : RepositoryBase
    {
        public List<TeamName> getListOfTeamNames()
        {
            var teams = dc.Teams.OrderBy(i => i.Name);
            var teamNames = new List<TeamName>();
            if (teams != null)
            {
                foreach (var team in teams)
                {
                    TeamName teamName = new TeamName();
                    teamName.Id = team.Id;
                    teamName.Name = team.Name;
                    teamNames.Add(teamName);
                }
            }
            return teamNames;
        }

        public Team getTeam(int? id)
        {
            var team = dc.Teams.FirstOrDefault(i => i.Id == id);

            if (team != null)
                return team;
            else
                return null;
        }

        public Team createTeam(Team team)
        {
            var t = new Team();

            t.Name = team.Name;
            t.Availability = team.Availability;

            dc.Teams.Add(t);
            dc.SaveChanges();

            return getTeam(t.Id);
        }

        public void deleteTeam(int id)
        {
            var t = dc.Teams.FirstOrDefault(b => b.Id == id);
            dc.Teams.Remove(t);

            dc.SaveChanges();
        }

        public Team editTeam(int Id, string Name, Availability Availability)
        {
            var t = dc.Teams.FirstOrDefault(b => b.Id == Id);
            t.Name = Name;
            t.Availability = Availability;
            dc.SaveChanges();

            return t;
        }
    }
}